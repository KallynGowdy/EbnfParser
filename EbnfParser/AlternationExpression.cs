using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EbnfParser
{
	/// <summary>
	///     Defines a class that represents a list of grammar elements that can be alternated. That is, one or more elements that can be parsed in place of each other.
	/// </summary>
	public class AlternationExpression : GrammarElement, IEquatable<AlternationExpression>, IEnumerable<GrammarElement>
	{
		private GrammarElement[] elements;

		/// <summary>
		///     Initializes a new <see cref="AlternationExpression" />.
		///     The given alternated expressions will be flattened into a single alternated expression containing all of the
		///     operands.
		/// </summary>
		/// <exception cref="ArgumentNullException">The value of 'grammars' cannot be null. </exception>
		public AlternationExpression(AlternationExpression left, AlternationExpression right) : this(new[] {left, right}.AsEnumerable())
		{
		}

		/// <summary>
		///     Initializes a new <see cref="AlternationExpression" />.
		/// </summary>
		/// <param name="grammars">The list of grammars that should be alternated.</param>
		/// <exception cref="ArgumentNullException">The value of 'grammars' cannot be null. </exception>
		public AlternationExpression(params GrammarElement[] grammars) : this(grammars.AsEnumerable())
		{
		}

		/// <summary>
		///     Initializes a new <see cref="AlternationExpression" />.
		/// </summary>
		/// <param name="expressions">The list of alternated expressions that should be flattened/alternated.</param>
		/// <exception cref="ArgumentNullException">The value of 'expressions' cannot be null. </exception>
		public AlternationExpression(params AlternationExpression[] expressions) : this(expressions.AsEnumerable())
		{
		}

		/// <summary>
		///     Initializes a new <see cref="AlternationExpression" />.
		/// </summary>
		/// <param name="terminals">The list of grammars that should be alternated.</param>
		/// <exception cref="ArgumentNullException">The value of 'grammars' cannot be null. </exception>
		public AlternationExpression(params Terminal[] terminals) : this(terminals.AsEnumerable())
		{
		}

		/// <summary>
		///     Initializes a new <see cref="AlternationExpression" />.
		/// </summary>
		/// <param name="grammars">The list of grammars that should be alternated.</param>
		/// <exception cref="ArgumentNullException">The value of 'grammars' cannot be null. </exception>
		public AlternationExpression(IEnumerable<GrammarElement> grammars) : this(grammars.Select(WrapToAlternatedExpression))
		{
		}

		/// <summary>
		///     Initializes a new <see cref="AlternationExpression" />.
		/// </summary>
		/// <param name="expressions">The list of alternated expressions that should be flattened/alternated.</param>
		/// <exception cref="ArgumentNullException">The value of 'expressions' cannot be null. </exception>
		private AlternationExpression(IEnumerable<AlternationExpression> expressions)
		{
			if (expressions == null) throw new ArgumentNullException("expressions");
			Elements = expressions.SelectMany(e => e).ToArray();
		}

		/// <summary>
		///     Initializes a new <see cref="AlternationExpression" />.
		/// </summary>
		/// <param name="grammars">The list of grammar elements that should be alternated.</param>
		private AlternationExpression(IList<GrammarElement> grammars)
		{
			Elements = grammars.ToArray();
		}

		/// <summary>
		///     Gets or sets the list of elements that are alternated by this expression.
		/// </summary>
		/// <exception cref="ArgumentNullException" accessor="set">The value of 'value' cannot be null. </exception>
		public GrammarElement[] Elements
		{
			get { return elements; }
			set
			{
				if (value == null) throw new ArgumentNullException("value");
				elements = value;
			}
		}

		/// <summary>
		///     Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		///     An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>
		///     Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
		/// </returns>
		/// <filterpriority>1</filterpriority>
		public IEnumerator<GrammarElement> GetEnumerator() => Elements.AsEnumerable().GetEnumerator();

		/// <summary>
		///     Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		///     true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(AlternationExpression other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(elements, other.elements);
		}

		/// <summary>
		///     Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		///     true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public override bool Equals(GrammarElement other)
		{
			return Equals(other as AlternationExpression);
		}

		/// <summary>
		/// Parses the grammar element from the given input stream.
		/// </summary>
		/// <param name="input">The input stream (Usually either a StringReader or StreamReader) that the element should be parsed from.</param>
		/// <returns></returns>
		public override ParseResult Parse(string input)
		{
			List<ParseResult> failedResults = new List<ParseResult>(Elements.Length);
			foreach (GrammarElement element in Elements)
			{
				var result = element.Parse(input);
				if (result.IsSuccess)
				{
					return Success(new AlternationNode(this, result.RootNode));
				}
				else
				{
					failedResults.Add(result);
				}
			}
			return Failure(failedResults.SelectMany(r => r.Errors).ToArray());
		}

		/// <summary>
		///     Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>
		///     A hash code for the current <see cref="T:System.Object" />.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			return elements?.GetHashCode() ?? 0;
		}

		public static AlternationExpression operator |(AlternationExpression left, Terminal right)
		{
			return new AlternationExpression(left, right);
		}

		/// <summary>
		///     Wraps the given element in an alternated expression or flattens the element if it is an alternated expression
		///     itself.
		/// </summary>
		/// <param name="element">The element that should be wrapped.</param>
		/// <returns></returns>
		private static AlternationExpression WrapToAlternatedExpression(GrammarElement element)
		{
			var expression = element as AlternationExpression;
			if (expression != null)
				return new AlternationExpression(expression.Elements.ToList());
			return new AlternationExpression(new List<GrammarElement> {element});
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
			return Equals(obj as AlternationExpression);
		}

		/// <summary>
		///     Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		///     A string that represents the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString() => string.Join(" | ", Elements.Select(e => e.ToString()));
	}
}