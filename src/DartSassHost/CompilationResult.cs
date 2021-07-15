using System.Collections.Generic;

namespace DartSassHost
{
	/// <summary>
	/// Compilation result
	/// </summary>
	public sealed class CompilationResult
	{
		/// <summary>
		/// Gets or sets a compilated Sass code
		/// </summary>
		public string CompiledContent
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a list of included files
		/// </summary>
		public IList<string> IncludedFilePaths
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a source map
		/// </summary>
		public string SourceMap
		{
			get;
			set;
		}

		/// <summary>
		/// Gets a list of the warnings
		/// </summary>
		public IList<ProblemInfo> Warnings
		{
			get;
			private set;
		}


		/// <summary>
		/// Constructs a instance of the compilation result
		/// </summary>
		public CompilationResult(string compiledContent, string sourceMap, IList<string> includedFilePaths,
			IList<ProblemInfo> warnings)
		{
			CompiledContent = compiledContent;
			IncludedFilePaths = includedFilePaths;
			SourceMap = sourceMap;
			Warnings = warnings;
		}
	}
}