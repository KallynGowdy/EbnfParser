using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EbnfParser.Tests
{
	public class ConcatenationExpressionTests
	{

		[Theory]
		[MemberData("ConcatenationElements")]
		public void Test_ConcatenationExpressionsAreFlattened(ConcatenationExpression[] concatenationExpressions, GrammarElement[] flattenedElements)
		{
			ConcatenationExpression expression = new ConcatenationExpression(concatenationExpressions);

			Assert.Collection(expression,
				flattenedElements.Select<GrammarElement, Action<GrammarElement>>(e => (a => Assert.True(a.Equals(e)))).ToArray());
		}

		[Theory]
		[MemberData("ConcatenationElements")]
		public void Test_PlusOperatorConcatenatesConcatenationElementsIntoOneFlattenedExpression(ConcatenationExpression[] concatenationExpressions, GrammarElement[] flattenedElements)
		{
			ConcatenationExpression expression = concatenationExpressions[0];
			for (int i = 1; i < concatenationExpressions.Length; i++)
			{
				expression = expression + concatenationExpressions[i];
			}

			Assert.Collection(expression,
				flattenedElements.Select<GrammarElement, Action<GrammarElement>>(e => (a => Assert.True(a.Equals(e)))).ToArray());
		}

		public static IEnumerable<object[]> ConcatenationElements
		{
			get
			{
				return new[]
				{
					new object[]
					{
						new ConcatenationExpression[]
						{
							new ConcatenationExpression("a", "b"),
							new ConcatenationExpression("c","d"),
						},
						new GrammarElement[]
						{
							(Terminal)"a",
							(Terminal)"b",
							(Terminal)"c",
							(Terminal)"d"
						}
					},
					new object[]
					{
						new ConcatenationExpression[]
						{
							new ConcatenationExpression("a", "q"),
							new ConcatenationExpression("n","r"),
							new ConcatenationExpression("p","l","s"),
						},
						new GrammarElement[]
						{
							(Terminal)"a",
							(Terminal)"q",
							(Terminal)"n",
							(Terminal)"r",
							(Terminal)"p",
							(Terminal)"l",
							(Terminal)"s"
						}
					}
				};
			}
		}
	}
}
