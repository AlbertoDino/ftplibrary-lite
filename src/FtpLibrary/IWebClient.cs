using System.Net;

namespace FtpLibrary
{
	/// <summary>
	/// Abstraction of WebClient implementations
	/// </summary>
	public interface IWebClient
	{
		/// <summary>
		/// Execute and FTP web request for Uploading a file
		/// </summary>
		void FtpUploadRequest(string ftprequest, string localfilepath, string host, string ftpUsername, string ftpPassword);

		/// <summary>
		/// Execute an FTP web command
		/// </summary>
		FtpWebResponse FtpRequest(string ftprequest, string ftpmethod, string host, string ftpUsername, string ftpPassword);
	}
}
