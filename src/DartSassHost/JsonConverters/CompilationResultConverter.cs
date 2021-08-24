using System;
using System.Collections.Generic;
#if NET45 || NET471 || NETSTANDARD
using System.Runtime.InteropServices;
#endif
using System.Text;

using AdvancedStringBuilder;
using Newtonsoft.Json;
#if NET40
using PolyfillsForOldDotNet.System.Runtime.InteropServices;
#endif

using DartSassHost.Extensions;
using DartSassHost.Helpers;
using DartSassHost.Utilities;

namespace DartSassHost.JsonConverters
{
	/// <summary>
	/// Converts an compilation result from JSON
	/// </summary>
	internal sealed class CompilationResultConverter : JsonConverter, IDisposable
	{
		/// <summary>
		/// File manager
		/// </summary>
		private IFileManager _fileManager;


		/// <summary>
		/// Constructs an instance of the compilation result JSON converter
		/// </summary>
		/// <param name="fileManager">File manager</param>
		public CompilationResultConverter(IFileManager fileManager)
		{
			_fileManager = fileManager;
		}


		private CompilationResult ReadResultJson(JsonReader reader)
		{
			string compiledContent = string.Empty;
			string sourceMap = string.Empty;
			List<string> includedFilePaths = null;
			Dictionary<string, string> warningSourcesCache = null;
			List<ProblemInfo> warnings = null;

			while (reader.Read())
			{
				if (reader.TokenType == JsonToken.EndObject)
				{
					return new CompilationResult(compiledContent, sourceMap, includedFilePaths ?? new List<string>(),
						warnings ?? new List<ProblemInfo>());
				}

				if (reader.TokenType == JsonToken.PropertyName)
				{
					string propertyName = reader.Value.ToString();
					reader.Read();

					switch (propertyName)
					{
						case "compiledContent":
							compiledContent = reader.Value.ToString();
							break;
						case "sourceMap":
							sourceMap = reader.Value.ToString();
							break;
						case "includedFilePaths":
							includedFilePaths = ReadIncludedFilePathsJson(reader);
							break;
						case "errors":
							SassCompilationException compilationException = ReadErrorsJson(reader);
							if (compilationException != null)
							{
								throw compilationException;
							}
							else
							{
								throw new JsonException();
							}
						case "warningSources":
							warningSourcesCache = ReadWarningSourcesJson(reader);
							break;
						case "warnings":
							warnings = ReadWarningsJson(reader, warningSourcesCache);
							warningSourcesCache?.Clear();
							break;
					}
				}
			}

			throw new JsonException();
		}

		private List<string> ReadIncludedFilePathsJson(JsonReader reader)
		{
			var includedFilePaths = new List<string>();

			while (reader.Read())
			{
				if (reader.TokenType == JsonToken.EndArray)
				{
					return includedFilePaths;
				}
				else if (reader.TokenType == JsonToken.String)
				{
					includedFilePaths.Add(reader.Value.ToString());
				}
			}

			throw new JsonException();
		}

		private SassCompilationException ReadErrorsJson(JsonReader reader)
		{
			SassCompilationException firstException = null;

			while (reader.Read())
			{
				if (reader.TokenType == JsonToken.EndArray)
				{
					return firstException;
				}
				else
				{
					if (firstException != null)
					{
						continue;
					}

					firstException = ReadErrorJson(reader);
				}
			}

			throw new JsonException();
		}

