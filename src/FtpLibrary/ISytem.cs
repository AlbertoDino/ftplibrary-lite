namespace FtpLibrary
{
	/// <summary>
	/// Abstraction of System functions
	/// </summary>
	public interface ISystem
	{
		/// <summary>
		/// Returns the path of a file from root.
		/// "C:/temp/sub/file.txt" -> "temp/sub/file.txt"
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		string GetPathFromRoot(string file);

		/// <summary>
		/// Returns the directory path of a file from root.
		/// "C:/temp/sub/file.txt" -> "temp/sub/"
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		string GetDirPathFromRoot(string file);

		/// <summary>
		/// Returns true if FileExists
		/// </summary>
		bool FileExists(string file);
	}
}
