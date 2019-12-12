using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace FtpLibrary
{
	/// <summary>
	/// FTPClient
	/// Creates FTP requests to a FTPServer
	/// </summary>
	public class FtpClient<WebClient, Sys> : IFTPRequest
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
			// Get the relative dirs path from local drive
			string directorypaths = system.GetDirPathFromRoot(file);
			if (!String.IsNullOrEmpty(directorypaths))
				CreateDirectory(directorypaths, host, ftpUsername, ftpPassword);
			// Upload the file to the ftp server
			string relativepath = system.GetPathFromRoot(file);
			new WebClient().FtpUploadRequest(BuildFTPRequestURI(host, relativepath)
					, file
					, host
					, ftpUsername
					, ftpPassword);
		}

		/// <summary>
		/// Create a directory path to a FTP server
		/// </summary>
		public void CreateDirectory(string path, string host, string ftpUsername, string ftpPassword)
		{
			using (var response = new WebClient().FtpRequest(BuildFTPRequestURI(host, path)
					, System.Net.WebRequestMethods.Ftp.MakeDirectory
					, host
					, ftpUsername
					, ftpPassword))
			{
				Stream responseStream = response.GetResponseStream();
				StreamReader reader = new StreamReader(responseStream);
				Console.WriteLine(reader.ReadToEnd());
			}
		}

		/// <summary>
		/// Returns list of FtpItems from the specified path
		/// </summary>
		public IList<FtpItem> GetList(string path, string host, string ftpUsername, string ftpPassword)
		{
			List<FtpItem> itemList = new List<FtpItem>();
			using (var response = new WebClient().FtpRequest(BuildFTPRequestURI(host, path)
					, System.Net.WebRequestMethods.Ftp.ListDirectory
					, host
					, ftpUsername
					, ftpPassword))
			{
				Stream responseStream = response.GetResponseStream();
				StreamReader reader = new StreamReader(responseStream);
				while (reader.Peek() >= 0)
				{
					string details = reader.ReadLine();
					itemList.Add(new FtpItem() {
						Name = details,
						Type = details.Contains(".") ? FtpItemType.File : FtpItemType.Directory
					});
				}
			}
			return itemList;
		}

		public string BuildFTPRequestURI(string host, string path)
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
			//Do nothing
		}
	}

	public class FtpWebClient : FtpClient<dotNetWeb, dotNetSystem>
	{
	}
}


