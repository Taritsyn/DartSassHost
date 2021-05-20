using System;

using Moq;

namespace DartSassHost.Tests
{
	public sealed class VirtualFileManager : IFileManager
	{
		private readonly IFileManager _fileManager;


		public VirtualFileManager(Mock<IFileManager> fileManagerMock)
		{
			_fileManager = fileManagerMock.Object;
		}


		#region IFileManager implementation

		public bool SupportsConversionToAbsolutePath
		{
			get { return _fileManager.SupportsConversionToAbsolutePath; }
		}


		public string GetCurrentDirectory()
		{
			return _fileManager.GetCurrentDirectory();
		}

		public bool FileExists(string path)
		{
			return _fileManager.FileExists(path);
		}

		public bool IsAbsolutePath(string path)
		{
			return _fileManager.IsAbsolutePath(path);
		}

		public string ToAbsolutePath(string path)
		{
			return _fileManager.ToAbsolutePath(path);
		}

		public string ReadFile(string path)
		{
			return _fileManager.ReadFile(path);
		}

		#endregion
	}
}