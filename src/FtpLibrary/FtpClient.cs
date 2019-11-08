using System;

namespace FtpLibrary
{
	/// <summary>
	/// FTPClient
	/// Creates FTP requests to a FTPServer
	/// </summary>
	public class FtpClient<WebClient,Sys> : IFTPRequest
		where WebClient : IWebClient, new()
		where Sys : ISystem, new()
	{
		#region Private Members

		private const string PROTOCOL = "ftp";
		ISystem system;

		#endregion

		public FtpClient()
		{
			system = new Sys();
		}

		/// <summary>
		/// Upload a file to a FTP server and creates all the sub directories relative to root
		/// </summary>
		public void UploadFile(string file, string host, string ftpUsername, string ftpPassword)
		{
			if (!system.FileExists(file))
			{
				throw new ArgumentException(string.Format("file {0} does not exists", file));
			}
			// Get the relative dirs path from the root
			string directorypaths = system.GetDirPathFromRoot(file);
			if (!String.IsNullOrEmpty(directorypaths))
				CreateDirectory(directorypaths, host, ftpUsername, ftpPassword);
			// Upload the file to the ftp server
			string relativepath = system.GetPathFromRoot(file);
			new WebClient().RunFtpWebRequestUpload(Build_FTPRequest(host, relativepath)
					, file
					, host
					, ftpUsername
					, ftpPassword);
		}

		/// <summary>
		/// Create a directory path to a FTP server
		/// </summary>
		public void CreateDirectory(string relativedirpath, string host, string ftpUsername, string ftpPassword)
		{
			new WebClient().RunFtpWebRequest(Build_FTPRequest(host, relativedirpath)
					, System.Net.WebRequestMethods.Ftp.MakeDirectory
					, host
					, ftpUsername
					, ftpPassword);
		}

		public string Build_FTPRequest(string host, string path)
		{
			string ftprequest = string.Format("{0}://{1}/{2}", PROTOCOL, host, path);
			if (!Uri.IsWellFormedUriString(ftprequest, UriKind.Absolute))
			{
				throw new ArgumentException(string.Format("ftprequest {0} is not well formatted", ftprequest));
			}
			return ftprequest;
		}

		public void Dispose()
		{
			//So nothing
		}
	}

	public class FtpWebClient : FtpClient<dotNetWeb,dotNetSystem>
	{
	}
}


