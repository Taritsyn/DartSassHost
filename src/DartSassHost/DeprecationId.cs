namespace DartSassHost
{
	/// <summary>
	/// Deprecation IDs
	/// </summary>
	public static class DeprecationId
	{
		/// <summary>
		/// Deprecation for passing a string directly to <c>meta.call()</c>
		/// </summary>
		/// <remarks>
		/// This deprecation was active in the first version of Dart Sass.
		/// </remarks>
		public const string CallString = "call-string";

		/// <summary>
		/// Deprecation for <c>@elseif</c>
		/// </summary>
		/// <remarks>
		/// This deprecation became active in Dart Sass 1.3.2.
		/// </remarks>
		public const string ElseIf = "elseif";

		/// <summary>
		/// Deprecation for <c>@-moz-document</c>
		/// </summary>
		/// <remarks>
		/// This deprecation became active in Dart Sass 1.7.2.
		/// </remarks>
		public const string MozDocument = "moz-document";

		/// <summary>
		/// Deprecation for imports using relative canonical URLs
		/// </summary>
		/// <remarks>
		/// This deprecation became active in Dart Sass 1.14.2.
		/// </remarks>
		public const string RelativeCanonical = "relative-canonical";

		/// <summary>
		/// Deprecation for declaring new variables with <c>!global</c>
		/// </summary>
		/// <remarks>
		/// This deprecation became active in Dart Sass 1.17.2.
		/// </remarks>
		public const string NewGlobal = "new-global";

		/// <summary>
		/// Deprecation for using <c>color</c> module functions in place of plain CSS functions
		/// </summary>
		/// <remarks>
		/// This deprecation became active in Dart Sass 1.23.0.
		/// </remarks>
		public const string ColorModuleCompat = "color-module-compat";

		/// <summary>
		/// Deprecation for <c>/</c> operator for division
		/// </summary>
		/// <remarks>
		/// This deprecation became active in Dart Sass 1.33.0.
		/// </remarks>
		public const string SlashDiv = "slash-div";

		/// <summary>
		/// Deprecation for leading, trailing, and repeated combinators
		/// </summary>
		/// <remarks>
		/// This deprecation became active in Dart Sass 1.54.0.
		/// </remarks>
		public const string BogusCombinators = "bogus-combinators";

		/// <summary>
		/// Deprecation for ambiguous <c>+</c> and <c>-</c> operators
		/// </summary>
		/// <remarks>
		/// This deprecation became active in Dart Sass 1.55.0.
		/// </remarks>
		public const string StrictUnary = "strict-unary";

		/// <summary>
		/// Deprecation for passing invalid units to built-in functions
		/// </summary>
		/// <remarks>
		/// This deprecation became active in Dart Sass 1.56.0.
		/// </remarks>
		public const string FunctionUnits = "function-units";

		/// <summary>
		/// Deprecation for using <c>!default</c> or <c>!global</c> multiple times for one variable
		/// </summary>
		/// <remarks>
		/// This deprecation became active in Dart Sass 1.62.0.
		/// </remarks>
		public const string DuplicateVarFlags = "duplicate-var-flags";

		/// <summary>
		/// Deprecation for passing <c>null</c> as alpha in the JS API
		/// </summary>
		/// <remarks>
		/// This deprecation became active in Dart Sass 1.62.3.
		/// </remarks>
		public const string NullAlpha = "null-alpha";

		/// <summary>
		/// Deprecation for passing percentages to the Sass <c>abs()</c> function
		/// </summary>
		/// <remarks>
		/// This deprecation became active in Dart Sass 1.65.0.
		/// </remarks>
		public const string AbsPercent = "abs-percent";

		/// <summary>
		/// Deprecation for using the current working directory as an implicit load path
		/// </summary>
		/// <remarks>
		/// This deprecation became active in Dart Sass 1.73.0.
		/// </remarks>
		public const string FsImporterCwd = "fs-importer-cwd";

		/// <summary>
		/// Deprecation for function and mixin names beginning with <c>--</c>
		/// </summary>
		/// <remarks>
		/// This deprecation became active in Dart Sass 1.76.0.
		/// </remarks>
		public const string CssFunctionMixin = "css-function-mixin";

		/// <summary>
		/// Deprecation for declarations after or between nested rules
		/// </summary>
		/// <remarks>
		/// This deprecation became active in Dart Sass 1.77.7.
		/// It became obsolete in Dart Sass 1.92.0.
		/// </remarks>
		public const string MixedDecls = "mixed-decls";

		/// <summary>
		/// Deprecation for <c>meta.feature-exists</c>
		/// </summary>
		/// <remarks>
		/// This deprecation became active in Dart Sass 1.78.0.
		/// </remarks>
		public const string FeatureExists = "feature-exists";

		/// <summary>
		/// Deprecation for certain uses of built-in <c>sass:color</c> functions
		/// </summary>
		/// <remarks>
		/// This deprecation became active in Dart Sass 1.79.0.
		/// </remarks>
		public const string Color4Api = "color-4-api";

		/// <summary>
		/// Deprecation for using global color functions instead of <c>sass:color</c>
		/// </summary>
		/// <remarks>
		/// This deprecation became active in Dart Sass 1.79.0.
		/// </remarks>
		public const string ColorFunctions = "color-functions";

		/// <summary>
		/// Deprecation for legacy JS API
		/// </summary>
		/// <remarks>
		/// This deprecation became active in Dart Sass 1.79.0.
		/// </remarks>
		public const string LegacyJsApi = "legacy-js-api";

		/// <summary>
		/// Deprecation for <c>@import</c> rules
		/// </summary>
		/// <remarks>
		/// This deprecation became active in Dart Sass 1.80.0.
		/// </remarks>
		public const string Import = "import";

		/// <summary>
		/// Deprecation for global built-in functions that are available in <c>sass:</c> modules
		/// </summary>
		/// <remarks>
		/// This deprecation became active in Dart Sass 1.80.0.
		/// </remarks>
		public const string GlobalBuiltin = "global-builtin";

		/// <summary>
		/// Deprecation for functions named "type"
		/// </summary>
		/// <remarks>
		/// This deprecation became active in Dart Sass 1.86.0.
		/// It became obsolete in Dart Sass 1.92.0.
		/// </remarks>
		public const string TypeFunction = "type-function";

		/// <summary>
		/// Deprecation for passing a relative url to <c>compileString()</c>
		/// </summary>
		/// <remarks>
		/// This deprecation became active in Dart Sass 1.88.0.
		/// </remarks>
		public const string CompileStringRelativeUrl = "compile-string-relative-url";

		/// <summary>
		/// Deprecation for a rest parameter before a positional or named parameter
		/// </summary>
		/// <remarks>
		/// This deprecation became active in Dart Sass 1.91.0.
		/// </remarks>
		public const string MisplacedRest = "misplaced-rest";

		/// <summary>
		/// Deprecation for configuring private variables in <c>@use</c>, <c>@forward</c>, or <c>load-css()</c>
		/// </summary>
		/// <remarks>
		/// This deprecation became active in Dart Sass 1.92.0.
		/// </remarks>
		public const string WithPrivate = "with-private";

		/// <summary>
		/// Used for any user-emitted deprecation warnings
		/// </summary>
		public const string UserAuthored = "user-authored";
	}
}