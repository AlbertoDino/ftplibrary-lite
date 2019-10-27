using System.Net;
using FtpLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace FTPLibrary.test
{
	public class FTPWebImpl : IWebClient
	{
		public FtpWebResponse RunFtpWebRequest(string ftprequest, string ftpmethod, string host, string ftpUsername, string ftpPassword)
		{
			//this would require a ftp server mockup
			return null;
		}

		public void RunFtpWebRequestUpload(string ftprequest, string localfilepath, string host, string ftpUsername, string ftpPassword)
		{
			//this would require a ftp server mockup
		}
	}

	public class FtpWebClientTest : FtpClient<FtpWebRequest<FTPWebImpl>>
	{
		public FtpWebClientTest() : base(new dotNetSystem()) { }
	}

	[TestClass]
	public class FtpClient_Test
	{

		string host = "localhost";
		string user = "admin";
		string pwd = "admin";

		[TestMethod]
		public void CreateDirectory_happypath()
		{
			bool success = true;
			try
			{
				new FtpWebClientTest().CreateDirectory("test", host, user, pwd);
			}
			catch
			{
				success = false;
			}
			Assert.IsTrue(success);
		}

		/// <summary>
		/// CreateDirectory
		/// expecting excection for badpath
		/// </summary>
		[TestMethod]
		public void CreateDirectory_badpath()
		{
			bool fail = false;
			try
			{
				new FtpWebClientTest().CreateDirectory("test!??/", host, user, pwd);
			}
			catch
			{
				fail = true;
			}
			Assert.IsTrue(fail);
		}

		/// <summary>
		/// CreateUploadFile
		/// </summary>
		[TestMethod]
		public void CreateUploadFile_happypath()
		{
			bool success = true;
			try
			{
				string testfile = string.Format("{0}/{1}", Directory.GetCurrentDirectory(), "file.txt");
				new FtpWebClientTest().UploadFile(testfile, host, user, pwd);
			}
			catch
			{
				success = false;
			}
			Assert.IsTrue(success);
		}

		/// <summary>
		/// CreateUploadFile 
		/// expecting execption for badfilePath
		/// </summary>
		[TestMethod]
		public void CreateUploadFile_badfilePath()
		{
			bool fail = false;
			try
			{
				string testfile = string.Format("{0}/test/{1}", Directory.GetCurrentDirectory(), "file.txt");
				new FtpWebClientTest().UploadFile(testfile, host, user, pwd);
			}
			catch
			{
				fail = true;
			}
			Assert.IsTrue(fail);
		}
	}
}
