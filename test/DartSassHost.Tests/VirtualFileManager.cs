using System;

using Moq;

namespace DartSassHost.Tests
{
	public sealed class VirtualFileManager : IFileManager
	{
		private readonly IFileManager _fileManager;
		private readonly string _applicationPath;


		public VirtualFileManager(Mock<IFileManager> fileManagerMock)
			: this(fileManagerMock, string.Empty)
		{ }

		public VirtualFileManager(Mock<IFileManager> fileManagerMock, string applicationPath)
		{
			_fileManager = fileManagerMock.Object;
			_applicationPath = applicationPath ?? string.Empty;
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
			if (string.IsNullOrWhiteSpace(path))
			{
				return false;
			}

			return path.StartsWith("~/");
		}

		public string ToAbsoluteVirtualPath(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException(nameof(path));
			}

			if (path.StartsWith("/"))
			{
				return path;
			}
			else if (path.StartsWith("~/") && !string.IsNullOrWhiteSpace(_applicationPath))
			{
				return _applicationPath + "/" + path.Substring(2);
			}

			throw new ArgumentException(
				string.Format("The relative virtual path '{0}' is not allowed here.", path),
				nameof(path)
			);
		}

		public string ReadFile(string path)
		{
			return _fileManager.ReadFile(path);
		}

		#endregion
	}
}