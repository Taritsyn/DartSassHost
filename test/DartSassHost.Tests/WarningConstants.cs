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
		public const string DeprecatedDivision = "Using / for division is deprecated and will be removed in Dart Sass 2.0.0.\n\n" +
			"Recommendation: {0}\n\n" +
			"More info and automated migrator: https://sass-lang.com/d/slash-div"
			;
	}
}