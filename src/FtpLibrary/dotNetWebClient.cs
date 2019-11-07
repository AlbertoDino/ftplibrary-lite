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
		void RunFtpWebRequestUpload(string ftprequest, string localfilepath, string host, string ftpUsername, string ftpPassword);

		/// <summary>
		/// Execute an FTP web command
		/// </summary>
		FtpWebResponse RunFtpWebRequest(string ftprequest, string ftpmethod, string host, string ftpUsername, string ftpPassword);
	}

	/// <summary>
	/// .Net Framework implementation of Webclient
	/// </summary>
	public class dotNetWeb : IWebClient
	{
		public FtpWebResponse RunFtpWebRequest(string ftprequest, string ftpmethod, string host, string ftpUsername, string ftpPassword)
		{
			System.Net.FtpWebResponse ftpwebResponse = null;
			System.Net.FtpWebRequest ftpwebrequest = (System.Net.FtpWebRequest)WebRequest.Create(ftprequest);
			ftpwebrequest.Method = ftpmethod;
			ftpwebrequest.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
			ftpwebResponse = (FtpWebResponse)ftpwebrequest.GetResponse();
			return ftpwebResponse;
		}

		public void RunFtpWebRequestUpload(string ftprequest, string localfilepath, string host, string ftpUsername, string ftpPassword)
		{
			string ftpfullrequest = ftprequest;
			using (var client = new WebClient())
			{
				client.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
				client.UploadFile(ftpfullrequest, WebRequestMethods.Ftp.UploadFile, localfilepath);
			}
		}
	}
}
