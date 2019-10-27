using System;
using System.IO;

namespace FtpLibrary
{
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
