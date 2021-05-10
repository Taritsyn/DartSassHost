using System;

namespace DartSassHost.Tests
{
	public sealed class PhysicalFileManager : IFileManager
	{
		private static readonly Lazy<PhysicalFileManager> _instance = new Lazy<PhysicalFileManager>(() => new PhysicalFileManager());

		public static IFileManager Instance
		{
			get { return _instance.Value; }
		}


		private PhysicalFileManager()
		{ }


		#region IFileManager implementation

		public bool SupportsConversionToAbsolutePath
		{
			get { return FileManager.Instance.SupportsConversionToAbsolutePath; }
		}


		public string GetCurrentDirectory()
		{
#if NETFULL
			return AppDomain.CurrentDomain.BaseDirectory;
#else
			return FileManager.Instance.GetCurrentDirectory();
#endif
		}

		public bool FileExists(string path)
		{
			return FileManager.Instance.FileExists(path);
		}

		public bool IsAbsolutePath(string path)
		{
			return FileManager.Instance.IsAbsolutePath(path);
		}

		public string ToAbsolutePath(string path)
		{
			return FileManager.Instance.ToAbsolutePath(path);
		}

		public string ReadFile(string path)
		{
			return FileManager.Instance.ReadFile(path);
		}

		#endregion
	}
}