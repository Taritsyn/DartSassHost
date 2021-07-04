using System;
using System.IO;

using JavaScriptEngineSwitcher.Core;

namespace DartSassHost.Tests
{
	public abstract class PhysicalFileSystemTestsBase : TestsBase
	{
		private readonly string _currentDirectory;
		private readonly string _fileExtension;
		private readonly string _subfolderName;
		private readonly bool _indentedSyntax;

		public abstract string BaseDirectoryPath { get; }

		public bool IndentedSyntax
		{
			get { return _indentedSyntax; }
		}


		protected PhysicalFileSystemTestsBase(SyntaxType syntaxType)
		{
#if NETFULL
			_currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
#else
			_currentDirectory = Directory.GetCurrentDirectory();
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


		public SassCompiler CreateSassCompiler()
		{
			return new SassCompiler(PhysicalFileManager.Instance);
		}

		public SassCompiler CreateSassCompiler(IJsEngineFactory jsEngineFactory)
		{
			return new SassCompiler(jsEngineFactory, PhysicalFileManager.Instance);
		}

		private string ToAbsolutePath(string path)
		{
			string absolutePath = Path.GetFullPath(Path.Combine(_currentDirectory, path));

			return absolutePath;
		}

		public string GenerateSassFilePath(string folderName, string fileName)
		{
			string fullFilePath = ToAbsolutePath(
				Path.Combine("Files", BaseDirectoryPath, folderName, _subfolderName, fileName + _fileExtension)
			);

			return fullFilePath;
		}

		public string GenerateSassDirectoryPath(string folderName, string directoryName)
		{
			string directoryFilePath = ToAbsolutePath(
				Path.Combine("Files", BaseDirectoryPath, folderName, _subfolderName, directoryName)
			);

			return directoryFilePath;
		}

		public string GenerateCssFilePath(string folderName, string fileName)
		{
			string fullFilePath = ToAbsolutePath(
				Path.Combine("Files", BaseDirectoryPath, folderName, fileName + ".css")
			);

			return fullFilePath;
		}

		public string GenerateSourceMapFilePath(string folderName, string fileName)
		{
			string fullFilePath = ToAbsolutePath(
				Path.Combine("Files", BaseDirectoryPath, folderName, _subfolderName, fileName + ".css.map")
			);

			return fullFilePath;
		}

		public string GenerateCssFileWithInlineSourceMapFilePath(string folderName, string fileName)
		{
			string fullFilePath = ToAbsolutePath(
				Path.Combine("Files", BaseDirectoryPath, folderName, _subfolderName, fileName + ".css")
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