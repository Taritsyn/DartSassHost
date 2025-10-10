using System;
using System.Collections.Generic;
#if MODERN_JSON_CONVERTER
using System.Text.Json;
using System.Text.Json.Serialization;
#else

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

			writer.WriteBoolean("charset", value.Charset);
			WriteStringList(writer, "fatalDeprecations", value.FatalDeprecations);
			WriteStringList(writer, "futureDeprecations", value.FutureDeprecations);
			WriteStringList(writer, "includePaths", value.IncludePaths);
			writer.WriteBoolean("quietDeps", value.QuietDependencies);
			writer.WriteBoolean("omitSourceMapUrl", value.OmitSourceMapUrl);
			writer.WriteString("outputStyle", GetOutputStyleCode(value.OutputStyle));
			WriteStringList(writer, "silenceDeprecations", value.SilenceDeprecations);
			writer.WriteBoolean("sourceMapContents", value.SourceMapIncludeContents);
			writer.WriteBoolean("sourceMapEmbed", value.InlineSourceMap);
			writer.WriteString("sourceMapRoot", value.SourceMapRootPath);
			writer.WriteBoolean("sourceMap", value.SourceMap);

			switch (value.WarningLevel)
			{
				case WarningLevel.Quiet:
					writer.WriteBoolean("quiet", true);
					break;

				case WarningLevel.Verbose:
					writer.WriteBoolean("verbose", true);
					break;
			}

			writer.WriteEndObject();
		}

		private static string GetOutputStyleCode(OutputStyle style)
		{
			string styleCode = style == OutputStyle.Expanded ? "expanded" : "compressed";

			return styleCode;
		}

		private static void WriteStringList(
#if MODERN_JSON_CONVERTER
			Utf8JsonWriter writer,
#else
			JsonTextWriter writer,
#endif
			string propertyName,
			IList<string> values
		)
		{
			if (values == null)
			{
				return;
			}

			writer.WriteStartArray(propertyName);

			int valueCount = values.Count;

			for (int valueIndex = 0; valueIndex < valueCount; valueIndex++)
			{
				writer.WriteStringValue(values[valueIndex]);
			}

			writer.WriteEndArray();
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