// ***********************************************************************
// Assembly         : Invisionware.Serialization.JsonNet
// Author           : Shawn Anderson
// Created          : 01-032019
//
// Last Modified By : Shawn Anderson
// Last Modified On : 01-03-2019
// ***********************************************************************
// <copyright file="NewtonSoftJsonDateConverter.cs" company="Invisionware">
//     Copyright (c) 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;

namespace Invisionware.Serialization.JsonNet.Converters
{
	public class NewtonSoftJsonDateTimeCustomFormatConverter : IsoDateTimeConverter
	{
		private readonly IFormatProvider _formatProvider;

		public NewtonSoftJsonDateTimeCustomFormatConverter(string format) : this(format, DateTimeStyles.None, null)
		{
		}

		public NewtonSoftJsonDateTimeCustomFormatConverter(string format, System.Globalization.DateTimeStyles dateStyles = System.Globalization.DateTimeStyles.None, IFormatProvider formatProvider = null)
		{
			DateTimeFormat = format;
			DateTimeStyles = dateStyles;
			_formatProvider = formatProvider ?? CultureInfo.CurrentCulture;
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
		{
			try
			{
				if (DateTime.TryParseExact((string)reader.Value, DateTimeFormat, _formatProvider, DateTimeStyles, out DateTime dt))
				{
					return base.ReadJson(reader, objectType, dt, serializer);
				}

				return base.ReadJson(reader, objectType, existingValue, serializer);
			}
			catch { return null; }
		}


		public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
		{
			if (value is DateTime dt)
			{
				var str = dt.ToString(DateTimeFormat, _formatProvider);
				
				writer.WriteValue(str);
			}
			else
			{
				base.WriteJson(writer, value, serializer);
			}
		}
	}
}
