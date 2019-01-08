// ***********************************************************************
// Assembly         : Invisionware.Serialization.JsonNet
// Author           : Shawn Anderson
// Created          : 01-31-2018
//
// Last Modified By : Shawn Anderson
// Last Modified On : 01-31-2018
// ***********************************************************************
// <copyright file="NewtonSoftJsonArrayConverter.cs" company="Invisionware">
//     Copyright (c) 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Invisionware.Serialization.JsonNet.Converters
{
	public class NewtonSoftJsonArrayConverter : Newtonsoft.Json.JsonConverter
	{
		private readonly string _seperator = ",";
		//private readonly string _format;

		public NewtonSoftJsonArrayConverter(string seperator/*, string format*/)
		{
			_seperator = seperator;
			//_format = format;
		}

		/// <summary>
		/// Writes the JSON representation of the object.
		/// </summary>
		/// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
		/// <param name="value">The value.</param>
		/// <param name="serializer">The calling serializer.</param>
		/// <exception cref="System.NotSupportedException">Only array types are supported</exception>
		/// <exception cref="System.ArgumentException">Enum not found</exception>
		/// TODO Edit XML Comment Template for WriteJson
		public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
		{
			var type = value.GetType();

			if (!type.IsCollectionType()) throw new NotSupportedException("Only array types are supported");

			string str;
			if (!(value is object[] array))
			{
				array = ((IEnumerable)value).Cast<object>().ToArray();
			}

			//if (!string.IsNullOrEmpty(_format))
			//{
			//	for (int i=0; i<array.Length;i++)
			//	{
			//		array[i] = string.Format(_format, array[i]);
			//	}
			//}

			str = string.Join(_seperator, array);

			writer.WriteValue(str);
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
			if (!objectType.IsCollectionType()) throw new NotSupportedException("Only array types are supported");

			var arrayString = (string)reader.Value;
				
			var array = arrayString.Split(new[] { _seperator }, StringSplitOptions.None);

			var jArray = Newtonsoft.Json.Linq.JArray.FromObject(array, serializer);
			var result = jArray.ToObject(objectType);

			return result;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType.IsCollectionType();
		}

		#endregion
	}
}
