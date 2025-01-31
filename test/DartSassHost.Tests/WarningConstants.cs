namespace DartSassHost.Tests
{
	/// <summary>
	/// Warnings constants
	/// </summary>
	internal static class WarningConstants
	{
		private const string DeprecatedDivisionDescription = "Using / for division outside of calc() is deprecated and will be removed in Dart Sass 2.0.0.";
		private const string DeprecatedDivisionRecommendation = "Recommendation: ";
		private const string DeprecatedDivisionMoreInfo = "More info and automated migrator: https://sass-lang.com/d/slash-div";


		public const string MathDivOnlySupportNumberArguments = "math.div() will only support number arguments in a future release.\n" +
			"Use list.slash() instead for a slash separator."
			;
		public const string AlwaysQuoteColorNamesInInterpolation = "You probably don't mean to use the color value {0} in interpolation here.\n" +
			"It may end up represented as {0}, which will likely produce invalid CSS.\n" +
			"Always quote color names when using them as strings or map keys (for example, \"{0}\").\n" +
			"If you really want to use the color value here, use '\"\" + {1}'."
			;
		public const string DeprecatedDivisionWithSimpleRecommendation = DeprecatedDivisionDescription + "\n\n" +
			DeprecatedDivisionRecommendation + "math.div({0}, {1}) or calc({0} / {1})" + "\n\n" +
			DeprecatedDivisionMoreInfo
			;
		public const string DeprecatedDivisionWithComplexRecommendation = DeprecatedDivisionDescription + "\n\n" +
			DeprecatedDivisionRecommendation + "math.div({0}, {1}) or calc(({0}) / {1})" + "\n\n" +
			DeprecatedDivisionMoreInfo
			;
		public const string ColorInversionWithNumberArgumentsDeprecated = "Passing a number ({0}) to color.invert() is deprecated.\n\n" +
			"Recommendation: invert({0})"
			;
		public const string UnknownVendorPrefix = "Unknown prefix {0}.";
		public const string RepetitiveDeprecationWarningsOmitted = "{0} repetitive deprecation warnings omitted.";
		public const string NumberValueWithoutPercentagesDeprecated = "${0}: Passing a number without unit % ({1}) is deprecated.\n\n" +
			"To preserve current behavior: ${0} * 1%\n\n" +
			"More info: https://sass-lang.com/d/function-units"
			;
		public const string GlobalBuiltinFunctionDeprecated = "Global built-in functions are deprecated and will be removed in Dart Sass 3.0.0.\n" +
			"Use {0} instead.\n\n" +
			"More info and automated migrator: https://sass-lang.com/d/import"
			;
		public const string SassImportRulesDeprecated = "Sass @import rules are deprecated and will be removed in Dart Sass 3.0.0.\n\n" +
			"More info and automated migrator: https://sass-lang.com/d/import"
			;
	}
}