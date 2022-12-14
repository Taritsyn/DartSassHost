using JavaScriptEngineSwitcher.Core;
#if !DEBUG && (NET471 || NETCOREAPP3_1_OR_GREATER)
using JavaScriptEngineSwitcher.NiL;
#endif
using NUnit.Framework;

namespace DartSassHost.Tests.Simple
{
	public abstract class ErrorTestsBase : PhysicalFileSystemTestsBase
	{
		public override string BaseDirectoryPath => "simple/errors";


		protected ErrorTestsBase(SyntaxType syntaxType)
			: base(syntaxType)
		{ }


#if !DEBUG && (NET471 || NETCOREAPP3_1_OR_GREATER)
		[Test]
		public void MappingSassCompilerLoadErrorDuringCompilation([Values]bool fromFile)
		{
			// Arrange
			IJsEngineFactory jsEngineFactory = new NiLJsEngineFactory();
			string inputPath = GenerateSassFilePath("simplest-working", "style");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;
			const string targetErrorDescription = "SyntaxError: Expected \";\" at +\r\n" +
				"   at 2:1"
				;

			// Act
			string output;
			SassCompilerLoadException exception = null;

			try
			{
				using (var sassCompiler = CreateSassCompiler(jsEngineFactory))
				{
					if (fromFile)
					{
						output = sassCompiler.CompileFile(inputPath).CompiledContent;
					}
					else
					{
						output = sassCompiler.Compile(input, inputPath).CompiledContent;
					}
				}
			}
			catch (SassCompilerLoadException e)
			{
				exception = e;
			}

			// Assert
			Assert.NotNull(exception);
			Assert.AreEqual(
				"During loading of Sass compiler error has occurred. See the original error message: " +
				"“" + targetErrorDescription + "”.",
				exception.Message
			);
			Assert.AreEqual(targetErrorDescription, exception.Description);
		}

#endif
		[Test]
		public void MappingFileNotFoundErrorDuringCompilationOfFile()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("non-existing-files", "style");

			// Act
			string output;
			SassCompilationException exception = null;

			try
			{
				using (var sassCompiler = CreateSassCompiler())
				{
					output = sassCompiler.CompileFile(inputPath).CompiledContent;
				}
			}
			catch (SassCompilationException e)
			{
				exception = e;
			}

			// Assert
			Assert.NotNull(exception);
			Assert.AreEqual(
				string.Format("Error: No such file or directory: {0}", inputPath),
				exception.Message
			);
			Assert.AreEqual(
				string.Format("No such file or directory: {0}", inputPath),
				exception.Description
			);
			Assert.AreEqual(3, exception.Status);
			Assert.IsEmpty(exception.File);
			Assert.AreEqual(0, exception.LineNumber);
			Assert.AreEqual(0, exception.ColumnNumber);
			Assert.IsEmpty(exception.SourceFragment);
			Assert.IsEmpty(exception.CallStack);
		}
	}
}