using System;
using System.Linq;
using System.Reflection;

namespace Invisionware.Serialization
{
	public static class ReflectionExtensions
	{
		//public static IEnumerable<T> GetCustomAttributesIncludingBaseInterfaces<T>(this Type type) where T : Attribute
		//{
		//    var result =
		//        type.GetRuntimeProperties().SelectMany(x => x.GetCustomAttributes<T>(true))
		//            .Union(
		//                type.GetTypeInfo().ImplementedInterfaces
		//                    .SelectMany(interfaceType => interfaceType.GetRuntimeProperties().SelectMany(y => y.GetCustomAttributes<T>(true))))
		//            .Distinct();

		//    return result.Cast<T>();
		//}

		/// <summary>
		/// Gets the custom attribute from the memberinfo even if it is defined on one of the implemented interfaces.
		/// </summary>
		/// <typeparam name="T">The attribute to look for</typeparam>
		/// <param name="info">The property/method to look at.</param>
		/// <returns>The attribute or null if not found.</returns>
		public static T GetCustomAttributeIncludingInterfaces<T>(this MemberInfo info) where T : Attribute
		{
			var result = info.GetCustomAttribute<T>() ??
						 info.DeclaringType.GetTypeInfo()
							 .ImplementedInterfaces.Select(
								 interfaceType => interfaceType.GetRuntimeProperty(info.Name)?.GetCustomAttribute<T>()).FirstOrDefault();

			return result;
		}

	}
}
	