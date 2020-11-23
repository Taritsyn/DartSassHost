using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace SassHost.Tests.Imports
{
	public abstract class IncludedFilePathsTestsBase : FileSystemTestsBase
	{
		public override string BaseDirectoryPath => "imports/included-file-paths";


		protected IncludedFilePathsTestsBase(SyntaxType syntaxType)
			: base(syntaxType)
		{ }


		[SetUp]
		public void Init()
		{
			JsEngineSwitcherInitializer.Initialize();
		}

		#region Code

		[Test]
		public void CompilationOfCode()
		{
			// Arrange
			string inputPath = ToAbsolutePath(GenerateSassFilePath("ordinary", "style"));
			string input = GetFileContent(inputPath);
			string firstImportedFilePath = ToAbsolutePath(GenerateSassFilePath("ordinary", @"foundation\_code"));
			string secondImportedFilePath = ToAbsolutePath(GenerateSassFilePath("ordinary", @"foundation\_lists"));

			// Act
			IList<string> includedFilePaths;

			using (var compiler = new SassCompiler())
			{
				includedFilePaths = compiler.Compile(input, inputPath).IncludedFilePaths;
			}

			// Assert
			Assert.AreEqual(3, includedFilePaths.Count);
			Assert.AreEqual(inputPath, includedFilePaths[0]);
			Assert.AreEqual(firstImportedFilePath, includedFilePaths[1]);
			Assert.AreEqual(secondImportedFilePath, includedFilePaths[2]);
		}

		#endregion

		#region Files

		[Test]
		public void CompilationOfFile()
		{
			// Arrange
			string inputPath = ToAbsolutePath(GenerateSassFilePath("ordinary", "style"));
			string firstImportedFilePath = ToAbsolutePath(GenerateSassFilePath("ordinary", @"foundation\_code"));
			string secondImportedFilePath = ToAbsolutePath(GenerateSassFilePath("ordinary", @"foundation\_lists"));

			// Act
			IList<string> includedFilePaths;

			using (var compiler = new SassCompiler())
			{
				includedFilePaths = compiler.CompileFile(inputPath).IncludedFilePaths;
			}

			// Assert
			Assert.AreEqual(3, includedFilePaths.Count);
			Assert.AreEqual(inputPath, includedFilePaths[0]);
			Assert.AreEqual(firstImportedFilePath, includedFilePaths[1]);
			Assert.AreEqual(secondImportedFilePath, includedFilePaths[2]);
		}

		#endregion
	}
}