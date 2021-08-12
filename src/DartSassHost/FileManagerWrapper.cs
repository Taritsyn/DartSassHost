namespace DartSassHost
{
	/// <summary>
	/// Wrapper around the file manager that provides compatibility with host object embedding mechanism
	/// of the Microsoft ClearScript.V8 library
	/// </summary>
	public sealed class FileManagerWrapper : IFileManager
	{
		/// <summary>
		/// File manager
		/// </summary>
		IFileManager _fileManager;


		/// <summary>
		/// Constructs an instance of the file manager wrapper
		/// </summary>
		/// <param name="fileManager">File manager</param>
		public FileManagerWrapper(IFileManager fileManager)
		{
			_fileManager = fileManager;
		}


		#region IFileManager implementation

		public bool SupportsVirtualPaths
		{
			get { return _fileManager.SupportsVirtualPaths; }
		}


		public string GetCurrentDirectory()
		{
			return _fileManager.GetCurrentDirectory();
		}

		public bool FileExists(string path)
		{
			return _fileManager.FileExists(path);
		}

		public bool IsAppRelativeVirtualPath(string path)
		{
			return _fileManager.IsAppRelativeVirtualPath(path);
		}

		public string ToAbsoluteVirtualPath(string path)
		{
			return _fileManager.ToAbsoluteVirtualPath(path);
		}

		public string ReadFile(string path)
		{
			return _fileManager.ReadFile(path);
		}

		#endregion
	}
}