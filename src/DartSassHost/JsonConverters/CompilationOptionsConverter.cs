using System;

using Newtonsoft.Json;

namespace DartSassHost.JsonConverters
{
	/// <summary>
	/// Converts an compilation options to JSON
	/// </summary>
	internal sealed class CompilationOptionsConverter : JsonConverter
	{
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

		#region JsonConverter overrides

		public override bool CanRead => false;
		public override bool CanWrite => true;


		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			var options = (CompilationOptions)value;

			writer.WriteStartObject();

			writer.WritePropertyName("includePaths");
			writer.WriteStartArray();
			foreach (string path in options.IncludePaths)
			{
				writer.WriteValue(path);
			}
			writer.WriteEndArray();

			writer.WritePropertyName("indentType");
			writer.WriteValue(GetIndentTypeCode(options.IndentType));

			writer.WritePropertyName("indentWidth");
			writer.WriteValue(options.IndentWidth);

			writer.WritePropertyName("linefeed");
			writer.WriteValue(GetLineFeedString(options.LineFeedType));

			writer.WritePropertyName("omitSourceMapUrl");
			writer.WriteValue(options.OmitSourceMapUrl);

			writer.WritePropertyName("outputStyle");
			writer.WriteValue(GetOutputStyleCode(options.OutputStyle));

			writer.WritePropertyName("sourceMapContents");
			writer.WriteValue(options.SourceMapIncludeContents);

			writer.WritePropertyName("sourceMapEmbed");
			writer.WriteValue(options.InlineSourceMap);

			writer.WritePropertyName("sourceMapRoot");
			writer.WriteValue(options.SourceMapRootPath);

			writer.WritePropertyName("sourceMap");
			writer.WriteValue(options.SourceMap);

			writer.WriteEndObject();
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(CompilationOptions);
		}

		#endregion
	}
}