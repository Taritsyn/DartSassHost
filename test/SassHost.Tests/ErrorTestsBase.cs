using NUnit.Framework;

namespace SassHost.Tests
{
	public abstract class ErrorTestsBase : FileSystemTestsBase
	{
		public override string BaseDirectoryPath => "errors";


		protected ErrorTestsBase(SyntaxType syntaxType)
			: base(syntaxType)
		{ }


		[SetUp]
		public void Init()
		{
			JsEngineSwitcherInitializer.Initialize();
		}

		#region Files

		[Test]
		public void MappingFileNotFoundErrorDuringCompilationOfFile()
		{
			// Arrange
			string inputPath = ToAbsolutePath(GenerateSassFilePath("non-existing-files", "style"));

			// Act
			string output;
			SassCompilationException exception = null;

			try
			{
				using (var sassCompiler = new SassCompiler())
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
		}

		#endregion
	}
}