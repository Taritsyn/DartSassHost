using System;
using System.IO;
using System.Reflection;

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

		public bool SupportsVirtualPaths
		{
			get { return FileManager.Instance.SupportsVirtualPaths; }
		}


		public string GetCurrentDirectory()
		{
			return Path.GetDirectoryName(this.GetType().Assembly.Location);
		}

		public bool FileExists(string path)
		{
			return FileManager.Instance.FileExists(path);
		}

		public bool IsAppRelativeVirtualPath(string path)
		{
			return FileManager.Instance.IsAppRelativeVirtualPath(path);
		}

		public string ToAbsoluteVirtualPath(string path)
		{
			return FileManager.Instance.ToAbsoluteVirtualPath(path);
		}

		public string ReadFile(string path)
		{
			return FileManager.Instance.ReadFile(path);
		}

		#endregion
	}
}