		private SassCompilationException ReadErrorJson(JsonReader reader)
		{
			string description = string.Empty;
			int status = 0;
			string type = string.Empty;
			string absoluteFilePath = string.Empty;
			int lineNumber = 0;
			int columnNumber = 0;
			string content = string.Empty;

			while (reader.Read())
			{
				if (reader.TokenType == JsonToken.EndObject)
				{
					string relativeFilePath = absoluteFilePath;
					string sourceFragment = string.Empty;
					string sourceLineFragment = string.Empty;

					if (!string.IsNullOrWhiteSpace(absoluteFilePath) && _fileManager != null)
					{
						string currentDirectory = _fileManager.GetCurrentDirectory();
						relativeFilePath = PathHelpers.PrettifyPath(currentDirectory, absoluteFilePath);
					}

					if (string.IsNullOrWhiteSpace(content))
					{
						if (!_fileManager.TryReadFile(absoluteFilePath, out content))
						{
							content = string.Empty;
						}
					}

					if (!string.IsNullOrWhiteSpace(content))
					{
						sourceFragment = SourceCodeNavigator.GetSourceFragment(content,
							new SourceCodeNodeCoordinates(lineNumber, columnNumber));
						sourceLineFragment = TextHelpers.GetTextFragment(content, lineNumber, columnNumber);
					}

					string message = SassErrorHelpers.GenerateCompilationErrorMessage(type, description, relativeFilePath,
						lineNumber, columnNumber, sourceLineFragment);

					var compilationException = new SassCompilationException(message)
					{
						Description = description,
						Status = status,
						File = absoluteFilePath,
						LineNumber = lineNumber,
						ColumnNumber = columnNumber,
						SourceFragment = sourceFragment
					};

					return compilationException;
				}

				if (reader.TokenType == JsonToken.PropertyName)
				{
					string propertyName = reader.Value.ToString();
					reader.Read();

					switch (propertyName)
					{
						case "description":
							description = reader.Value.ToString();
							break;
						case "status":
							status = Convert.ToInt32(reader.Value);
							break;
						case "type":
							type = reader.Value.ToString();
							break;
						case "file":
							absoluteFilePath = reader.Value.ToString();
							break;
						case "lineNumber":
							lineNumber = Convert.ToInt32(reader.Value);
							break;
						case "columnNumber":
							columnNumber = Convert.ToInt32(reader.Value);
							break;
						case "source":
							content = reader.Value.ToString();
							break;
					}
				}
			}

			throw new JsonException();
		}

		private Dictionary<string, string> ReadWarningSourcesJson(JsonReader reader)
		{
			IEqualityComparer<string> comparer = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
				StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal;
			var warningSources = new Dictionary<string, string>(comparer);

			while (reader.Read())
			{
				if (reader.TokenType == JsonToken.EndObject)
				{
					return warningSources;
				}

				if (reader.TokenType == JsonToken.PropertyName)
				{
					string path = reader.Value.ToString();
					reader.Read();
					string content = reader.Value.ToString();

					warningSources.Add(path, content);
				}
			}

			throw new JsonException();
		}

		private List<ProblemInfo> ReadWarningsJson(JsonReader reader, Dictionary<string, string> sourcesCache)
		{
			if (sourcesCache == null)
			{
				throw new ArgumentNullException(nameof(sourcesCache));
			}

			var warnings = new List<ProblemInfo>();
			string currentDirectory = _fileManager?.GetCurrentDirectory();

			while (reader.Read())
			{
				if (reader.TokenType == JsonToken.EndArray)
				{
					return warnings;
				}
				else
				{
					ProblemInfo warning = ReadWarningJson(reader, currentDirectory, sourcesCache);
					warnings.Add(warning);
				}
			}

			throw new JsonException();
		}

