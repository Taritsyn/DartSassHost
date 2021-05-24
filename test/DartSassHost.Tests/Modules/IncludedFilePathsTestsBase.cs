using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace DartSassHost.Tests.Modules
{
	public abstract class IncludedFilePathsTestsBase : PhysicalFileSystemTestsBase
	{
		public override string BaseDirectoryPath => "modules/included-file-paths";


		protected IncludedFilePathsTestsBase(SyntaxType syntaxType)
			: base(syntaxType)
		{ }


		#region Code

		[Test]
		public void CompilationOfCode()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("ordinary", "style");
			string input = GetFileContent(inputPath);
			string firstImportedFilePath = GenerateSassFilePath("ordinary", "bootstrap");
			string secondImportedFilePath = GenerateSassFilePath("ordinary", @"src\_code");
			string thirdImportedFilePath = GenerateSassFilePath("ordinary", @"src\_lists");

			// Act
			IList<string> includedFilePaths;

			using (var compiler = CreateSassCompiler())
			{
				includedFilePaths = compiler.Compile(input, inputPath).IncludedFilePaths;
			}

			// Assert
			Assert.AreEqual(4, includedFilePaths.Count);
			Assert.AreEqual(inputPath, includedFilePaths[0]);
			Assert.AreEqual(firstImportedFilePath, includedFilePaths[1]);
			Assert.AreEqual(secondImportedFilePath, includedFilePaths[2]);
			Assert.AreEqual(thirdImportedFilePath, includedFilePaths[3]);
		}

		#endregion

		#region Files

		[Test]
		public void CompilationOfFile()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("ordinary", "style");
			string firstImportedFilePath = GenerateSassFilePath("ordinary", "bootstrap");
			string secondImportedFilePath = GenerateSassFilePath("ordinary", @"src\_code");
			string thirdImportedFilePath = GenerateSassFilePath("ordinary", @"src\_lists");

			// Act
			IList<string> includedFilePaths;

			using (var compiler = CreateSassCompiler())
			{
				includedFilePaths = compiler.CompileFile(inputPath).IncludedFilePaths;
			}

			// Assert
			Assert.AreEqual(4, includedFilePaths.Count);
			Assert.AreEqual(inputPath, includedFilePaths[0]);
			Assert.AreEqual(firstImportedFilePath, includedFilePaths[1]);
			Assert.AreEqual(secondImportedFilePath, includedFilePaths[2]);
			Assert.AreEqual(thirdImportedFilePath, includedFilePaths[3]);
		}

		#endregion
	}
}