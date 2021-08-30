#if MODERN_JSON_CONVERTER
/* This reader based on the code of the Microsoft.Extensions.DependencyModel library */

/* UnifiedJsonReader.Utf8JsonReader.cs
 * Jul 31, 2019
 *
 * Copyright (c) .NET Foundation and Contributors
 */

using System;
using System.Collections;
using System.Reflection;
using System.Text.Json;

using DartSassHost.Helpers;
using DartSassHost.Resources;

namespace DartSassHost.Extensions
{
	/// <summary>
	/// Extensions for the <see cref="Utf8JsonReader"/>
	/// </summary>
	internal static class Utf8JsonReaderExtensions
	{
		public static string GetStringValue(this ref Utf8JsonReader source) => source.GetString();

		public static bool IsTokenTypeProperty(this ref Utf8JsonReader source) =>
			source.TokenType == JsonTokenType.PropertyName;

		public static bool IsTokenTypeString(this ref Utf8JsonReader source) =>
			source.TokenType == JsonTokenType.String;

		public static bool IsTokenTypeStartObject(this ref Utf8JsonReader source) =>
			source.TokenType == JsonTokenType.StartObject;

		public static void ReadStartObject(this ref Utf8JsonReader source)
		{
			source.Read();
			source.CheckStartObject();
		}

		public static void CheckStartObject(this ref Utf8JsonReader source)
		{
			if (source.TokenType != JsonTokenType.StartObject)
			{
				throw CreateUnexpectedException(ref source, Strings.JsonTokenTypeName_StartObject);
			}
		}

		public static void CheckEndObject(this ref Utf8JsonReader source)
		{
			if (source.TokenType != JsonTokenType.EndObject)
			{
				throw CreateUnexpectedException(ref source, Strings.JsonTokenTypeName_EndObject);
			}
		}

		public static void ReadStartArray(this ref Utf8JsonReader source)
		{
			source.Read();
			source.CheckStartArray();
		}

		public static void CheckStartArray(this ref Utf8JsonReader source)
		{
			if (source.TokenType != JsonTokenType.StartArray)
			{
				throw CreateUnexpectedException(ref source, Strings.JsonTokenTypeName_StartArray);
			}
		}

		public static void CheckEndArray(this ref Utf8JsonReader source)
		{
			if (source.TokenType != JsonTokenType.EndArray)
			{
				throw CreateUnexpectedException(ref source, Strings.JsonTokenTypeName_EndArray);
			}
		}

		public static string ReadAsString(this ref Utf8JsonReader source)
		{
			source.Read();

			if (source.TokenType == JsonTokenType.Null)
			{
				return null;
			}

			if (source.TokenType != JsonTokenType.String)
			{
				throw CreateUnexpectedException(ref source, Strings.JsonTokenTypeName_String);
			}

			return source.GetStringValue();
		}

		public static int ReadAsInt32(this ref Utf8JsonReader source, int defaultValue)
		{
			source.Read();

			if (source.TokenType == JsonTokenType.Null)
			{
				return defaultValue;
			}

			if (source.TokenType != JsonTokenType.Number)
			{
				throw CreateUnexpectedException(ref source, Strings.JsonTokenTypeName_Number);
			}

			return source.GetInt32();
		}

		public static bool ReadAsBoolean(this ref Utf8JsonReader source, bool defaultValue)
		{
			source.Read();

			if (source.TokenType == JsonTokenType.Null)
			{
				return defaultValue;
			}

			if (source.TokenType != JsonTokenType.True && source.TokenType != JsonTokenType.False)
			{
				throw CreateUnexpectedException(ref source, Strings.JsonTokenTypeName_Boolean);
			}

			return source.GetBoolean();
		}

		private static Exception CreateUnexpectedException(ref Utf8JsonReader reader, string expected)
		{
			object boxedState = reader.CurrentState;
			long lineNumber = (long)(typeof(JsonReaderState)
				.GetField("_lineNumber", BindingFlags.Instance | BindingFlags.NonPublic)
				?.GetValue(boxedState) ?? -1)
				;
			long columnNumber = (long)(typeof(JsonReaderState)
				.GetField("_bytePositionInLine", BindingFlags.Instance | BindingFlags.NonPublic)
				?.GetValue(boxedState) ?? -1)
				;
			string description = string.Format(Strings.JsonReader_UnexpectedCharacterEncountered, expected);
			string message = SassErrorHelpers.GenerateCompilationErrorMessage("JsonReaderError", description,
				string.Empty, (int)lineNumber, (int)columnNumber, string.Empty);

			return new FormatException(message);
		}
	}
}
#endif