using System;
using System.IO;

namespace EbnfParser
{
	/// <summary>
	///     Defines a class that represents a terminal. That is, a static string.
	/// </summary>
	public class Terminal : GrammarElement, IEquatable<Terminal>, IEquatable<string>
	{
		/// <summary>
		///     Gets or sets the value of the terminal.
		///     Never null.
		/// </summary>
		public string Value { get; }

		/// <summary>
		///     Initializes a new terminal from the given string.
		/// </summary>
		/// <param name="value">The value that the terminal represents.</param>
		/// <exception cref="ArgumentNullException">The value of 'value' cannot be null. </exception>
		public Terminal(string value)
		{
			if (value == null) throw new ArgumentNullException("value");
			Value = value;
		}

		/// <summary>
		///     Implicitly converts the given string into a new terminal.
		/// </summary>
		/// <param name="value">The value that will be wrapped.</param>
		public static implicit operator Terminal(string value)
		{
			return new Terminal(value);
		}

		/// <summary>
		///     Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		///     true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(Terminal other)
		{
			return other != null &&
			       other.Value == Value;
		}

		/// <summary>
		///     Determines whether the specified <see cref="T:System.Object" /> is equal to the current
		///     <see cref="T:System.Object" />.
		/// </summary>
		/// <returns>
		///     true if the specified object  is equal to the current object; otherwise, false.
		/// </returns>
		/// <param name="obj">The object to compare with the current object. </param>
		/// <filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			var t = obj as Terminal;
			if (t != null)
				return Equals(t);
			return Equals(obj as string);
		}

		/// <summary>
		///     Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>
		///     A hash code for the current <see cref="T:System.Object" />.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode() => new {Value}.GetHashCode();

		/// <summary>
		///     Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		///     true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(string other) => other != null &&
		                                    String.Equals(Value, other, StringComparison.Ordinal);

		/// <summary>
		///     Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		///     true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public override bool Equals(GrammarElement other) => Equals(other as Terminal);

		public override ParseResult Parse(TextReader input)
		{
			
		}

		/// <summary>
		///     Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		///     A string that represents the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return $"'{Value}'";
		}
	}
}