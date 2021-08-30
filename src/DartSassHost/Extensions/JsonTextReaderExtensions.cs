#if !MODERN_JSON_CONVERTER
/* This reader based on the code of the Microsoft.Extensions.DependencyModel library */

/* UnifiedJsonReader.JsonTextReader.cs
 * Mar 21, 2019
 *
 * Copyright (c) .NET Foundation and Contributors
 */

using System;

using Newtonsoft.Json;

using DartSassHost.Helpers;
using DartSassHost.Resources;

namespace DartSassHost.Extensions
{
	/// <summary>
	/// Extensions for the <see cref="JsonTextReader"/>
	/// </summary>
	internal static class JsonTextReaderExtensions
	{
		public static string GetStringValue(this JsonTextReader source) => (string)source.Value;

		public static bool IsTokenTypeProperty(this JsonTextReader source) =>
			source.TokenType == JsonToken.PropertyName;

		public static bool IsTokenTypeString(this JsonTextReader source) =>
			source.TokenType == JsonToken.String;

		public static bool IsTokenTypeStartObject(this JsonTextReader source) =>
			source.TokenType == JsonToken.StartObject;

		public static void ReadStartObject(this JsonTextReader source)
		{
			source.Read();
			source.CheckStartObject();
		}

		public static void CheckStartObject(this JsonTextReader source)
		{
			if (source.TokenType != JsonToken.StartObject)
			{
				throw CreateUnexpectedException(source, Strings.JsonTokenTypeName_StartObject);
			}
		}

		public static void CheckEndObject(this JsonTextReader source)
		{
			if (source.TokenType != JsonToken.EndObject)
			{
				throw CreateUnexpectedException(source, Strings.JsonTokenTypeName_EndObject);
			}
		}

		public static void ReadStartArray(this JsonTextReader source)
		{
			source.Read();
			source.CheckStartArray();
		}

		public static void CheckStartArray(this JsonTextReader source)
		{
			if (source.TokenType != JsonToken.StartArray)
			{
				throw CreateUnexpectedException(source, Strings.JsonTokenTypeName_StartArray);
			}
		}

		public static void CheckEndArray(this JsonTextReader source)
		{
			if (source.TokenType != JsonToken.EndArray)
			{
				throw CreateUnexpectedException(source, Strings.JsonTokenTypeName_EndArray);
			}
		}

		public static int ReadAsInt32(this JsonTextReader source, int defaultValue)
		{
			int? nullableBool = source.ReadAsInt32();

			return nullableBool ?? defaultValue;
		}

		public static bool ReadAsBoolean(this JsonTextReader source, bool defaultValue)
		{
			bool? nullableBool = source.ReadAsBoolean();

			return nullableBool ?? defaultValue;
		}

		private static Exception CreateUnexpectedException(JsonTextReader reader, string expected)
		{
			string description = string.Format(Strings.JsonReader_UnexpectedCharacterEncountered, expected);
			string message = SassErrorHelpers.GenerateCompilationErrorMessage("JsonReaderError", description,
				string.Empty, reader.LineNumber, reader.LinePosition, reader.Path);

			return new FormatException(message);
		}
	}
}
#endif