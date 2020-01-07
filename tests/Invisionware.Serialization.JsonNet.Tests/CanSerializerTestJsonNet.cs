using Invisionware.Serialization;
using Invisionware.Serialization.JsonNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using TextSerializationTests;

namespace Invisionware.Serialization.UnitTests
{
	[TestFixture()]
	public class CanSerializerTestsJsonNet : CanSerializerTests
	{
		private readonly JsonSerializerSettings _jsonSettings;

		public CanSerializerTestsJsonNet()
		{
			_jsonSettings = new JsonSerializerSettings()
			{
				ContractResolver = new ContractResolverDelegate(type =>
				{
					if (type == typeof(IAnimal) || type == typeof(Animal) || type == typeof(Cat))
					{
						return true;
					}

					return false;
				}, type =>
				{
					if (type == typeof(IAnimal)) return new Cat();
					if (type == typeof(Animal)) return new Dog();

					return null;
				})
			};

		}

		protected override ISerializer Serializer
		{
			get { return new global::Invisionware.Serialization.JsonNet.JsonSerializer(_jsonSettings); }
		}
	}
}
