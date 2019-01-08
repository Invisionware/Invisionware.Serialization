using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Invisionware.Serialization
{
	public static class TypeExtensions
	{
		public static bool IsCollectionType(this Type type)
		{
			//if (!type.GetGenericArguments().Any())
			//	return false;

			if (type.IsArray || type.GetTypeInfo().IsArray) return true;
			if (!type.GetTypeInfo().IsGenericType) return false;

			Type genericTypeDefinition = type.GetGenericTypeDefinition();
			var collectionTypes = new[] { typeof(IEnumerable<>), typeof(ICollection<>), typeof(IList<>), typeof(List<>) };

			return collectionTypes.Any(x => x.GetTypeInfo().IsAssignableFrom(genericTypeDefinition.GetTypeInfo()));
		}
	}
}
