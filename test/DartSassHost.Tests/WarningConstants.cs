namespace DartSassHost.Tests
{
	/// <summary>
	/// Warnings constants
	/// </summary>
	internal static class WarningConstants
	{
		public const string MathDivOnlySupportNumberArguments = "math.div() will only support number arguments in a future release.\n" +
			"Use list.slash() instead for a slash separator."
			;
		public const string AlwaysQuoteColorNamesInInterpolation = "You probably don't mean to use the color value {0} in interpolation here.\n" +
			"It may end up represented as {0}, which will likely produce invalid CSS.\n" +
			"Always quote color names when using them as strings or map keys (for example, \"{0}\").\n" +
			"If you really want to use the color value here, use '\"\" + {1}'."
			;
		public const string DeprecatedDivision = "Using / for division is deprecated and will be removed in Dart Sass 2.0.0.\n\n" +
			"Recommendation: {0}\n\n" +
			"More info and automated migrator: https://sass-lang.com/d/slash-div"
			;
		public const string ColorInversionWithNumberArgumentsDeprecated = "Passing a number ({0}) to color.invert() is deprecated.\n\n" +
			"Recommendation: invert({0})"
			;
		public const string UnknownVendorPrefix = "Unknown prefix {0}.";
		public const string RepetitiveDeprecationWarningsOmitted = "{0} repetitive deprecation warnings omitted.";
		public const string NumberValueWithoutPercentagesDeprecated = "${0}: Passing a number without unit % ({1}) is deprecated.\n\n" +
			"To preserve current behavior: ${0} * 1%"
			;
	}
}