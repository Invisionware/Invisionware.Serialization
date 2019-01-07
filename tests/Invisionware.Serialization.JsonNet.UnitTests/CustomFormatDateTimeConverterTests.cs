using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Invisionware.Serialization.JsonNet.Converters;
using NUnit.Framework;

namespace Invisionware.Serialization.UnitTests
{
	[TestFixture]
	public class CustomFormatDateTimeConverterTests
	{
		public void DateTimeCustomFormatConverterTest()
		{			
		}
	}

	public class CustomFormatDateTimeConverterTestClass
	{
		[JsonConverter(typeof(NewtonSoftJsonDateTimeCustomFormatConverter), "dd-MM-yyyy")]
		public DateTime DayMonthYear { get; set; } = DateTime.Now;
	}
}
