
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using AzureBlob.Services;
using Microsoft.Extensions.Configuration;
using Azure.Extensions.AspNetCore.Configuration.Secrets;

namespace AzureBlob
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            
            builder.Services.AddScoped<IContainerService, ContainerService>();  
            builder.Services.AddScoped<IBlobService, BlobService>();  

            builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            // Configure Azure Key Vault
            var keyVaultURL = builder.Configuration["KeyVaultConfiguration:KeyVaultURL"];
            var keyVaultClientId = builder.Configuration["KeyVaultConfiguration:ClientId"];
            var keyVaultClientSecret = builder.Configuration["KeyVaultConfiguration:ClientSecret"];
            var keyVaultTenantId = builder.Configuration["KeyVaultConfiguration:TenantId"];

            var credential = new ClientSecretCredential(keyVaultTenantId, keyVaultClientId, keyVaultClientSecret);

            var client = new SecretClient(new Uri(keyVaultURL!), credential);
            builder.Configuration.AddAzureKeyVault(client,new AzureKeyVaultConfigurationOptions() { Manager = new PrefixKeyVaultManager("AzureBlob")});

           

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.MapRazorPages();

            app.Run();
        }
    }
}