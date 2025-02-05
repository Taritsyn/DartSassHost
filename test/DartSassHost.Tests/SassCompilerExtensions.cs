namespace DartSassHost.Tests
{
	internal static class SassCompilerExtensions
	{
		public static CompilationResult AdvancedCompile(this SassCompiler source, bool fromFile, string content,
			string inputPath, string outputPath = null, string sourceMapPath = null, CompilationOptions options = null)
		{
			if (fromFile)
			{
				return source.CompileFile(inputPath, outputPath, sourceMapPath, options);
			}
			else
			{
				return source.Compile(content, inputPath, outputPath, sourceMapPath, options);
			}
		}
	}
}