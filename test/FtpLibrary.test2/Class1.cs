using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpLibrary.test
{
	public class TestMain
	{
		static void Main(string[] args)
		{

			new FtpWebClient().UploadFile(@"C:\temp\a\b\x.txt", "localhost", "admin", "admin");
			new FtpWebClient().UploadFile(@"C:\temp\y.txt", "localhost", "admin", "admin");
			new FtpWebClient().UploadFile(@"C:\temp\a.txt", "localhost", "admin", "admin");
			return;
		}

	}
}
