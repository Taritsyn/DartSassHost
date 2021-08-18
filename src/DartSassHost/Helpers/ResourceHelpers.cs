using System.Reflection;

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
		private static readonly string _resourcesNamespace;

		/// <summary>
		/// Gets a namespace for resources
		/// </summary>
		internal static string ResourcesNamespace
		{
			get { return _resourcesNamespace; }
		}


		/// <summary>
		/// Static constructor
		/// </summary>
		static ResourceHelpers()
		{
			var type = typeof(SassCompiler)
#if !NET40
				.GetTypeInfo()
#endif
				;
			_resourcesNamespace = type.Namespace + ".Resources";
		}


		/// <summary>
		/// Gets a resource name
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <returns>Resource name</returns>
		internal static string GetResourceName(string fileName)
		{
			string resourceName = _resourcesNamespace + "." + fileName;

			return resourceName;
		}
	}
}