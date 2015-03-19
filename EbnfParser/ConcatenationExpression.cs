using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EbnfParser
{
	/// <summary>
	///     Defines a class that represents an expression of concatenation of two grammar elements.
	/// </summary>
	public class ConcatenationExpression : GrammarElement, IEquatable<ConcatenationExpression>, IEnumerable<GrammarElement>
	{
		/// <summary>
		///     Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>
		///     A hash code for the current <see cref="T:System.Object" />.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			unchecked
			{
				return Elements?.GetHashCode() ?? 0;
			}
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		IEnumerator IEnumerable.GetEnumerator() => Elements.GetEnumerator();

		/// <summary>
		/// Gets or sets the list of elements that are concatenated by this expression.
		/// </summary>
		public GrammarElement[] Elements
		{
			get;
		}

		/// <summary>
		///     Initializes a new <see cref="ConcatenationExpression" /> from the given grammar elements.
		/// </summary>
		/// <param name="left">The left hand side of the expression.</param>
		/// <param name="right">The right hand side of the expression</param>
		/// <exception cref="ArgumentNullException">The value of 'left' and 'right' cannot be null. </exception>
		public ConcatenationExpression(GrammarElement left, GrammarElement right) : this(new[] { left, right })
		{
			if (left == null) throw new ArgumentNullException("left");
			if (right == null) throw new ArgumentNullException("right");
		}

		/// <summary>
		///     Initializes a new <see cref="ConcatenationExpression" /> from the given grammar elements.
		/// </summary>
		/// <param name="grammarElements">The elements that should be concatenated.</param>
		public ConcatenationExpression(IEnumerable<GrammarElement> grammarElements) : this(grammarElements.Select(WrapToConcatenationExpression).ToList())
		{
		}

		/// <summary>
		///     Initializes a new <see cref="ConcatenationExpression" /> from the given grammar elements.
		/// </summary>
		/// <param name="elements">The elements that should be concatenated.</param>
		public ConcatenationExpression(params GrammarElement[] elements) : this(elements.AsEnumerable())
		{
		}

		/// <summary>
		///     Initializes a new <see cref="ConcatenationExpression" /> from the given terminals.
		/// </summary>
		/// <param name="concatenationExpressions">The terminals that should be concatenated.</param>
		public ConcatenationExpression(params ConcatenationExpression[] concatenationExpressions) : this(concatenationExpressions.AsEnumerable())
		{
		}

		/// <summary>
		///     Initializes a new <see cref="ConcatenationExpression" /> from the given terminals.
		/// </summary>
		/// <param name="terminals">The terminals that should be concatenated.</param>
		public ConcatenationExpression(params Terminal[] terminals) : this(terminals.AsEnumerable())
		{	
		}

		private ConcatenationExpression(IList<ConcatenationExpression> expressions)
		{
			if (expressions == null) throw new ArgumentNullException("expressions");
			Elements = expressions.SelectMany(e => e).ToArray();
		}

		private ConcatenationExpression(List<GrammarElement> list)
		{
			Elements = list.ToArray();
		}


		/// <summary>
		/// Wraps the given element in a <see cref="ConcatenationExpression"/> or flattens the element if it is a <see cref="ConcatenationExpression"/>.
		/// </summary>
		/// <param name="element">The element that should be wrapped.</param>
		/// <returns></returns>
		private static ConcatenationExpression WrapToConcatenationExpression(GrammarElement element)
		{
			var expression = element as ConcatenationExpression;
			if (expression != null)
				return new ConcatenationExpression(expression.Elements.ToList());
			return new ConcatenationExpression(new List<GrammarElement> { element });
		}

		/// <summary>
		///     Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		///     true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public override bool Equals(GrammarElement other) => Equals(other as ConcatenationExpression);

		/// <summary>
		///     Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		///     true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(ConcatenationExpression other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Elements.SequenceEqual(Elements);
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
			return Equals(obj as ConcatenationExpression);
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
		/// </returns>
		/// <filterpriority>1</filterpriority>
		public IEnumerator<GrammarElement> GetEnumerator() => Elements.AsEnumerable().GetEnumerator();

		/// <summary>
		///     Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		///     A string that represents the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return string.Join(" + ", Elements.AsEnumerable());
		}
	}
}