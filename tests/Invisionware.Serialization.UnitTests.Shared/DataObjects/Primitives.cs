using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TextSerializationTests
{
	[DataContract]
	public class Primitives : IEquatable<Primitives>
	{
		[DataMember(Order = 1)]
		public int Int { get; set; }
		[DataMember(Order = 2)]
		public long Long { get; set; }
		[DataMember(Order = 3)]
		public float Float { get; set; }
		[DataMember(Order = 4)]
		public double Double { get; set; }
		[DataMember(Order = 5)]
		public bool Boolean { get; set; }
		[DataMember(Order = 6)]
		public string String { get; set; }

		public Primitives()
		{
		}

		public override string ToString()
		{
			return string.Format("[Primitives: Int={0}, Long={1}, Float={2}, Double={3}, Boolean={4}]", Int, Long, Float, Double, Boolean);
		}

		public static Primitives Create(int i)
		{
			var p = new Primitives()
			{
				Int = i,
				Long = i,
				Float = i,
				Double = i,
				Boolean = i % 2 == 0,
			};

			p.String = p.ToString();

			return p;
		}

		#region IEquatable implementation
		public override bool Equals(object obj)
		{
			return Equals(obj as Primitives);
		}

		public override int GetHashCode()
		{
			return Int ^ Long.GetHashCode()
				^ Float.GetHashCode()
					^ Double.GetHashCode()
					^ Boolean.GetHashCode()
					^ String.GetHashCode();
		}

		public bool Equals(Primitives other)
		{
			if (other == null) return false;
			return Int == other.Int && Long == other.Long && Float == other.Float && Double == other.Double
				&& Boolean == other.Boolean && String == other.String;
		}

		#endregion
	}

	[DataContract]
	public class PrimitiveList : IEquatable<PrimitiveList>
	{
		public PrimitiveList()
		{
			Items = new List<Primitives>();
		}

		[DataMember(Order=1)]
		public List<Primitives> Items { get; set; }

		#region IEquatable implementation
		public override bool Equals(object obj)
		{
			return (obj is PrimitiveList list && Equals(list));
		}

		public bool Equals(PrimitiveList other)
		{
			return other != null && Items.SequenceEqual(other.Items);
		}

		public override int GetHashCode()
		{
			return Items?.GetHashCode() ?? -1;
		}

		#endregion
	}
}

