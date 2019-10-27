using System;

namespace FtpLibrary
{
	public interface IFTPRequest : IDisposable
	{
		/// <summary>
		/// Get an ftp URI request
		/// </summary>
		string BuildURIRequest(string host, string path);

		/// <summary>
		/// Send an ftp request to an ftp server
		/// </summary>
		void Send(string ftprequest, string ftpmethod, string host, string ftpUsername, string ftpPassword);

		/// <summary>
		/// Upload a file to a FTP server. 
		/// the specified location is specified in the ftprequest
		/// </summary>
		void UploadFile(string ftprequest, string localfilepath, string host, string ftpUsername, string ftpPassword);
	}
}
