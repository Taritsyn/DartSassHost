using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace SassHost.Tests.Simple
{
	public abstract class IncludedFilePathsTestsBase : PhysicalFileSystemTestsBase
	{
		public override string BaseDirectoryPath => "simple/included-file-paths";


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
			string inputPath = GenerateSassFilePath("ordinary", "interpolation");
			string input = GetFileContent(inputPath);

			// Act
			IList<string> includedFilePaths1;
			IList<string> includedFilePaths2;

			using (var compiler = new SassCompiler())
			{
				includedFilePaths1 = compiler.Compile(input, this.IndentedSyntax).IncludedFilePaths;
				includedFilePaths2 = compiler.Compile(input, inputPath).IncludedFilePaths;
			}

			// Assert
			Assert.AreEqual(0, includedFilePaths1.Count);

			Assert.AreEqual(1, includedFilePaths2.Count);
			Assert.AreEqual(ToAbsolutePath(inputPath), includedFilePaths2[0]);
		}

		#endregion

		#region Files

		[Test]
		public void CompilationOfFile()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("ordinary", "interpolation");
			string input = GetFileContent(inputPath);

			// Act
			IList<string> includedFilePaths;

			using (var compiler = new SassCompiler())
			{
				includedFilePaths = compiler.CompileFile(inputPath).IncludedFilePaths;
			}

			// Assert
			Assert.AreEqual(1, includedFilePaths.Count);
			Assert.AreEqual(ToAbsolutePath(inputPath), includedFilePaths[0]);
		}

		#endregion
	}
}