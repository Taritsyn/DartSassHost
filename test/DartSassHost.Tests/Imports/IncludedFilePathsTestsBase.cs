using System.Collections.Generic;

using NUnit.Framework;

namespace DartSassHost.Tests.Imports
{
	public abstract class IncludedFilePathsTestsBase : PhysicalFileSystemTestsBase
	{
		public override string BaseDirectoryPath => "imports/included-file-paths";


		protected IncludedFilePathsTestsBase(SyntaxType syntaxType)
			: base(syntaxType)
		{ }


		[Test]
		public void Compilation([Values]bool fromFile)
		{
			// Arrange
			string inputPath = GenerateSassFilePath("ordinary", "style");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;
			string firstImportedFilePath = GenerateSassFilePath("ordinary", @"foundation/_code");
			string secondImportedFilePath = GenerateSassFilePath("ordinary", @"foundation/_lists");

			// Act
			IList<string> includedFilePaths;

			using (var compiler = CreateSassCompiler())
			{
				if (fromFile)
				{
					includedFilePaths = compiler.CompileFile(inputPath).IncludedFilePaths;
				}
				else
				{
					includedFilePaths = compiler.Compile(input, inputPath).IncludedFilePaths;
				}
			}

			// Assert
			Assert.AreEqual(3, includedFilePaths.Count);
			Assert.AreEqual(inputPath, includedFilePaths[0]);
			Assert.AreEqual(firstImportedFilePath, includedFilePaths[1]);
			Assert.AreEqual(secondImportedFilePath, includedFilePaths[2]);
		}
	}
}