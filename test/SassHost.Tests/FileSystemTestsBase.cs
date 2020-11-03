using System;
using System.Collections.Concurrent;
using System.IO;
#if NETCOREAPP1_0

using Microsoft.Extensions.PlatformAbstractions;
#endif

using SassHost.Helpers;

namespace SassHost.Tests
{
	public abstract class FileSystemTestsBase
	{
		private readonly string _appDirectoryPath;
		private readonly string _fileExtension;
		private readonly string _subfolderName;
		private readonly bool _indentedSyntax;

		public abstract string BaseDirectoryPath { get; }

		public bool IndentedSyntax
		{
			get { return _indentedSyntax; }
		}


		protected FileSystemTestsBase(SyntaxType syntaxType)
		{
#if NETCOREAPP1_0
			_appDirectoryPath = PlatformServices.Default.Application.ApplicationBasePath;
#else
			_appDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
#endif

			if (syntaxType == SyntaxType.Sass)
			{
				_fileExtension = ".sass";
				_subfolderName = "sass";
				_indentedSyntax = true;
			}
			else if (syntaxType == SyntaxType.Scss)
			{
				_fileExtension = ".scss";
				_subfolderName = "scss";
				_indentedSyntax = false;
			}
			else
			{
				throw new NotSupportedException();
			}
		}


		public string ToAbsolutePath(string path)
		{
			string absolutePath = Path.GetFullPath(Path.Combine(_appDirectoryPath, path));

			return absolutePath;
		}

		public string GenerateSassFilePath(string folderName, string fileName)
		{
			string fullFilePath = PathHelpers.ProcessBackSlashes(
				Path.Combine("Files", BaseDirectoryPath, folderName, _subfolderName, fileName + _fileExtension)
			);

			return fullFilePath;
		}

		public string GenerateSassDirectoryPath(string folderName, string directoryName)
		{
			string directoryFilePath = PathHelpers.ProcessBackSlashes(
				Path.Combine("Files", BaseDirectoryPath, folderName, _subfolderName, directoryName)
			);

			return directoryFilePath;
		}

		public string GenerateCssFilePath(string folderName, string fileName)
		{
			string fullFilePath = PathHelpers.ProcessBackSlashes(
				Path.Combine("Files", BaseDirectoryPath, folderName, fileName + ".css")
			);

			return fullFilePath;
		}

		public string GetFileContent(string filePath)
		{
			string content = File.ReadAllText(filePath);

			return content;
		}

		public string GetFileContent(string filePath, LineFeedType lineFeedType)
		{
			string lineFeed;

			switch (lineFeedType)
			{
				case LineFeedType.Cr:
					lineFeed = "\r";
					break;

				case LineFeedType.CrLf:
					lineFeed = "\r\n";
					break;

				case LineFeedType.Lf:
					lineFeed = "\n";
					break;

				case LineFeedType.LfCr:
					lineFeed = "\n\r";
					break;

				default:
					throw new NotSupportedException();
			}

			string[] lines = File.ReadAllLines(filePath);
			string content = string.Join(lineFeed, lines);

			return content;
		}
	}
}