using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Invisionware.Serialization.JsonNet.Converters;
using NUnit.Framework;
using FluentAssertions;

namespace Invisionware.Serialization.UnitTests
{
	[TestFixture]
	public class CustomFormatDateTimeConverterTests
	{
		[Test]
		public void DateTimeCustomFormatConverterTest()
		{
			var obj = new DateTimeFormatConverterTestClass1();

			var result = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

			result.Should().NotBeNullOrEmpty();
			result.Should().Contain(obj.SomeDate.ToString("dd-MM-yyyy"));
		}
	}
}
