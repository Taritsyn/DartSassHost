using System.Collections.Generic;

namespace DartSassHost
{
	/// <summary>
	/// Compilation options
	/// </summary>
	public sealed class CompilationOptions
	{
		/// <summary>
		/// Gets or sets a flag for whether to emit a <c>@charset</c> or BOM for CSS with non-ASCII characters
		/// </summary>
		public bool Charset
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a list of paths that library can look in to attempt to resolve <с>@import</с> declarations
		/// </summary>
		/// <remarks>
		/// <para>When using <see cref="SassCompiler.Compile(string, bool, CompilationOptions)"/> method, it is
		/// recommended that use this property.</para>
		/// </remarks>
		public IList<string> IncludePaths
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a indent type
		/// </summary>
		public IndentType IndentType
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a number of spaces or tabs to be used for indentation
		/// </summary>
		public int IndentWidth
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a flag for whether to embed <с>sourceMappingUrl</с> as data uri
		/// </summary>
		public bool InlineSourceMap
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a line feed type
		/// </summary>
		public LineFeedType LineFeedType
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a flag for whether to disable <с>sourceMappingUrl</с> in css output
		/// </summary>
		public bool OmitSourceMapUrl
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a output style for the generated css code
		/// </summary>
		public OutputStyle OutputStyle
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a flag for whether to silence compiler warnings from stylesheets loaded by using the
		/// <see cref="IncludePaths"/> property
		/// </summary>
		public bool QuietDependencies
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a list of active deprecations to ignore
		/// </summary>
		/// <remarks>
		/// <para>If a deprecation warning of any provided ID is encountered during compilation, the compiler will
		/// ignore it instead.</para>
		/// </remarks>
		public IList<string> SilenceDeprecations
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a flag for whether to enable source map generation
		/// </summary>
		public bool SourceMap
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a flag for whether to include contents in maps
		/// </summary>
		public bool SourceMapIncludeContents
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value will be emitted as <с>sourceRoot</с> in the source map information
		/// </summary>
		public string SourceMapRootPath
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a warning level
		/// </summary>
		public WarningLevel WarningLevel
		{
			get;
			set;
		}


		/// <summary>
		/// Constructs a instance of the Sass compilation options
		/// </summary>
		public CompilationOptions()
		{
			Charset = true;
			IncludePaths = new List<string>();
			IndentType = IndentType.Space;
			IndentWidth = 2;
			InlineSourceMap = false;
			LineFeedType = LineFeedType.Lf;
			OmitSourceMapUrl = false;
			OutputStyle = OutputStyle.Expanded;
			QuietDependencies = false;
			SilenceDeprecations = new List<string>();
			SourceMap = false;
			SourceMapIncludeContents = false;
			SourceMapRootPath = string.Empty;
			WarningLevel = WarningLevel.Default;
		}
	}
}