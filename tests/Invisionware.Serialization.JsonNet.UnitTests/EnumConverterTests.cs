using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
using Invisionware.Serialization.JsonNet.Converters;
using FluentAssertions;

namespace Invisionware.Serialization.UnitTests
{
	[TestFixture]
	public class EnumConverterTests
	{
		[Test]
		public void EnumSerializeTest1()
		{
			var obj = new EnumConverterTestClass1 { SomeEnum = EnumConverterTestEnum1.Enum2 };

			var result = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

			result.Should().NotBeNull();
			result.Should().Contain("someEnumName2");
		}
	}
}
