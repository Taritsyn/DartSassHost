namespace DartSassHost
{
	/// <summary>
	/// Warning levels
	/// </summary>
	public enum WarningLevel
	{
		/// <summary>
		/// Warnings are not displayed
		/// </summary>
		Quiet,

		/// <summary>
		/// Displayed only 5 instances of the same deprecation warning per compilation
		/// </summary>
		Default,

		/// <summary>
		/// Displayed all deprecation warnings
		/// </summary>
		Verbose
	}
}