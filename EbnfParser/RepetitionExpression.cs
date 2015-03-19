using System;

namespace EbnfParser
{
	/// <summary>
	///     Defines a class that represents the repetition of a grammar element.
	/// </summary>
	public class RepetitionExpression : GrammarElement
	{
		/// <summary>
		///     Initializes a new <see cref="RepetitionExpression" /> object using the given grammar element.
		/// </summary>
		/// <param name="repeatedValue">The element that should be repeated.</param>
		/// <exception cref="ArgumentNullException">The value of 'repeatedValue' cannot be null. </exception>
		public RepetitionExpression(GrammarElement repeatedValue)
		{
			if (repeatedValue == null) throw new ArgumentNullException("repeatedValue");
			RepeatedValue = repeatedValue;
		}

		/// <summary>
		///     Gets or sets the grammar element that is repeated in the expression.
		/// </summary>
		public GrammarElement RepeatedValue { get; }

		/// <summary>
		///     Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		///     true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		protected bool Equals(RepetitionExpression other) => other != null && Equals(RepeatedValue, other.RepeatedValue);

		/// <summary>
		///     Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		///     true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public override bool Equals(GrammarElement other) => Equals(other as RepetitionExpression);

		/// <summary>
		///     Determines whether the specified <see cref="T:System.Object" /> is equal to the current
		///     <see cref="T:System.Object" />.
		/// </summary>
		/// <returns>
		///     true if the specified object  is equal to the current object; otherwise, false.
		/// </returns>
		/// <param name="obj">The object to compare with the current object. </param>
		/// <filterpriority>2</filterpriority>
		public override bool Equals(object obj) => Equals(obj as GrammarElement);

		/// <summary>
		///     Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>
		///     A hash code for the current <see cref="T:System.Object" />.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode() => RepeatedValue?.GetHashCode() ?? 0;

		/// <summary>
		///     Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		///     A string that represents the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return $"[{RepeatedValue}]";
		}
	}
}