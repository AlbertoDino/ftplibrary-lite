using System;

namespace FtpLibrary
{
	public interface IFTPRequest : IDisposable
	{
		/// <summary>
		/// Send an ftp request to an ftp server
		/// </summary>
		void CreateDirectory(string relativedirpath, string host, string ftpUsername, string ftpPassword);

		/// <summary>
		/// Upload a file to a FTP server. 
		/// the specified location is specified in the ftprequest
		/// </summary>
		void UploadFile(string file, string host, string ftpUsername, string ftpPassword);
	}
}
