using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbnfParser
{
	/// <summary>
	/// Defines a class that represents a terminal. That is, a static string.
	/// </summary>
	public class Terminal : IGrammarElement, IEquatable<Terminal>, IEquatable<string>
	{
		/// <summary>
		/// Gets or sets the value of the terminal.
		/// Never null.
		/// </summary>
		public string Value { get; }

		/// <summary>
		/// Initializes a new terminal from the given string.
		/// </summary>
		/// <param name="value">The value that the terminal represents.</param>
		/// <exception cref="ArgumentNullException">The value of 'value' cannot be null. </exception>
		public Terminal(string value)
		{
			if (value == null) throw new ArgumentNullException("value");
			this.Value = value;
		}

		/// <summary>
		/// Concatenates the two terminals together via returning a Concatenation expression.
		/// </summary>
		/// <param name="left">The left-hand terminal.</param>
		/// <param name="right">The right-hand terminal.</param>
		/// <returns>Returns a <see cref="ConcatenationExpression"/>.</returns>
		public static ConcatenationExpression operator +(Terminal left, Terminal right)
		{
			return new ConcatenationExpression(left, right);
		}

		/// <summary>
		/// Alternates the two terminals via returning a <see cref="AlternatedExpression"/>.
		/// </summary>
		/// <param name="left">The left-hand terminal.</param>
		/// <param name="right">The right-hand terminal.</param>
		/// <returns>Returns a <see cref="AlternatedExpression"/>.</returns>
		public static AlternatedExpression operator |(Terminal left, Terminal right)
		{
			return new AlternatedExpression(left, right);
		}

		/// <summary>
		/// Alternates the Alternated expression and terminal via returning a <see cref="AlternatedExpression"/>.
		/// </summary>
		/// <param name="left">The left-hand expression.</param>
		/// <param name="right">The right-hand terminal.</param>
		/// <returns>Returns a <see cref="AlternatedExpression"/>.</returns>
		public static AlternatedExpression operator |(AlternatedExpression left, Terminal right)
		{
			return new AlternatedExpression(left, right);
		}

		/// <summary>
		/// Implicitly converts the given string into a new terminal.
		/// </summary>
		/// <param name="value">The value that will be wrapped.</param>
		public static implicit operator Terminal(string value)
		{
			return new Terminal(value);
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(Terminal other)
		{
			return other != null &&
				other.Value == this.Value;
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
			var t = obj as Terminal;
			if (t != null)
			{
				return Equals(t);
			}
			return Equals(obj as string);
		}

		/// <summary>
		/// Serves as a hash function for a particular type. 
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object"/>.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode() => new { Value }.GetHashCode();

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(string other) => other != null &&
											String.Equals(this.Value, other, StringComparison.Ordinal);

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(IGrammarElement other) => Equals(other as Terminal);

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return $"'{Value}'";
		}
	}
}
