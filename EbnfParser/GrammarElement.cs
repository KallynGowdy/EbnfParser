using System;
using System.IO;

namespace EbnfParser
{
	/// <summary>
	/// Defines an abstract class for a EBNF grammar element. (Terminal, NonTerminal, Expression)
	/// </summary>
	public abstract class GrammarElement : IEquatable<GrammarElement>
	{
		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public abstract bool Equals(GrammarElement other);

		/// <summary>
		///     Constructs a new <see cref="RepetitionExpression" /> using the grammar element.
		/// </summary>
		/// <param name="left">The terminal that should be repeated.</param>
		/// <returns>Returns a new <see cref="RepetitionExpression" />.</returns>
		public static RepetitionExpression operator !(GrammarElement left)
		{
			if (left == null) throw new ArgumentNullException("left");
			return new RepetitionExpression(left);
		}

		/// <summary>
		///     Concatenates the two terminals together via returning a Concatenation expression.
		/// </summary>
		/// <param name="left">The left-hand terminal.</param>
		/// <param name="right">The right-hand terminal.</param>
		/// <returns>Returns a <see cref="ConcatenationExpression" />.</returns>
		public static ConcatenationExpression operator +(GrammarElement left, GrammarElement right)
		{
			return new ConcatenationExpression(left, right);
		}

		/// <summary>
		///     Concatenates the two terminals together via returning a Concatenation expression.
		/// </summary>
		/// <param name="left">The left-hand terminal.</param>
		/// <param name="right">The right-hand terminal.</param>
		/// <returns>Returns a <see cref="ConcatenationExpression" />.</returns>
		public static ConcatenationExpression operator +(string left, GrammarElement right)
		{
			return new ConcatenationExpression((Terminal)left, right);
		}

		/// <summary>
		///     Concatenates the two terminals together via returning a Concatenation expression.
		/// </summary>
		/// <param name="left">The left-hand terminal.</param>
		/// <param name="right">The right-hand terminal.</param>
		/// <returns>Returns a <see cref="ConcatenationExpression" />.</returns>
		public static ConcatenationExpression operator +(GrammarElement left, string right)
		{
			return new ConcatenationExpression(left, (Terminal)right);
		}

		/// <summary>
		///     Alternates the two terminals via returning a <see cref="AlternationExpression" />.
		/// </summary>
		/// <param name="left">The left-hand terminal.</param>
		/// <param name="right">The right-hand terminal.</param>
		/// <returns>Returns a <see cref="AlternationExpression" />.</returns>
		public static AlternationExpression operator |(GrammarElement left, GrammarElement right)
		{
			return new AlternationExpression(left, right);
		}

		/// <summary>
		///     Alternates the two terminals via returning a <see cref="AlternationExpression" />.
		/// </summary>
		/// <param name="left">The left-hand terminal.</param>
		/// <param name="right">The right-hand terminal.</param>
		/// <returns>Returns a <see cref="AlternationExpression" />.</returns>
		public static AlternationExpression operator |(string left, GrammarElement right)
		{
			return new AlternationExpression((Terminal)left, right);
		}

		/// <summary>
		///     Alternates the two terminals via returning a <see cref="AlternationExpression" />.
		/// </summary>
		/// <param name="left">The left-hand terminal.</param>
		/// <param name="right">The right-hand terminal.</param>
		/// <returns>Returns a <see cref="AlternationExpression" />.</returns>
		public static AlternationExpression operator |(GrammarElement left, string right)
		{
			return new AlternationExpression(left, (Terminal)right);
		}

		/// <summary>
		/// Parses the grammar element from the given input.
		/// </summary>
		/// <param name="input">The input that the element should be parsed from.</param>
		/// <returns></returns>
		public abstract ParseResult Parse(string input);

		/// <summary>
		/// Creates a new successful <see cref="ParseResult"/> using the given parse node.
		/// </summary>
		/// <param name="node">The parse node that represents the result of the successful operation.</param>
		/// <returns></returns>
		protected ParseResult Success(ParseNode node)
		{
			return ParseResult.Success(node);
		}

		/// <summary>
		/// Creates a new parse result that represents a failed parsing operation.
		/// </summary>
		/// <param name="errors">The list of errors that occurred while parsing.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">The value of 'failingElement' cannot be null. </exception>
		protected ParseResult Failure(params string[] errors)
		{
			return ParseResult.Failure(this, errors);
		}
	}
}