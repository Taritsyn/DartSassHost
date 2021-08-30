using System;
using System.Collections.Generic;
#if !NET40
using System.Runtime.InteropServices;
#endif
using System.Text;
#if MODERN_JSON_CONVERTER
using System.Text.Json;
using System.Text.Json.Serialization;
#endif

using AdvancedStringBuilder;
#if !MODERN_JSON_CONVERTER
using Newtonsoft.Json;
#endif
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
	internal sealed class CompilationResultConverter : JsonConverter<CompilationResult>
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


		private CompilationResult ReadResult(
#if MODERN_JSON_CONVERTER
			ref Utf8JsonReader reader
#else
			JsonTextReader reader
#endif
		)
		{
			reader.CheckStartObject();

			string compiledContent = string.Empty;
			string sourceMap = string.Empty;
			List<string> includedFilePaths = null;
			Dictionary<string, string> warningSourcesCache = null;
			List<ProblemInfo> warnings = null;

			while (reader.Read() && reader.IsTokenTypeProperty())
			{
				string propertyName = reader.GetStringValue();

				switch (propertyName)
				{
					case "compiledContent":
						compiledContent = reader.ReadAsString();
						break;
					case "sourceMap":
						sourceMap = reader.ReadAsString();
						break;
					case "includedFilePaths":
						includedFilePaths = ReadIncludedFilePaths(
#if MODERN_JSON_CONVERTER
							ref reader
#else
							reader
#endif
						);
						break;
					case "errors":
						SassCompilationException compilationException = ReadFirstError(
#if MODERN_JSON_CONVERTER
							ref reader
#else
							reader
#endif
						);
						throw compilationException;
					case "warningSources":
						warningSourcesCache = ReadWarningSources(
#if MODERN_JSON_CONVERTER
							ref reader
#else
							reader
#endif
						);
						break;
					case "warnings":
						warnings = ReadWarnings(
#if MODERN_JSON_CONVERTER
							ref reader,
#else
							reader,
#endif
							warningSourcesCache
						);
						warningSourcesCache?.Clear();
						break;
					default:
						reader.Skip();
						break;
				}
			}

			reader.CheckEndObject();

			var result = new CompilationResult(compiledContent, sourceMap, includedFilePaths ?? new List<string>(),
				warnings ?? new List<ProblemInfo>());

			return result;
		}

		private List<string> ReadIncludedFilePaths(
#if MODERN_JSON_CONVERTER
			ref Utf8JsonReader reader
#else
			JsonTextReader reader
#endif
		)
		{
			reader.ReadStartArray();

			var includedFilePaths = new List<string>();

			while (reader.Read() && reader.IsTokenTypeString())
			{
				includedFilePaths.Add(reader.GetStringValue());
			}

			reader.CheckEndArray();

			return includedFilePaths;
		}

		private SassCompilationException ReadFirstError(
#if MODERN_JSON_CONVERTER
			ref Utf8JsonReader reader
#else
			JsonTextReader reader
#endif
		)
		{
			reader.ReadStartArray();

			SassCompilationException firstException = null;

			while (reader.Read() && reader.IsTokenTypeStartObject())
			{
				if (firstException != null)
				{
					continue;
				}

				firstException = ReadError(
#if MODERN_JSON_CONVERTER
					ref reader
#else
					reader
#endif
				);
			}

			reader.CheckEndArray();

			return firstException;
		}

		private SassCompilationException ReadError(
#if MODERN_JSON_CONVERTER
			ref Utf8JsonReader reader
#else
			JsonTextReader reader
#endif
		)
		{
			reader.CheckStartObject();

			string description = string.Empty;
			int status = 0;
			string type = string.Empty;
			string absoluteFilePath = string.Empty;
			int lineNumber = 0;
			int columnNumber = 0;
			string content = string.Empty;

			while (reader.Read() && reader.IsTokenTypeProperty())
			{
				string propertyName = reader.GetStringValue();

				switch (propertyName)
				{
					case "description":
						description = reader.ReadAsString();
						break;
					case "status":
						status = reader.ReadAsInt32(0);
						break;
					case "type":
						type = reader.ReadAsString();
						break;
					case "file":
						absoluteFilePath = reader.ReadAsString();
						break;
					case "lineNumber":
						lineNumber = reader.ReadAsInt32(0);
						break;
					case "columnNumber":
						columnNumber = reader.ReadAsInt32(0);
						break;
					case "source":
						content = reader.ReadAsString();
						break;
					default:
						reader.Skip();
						break;
				}
			}

			reader.CheckEndObject();

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

		private Dictionary<string, string> ReadWarningSources(
#if MODERN_JSON_CONVERTER
			ref Utf8JsonReader reader
#else
			JsonTextReader reader
#endif
		)
		{
			reader.ReadStartObject();

			IEqualityComparer<string> comparer = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
				StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal;
			var warningSources = new Dictionary<string, string>(comparer);

			while (reader.Read() && reader.IsTokenTypeProperty())
			{
				string path = reader.GetStringValue();
				string content = reader.ReadAsString();

				warningSources.Add(path, content);
			}

			reader.CheckEndObject();

			return warningSources;
		}

		private List<ProblemInfo> ReadWarnings(
#if MODERN_JSON_CONVERTER
			ref Utf8JsonReader reader,
#else
			JsonTextReader reader,
#endif
			Dictionary<string, string> sourcesCache
		)
		{
			if (sourcesCache == null)
			{
				throw new ArgumentNullException(nameof(sourcesCache));
			}

			reader.ReadStartArray();

			var warnings = new List<ProblemInfo>();
			string currentDirectory = _fileManager?.GetCurrentDirectory();

			while (reader.Read() && reader.IsTokenTypeStartObject())
			{
				ProblemInfo warning = ReadWarning(
#if MODERN_JSON_CONVERTER
					ref reader,
#else
					reader,
#endif
					currentDirectory,
					sourcesCache
				);
				warnings.Add(warning);
			}

			reader.CheckEndArray();

			return warnings;
		}

		private ProblemInfo ReadWarning(
#if MODERN_JSON_CONVERTER
			ref Utf8JsonReader reader,
#else
			JsonTextReader reader,
#endif
			string currentDirectory,
			Dictionary<string, string> sourcesCache
		)
		{
			reader.CheckStartObject();

			string description = string.Empty;
			bool isDeprecation = false;
			string absoluteFilePath = string.Empty;
			int lineNumber = 0;
			int columnNumber = 0;
			string content = string.Empty;
			string callStack = string.Empty;

			while (reader.Read() && reader.IsTokenTypeProperty())
			{
				string propertyName = reader.GetStringValue();

				switch (propertyName)
				{
					case "message":
						description = reader.ReadAsString();
						break;
					case "deprecation":
						isDeprecation = reader.ReadAsBoolean(false);
						break;
					case "file":
						absoluteFilePath = reader.ReadAsString();

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
						lineNumber = reader.ReadAsInt32(0);
						break;
					case "columnNumber":
						columnNumber = reader.ReadAsInt32(0);
						break;
					case "stackFrames":
						callStack = ReadStackFrames(
#if MODERN_JSON_CONVERTER
							ref reader,
#else
							reader,
#endif
							currentDirectory,
							sourcesCache
						);
						break;
					default:
						reader.Skip();
						break;
				}
			}

			reader.CheckEndObject();

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

		private string ReadStackFrames(
#if MODERN_JSON_CONVERTER
			ref Utf8JsonReader reader,
#else
			JsonTextReader reader,
#endif
			string currentDirectory,
			Dictionary<string, string> sourcesCache
		)
		{
			reader.ReadStartArray();

			string callStack = string.Empty;
			var stringBuilderPool = StringBuilderPool.Shared;
			StringBuilder callStackBuilder = stringBuilderPool.Rent();

			try
			{
				while (reader.Read() && reader.IsTokenTypeStartObject())
				{
					ReadStackFrame(
						callStackBuilder,
#if MODERN_JSON_CONVERTER
						ref reader,
#else
						reader,
#endif
						currentDirectory,
						sourcesCache
					);
				}

				reader.CheckEndArray();

				callStackBuilder.TrimEnd();
				callStack = callStackBuilder.ToString();
			}
			finally
			{
				stringBuilderPool.Return(callStackBuilder);
			}

			return callStack;
		}

		private void ReadStackFrame(
			StringBuilder callStackBuilder,
#if MODERN_JSON_CONVERTER
			ref Utf8JsonReader reader,
#else
			JsonTextReader reader,
#endif
			string currentDirectory,
			Dictionary<string, string> sourcesCache
		)
		{
			reader.CheckStartObject();

			string absoluteFilePath = string.Empty;
			int lineNumber = 0;
			int columnNumber = 0;
			string memberName = string.Empty;

			while (reader.Read() && reader.IsTokenTypeProperty())
			{
				string propertyName = reader.GetStringValue();

				switch (propertyName)
				{
					case "file":
						absoluteFilePath = reader.ReadAsString();
						break;
					case "lineNumber":
						lineNumber = reader.ReadAsInt32(0);
						break;
					case "columnNumber":
						columnNumber = reader.ReadAsInt32(0);
						break;
					case "memberName":
						memberName = reader.ReadAsString();
						break;
					default:
						reader.Skip();
						break;
				}
			}

			reader.CheckEndObject();

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

		#region JsonConverter<T> overrides

#if MODERN_JSON_CONVERTER
		public override CompilationResult Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return ReadResult(ref reader);
		}

		public override void Write(Utf8JsonWriter writer, CompilationResult value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}
#else
		public override bool CanRead => true;

		public override bool CanWrite => false;


		public override CompilationResult ReadJson(JsonReader reader, Type objectType, CompilationResult existingValue,
			bool hasExistingValue, JsonSerializer serializer)
		{
			var textReader = (JsonTextReader)reader;

			return ReadResult(textReader);
		}

		public override void WriteJson(JsonWriter writer, CompilationResult value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
#endif

		#endregion
	}
}