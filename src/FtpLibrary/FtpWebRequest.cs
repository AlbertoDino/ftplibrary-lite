using System;
using System.Net;

namespace FtpLibrary
{

	/// <summary>
	/// FtpWebRequest sends ftp requests to an FTP Server using ftp web protocol
	/// </summary>
	public class FtpWebRequest<WebClient> : IFTPRequest
		where WebClient : IWebClient, new()
	{
		private const string PROTOCOL = "ftp";

		public void UploadFile(string ftprequest, string localfilepath, string host, string ftpUsername, string ftpPassword)
		{
			WebException webException = null;
			try
			{
				string ftpfullrequest = BuildURIRequest(host, ftprequest);
				new WebClient().RunFtpWebRequestUpload(ftpfullrequest, localfilepath, host, ftpUsername, ftpPassword);
			}
			catch (WebException wex)
			{
				webException = wex;
				throw wex;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public void Send(string ftprequest, string ftpmethod, string host, string ftpUsername, string ftpPassword)
		{
			System.Net.FtpWebResponse ftpwebResponse = null;
			WebException webException = null;
			try
			{
				string ftpfullrequest = BuildURIRequest(host, ftprequest);
				ftpwebResponse = new WebClient().RunFtpWebRequest(ftpfullrequest, ftpmethod, host, ftpUsername, ftpPassword);
			}
			catch (WebException wex)
			{
				webException = wex;
				ftpwebResponse = (FtpWebResponse)wex.Response;
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				// Catching the response out of an exception
				if (ftpwebResponse != null)
				{
					switch (ftpwebResponse.StatusCode)
					{
						case FtpStatusCode.ActionNotTakenFileUnavailable:
							// Item already exists
							break;
						case FtpStatusCode.PathnameCreated:
						case FtpStatusCode.CommandOK:
							// OK Success
							break;
						default:
							if (webException != null) throw webException;
							break;
					}
					ftpwebResponse.Close();
					ftpwebResponse.Dispose();
				}
			}
		}

		public string BuildURIRequest(string host, string path)
		{
			string ftprequest = string.Format("{0}://{1}/{2}", PROTOCOL, host, path);
			if (!Uri.IsWellFormedUriString(ftprequest, UriKind.Absolute))
			{
				throw new ArgumentException(string.Format("ftprequest {0} is not well formatted",ftprequest));
			}
			return ftprequest;
		}

		public void Dispose()
		{
			//nothing
		}
	}
}
