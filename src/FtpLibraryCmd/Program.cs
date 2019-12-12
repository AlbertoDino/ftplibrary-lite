using FtpLibrary;
using System;
using System.Collections.Generic;

namespace FtpLibraryCmd
{
	/// <summary>
	/// FTpLibrary Cmd line test program
	/// 
	/// Command line arguments:
	/// interactive mode
	/// > FtpLibraryCmd.exe i
	/// 
	/// cmd
	/// > FtpLibraryCmd.exe <method> <request> <server> <username> <password>
	/// 
	/// example:
	/// FtpLibraryCmd.exe 
	///   upload C:\file.txt localhost admin admin
	///   upload C:\temp\a\b\x.txt
	///   dir temp/new/
	///   list temp/a/b
	/// </summary>
	static class Program
	{
		static void Main(string[] args)
		{
			if (args.IsInteractive()) // Interactive mode
			{
				string arg = string.Empty;
				Console.WriteLine("FtpLibraryCmd");
				do
				{
					try
					{
						Console.Write("args> ");
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
					new FtpWebClient().UploadFile(request, host, username, pwd);
					break;
				case "dir":
					new FtpWebClient().CreateDirectory(request, host, username, pwd);
					break;
				case "list":
					IList<FtpItem> ftp = new FtpWebClient().GetList(request, host, username, pwd);
					foreach (var item in ftp)
					{
						Console.WriteLine(string.Format("{0} -- {1}", item.Name, item.Type));
					}
					break;
				default:
					throw new InvalidOperationException(string.Format("{0} not supported.",method));
			}
		}

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
	}
}
