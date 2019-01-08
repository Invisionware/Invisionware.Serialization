using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Invisionware.Serialization.UnitTests
{
	[TestFixture]
	public class ArrayConverterTests
	{
		[Test]
		public void ObjectSerializeWithStringArrayConverterTest1()
		{
			var obj = new ObjectWithStringArrayConverterTestClass1() { SomeArray = new[] { "value1", "value2", "value3" } };

			var result = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

			result.Should().NotBeNullOrEmpty();
			result.Should().Be("{\"SomeArray\":\"value1,value2,value3\"}");
		}

		[Test]
		public void ObjectSerializeWithIntArrayConverterTest1()
		{
			var obj = new ObjectWithIntArrayConverterTestClass1() { SomeArray = new[] { 1, 2, 3 } };

			var result = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

			result.Should().NotBeNullOrEmpty();
			result.Should().Be("{\"SomeArray\":\"1,2,3\"}");
		}

		[Test]
		public void ObjectSerializeWithStringListConverterTest1()
		{
			var obj = new ObjectWithStringListConverterTestClass1() { SomeArray = new List<string> { "value1", "value2", "value3" } };

			var result = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

			result.Should().NotBeNullOrEmpty();
			result.Should().Be("{\"SomeArray\":\"value1,value2,value3\"}");
		}

		[Test]
		public void ObjectSerializeWithIntListConverterTest1()
		{
			var obj = new ObjectWithIntListConverterTestClass1() { SomeArray = new List<int> { 1, 2, 3 } };

			var result = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

			result.Should().NotBeNullOrEmpty();
			result.Should().Be("{\"SomeArray\":\"1,2,3\"}");
		}

		[Test]
		public void ObjectSerializeWithDateListConverterTest1()
		{
			var dt = DateTime.Now;
			var obj = new ObjectWithDateListConverterTestClass1() { SomeArray = new List<DateTime> { dt, dt.AddDays(1) } };

			var result = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

			result.Should().NotBeNullOrEmpty();
			result.Should().Contain(dt.ToString());
		}

		[Test]
		public void ObjectSerializeWithNullIntListConverterTest1()
		{
			var obj = new ObjectWithIntListConverterTestClass1() { SomeArray = null };

			var result = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

			result.Should().NotBeNullOrEmpty();
			result.Should().Be("{\"SomeArray\":null}");
		}

		[Test]
		public void ObjectDeserializeWithStringArrayConverterTest1()
		{
			var str = "{\"SomeArray\":\"value1,value2, value3\"}";

			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ObjectWithStringArrayConverterTestClass1>(str);

			result.Should().NotBeNull();
			result.SomeArray.Should().NotBeEmpty();
			result.SomeArray.Should().Contain("value1");
		}

		[Test]
		public void ObjectDeserializeWithIntArrayConverterTest1()
		{
			var str = "{\"SomeArray\":\"1,2, 3\"}";

			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ObjectWithIntArrayConverterTestClass1>(str);

			result.Should().NotBeNull();
			result.SomeArray.Should().NotBeEmpty();
			result.SomeArray.Should().Contain(1);
		}

		[Test]
		public void ObjectDeserializeWithStringListConverterTest1()
		{
			var str = "{\"SomeArray\":\"value1,value2, value3\"}";

			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ObjectWithStringListConverterTestClass1>(str);

			result.Should().NotBeNull();
			result.SomeArray.Should().NotBeEmpty();
			result.SomeArray.Should().Contain("value1");
		}

		[Test]
		public void ObjectDeserializeWithIntListConverterTest1()
		{
			var str = "{\"SomeArray\":\"1,2, 3\"}";

			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ObjectWithIntListConverterTestClass1>(str);

			result.Should().NotBeNull();
			result.SomeArray.Should().NotBeEmpty();
			result.SomeArray.Should().Contain(1);
		}

		[Test]
		public void ObjectDeserializeWithDateListConverterTest1()
		{
			var str = "{\"SomeArray\":\"1/8/2019 9:19:32 AM,1/9/2019 9:19:32 AM\"}";


			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ObjectWithDateListConverterTestClass1>(str);

			result.Should().NotBeNull();
			result.SomeArray.Should().NotBeEmpty();
			result.SomeArray.Should().Contain(DateTime.Parse("1/8/2019 9:19:32 AM"));
		}
	}
}
