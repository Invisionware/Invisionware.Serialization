using System;

namespace Invisionware.Serialization
{
	[AttributeUsage(AttributeTargets.Enum)]
	public class JsonEnumAttribute : Attribute
	{
		public string Name { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="JsonEnumAttribute"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		public JsonEnumAttribute(string name)
		{
			Name = name;
		}
	}
}
