using Invisionware.Serialization;
using NUnit.Framework;

namespace Invisionware.Serialization.UnitTests
{
    [TestFixture()]
    public abstract class ExtensionTests
    {
        protected abstract ISerializer Serializer { get; }
    }
}
