using System;
using System.IO;
using System.Net;

namespace FtpLibrary
{

	/// <summary>
	/// .Net Framework implementation of Webclient
	/// </summary>
	public class dotNetWeb : IWebClient
	{
		public FtpWebResponse FtpRequest(string ftprequest, string ftpmethod, string host, string ftpUsername, string ftpPassword)
		{
			System.Net.FtpWebResponse ftpwebResponse = null;
			WebException webException = null;
			try
			{
				System.Net.FtpWebRequest ftpwebrequest = (System.Net.FtpWebRequest)WebRequest.Create(ftprequest);
				ftpwebrequest.Method = ftpmethod;
				ftpwebrequest.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
				ftpwebResponse = (FtpWebResponse)ftpwebrequest.GetResponse();
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
				}
			}
			return ftpwebResponse;
		}

		public void FtpUploadRequest(string ftprequest, string localfilepath, string host, string ftpUsername, string ftpPassword)
		{
			WebException webException = null;
			try
			{
				string ftpfullrequest = ftprequest;
				using (var client = new WebClient())
				{
					client.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
					client.UploadFile(ftpfullrequest, WebRequestMethods.Ftp.UploadFile, localfilepath);
				}
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
	}

	/// <summary>
	/// dotNetSystem helper class
	/// </summary>
	public class dotNetSystem : ISystem
	{
		public string GetPathFromRoot(string file)
		{
			string root = Path.GetPathRoot(file);
			string fullPath = Path.GetFullPath(file);
			string relative = fullPath.Replace(root, "");
			relative = relative.Replace("\\", "/");
			return relative;
		}

		public string GetDirPathFromRoot(string file)
		{
			string root = Path.GetPathRoot(file);
			string fullPath = new FileInfo(file).Directory.FullName;
			string relative = fullPath.Replace(root, "");
			relative = relative.Replace("\\", "/");
			return relative;
		}

		public bool FileExists(string file)
		{
			return File.Exists(file);
		}
	}
}
