using Invisionware.Serialization.Net45;
using NUnit.Framework;

namespace Invisionware.Serialization.UnitTests
{
	[TestFixture()]
	public class CanSerializerTestSystemXml : CanSerializerTests
	{
		protected override ISerializer Serializer
		{
			get { return new SystemXmlSerializer(); }
		}
	}
}