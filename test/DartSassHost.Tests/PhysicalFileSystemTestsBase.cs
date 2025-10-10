using System;
using System.IO;
using System.Reflection;

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
			_currentDirectory = Path.GetDirectoryName(this.GetType().Assembly.Location);

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


		public SassCompiler CreateSassCompiler(CompilationOptions options = null)
		{
			return new SassCompiler(PhysicalFileManager.Instance, options);
		}

		public SassCompiler CreateSassCompiler(IJsEngineFactory jsEngineFactory, CompilationOptions options = null)
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
	}
}