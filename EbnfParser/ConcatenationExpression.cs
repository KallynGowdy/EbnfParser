using System;

namespace EbnfParser
{
	/// <summary>
	/// Defines a class that represents an expression of concatenation of two grammar elements.
	/// </summary>
	public class ConcatenationExpression : IGrammarElement, IEquatable<ConcatenationExpression>
	{
		public override int GetHashCode()
		{
			unchecked
			{
				return ((left?.GetHashCode() ?? 0)*397) ^ (right?.GetHashCode() ?? 0);
			}
		}

		private IGrammarElement left;
		private IGrammarElement right;

		/// <summary>
		/// Gets or sets the left element of the grammar.
		/// </summary>
		/// <exception cref="ArgumentNullException" accessor="set">The value of 'value' cannot be null. </exception>
		public IGrammarElement Left
		{
			get { return left; }
			set
			{
				if (value == null) throw new ArgumentNullException("value");
				left = value;
			}
		}

		/// <summary>
		/// Gets or sets the right element of the grammar.
		/// </summary>
		/// <exception cref="ArgumentNullException" accessor="set">The value of 'value' cannot be null. </exception>
		public IGrammarElement Right
		{
			get { return right; }
			set
			{
				if (value == null) throw new ArgumentNullException("value");
				right = value;
			}
		}

		/// <summary>
		/// Initializes a new <see cref="ConcatenationExpression"/> from the given grammar elements.
		/// </summary>
		/// <param name="left">The left hand side of the expression.</param>
		/// <param name="right">The right hand side of the expression</param>
		/// <exception cref="ArgumentNullException">The value of 'left' and 'right' cannot be null. </exception>
		public ConcatenationExpression(IGrammarElement left, IGrammarElement right)
		{
			if (left == null) throw new ArgumentNullException("left");
			if (right == null) throw new ArgumentNullException("right");
			this.Left = left;
			this.Right = right;
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(IGrammarElement other) => Equals(other as ConcatenationExpression);

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(ConcatenationExpression other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(left, other.left) && Equals(right, other.right);
		}

		/// <summary>
		/// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// true if the specified object  is equal to the current object; otherwise, false.
		/// </returns>
		/// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((ConcatenationExpression) obj);
		}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return $"{Left} + {Right}";
		}
	}
}