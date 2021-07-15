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
		public static string PrettifyPath(string currentDirectory, string path)
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

#if NETSTANDARD2_1
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
#if !NETSTANDARD2_1

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

			if (!relativeTo.EndsWith(Path.DirectorySeparatorChar.ToString())
				&& !relativeTo.EndsWith(Path.AltDirectorySeparatorChar.ToString()))
			{
				relativeTo += Path.DirectorySeparatorChar;
			}

			Uri baseUri = new Uri(relativeTo);
			Uri uri = new Uri(path);

			if (baseUri.Scheme != uri.Scheme)
			{
				return path;
			}

			Uri relativeUri = baseUri.MakeRelativeUri(uri);
			string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

			if (uri.Scheme.Equals("file", StringComparison.OrdinalIgnoreCase))
			{
				relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
			}

			return relativePath;
		}
#endif
	}
}