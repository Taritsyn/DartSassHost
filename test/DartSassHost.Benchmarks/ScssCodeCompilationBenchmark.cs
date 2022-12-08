using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

namespace DartSassHost.Benchmarks
{
	[MemoryDiagnoser]
	public class ScssCodeCompilationBenchmark : ScssCompilationBenchmarkBase
	{
		[Benchmark]
		public void Compile()
		{
			Document document = Documents[DocumentName];
			var options = new CompilationOptions
			{
				IncludePaths = document.IncludePaths,
				SourceMap = true
			};

			using (var compiler = new SassCompiler(options))
			{
				CompilationResult result = compiler.Compile(document.Content, document.AbsolutePath);
			}
		}
	}
}