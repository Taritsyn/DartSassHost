using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;

using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.Core.Helpers;

using CoreStrings = JavaScriptEngineSwitcher.Core.Resources.Strings;

namespace DartSassHost.Extensions
{
	/// <summary>
	/// Extensions for the <see cref="IJsEngine"/>
	/// </summary>
	internal static class JsEngineExtensions
	{
		/// <summary>
		/// Script code cache
		/// </summary>
		private static ConcurrentDictionary<string, string> _scriptCodeCache;

		/// <summary>
		/// Synchronizer of script code cache initialization
		/// </summary>
		private static readonly object _scriptCodeCacheInitializationSynchronizer = new object();

		/// <summary>
		/// Flag indicating whether the script code cache is initialized
		/// </summary>
		private static bool _scriptCodeCacheInitialized;

		/// <summary>
		/// Executes a code from embedded JS resource
		/// </summary>
		/// <param name="source">JS engine</param>
		/// <param name="resourceName">The case-sensitive resource name</param>
		/// <param name="assembly">The assembly, which contains the embedded resource</param>
		/// <param name="useCache">Flag for whether to use the cache for script code retrieved from
		/// the embedded resource</param>
		/// <exception cref="ObjectDisposedException"/>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="NullReferenceException"/>
		/// <exception cref="JsUsageException"/>
		/// <exception cref="JsCompilationException"/>
		/// <exception cref="JsTimeoutException"/>
		/// <exception cref="JsInterruptedException"/>
		/// <exception cref="JsRuntimeException"/>
		/// <exception cref="JsException"/>
		internal static void ExecuteResource(this IJsEngine source, string resourceName, Assembly assembly, bool useCache)
		{
			if (useCache)
			{
				if (resourceName == null)
				{
					throw new ArgumentNullException(
						nameof(resourceName),
						string.Format(CoreStrings.Common_ArgumentIsNull, nameof(resourceName))
					);
				}

				if (assembly == null)
				{
					throw new ArgumentNullException(
						nameof(assembly),
						string.Format(CoreStrings.Common_ArgumentIsNull, nameof(assembly))
					);
				}

				if (string.IsNullOrWhiteSpace(resourceName))
				{
					throw new ArgumentException(
						string.Format(CoreStrings.Common_ArgumentIsEmpty, nameof(resourceName)),
						nameof(resourceName)
					);
				}

				if (!ValidationHelpers.CheckDocumentNameFormat(resourceName))
				{
					throw new ArgumentException(
						string.Format(CoreStrings.Usage_InvalidResourceNameFormat, resourceName),
						nameof(resourceName)
					);
				}

				if (!_scriptCodeCacheInitialized)
				{
					lock (_scriptCodeCacheInitializationSynchronizer)
					{
						if (!_scriptCodeCacheInitialized)
						{
							_scriptCodeCache = new ConcurrentDictionary<string, string>();
							_scriptCodeCacheInitialized = true;
						}
					}
				}

				string scriptCode = _scriptCodeCache.GetOrAdd(resourceName, key => ReadResourceAsString(key, assembly));
				source.Execute(scriptCode, resourceName);
			}
			else
			{
				source.ExecuteResource(resourceName, assembly);
			}
		}

		/// <summary>
		/// Gets a content of the embedded resource as string
		/// </summary>
		/// <param name="resourceName">The case-sensitive resource name</param>
		/// <param name="assembly">The assembly, which contains the embedded resource</param>
		/// <returns>Сontent of the embedded resource as string</returns>
		private static string ReadResourceAsString(string resourceName, Assembly assembly)
		{
			using (Stream stream = assembly.GetManifestResourceStream(resourceName))
			{
				if (stream == null)
				{
					throw new NullReferenceException(
						string.Format(CoreStrings.Common_ResourceIsNull, resourceName)
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