using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;

using DartSassHost.Resources;

namespace DartSassHost
{
	/// <summary>
	/// Storage for script resources
	/// </summary>
	internal class ScriptResourceStorage
	{
		/// <summary>
		/// Namespace for resources
		/// </summary>
		private const string RESOURCES_NAMESPACE = "DartSassHost.Resources";

		/// <summary>
		/// The assembly, which contains the embedded resources
		/// </summary>
		private readonly Assembly _assembly = typeof(SassCompiler)
#if !NET40
			.GetTypeInfo()
#endif
			.Assembly
			;

		/// <summary>
		/// Script code cache
		/// </summary>
		private readonly ConcurrentDictionary<string, string> _scriptCodeCache = new ConcurrentDictionary<string, string>();


		/// <summary>
		/// Reads a script code from an embedded resource if the it does not already cached
		/// </summary>
		/// <param name="scriptName">Script name</param>
		/// <returns>Script code</returns>
		internal string GetOrReadScriptCode(string scriptName)
		{
			string resourceName = RESOURCES_NAMESPACE + "." + scriptName;
			string scriptCode = _scriptCodeCache.GetOrAdd(resourceName, ReadResourceAsString);

			return scriptCode;
		}

		/// <summary>
		/// Gets a content of the embedded resource as string
		/// </summary>
		/// <param name="resourceName">The case-sensitive resource name</param>
		/// <returns>Сontent of the embedded resource as string</returns>
		private string ReadResourceAsString(string resourceName)
		{
			using (Stream stream = _assembly.GetManifestResourceStream(resourceName))
			{
				if (stream == null)
				{
					throw new NullReferenceException(
						string.Format(Strings.Common_ResourceIsNull, resourceName)
					);
				}

				using (var reader = new StreamReader(stream))
				{
					return reader.ReadToEnd();
				}
			}
		}
	}
}