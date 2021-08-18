namespace DartSassHost.Extensions
{
	/// <summary>
	/// Extensions for the <see cref="IFileManager"/>
	/// </summary>
	internal static class FileManagerExtensions
	{
		/// <summary>
		/// Reads a text contents of the specified file.
		/// A return value indicates whether the operation succeeded.
		/// </summary>
		/// <param name="source">File manager</param>
		/// <param name="path">The file to open for reading</param>
		/// <param name="content">The string containing all lines of the file</param>
		/// <returns>true if the file was read successfully; otherwise, false</returns>
		internal static bool TryReadFile(this IFileManager source, string path, out string content)
		{
			bool result = false;
			content = null;

			if (!string.IsNullOrWhiteSpace(path) && source != null && source.FileExists(path))
			{
				try
				{
					content = source.ReadFile(path);
					result = true;
				}
				catch
				{
					content = null;
				}
			}

			return result;
		}
	}
}