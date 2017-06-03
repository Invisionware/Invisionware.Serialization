using FluentAssertions;
using Invisionware.Net.WebUtils;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;

namespace Invisionware.Serialization.Tests
{
	[TestFixture]
	public class DictionarySerializationTests
	{
		private DictionarySerializeOptions _serializationOptionsAttributeFilter1 =
			new DictionarySerializeOptions
			{
					IgnorePropertiesWithoutAttribute = true
				};

		private DictionarySerializeOptions _serializationOptionsAttributeFilter2 =
			new DictionarySerializeOptions
			{
				PropertyFilter = (p) => p.Name != "ParamList1" && p.GetCustomAttribute<DictionaryElementAttribute>()?.Name != "ParamList1"
			};

		private DictionarySerializeOptions _serializationOptionsUrlDisableEncode1 =
			new DictionarySerializeOptions
			{
				UrlEncodeKeyName = false,
				UrlEncodeValue = false
			};


		[SetUp]
		public void Initialize()
		{

		}

		[Test]
		public void TestSimpleClassDefaultOptions()
		{
			var obj = new DictionaryObjectTestClass1();
			var result = obj.SerializeToDictionary();

			result.Should().NotBeNull("Dictionary creation failed");
			result.Should().Contain("Param1", "Class1%20Value");
			result.Should().Contain("Param2", "1");
			result.Should().Contain("ParamList1", "listItem1%2ClistItem2%2ClistItem3");
			result.Should().NotContain("someParam1", "Class1Value");
		}

		[Test]
		public void TestSimpleClassOnlyMarkedAttributes()
		{
			var obj = new DictionaryObjectTestClass1();
			var result = obj.SerializeToDictionary(_serializationOptionsAttributeFilter1);

			result.Should().NotBeNull("Dictionary creation failed");
			result.Should().Contain("Param1", "Class1%20Value");
			result.Should().Contain("Param2", "1");
			result.Should().NotContain("ParamList1", "listItem1%2ClistItem2%2ClistItem3");

		}

		[Test]
		public void TestSimpleClassIgnoreBasedOnAttributes()
		{
			var obj = new DictionaryObjectTestClass1();
			var result = obj.SerializeToDictionary(_serializationOptionsAttributeFilter2);

			result.Should().NotBeNull("Dictionary creation failed");
			result.Should().Contain("Param1", "Class1%20Value");
			result.Should().Contain("Param2", "1");
			result.Should().NotContain("ParamList1", "listItem1%2ClistItem2%2ClistItem3");
		}

		[Test]
		public void TestClassDoNotEncodListValues()
		{
			var obj = new DictionaryObjectTestClass1();
			var result = obj.SerializeToDictionary(_serializationOptionsUrlDisableEncode1);

			result.Should().NotBeNull("Dictionary creation failed");
			result.Should().Contain("Param1", "Class1 Value");
			result.Should().Contain("Param2", "1");
			result.Should().Contain("ParamList1", "listItem1,listItem2,listItem3");            
		}

		[Test]
		public void TestJsonPropertyName()
		{
			var obj = new DictionaryObjectTestClass2();
			var result = obj.SerializeToDictionary();

			result.Should().NotBeNull("Dictionary creation failed");
			result.Should().Contain("someParam1Json", "Class2Value");
			result.Should().Contain("someParam2", "2");
			result.Should().Contain("someParamList1Xml", "listItem1%2ClistItem2%2ClistItem3");
		}

		[Test]
		public void TestXmlElementPropertyName()
		{
			var obj = new DictionaryObjectTestClass2();
			var result = obj.SerializeToDictionary();

			result.Should().NotBeNull("Dictionary creation failed");
			result.Should().Contain("someParam1Json", "Class2Value");
			result.Should().NotContain("someParam1", "Class2Value");
			result.Should().Contain("someParam2", "2");
			result.Should().Contain("someParamList1Xml", "listItem1%2ClistItem2%2ClistItem3");
		}

		[Test]
		public void TestMultipleAttributePropertyName()
		{
			var obj = new DictionaryObjectTestClass3();
			var result = obj.SerializeToDictionary();

			result.Should().NotBeNull("Dictionary creation failed");
			result.Should().Contain("someParam1", "ClassInterfaceValue1");
			result.Should().NotContain("someParam1Json", "ClassInterfaceValue1");
			result.Should().Contain("someParam2", "3");
			result.Should().NotContain("Param2", "3");
			result.Should().Contain("someParam3", "Class3Param3%20Value");
			result.Should().NotContain("someParam3Json", "Class3Param3 Value");
		}

		[Test]
		public void TestInterface()
		{
			var obj = new DictionaryObjectTestClass3();
			var result = obj.SerializeToDictionary(_serializationOptionsUrlDisableEncode1);

			result.Should().NotBeNull("Dictionary creation failed");
			result.Should().Contain("someParam1", "ClassInterfaceValue1");
			result.Should().Contain("someParam2","3");
		}
	}

	public class DictionaryObjectTestClass1
	{
		[DictionaryElement()]
		public string Param1 { get; set; } = "Class1 Value";
		[DictionaryElement()]
		public int Param2 { get; set; } = 1;
		public List<string> ParamList1 { get; set; } = new List<string> {"listItem1", "listItem2", "listItem3"};
	}

	public class DictionaryObjectTestClass2
	{
		[JsonProperty("someParam1Json")]
		public string Param1 { get; set; } = "Class2Value";

		[DictionaryElement("someParam2")]
		public int Param2 { get; set; } = 2;

		[XmlElement("someParamList1Xml")]
		public List<string> ParamList1 { get; set; } = new List<string> { "listItem1", "listItem2", "listItem3" };
	}

	public interface IDictionaryObjectTestInterface1
	{
		[DictionaryElement("someParam1")]
		string Param1 { get; set; }
		[DictionaryElement("someParam2")]
		int Param2 { get; set; }
	}

	public class DictionaryObjectTestClass3 : IDictionaryObjectTestInterface1
	{
		#region Implementation of IQueryStringInterface1

		public string Param1 { get; set; } = "ClassInterfaceValue1";
		public int Param2 { get; set; } = 3;

		[DictionaryElement("someParam3")]
		[JsonProperty("someParam3Json")]
		public string Param3 { get; set; } = "Class3Param3 Value";

		#endregion
	}
}
