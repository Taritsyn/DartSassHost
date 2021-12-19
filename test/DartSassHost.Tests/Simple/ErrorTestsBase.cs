using JavaScriptEngineSwitcher.Core;
#if NET471 || NETCOREAPP2_1_OR_GREATER
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


		#region Code

#if NET471 || NETCOREAPP2_1_OR_GREATER
		[Test]
		public void MappingSassCompilerLoadErrorDuringCompilationOfCode()
		{
			// Arrange
			IJsEngineFactory jsEngineFactory = new NiLJsEngineFactory();
			string inputPath = GenerateSassFilePath("simplest-working", "style");
			string input = GetFileContent(inputPath);

			// Act
			string output;
			SassCompilerLoadException exception = null;

			try
			{
				using (var sassCompiler = CreateSassCompiler(jsEngineFactory))
				{
					output = sassCompiler.Compile(input, inputPath).CompiledContent;
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
				"“TypeError: Can't set property \"keys\" of \"undefined\"”.",
				exception.Message
			);
			Assert.AreEqual("TypeError: Can't set property \"keys\" of \"undefined\"", exception.Description);
		}
#endif

		#endregion

		#region Files

#if NET471 || NETCOREAPP2_1_OR_GREATER
		[Test]
		public void MappingSassCompilerLoadErrorDuringCompilationOfFile()
		{
			// Arrange
			IJsEngineFactory jsEngineFactory = new NiLJsEngineFactory();
			string inputPath = GenerateSassFilePath("simplest-working", "style");

			// Act
			string output;
			SassCompilerLoadException exception = null;

			try
			{
				using (var sassCompiler = CreateSassCompiler(jsEngineFactory))
				{
					output = sassCompiler.CompileFile(inputPath).CompiledContent;
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
				"“TypeError: Can't set property \"keys\" of \"undefined\"”.",
				exception.Message
			);
			Assert.AreEqual("TypeError: Can't set property \"keys\" of \"undefined\"", exception.Description);
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

		#endregion
	}
}