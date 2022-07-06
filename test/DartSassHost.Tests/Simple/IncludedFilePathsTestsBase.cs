using System.Collections.Generic;

using NUnit.Framework;

namespace DartSassHost.Tests.Simple
{
	public abstract class IncludedFilePathsTestsBase : PhysicalFileSystemTestsBase
	{
		public override string BaseDirectoryPath => "simple/included-file-paths";


		protected IncludedFilePathsTestsBase(SyntaxType syntaxType)
			: base(syntaxType)
		{ }


		[Test]
		public void Compilation([Values]bool fromFile)
		{
			// Arrange
			string inputPath = GenerateSassFilePath("ordinary", "interpolation");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;

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
			Assert.AreEqual(1, includedFilePaths.Count);
			Assert.AreEqual(inputPath, includedFilePaths[0]);
		}

		[Test]
		public void CompilationOfCodeWithoutInputPathParameter()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("ordinary", "interpolation");
			string input = GetFileContent(inputPath);

			// Act
			IList<string> includedFilePaths;

			using (var compiler = CreateSassCompiler())
			{
				includedFilePaths = compiler.Compile(input, this.IndentedSyntax).IncludedFilePaths;
			}

			// Assert
			Assert.AreEqual(0, includedFilePaths.Count);
		}
	}
}