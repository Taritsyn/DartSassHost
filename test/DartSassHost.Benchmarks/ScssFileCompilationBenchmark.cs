using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

namespace DartSassHost.Benchmarks
{
	[MemoryDiagnoser]
	public class ScssFileCompilationBenchmark : ScssCompilationBenchmarkBase
	{
		[Benchmark]
		public void Compile()
		{
			Document document = Documents[DocumentName];
			var options = new CompilationOptions { SourceMap = true };

			using (var compiler = new SassCompiler(options))
			{
				CompilationResult result = compiler.CompileFile(document.AbsolutePath);
			}
		}
	}
}