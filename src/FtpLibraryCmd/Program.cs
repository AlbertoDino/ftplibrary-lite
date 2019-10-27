using System;

namespace FtpLibraryCmd
{
	/// <summary>
	/// FTpLibrary Cmd line
	/// </summary>
	static class Program
	{
		static string OrDefault(this string[] args, int index, string _default)
		{
			if (args.Length > index) return args[index];
			else return _default;
		}

		static bool ValidArgsforCmd(this string[] args)
		{
			return args != null && args.Length >= 2;
		}

		static bool IsInteractive(this string[] args)
		{
			return args != null && args.Length == 1 && args[0] == "i";
		}

		/// <summary>
		/// FTpLibrary
		/// 
		/// Command line arguments:
		/// interactive mode
		/// > FtpLibraryCmd.exe i
		/// 
		/// cmd
		/// > FtpLibraryCmd.exe <method> <request> <server> <username> <password>
		/// 
		/// example:
		/// FtpLibraryCmd.exe upload C:\file.txt localhost admin admin
		/// FtpLibraryCmd.exe upload C:\temp\a\b\x.txt
		/// </summary>
		static void Main(string[] args)
		{
			if(args.IsInteractive())
			{
				// Interactive
				string arg = string.Empty;
				Console.WriteLine("FtpLibraryCmd");
				do
				{
					try
					{
						Console.Write("args > ");
						arg = Console.ReadLine();
						Cmd(arg.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
					}
					catch (Exception ex)
					{
						Console.WriteLine(string.Format("Exception: {0}", ex.Message));
					}
				} while (!arg.StartsWith("q"));
			}
			else
			{
				Cmd(args);
			}
		}

		static void Cmd(string[] args)
		{
			if (args.ValidArgsforCmd())
			{
				Run(args[0], args[1], args.OrDefault(2, "localhost"), args.OrDefault(3, "admin"), args.OrDefault(4, "admin"));
			}
		}

		static void Run(string method, string request, string host, string username, string pwd)
		{
			switch (method.ToLower())
			{
				case "upload":
					new FtpLibrary.FtpWebClient().UploadFile(request, host, username, pwd);
					break;
				default:
					new FtpLibrary.FtpWebClient().CreateDirectory(request, host, username, pwd);
					break;
			}
		}
	}
}
