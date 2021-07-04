using System;
using System.IO;

using DartSassHost.Resources;

namespace DartSassHost
{
	/// <summary>
	/// File manager
	/// </summary>
	public sealed class FileManager : IFileManager
	{
		/// <summary>
		/// Instance of file manager
		/// </summary>
		private static readonly Lazy<FileManager> _instance = new Lazy<FileManager>(() => new FileManager());

		/// <summary>
		/// Current working directory of the application
		/// </summary>
		private readonly string _currentDirectory;

		/// <summary>
		/// Gets a instance of file manager
		/// </summary>
		public static IFileManager Instance
		{
			get { return _instance.Value; }
		}


		/// <summary>
		/// Private constructor for implementation Singleton pattern
		/// </summary>
		private FileManager()
		{
			_currentDirectory = Directory.GetCurrentDirectory();
		}


		#region IFileManager implementation

		public bool SupportsVirtualPaths
		{
			get { return false; }
		}


		public string GetCurrentDirectory()
		{
			return _currentDirectory;
		}

		public bool FileExists(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException(
					nameof(path),
					string.Format(Strings.Common_ArgumentIsNull, nameof(path))
				);
			}

			bool result = File.Exists(path);

			return result;
		}

		public bool IsAppRelativeVirtualPath(string path)
		{
			throw new NotImplementedException();
		}

		public string ToAbsoluteVirtualPath(string path)
		{
			throw new NotImplementedException();
		}

		public string ReadFile(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException(
					nameof(path),
					string.Format(Strings.Common_ArgumentIsNull, nameof(path))
				);
			}

			string content = File.ReadAllText(path);

			return content;
		}

		#endregion
	}
}