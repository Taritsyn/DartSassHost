using System;
using System.Linq;
#if MODERN_JSON_CONVERTER
using System.Text.Json;
#else

using Newtonsoft.Json;
#endif

using DartSassHost.Extensions;
using DartSassHost.Resources;

namespace DartSassHost
{
	/// <summary>
	/// Unified JSON serializer
	/// </summary>
	internal sealed class UnifiedJsonSerializer
	{
		/// <summary>
		/// List of primitive type codes
		/// </summary>
		private static readonly TypeCode[] _primitiveTypeCodes =
		{
			TypeCode.Boolean,
			TypeCode.SByte, TypeCode.Byte,
			TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64,
			TypeCode.Single, TypeCode.Double, TypeCode.Decimal,
			TypeCode.Char, TypeCode.String
		};

		/// <summary>
		/// JSON serializer options
		/// </summary>
#if MODERN_JSON_CONVERTER
		private JsonSerializerOptions _options;
#else
		private JsonSerializerSettings _options;
#endif


		/// <summary>
		/// Constructs an instance of the Unified JSON serializer
		/// </summary>
		/// <param name="options">JSON serializer options</param>
#if MODERN_JSON_CONVERTER
		public UnifiedJsonSerializer(JsonSerializerOptions options)
#else
		public UnifiedJsonSerializer(JsonSerializerSettings options)
#endif
		{
			if (options == null)
			{
				throw new ArgumentNullException(nameof(options));
			}

			_options = options;
		}


		/// <summary>
		/// Checks whether .NET type is primitive
		/// </summary>
		/// <param name="type">.NET type</param>
		/// <returns>Result of check (true - is primitive; false - is not primitive)</returns>
		private static bool IsPrimitiveType(Type type)
		{
			TypeCode typeCode = type.GetTypeCode();
			bool result = _primitiveTypeCodes.Contains(typeCode);

			return result;
		}

		/// <summary>
		/// Serializes a value of primitive type
		/// </summary>
		/// <typeparam name="T">Type of value</typeparam>
		/// <param name="value">Value of primitive type</param>
		/// <returns>Serialized value</returns>
		public string SerializePrimitiveType<T>(T value)
		{
			var type = typeof(T);
			if (!IsPrimitiveType(type))
			{
				throw new NotSupportedException(string.Format(Strings.JsonSerializer_TypeIsNotPrimitive, type.FullName));
			}

#if MODERN_JSON_CONVERTER
			return JsonSerializer.Serialize(value);
#else
			return JsonConvert.SerializeObject(value);
#endif
		}

		/// <summary>
		/// Serializes a specified object to JSON format
		/// </summary>
		/// <param name="value">The object to serialize</param>
		/// <returns>Serialized object in JSON format</returns>
		public string SerializeObject<T>(T value)
		{
#if MODERN_JSON_CONVERTER
			return JsonSerializer.Serialize(value, _options);
#else
			return JsonConvert.SerializeObject(value, _options);
#endif
		}

		/// <summary>
		/// Deserializes a JSON string to the specified .NET type
		/// </summary>
		/// <typeparam name="T">The type of the object to deserialize to</typeparam>
		/// <param name="json">The JSON string to deserialize</param>
		/// <returns>The deserialized object from the JSON string</returns>
		public T DeserializeObject<T>(string json)
		{
#if MODERN_JSON_CONVERTER
			return JsonSerializer.Deserialize<T>(json, _options);
#else
			return JsonConvert.DeserializeObject<T>(json, _options);
#endif
		}
	}
}