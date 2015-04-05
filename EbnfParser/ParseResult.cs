using System;
using System.Linq;

namespace EbnfParser
{
	/// <summary>
	/// Defines a structure that represents the result of a parsing operation.
	/// </summary>
	public struct ParseResult : IEquatable<ParseResult>
	{
		private ParseResult(bool success, GrammarElement failingElement, ParseNode rootNode, string[] errors)
		{
			IsSuccess = success;
			RootNode = rootNode;
			Errors = errors;
			FailingElement = failingElement;
		}

		/// <summary>
		/// Creates a new parse result that represents a successful parsing operation.
		/// </summary>
		/// <param name="node">The result of the parsing operation.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">The value of 'node' cannot be null. </exception>
		public static ParseResult Success(ParseNode node)
		{
			if (node == null) throw new ArgumentNullException("node");
			return new ParseResult(true, null, node, new string[0]);
		}

		/// <summary>
		/// Creates a new parse result that represents a failed parsing operation.
		/// </summary>
		/// <param name="failingElement">The grammar element that the parsing failed at.</param>
		/// <param name="errors">The list of errors that occurred while parsing.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">The value of 'failingElement' cannot be null. </exception>
		public static ParseResult Failure(GrammarElement failingElement, params string[] errors)
		{
			if (failingElement == null) throw new ArgumentNullException("failingElement");

			return new ParseResult(false, failingElement, null, errors ?? new string[0]);
		}

		/// <summary>
		/// Gets whether the parser was able to successfully parse the input.
		/// </summary>
		public bool IsSuccess { get; }

		/// <summary>
		/// Gets whether the parser was not able to successfully parse the input.
		/// </summary>
		public bool IsFailure => !IsSuccess;

		/// <summary>
		/// Gets the root node that is a part of the parse tree.
		/// </summary>
		public ParseNode RootNode { get; }

		/// <summary>
		/// Gets the list of errors that occurred during parsing.
		/// </summary>
		public string[] Errors { get; }

		/// <summary>
		/// Gets the grammar element that the failure occurs at.
		/// </summary>
		public GrammarElement FailingElement { get; }

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(ParseResult other) =>
			IsSuccess.Equals(other.IsSuccess) &&
			Equals(FailingElement, other.FailingElement) &&
			Errors.SequenceEqual(other.Errors) &&
			Equals(RootNode, other.RootNode);

		/// <summary>
		/// Indicates whether this instance and a specified object are equal.
		/// </summary>
		/// <returns>
		/// true if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, false.
		/// </returns>
		/// <param name="obj">Another object to compare to. </param><filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			return obj is ParseResult && Equals((ParseResult) obj);
		}

		/// <summary>
		/// Returns the hash code for this instance.
		/// </summary>
		/// <returns>
		/// A 32-bit signed integer that is the hash code for this instance.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			unchecked
			{
				int hashCode = (FailingElement != null ? FailingElement.GetHashCode() : 0);
				hashCode = (hashCode*397) ^ (Errors != null ? Errors.GetHashCode() : 0);
				hashCode = (hashCode*397) ^ (RootNode != null ? RootNode.GetHashCode() : 0);
				hashCode = (hashCode*397) ^ IsSuccess.GetHashCode();
				return hashCode;
			}
		}

		public static bool operator ==(ParseResult left, ParseResult right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(ParseResult left, ParseResult right)
		{
			return !left.Equals(right);
		}
	}
}