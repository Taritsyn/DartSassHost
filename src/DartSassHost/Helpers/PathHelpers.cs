using System;
using System.IO;

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

#if NETSTANDARD2_1 || NETCOREAPP3_1_OR_GREATER
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
#if !NETSTANDARD2_1 && !NETCOREAPP3_1_OR_GREATER

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

			Uri baseUri = new Uri(processedRelativeTo);
			Uri uri = new Uri(processedPath);

			if (baseUri.Scheme != uri.Scheme)
			{
				return processedPath;
			}

			Uri relativeUri = baseUri.MakeRelativeUri(uri);
			string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

			return relativePath;
		}
#endif
	}
}