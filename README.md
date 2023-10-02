
![cbimage (1)](https://github.com/jil1710/AzureBlob/assets/125335932/90c71b8b-6242-4eb5-9af8-b0a85fa41175)
# Azure Blob Storage

- It is Microsoft's object storage solution for the cloud. Blob storage is optimized for storing a massive amount of unstructured data, such as text or binary data.

-  Azure blob storage is fundamental for the entire Microsoft Azure because many other Azure services will store the data within a storage account, inside the blob storage, and act upon that data. And every blob should be stored in a container.

## Blob storage usages:
    
- It serves images or documents directly to a browser.
- It stores files for distributed access.
- We can stream video and audio using blob storage.
- Easy writing to log files.
- It stores the data for backup, restore, disaster recovery, and archiving.
- It stores the data for analysis by an on-premises or Azure-hosted service.

## Container

- The container is more like a folder where different blobs are stored. At the container level, we can define security policies and assign those policies to the container, which will be cascaded to all the blobs under the same container.

- A storage account can contain an unlimited number of containers, and each container can contain an unlimited number of blobs up to the maximum limit of storage account size (up to 500 TB).


## Blob

- Blob stands for Binary Large Object, which includes objects such as images and multimedia files.

## Let's implement Azure Blob Storage using ASP.Net core

- **Step 1 :** 
    
    Create Azure storage account in Azure portal and click on the access key menu to get the connection string after that mark down the primary connection string.

- **Step 2 :**

    Download the below nuget package in order to start with azure storage blob.

    ```csharp
        Azure.Storage.Blobs
    ```

- ** Step 4 :**

    Using `BlobServiceClient` connect an appliction with azure storage account to get access of containers and blobs from application end.

    ```json
        "ConnectionStrings": {
            "AzureStorageAccount": "[YOUR-STORAGE-ACCOUNT-CONNECTION-STRING]"
        }
    ```

    ![image](https://github.com/jil1710/AzureBlob/assets/125335932/5dbd65f4-13fb-48da-a188-e64397d69e92)



- **Create the container into storage account :**

    ![image](https://github.com/jil1710/AzureBlob/assets/125335932/4df5e699-8cbd-4d3f-b324-08aa0215f0bd)

- **Delete the container from storage account :**

    ![image](https://github.com/jil1710/AzureBlob/assets/125335932/25d023fd-721a-468c-8575-9195cd7cd947)

- **Access all the blobs in container :**

    ![image](https://github.com/jil1710/AzureBlob/assets/125335932/f0c93672-d0ed-43ce-86f0-721feae3927d)

- **Delete particular blob from specific container :**

    ![image](https://github.com/jil1710/AzureBlob/assets/125335932/6d05b29e-4d5c-4b24-a154-1d7acb3974f4)

- **Download particular blob :**

    ![image](https://github.com/jil1710/AzureBlob/assets/125335932/554e961a-0298-46f4-b913-315305970f45)


- Here is the some of the misc relate azure blob storage to see in detail clone this web application and just paste the azure connection string and run the app.



