using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Invisionware.Serialization.JsonNet.Converters;

namespace Invisionware.Serialization.UnitTests
{
	public class ObjectWithDictionaryAttributeTestClass1
	{
		[DictionaryElement()]
		public string Param1 { get; set; } = "Class1 Value";
		[DictionaryElement()]
		public int Param2 { get; set; } = 1;
		public List<string> ParamList1 { get; set; } = new List<string> { "listItem1", "listItem2", "listItem3" };
	}

	public class ObjectWithJsonDictionaryXmlAttributeTestClass1
	{
		[JsonProperty("someParam1Json")]
		public string Param1 { get; set; } = "Class2Value";

		[DictionaryElement("someParam2")]
		public int Param2 { get; set; } = 2;

		[XmlElement("someParamList1Xml")]
		public List<string> ParamList1 { get; set; } = new List<string> { "listItem1", "listItem2", "listItem3" };
	}

	public interface IObjectWithDictionaryAttributeTestInterface1
	{
		[DictionaryElement("someParam1")]
		string Param1 { get; set; }
		[DictionaryElement("someParam2")]
		int Param2 { get; set; }
	}

	public class ObjectWithDictionaryJsonAttributeTestClass1 : IObjectWithDictionaryAttributeTestInterface1
	{
		#region Implementation of IQueryStringInterface1

		public string Param1 { get; set; } = "ClassInterfaceValue1";
		public int Param2 { get; set; } = 3;

		[DictionaryElement("someParam3")]
		[JsonProperty("someParam3Json")]
		public string Param3 { get; set; } = "Class3Param3 Value";

		#endregion
	}

	public class ObjectWithStringArrayConverterTestClass1
	{
		[JsonConverter(typeof(NewtonSoftJsonArrayConverter), ",")]
		public string[] SomeArray { get; set; }
	}

	public class ObjectWithIntArrayConverterTestClass1
	{
		[JsonConverter(typeof(NewtonSoftJsonArrayConverter), ",")]
		public int[] SomeArray { get; set; }
	}

	public class ObjectWithStringListConverterTestClass1
	{
		[JsonConverter(typeof(NewtonSoftJsonArrayConverter), ",")]
		public List<string> SomeArray { get; set; } = new List<string>();
	}

	public class ObjectWithIntListConverterTestClass1
	{
		[JsonConverter(typeof(NewtonSoftJsonArrayConverter), ",")]
		public List<int> SomeArray { get; set; } = new List<int>();
	}

	public class ObjectWithDateListConverterTestClass1
	{
		[JsonConverter(typeof(NewtonSoftJsonArrayConverter), ",")]
		public List<DateTime> SomeArray { get; set; } = new List<DateTime>();
	}

	public class DateTimeFormatConverterTestClass1
	{
		[JsonConverter(typeof(NewtonSoftJsonDateTimeCustomFormatConverter), "dd-MM-yyyy")]
		public DateTime SomeDate { get; set; } = DateTime.Now;
	}

	public class EnumConverterTestClass1
	{
		[JsonConverter(typeof(NewtonSoftJsonEnumConverter))]
		public EnumConverterTestEnum1 SomeEnum { get; set; }
	}

	public enum EnumConverterTestEnum1
	{
		[JsonEnum("someEnumName1")]
		Enum1,
		[JsonEnum("someEnumName2")]
		Enum2,
		[JsonEnum("someEnumName3")]
		Enum3
	}
}
