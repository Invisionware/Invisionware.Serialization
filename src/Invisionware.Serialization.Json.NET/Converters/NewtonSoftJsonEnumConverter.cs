using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Invisionware.Serialization.JsonNET.Converters
{
	class NewtonSoftJsonEnumConverter : Newtonsoft.Json.Converters.StringEnumConverter
	{
		/// <summary>
		/// Writes the JSON representation of the object.
		/// </summary>
		/// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
		/// <param name="value">The value.</param>
		/// <param name="serializer">The calling serializer.</param>
		/// <exception cref="System.InvalidOperationException">Only type Enum is supported</exception>
		/// <exception cref="System.ArgumentException">Enum not found</exception>
		/// TODO Edit XML Comment Template for WriteJson
		public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
		{
			var type = value.GetType();

			if (!type.GetTypeInfo().IsEnum) throw new InvalidOperationException("Only type Enum is supported");

			var field = type.GetRuntimeFields().FirstOrDefault(f => f.Name == value.ToString());
			if (field != null)
			{
                writer.WriteValue(field.GetCustomAttribute(typeof(JsonEnumAttribute)) is JsonEnumAttribute attribute ? attribute.Name : field.Name);

                return;
			}

			throw new ArgumentException("Enum not found");
		}

		#region Overrides of StringEnumConverter

		/// <summary>
		/// Reads the JSON representation of the object.
		/// </summary>
		/// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
		/// <param name="objectType">Type of the object.</param>
		/// <param name="existingValue">The existing value of object being read.</param>
		/// <param name="serializer">The calling serializer.</param>
		/// <returns>The object value.</returns>
		/// <exception cref="System.InvalidOperationException">
		/// Only type Enum is supported
		/// or
		/// Only type Enum is supported
		/// </exception>
		/// TODO Edit XML Comment Template for ReadJson
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
		{
			var enumString = (string)reader.Value;

			try
			{
				return Enum.Parse(objectType, enumString, true);
			}
			catch
			{
				var objectTypeInfo = objectType.GetTypeInfo();

				if (objectTypeInfo.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Nullable<>))
				{
					if (!Nullable.GetUnderlyingType(objectType).GetTypeInfo().IsEnum)
						throw new InvalidOperationException("Only type Enum is supported");
				}
				else if (!objectTypeInfo.IsEnum) throw new InvalidOperationException("Only type Enum is supported");

				foreach (var field in objectType.GetRuntimeFields())
				{
                    if (field.GetCustomAttribute(typeof(JsonEnumAttribute)) is JsonEnumAttribute attribute)
                    {
                        if (attribute.Name == enumString)
                        {
                            //return field;
                            return Enum.Parse(objectType, field.Name, true);
                        }
                    }
                }
			}

			return null;
		}

		#endregion
	}
}
