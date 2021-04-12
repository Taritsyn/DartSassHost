namespace DartSassHost.Helpers
{
	/// <summary>
	/// Resource helpers
	/// </summary>
	internal static class ResourceHelpers
	{
		/// <summary>
		/// Namespace for resources
		/// </summary>
		private const string RESOURCES_NAMESPACE = "DartSassHost.Resources";

		/// <summary>
		/// Gets a namespace for resources
		/// </summary>
		internal static string ResourcesNamespace
		{
			get { return RESOURCES_NAMESPACE; }
		}


		/// <summary>
		/// Gets a resource name
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <returns>Resource name</returns>
		internal static string GetResourceName(string fileName)
		{
			string resourceName = RESOURCES_NAMESPACE + "." + fileName;

			return resourceName;
		}
	}
}