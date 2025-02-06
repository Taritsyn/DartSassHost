using System;
#if NETSTANDARD2_1 || NET8_0_OR_GREATER
using System.IO;
#else
using System.Collections.Generic;
#if !NET40
using System.Runtime.InteropServices;
#else

using PolyfillsForOldDotNet.System.Runtime.InteropServices;
#endif
#endif

using DartSassHost.Resources;

namespace DartSassHost.Helpers
{
	/// <summary>
	/// Path helpers
	/// </summary>
	internal static class PathHelpers
	{
		internal static string PrettifyPath(string currentDirectory, string path)
		{
			if (currentDirectory == null)
			{
				throw new ArgumentNullException(
					nameof(currentDirectory),
					string.Format(Strings.Common_ArgumentIsNull, nameof(currentDirectory))
				);
			}

			if (path == null)
			{
				throw new ArgumentNullException(
					nameof(path),
					string.Format(Strings.Common_ArgumentIsNull, nameof(path))
				);
			}

#if NETSTANDARD2_1 || NET8_0_OR_GREATER
			string processedPath = Path.GetRelativePath(currentDirectory, path);
#else
			string processedPath = MakeRelativePath(currentDirectory, path);
#endif
			processedPath = ProcessBackSlashes(processedPath);

			return processedPath;
		}

		/// <summary>
		/// Converts a back slashes to forward slashes
		/// </summary>
		/// <param name="path">Path with back slashes</param>
		/// <returns>Path with forward slashes</returns>
		private static string ProcessBackSlashes(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException(
					nameof(path),
					string.Format(Strings.Common_ArgumentIsNull, nameof(path))
				);
			}

			if (string.IsNullOrWhiteSpace(path))
			{
				return path;
			}

			string result = path.Replace('\\', '/');

			return result;
		}
#if !NETSTANDARD2_1 && !NET8_0_OR_GREATER

		private static string MakeRelativePath(string relativeTo, string path)
		{
			if (relativeTo == null)
			{
				throw new ArgumentNullException(
					nameof(relativeTo),
					string.Format(Strings.Common_ArgumentIsNull, nameof(relativeTo))
				);
			}

			if (path == null)
			{
				throw new ArgumentNullException(
					nameof(path),
					string.Format(Strings.Common_ArgumentIsNull, nameof(path))
				);
			}

			if (string.IsNullOrWhiteSpace(path))
			{
				throw new ArgumentException(
					nameof(path),
					string.Format(Strings.Common_ArgumentIsEmpty, nameof(path))
				);
			}

			if (string.IsNullOrWhiteSpace(relativeTo))
			{
				return ProcessBackSlashes(path);
			}

			string processedRelativeTo = ProcessBackSlashes(relativeTo);
			string processedPath = ProcessBackSlashes(path);

			if (processedRelativeTo == "/" && processedPath.StartsWith("/"))
			{
				return processedPath.TrimStart(new[] { '/' });
			}

			if (!processedRelativeTo.EndsWith("/"))
			{
				processedRelativeTo += "/";
			}

			string relativePath;

			try
			{
				Uri baseUri = new Uri(processedRelativeTo);
				Uri uri = new Uri(processedPath);

				if (baseUri.Scheme != uri.Scheme)
				{
					return processedPath;
				}

				Uri relativeUri = baseUri.MakeRelativeUri(uri);
				relativePath = Uri.UnescapeDataString(relativeUri.ToString());
			}
			catch (UriFormatException)
			{
				// Primitive fallback
				processedRelativeTo = NormalizePath(processedRelativeTo);
				processedPath = NormalizePath(processedPath);
				StringComparison comparisonType = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
					StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

				relativePath = processedPath;
				if (processedPath.StartsWith(processedRelativeTo, comparisonType))
				{
					relativePath = processedPath.Substring(processedRelativeTo.Length);
				}
			}

			return relativePath;
		}

		/// <summary>
		/// Normalizes a path
		/// </summary>
		/// <param name="path">Path</param>
		/// <returns>Normalized path</returns>
		private static string NormalizePath(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException(
				nameof(path),
					string.Format(Strings.Common_ArgumentIsNull, nameof(path))
				);
			}

			if (string.IsNullOrWhiteSpace(path))
			{
				return path;
			}

			string resultPath = path;

			if (path.IndexOf("./", StringComparison.Ordinal) != -1)
			{
				string[] pathParts = path.Split('/');
				int pathPartCount = pathParts.Length;
				if (pathPartCount == 0)
				{
					return path;
				}

				var resultPathParts = new List<string>();

				for (int pathPartIndex = 0; pathPartIndex < pathPartCount; pathPartIndex++)
				{
					string pathPart = pathParts[pathPartIndex];

					switch (pathPart)
					{
						case "..":
							int resultPathPartCount = resultPathParts.Count;
							int resultPathPartLastIndex = resultPathPartCount - 1;

							if (resultPathPartCount == 0 || resultPathParts[resultPathPartLastIndex] == "..")
							{
								resultPathParts.Add(pathPart);
							}
							else
							{
								resultPathParts.RemoveAt(resultPathPartLastIndex);
							}
							break;
						case ".":
							break;
						default:
							resultPathParts.Add(pathPart);
							break;
					}
				}

				resultPath = string.Join("/", resultPathParts);
				resultPathParts.Clear();
			}

			return resultPath;
		}
#endif
	}
}