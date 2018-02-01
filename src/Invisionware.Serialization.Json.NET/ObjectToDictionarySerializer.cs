// ***********************************************************************
// Assembly         : Invisionware.Serialization.Json.NET
// Author           : Shawn Anderson
// Created          : 01-31-2018
//
// Last Modified By : Shawn Anderson
// Last Modified On : 01-31-2018
// ***********************************************************************
// <copyright file="ObjectToDictionarySerializer.cs" company="Invisionware.Serialization.Json.NET">
//     Copyright (c) Blockchain LLama. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Invisionware.Serialization.JsonNET
{
	public static class ObjectToDictionarySerializer
	{
		/// <summary>
		/// Creates a IDictionary&lt;string,string&gt; from the specified object
		/// Note: QueryStringParamAttribute, JsonPropertyAttribute, and XmlElementAttribute (in that order) are all supproted for configuing 
		/// the name of the parameter
		/// </summary>
		/// <typeparam name="T">The object type to serialize to a query string</typeparam>
		/// <param name="obj">Instance of the object to serialize.</param>
		/// <param name="options">The options that control how the query string is generate.</param>
		/// <returns>A valid formated query string.</returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static IDictionary<string, string> SerializeToDictionary<T>(this T obj, DictionarySerializeOptions options = null) 
		{
			if (obj == null)
				throw new ArgumentNullException(nameof(obj));

			if (options == null) options = new DictionarySerializeOptions();

			// Get all properties on the object
			var properties = obj.GetType().GetRuntimeProperties()
				.Where(x => !options.IgnorePropertiesWithoutAttribute || (options.IgnorePropertiesWithoutAttribute && x.GetCustomAttributeIncludingInterfaces<DictionaryElementAttribute>() != null))
				.Where(x => options.PropertyFilter(x))
				.Where(x => x.CanRead)
				.Where(x => x.GetValue(obj, null) != null)
				//.ToDictionary(x => x.Name, x => x.GetValue(obj, null));
				.ToDictionary(x => x.Name, x => new ObjectPropertySerializationInfo { Property = x, ParentObject = obj });

			// Get names for all IEnumerable properties (excl. string)
			var propertyNames = properties
				.Where(x => !(x.Value.Value is string) && x.Value.Value is IEnumerable)
				.Select(x => x.Key)
				.ToList();

			// Concat all IEnumerable properties into a comma separated string
			foreach (var key in propertyNames)
			{
				var valueType = properties[key].Value.GetType();
				var valueElemType = valueType.GetTypeInfo().IsGenericType
										? valueType.GenericTypeArguments[0]
										: valueType.GetElementType();

				if (valueElemType.GetTypeInfo().IsPrimitive || valueElemType == typeof(string))
				{
					var enumerable = properties[key].Value as IEnumerable;
					properties[key].FormattedValue = options.MultipleValueParamFunc(key, string.Join(options.MultipleValueSeperator, enumerable.Cast<object>()));
				}
			}

			// Concat all key/value pairs into a string separated by ampersand
			return
				properties.ToDictionary(
					x => options.UrlEncodeKeyName ? Uri.EscapeDataString(x.Value.FormattedName) : x.Value.FormattedName,
					x => options.UrlEncodeValue ? Uri.EscapeDataString(x.Value.FormattedValue) : x.Value.FormattedValue);
		}

		/// <summary>
		/// An internal class for holding information temporarly while building the 
		/// </summary>
		internal class ObjectPropertySerializationInfo
		{
			private object _value = null;
			private string _formattedValue;

			/// <summary>
			/// Gets the name that will be used for the "key".
			/// </summary>
			/// <value>The name.</value>
			public string Name { get { return Property.Name; } }
			/// <summary>
			/// Gets the value from the underlying object property/field.
			/// </summary>
			/// <value>The value.</value>
			public object Value { get { return _value ?? (_value = Property.GetValue(ParentObject, null)); } }
			/// <summary>
			/// Gets or sets the property that is going to be used to build this dictionary item.
			/// </summary>
			/// <value>The property.</value>
			public PropertyInfo Property { get; set; }
			/// <summary>
			/// Gets or sets the reference to parent object.
			/// </summary>
			/// <value>The parent object.</value>
			public object ParentObject { get; set; }
			/// <summary>
			/// Gets the type of the parent object.
			/// </summary>
			/// <value>The type of the parent.</value>
			public Type ParentType { get { return ParentObject?.GetType(); } }
			/// <summary>
			/// Gets the dictionary item property attribute.
			/// </summary>
			/// <value>The dictionary item property attribute.</value>
			public DictionaryElementAttribute DictionaryItemPropertyAttribute { get {  return Property.GetCustomAttributeIncludingInterfaces<DictionaryElementAttribute>(); } }
			/// <summary>
			/// Gets the json property attribute.
			/// </summary>
			/// <value>The json property attribute.</value>
			public JsonPropertyAttribute JsonPropertyAttribute { get {  return Property.GetCustomAttributeIncludingInterfaces<JsonPropertyAttribute>(); } }
			/// <summary>
			/// Gets the XML element attribute.
			/// </summary>
			/// <value>The XML element attribute.</value>
			//public XmlElementAttribute XmlElementAttribute { get { return Property.GetCustomAttributeIncludingInterfaces<XmlElementAttribute>(); } }
			/// <summary>
			/// Gets a formatted versio nof the name (looks at attributes first)
			/// Order: DictionaryElementAttribute, JsonPropertyAttribute, XmlElementAttribute, and then field name
			/// </summary>
			/// <value>The name of the formatted.</value>
			public string FormattedName { get { return DictionaryItemPropertyAttribute?.Name ?? JsonPropertyAttribute?.PropertyName ?? Name /*?? XmlElementAttribute?.ElementName */; } }
			/// <summary>
			/// Gets or sets the formatted value.
			/// </summary>
			/// <value>The formatted value.</value>
			public string FormattedValue { get { return _formattedValue ?? _value?.ToString(); } set { _formattedValue = value; } }
		}
	}

	/// <summary>
	/// Settings object for serializing the object to a Dictionary
	/// </summary>
	public class DictionarySerializeOptions
	{
		/// <summary>
		/// Gets or sets the multiple value seperator.
		/// </summary>
		/// <value>The multiple value seperator.</value>
		public string MultipleValueSeperator { get; set; } = ",";

		/// <summary>
		/// Gets or sets the multiple value parameter function.
		/// </summary>
		/// <value>The multiple value parameter function.</value>
		/// <example>
		/// MultipleValueParamFunc = (key, keyValue) { if (key == "someParamName") return "or=" + keyValue; else return keyValue;
		/// </example>
		public Func<string, string, string> MultipleValueParamFunc { get; set; } = (key, keyValue) => keyValue;

		/// <summary>
		/// Gets or sets the query string property filter.
		/// </summary>
		/// <value>The query string property filter.</value>
		/// <example>
		/// PropertyFilter = (PropertyInfo p) => x.Name != "ignoreThisParam";
		/// PropertyFilter = (PropertyInfo p) => p.GetCustomAttribute&lt;QueryStringParamAttribute&gt;() != null;
		/// </example>
		public Func<PropertyInfo, bool> PropertyFilter { get; set; } = (p) => true;

		/// <summary>
		/// Gets or sets a value indicating whether [URL encode key name].
		/// </summary>
		/// <value><c>true</c> if [URL encode key name]; otherwise, <c>false</c>.</value>
		public bool UrlEncodeKeyName { get; set; } = true;

		/// <summary>
		/// Gets or sets a value indicating whether [URL encode value].
		/// </summary>
		/// <value><c>true</c> if [URL encode value]; otherwise, <c>false</c>.</value>
		public bool UrlEncodeValue { get; set; } = true;

		/// <summary>
		/// Gets or sets a value indicating whether [ignore properties without attribute].
		/// </summary>
		/// <value><c>true</c> if [ignore properties without attribute]; otherwise, <c>false</c>.</value>
		public bool IgnorePropertiesWithoutAttribute { get; set; } = false;
	}
}
