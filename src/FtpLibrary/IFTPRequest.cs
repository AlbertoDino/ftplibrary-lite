using System;
using System.Collections.Generic;

namespace FtpLibrary
{
	public enum FtpItemType
	{
		File,
		Directory
	}

	public class FtpItem
	{
		public string Name {get;set; }
		public FtpItemType Type {get;set; }
	}

	public interface IFTPRequest : IDisposable
	{
		/// <summary>
		/// Send an ftp request to an ftp server
		/// </summary>
		void CreateDirectory(string path, string host, string ftpUsername, string ftpPassword);

		/// <summary>
		/// Upload a file to a FTP server. 
		/// the specified location is specified in the ftprequest
		/// </summary>
		void UploadFile(string file, string host, string ftpUsername, string ftpPassword);

		/// <summary>
		/// Returns list of Item { file / directory } from a path
		/// </summary>
		IList<FtpItem> GetList(string path, string host, string ftpUsername, string ftpPassword);
	}
}
