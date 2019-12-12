# FtpLibrary-Lite

simple FTP class for 
* Uploading a file to an ftp server
```c#
void UploadFile(string file, string host, string ftpUsername, string ftpPassword)
```
* Creating a folder to an ftp server
```c#
void CreateDirectory(string path, string host, string ftpUsername, string ftpPassword);
```
* return list of items from ftp server path
```c#
IList<FtpItem> GetList(string path, string host, string ftpUsername, string ftpPassword);
```


