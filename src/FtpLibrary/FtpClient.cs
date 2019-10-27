using System;
using System.Net;

namespace FtpLibrary
{
	/// <summary>
	/// FTPClient
	/// Manages FTP requests to a FTPServer
	/// FTP passed as a generic, ment to be used in function without any state being stored
	/// </summary>
	public class FtpClient<FTP> 
		where FTP : IFTPRequest , new()
	{
		ISystem system;

		public FtpClient(ISystem sys)
		{
			if (sys == null)
				throw new ArgumentNullException("Argument is a null reference");
			system = sys;
		}

		/// <summary>
		/// Upload a file to a FTP server and remote sub dirs to the FTP Server
		/// </summary>
		public void UploadFile(string file, string host, string ftpUsername, string ftpPassword)
		{
			if(!system.FileExists(file))
			{
				throw new ArgumentException(string.Format("file {0} does not exists",file));
			}
			// get the relative dirs path from the root
			string directorypaths = system.GetDirPathFromRoot(file);
			// FTP to create the path on the ftp server
			if (!String.IsNullOrEmpty(directorypaths))
				CreateDirectory(directorypaths, host, ftpUsername, ftpPassword);
			// upload the file to the ftp server
			string relativepath = system.GetPathFromRoot(file);
			using (var ftp = new FTP())
			{
				ftp.UploadFile(relativepath, file, host, ftpUsername, ftpPassword);
			}
		}

		/// <summary>
		/// Create a directory path to a FTP server
		/// </summary>
		public void CreateDirectory(string relativedirpath, string host, string ftpUsername, string ftpPassword)
		{
			using (var ftp = new FTP())
			{
				ftp.Send(relativedirpath, WebRequestMethods.Ftp.MakeDirectory, host, ftpUsername, ftpPassword);
			}
		}
	}

	public class FtpWebClient : FtpClient<FtpWebRequest<dotNetFTP>>
	{
		public FtpWebClient() :base(new dotNetSystem()) { }
	}
}