		private ProblemInfo ReadWarningJson(JsonReader reader, string currentDirectory, Dictionary<string, string> sourcesCache)
		{
			string description = string.Empty;
			bool isDeprecation = false;
			string absoluteFilePath = string.Empty;
			int lineNumber = 0;
			int columnNumber = 0;
			string content = string.Empty;
			string callStack = string.Empty;

			while (reader.Read())
			{
				if (reader.TokenType == JsonToken.EndObject)
				{
					string message;
					string sourceFragment = string.Empty;

					if (!string.IsNullOrWhiteSpace(content))
					{
						sourceFragment = SourceCodeNavigator.GetSourceFragment(content,
							new SourceCodeNodeCoordinates(lineNumber, columnNumber));
					}

					if (callStack.Length > 0)
					{
						message = SassErrorHelpers.GenerateCompilationWarningMessage(description, isDeprecation, callStack);
					}
					else
					{
						string relativeFilePath = absoluteFilePath;
						if (!string.IsNullOrWhiteSpace(absoluteFilePath) && !string.IsNullOrWhiteSpace(currentDirectory))
						{
							relativeFilePath = PathHelpers.PrettifyPath(currentDirectory, absoluteFilePath);
						}
						string sourceLineFragment = TextHelpers.GetTextFragment(content, lineNumber, columnNumber);

						message = SassErrorHelpers.GenerateCompilationWarningMessage(description, isDeprecation,
							relativeFilePath, lineNumber, columnNumber, sourceLineFragment);
					}

					var warning = new ProblemInfo()
					{
						Message = message,
						Description = description,
						IsDeprecation = isDeprecation,
						File = absoluteFilePath,
						LineNumber = lineNumber,
						ColumnNumber = columnNumber,
						SourceFragment = sourceFragment,
						CallStack = callStack
					};

					return warning;
				}

				if (reader.TokenType == JsonToken.PropertyName)
				{
					string propertyName = reader.Value.ToString();
					reader.Read();

					switch (propertyName)
					{
						case "message":
							description = reader.Value.ToString();
							break;
						case "deprecation":
							isDeprecation = Convert.ToBoolean(reader.Value);
							break;
						case "file":
							absoluteFilePath = reader.Value.ToString();

							if (!sourcesCache.TryGetValue(absoluteFilePath, out content))
							{
								if (_fileManager.TryReadFile(absoluteFilePath, out content))
								{
									sourcesCache.Add(absoluteFilePath, content);
								}
								else
								{
									content = string.Empty;
								}
							}
							break;
						case "lineNumber":
							lineNumber = Convert.ToInt32(reader.Value);
							break;
						case "columnNumber":
							columnNumber = Convert.ToInt32(reader.Value);
							break;
						case "stackFrames":
							callStack = ReadStackFramesJson(reader, currentDirectory, sourcesCache);
							break;
					}
				}
			}

			throw new JsonException();
		}

		private string ReadStackFramesJson(JsonReader reader, string currentDirectory,
			Dictionary<string, string> sourcesCache)
		{
			var stringBuilderPool = StringBuilderPool.Shared;
			StringBuilder callStackBuilder = stringBuilderPool.Rent();

			while (reader.Read())
			{
				if (reader.TokenType == JsonToken.EndArray)
				{
					callStackBuilder.TrimEnd();

					string callStack = callStackBuilder.ToString();
					stringBuilderPool.Return(callStackBuilder);
					callStackBuilder = null;

					return callStack;
				}
				else
				{
					ReadStackFrameJson(callStackBuilder, reader, currentDirectory, sourcesCache);
				}
			}

			if (callStackBuilder != null)
			{
				stringBuilderPool.Return(callStackBuilder);
			}

			throw new JsonException();
		}

		private void ReadStackFrameJson(StringBuilder callStackBuilder, JsonReader reader, string currentDirectory,
			Dictionary<string, string> sourcesCache)
		{
			string absoluteFilePath = string.Empty;
			int lineNumber = 0;
			int columnNumber = 0;
			string memberName = string.Empty;

			while (reader.Read())
			{
				if (reader.TokenType == JsonToken.EndObject)
				{
					string relativeFilePath = absoluteFilePath;
					if (!string.IsNullOrWhiteSpace(absoluteFilePath) && !string.IsNullOrWhiteSpace(currentDirectory))
					{
						relativeFilePath = PathHelpers.PrettifyPath(currentDirectory, absoluteFilePath);
					}

					SassErrorHelpers.WriteErrorLocationLine(callStackBuilder, memberName, relativeFilePath,
						lineNumber, columnNumber);
					callStackBuilder.AppendLine();

					return;
				}

				if (reader.TokenType == JsonToken.PropertyName)
				{
					string propertyName = reader.Value.ToString();
					reader.Read();

					switch (propertyName)
					{
						case "file":
							absoluteFilePath = reader.Value.ToString();
							break;
						case "lineNumber":
							lineNumber = Convert.ToInt32(reader.Value);
							break;
						case "columnNumber":
							columnNumber = Convert.ToInt32(reader.Value);
							break;
						case "memberName":
							memberName = reader.Value.ToString();
							break;
					}
				}
			}

			throw new JsonException();
		}

		#region JsonConverter overrides

		public override bool CanRead => true;
		public override bool CanWrite => false;


		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return ReadResultJson(reader);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(CompilationResult);
		}

		#endregion

		#region IDisposable implementation

		public void Dispose()
		{
			_fileManager = null;
		}

		#endregion
	}
}