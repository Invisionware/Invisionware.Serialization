using System;

namespace Invisionware.Net.WebUtils
{
	/// <summary>
	/// Used to defeine if and how a class/interface field/property will be serialized as a dictionary element.
	/// </summary>
	/// <seealso cref="System.Attribute" />
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true)]
	public class DictionaryElementAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DictionaryElementAttribute" /> class.
		/// </summary>
		public DictionaryElementAttribute()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DictionaryElementAttribute"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		public DictionaryElementAttribute(string name)
		{
			Name = name;
		}

		/// <summary>
		/// Gets or sets the name to be used for the query string parameters.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }

		//// <summary>
		//// Gets or sets a value indicating whether [include if null].
		//// </summary>
		//// <value><c>true</c> if [include if null]; otherwise, <c>false</c>.</value>
		//public bool IncludeIfNull { get; set; }
	}
}
