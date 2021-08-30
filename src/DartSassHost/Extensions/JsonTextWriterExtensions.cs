#if !MODERN_JSON_CONVERTER
/* This writer based on the code of the Microsoft.Extensions.DependencyModel library */

/* UnifiedJsonWriter.JsonTextWriter.cs
 * Mar 21, 2019
 *
 * Copyright (c) .NET Foundation and Contributors
 */

using Newtonsoft.Json;

namespace DartSassHost.Extensions
{
	/// <summary>
	/// Extensions for the <see cref="JsonTextWriter"/>
	/// </summary>
	internal static class JsonTextWriterExtensions
	{
		public static void WriteStartArray(this JsonTextWriter source, string propertyName)
		{
			source.WritePropertyName(propertyName);
			source.WriteStartArray();
		}

		public static void WriteString(this JsonTextWriter source, string propertyName, string value)
		{
			source.WritePropertyName(propertyName);
			source.WriteValue(value);
		}

		public static void WriteNumber(this JsonTextWriter source, string propertyName, int value)
		{
			source.WritePropertyName(propertyName);
			source.WriteValue(value);
		}

		public static void WriteBoolean(this JsonTextWriter source, string propertyName, bool value)
		{
			source.WritePropertyName(propertyName);
			source.WriteValue(value);
		}

		public static void WriteStringValue(this JsonTextWriter source, string value) => source.WriteValue(value);
	}
}
#endif