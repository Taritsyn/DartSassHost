using System;
using System.Collections.Generic;
#if MODERN_JSON_CONVERTER
using System.Text.Json;
using System.Text.Json.Serialization;
#endif
#if !MODERN_JSON_CONVERTER

using Newtonsoft.Json;
#endif

using DartSassHost.Extensions;

namespace DartSassHost.JsonConverters
{
	/// <summary>
	/// Converts an compilation options to JSON
	/// </summary>
	internal sealed class CompilationOptionsConverter : JsonConverter<CompilationOptions>
	{
		private void WriteOptionsJson(
#if MODERN_JSON_CONVERTER
			Utf8JsonWriter writer,
#else
			JsonTextWriter writer,
#endif
			CompilationOptions value
		)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			writer.WriteStartObject();

			writer.WriteStartArray("includePaths");

			IList<string> paths = value.IncludePaths;
			int pathCount = paths.Count;

			for (int pathIndex = 0; pathIndex < pathCount; pathIndex++)
			{
				writer.WriteStringValue(paths[pathIndex]);
			}

			writer.WriteEndArray();

			writer.WriteString("indentType", GetIndentTypeCode(value.IndentType));
			writer.WriteNumber("indentWidth", value.IndentWidth);
			writer.WriteString("linefeed", GetLineFeedString(value.LineFeedType));
			writer.WriteBoolean("omitSourceMapUrl", value.OmitSourceMapUrl);
			writer.WriteString("outputStyle", GetOutputStyleCode(value.OutputStyle));
			writer.WriteBoolean("sourceMapContents", value.SourceMapIncludeContents);
			writer.WriteBoolean("sourceMapEmbed", value.InlineSourceMap);
			writer.WriteString("sourceMapRoot", value.SourceMapRootPath);
			writer.WriteBoolean("sourceMap", value.SourceMap);

			writer.WriteEndObject();
		}

		private static string GetIndentTypeCode(IndentType type)
		{
			string typeCode = type == IndentType.Tab ? "tab" : "space";

			return typeCode;
		}

		private static string GetLineFeedString(LineFeedType type)
		{
			string lineFeed;

			switch (type)
			{
				case LineFeedType.Cr:
					lineFeed = "cr";
					break;
				case LineFeedType.CrLf:
					lineFeed = "crlf";
					break;
				case LineFeedType.Lf:
					lineFeed = "lf";
					break;
				case LineFeedType.LfCr:
					lineFeed = "lfcr";
					break;
				default:
					throw new NotSupportedException();
			}

			return lineFeed;
		}

		private static string GetOutputStyleCode(OutputStyle style)
		{
			string styleCode = style == OutputStyle.Expanded ? "expanded" : "compressed";

			return styleCode;
		}

		#region JsonConverter<T> overrides

#if MODERN_JSON_CONVERTER
		public override CompilationOptions Read(ref Utf8JsonReader reader, Type typeToConvert,
			JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}

		public override void Write(Utf8JsonWriter writer, CompilationOptions value,
			JsonSerializerOptions options)
		{
			WriteOptionsJson(writer, value);
		}
#else
		public override bool CanRead => false;

		public override bool CanWrite => true;


		public override CompilationOptions ReadJson(JsonReader reader, Type objectType, CompilationOptions existingValue,
			bool hasExistingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override void WriteJson(JsonWriter writer, CompilationOptions value, JsonSerializer serializer)
		{
			var textWriter = (JsonTextWriter)writer;

			WriteOptionsJson(textWriter, value);
		}
#endif

		#endregion
	}
}