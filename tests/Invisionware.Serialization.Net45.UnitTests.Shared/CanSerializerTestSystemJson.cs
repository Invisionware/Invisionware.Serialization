using Invisionware.Serialization.Net45;

namespace Invisionware.Serialization.UnitTests
{
    using Invisionware.Serialization;

    public class CanSerializerTestSystemJson : CanSerializerTests
    {

        protected override ISerializer Serializer
        {
            get
            {
                return new SystemJsonSerializer();
            }
        }
    }
}
