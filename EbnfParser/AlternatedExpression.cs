using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EbnfParser
{
	/// <summary>
	/// Defines a class that represents a list of grammar elements that can be alternated.
	/// </summary>
	public class AlternatedExpression : IGrammarElement, IEquatable<AlternatedExpression>, IEnumerable<IGrammarElement>
	{
		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(AlternatedExpression other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(elements, other.elements);
		}

		/// <summary>
		/// Serves as a hash function for a particular type. 
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object"/>.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			return elements?.GetHashCode() ?? 0;
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		private IGrammarElement[] elements;

		/// <summary>
		/// Initializes a new <see cref="AlternatedExpression"/>.
		/// The given alternated expressions will be flattened into a single alternated expression containing all of the operands.
		/// </summary>
		/// <exception cref="ArgumentNullException">The value of 'grammars' cannot be null. </exception>
		public AlternatedExpression(AlternatedExpression left, AlternatedExpression right) : this(new[] { left, right }.AsEnumerable())
		{
		}

		/// <summary>
		/// Initializes a new <see cref="AlternatedExpression"/>.
		/// </summary>
		/// <param name="grammars">The list of grammars that should be alternated.</param>
		/// <exception cref="ArgumentNullException">The value of 'grammars' cannot be null. </exception>
		public AlternatedExpression(params IGrammarElement[] grammars) : this(grammars.AsEnumerable())
		{
		}

		public AlternatedExpression(params AlternatedExpression[] expressions) : this(expressions.AsEnumerable())
		{

		}

		/// <summary>
		/// Initializes a new <see cref="AlternatedExpression"/>.
		/// </summary>
		/// <param name="terminals">The list of grammars that should be alternated.</param>
		/// <exception cref="ArgumentNullException">The value of 'grammars' cannot be null. </exception>
		public AlternatedExpression(params Terminal[] terminals) : this(terminals.AsEnumerable())
		{
		}

		/// <summary>
		/// Initializes a new <see cref="AlternatedExpression"/>.
		/// </summary>
		/// <param name="grammars">The list of grammars that should be alternated.</param>
		/// <exception cref="ArgumentNullException">The value of 'grammars' cannot be null. </exception>
		public AlternatedExpression(IEnumerable<IGrammarElement> grammars) : this(grammars.Select(WrapToAlternatedExpression))
		{

		}

		private AlternatedExpression(IEnumerable<AlternatedExpression> expressions)
		{
			if (expressions == null) throw new ArgumentNullException("expressions");
			this.Elements = expressions.SelectMany(e => e).ToArray();
		}

		private AlternatedExpression(IList<IGrammarElement> grammars)
		{
			this.Elements = grammars.ToArray();
		}

		/// <summary>
		/// Wraps the given element in an alternated expression or flattens the element if it is an alternated expression itself.
		/// </summary>
		/// <param name="element">The element that should be wrapped.</param>
		/// <returns></returns>
		private static AlternatedExpression WrapToAlternatedExpression(IGrammarElement element)
		{
			AlternatedExpression expression = element as AlternatedExpression;
			if (expression != null)
			{
				return new AlternatedExpression(expression.Elements.ToList());
			}
			else
			{
				return new AlternatedExpression(new List<IGrammarElement>() { element });
			}
		}

		/// <summary>
		/// Gets or sets the list of elements that are alternated by this expression.
		/// </summary>
		/// <exception cref="ArgumentNullException" accessor="set">The value of 'value' cannot be null. </exception>
		public IGrammarElement[] Elements
		{
			get { return elements; }
			set
			{
				if (value == null) throw new ArgumentNullException("value");
				elements = value;
			}
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(IGrammarElement other)
		{
			return Equals(other as AlternatedExpression);
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
			return Equals(obj as AlternatedExpression);
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
		/// </returns>
		/// <filterpriority>1</filterpriority>
		public IEnumerator<IGrammarElement> GetEnumerator() => Elements.AsEnumerable().GetEnumerator();

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString() => string.Join(" | ", Elements.Select(e => e.ToString()));
	}
}