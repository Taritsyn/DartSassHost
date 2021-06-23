using System;
using System.Collections.Generic;
using System.Text;

namespace DartSassHost
{
	/// <summary>
	/// Defines a interface of file manager
	/// </summary>
	public interface IFileManager
	{
		/// <summary>
		/// Gets a value that indicates if the file manager supports virtual paths
		/// </summary>
		bool SupportsVirtualPaths
		{
			get;
		}


		/// <summary>
		/// Gets a current working directory of the application
		/// </summary>
		/// <returns>The string containing the path of the current working directory</returns>
		string GetCurrentDirectory();

		/// <summary>
		/// Determines whether the specified file exists
		/// </summary>
		/// <param name="path">The file to check</param>
		/// <returns>true if the caller has the required permissions and path contains
		/// the name of an existing file; otherwise, false</returns>
		bool FileExists(string path);

		/// <summary>
		/// Gets a value indicating whether the specified virtual path is relative to the application
		/// </summary>
		/// <param name="path">The virtual path to check</param>
		/// <returns>true if virtual path is relative to the application; otherwise, false</returns>
		bool IsAppRelativeVirtualPath(string path);

		/// <summary>
		/// Converts a application-relative path to an absolute virtual path
		/// </summary>
		/// <param name="path">The virtual path</param>
		/// <returns>The absolute path representation of the specified virtual path</returns>
		string ToAbsoluteVirtualPath(string path);

		/// <summary>
		/// Opens a file, reads all lines of the file, and then closes the file
		/// </summary>
		/// <param name="path">The file to open for reading</param>
		/// <returns>The string containing all lines of the file</returns>
		string ReadFile(string path);
	}
}