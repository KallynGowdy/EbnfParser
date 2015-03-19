using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EbnfParser.Tests
{
	public class AlternatedExpressionTests
	{
		[Theory]
		[InlineData("a", "b", "c")]
		[InlineData("b", "c", "a")]
		public void Test_GrammarElementsAreStored(string firstTerminal, string secondTerminal, string thirdTerminal)
		{
			AlternationExpression alternationExpression = new AlternationExpression(firstTerminal, secondTerminal, thirdTerminal);

			Assert.Collection(alternationExpression,
				a => a.Equals(firstTerminal),
				b => b.Equals(secondTerminal),
				c => c.Equals(thirdTerminal));
		}

		[Theory]
		[MemberData("AlternatedExpressions")]
		public void Test_AlternatedExpressionsAreFlattened(AlternationExpression[] alternationExpressions, GrammarElement[] flattenedElements)
		{
			AlternationExpression expression = new AlternationExpression(alternationExpressions);

			Assert.Collection(expression, 
				flattenedElements.Select<GrammarElement, Action<GrammarElement>>(e => (a => Assert.True(a.Equals(e)))).ToArray());
		}

		[Theory]
		[MemberData("AlternatedExpressions")]
		public void Test_OrOperatorAlternatesAlternatedExpressionsIntoOneFlattenedExpression(AlternationExpression[] alternationExpressions, GrammarElement[] flattenedElements)
		{
			AlternationExpression expression = alternationExpressions[0];
			for (int i = 1; i < alternationExpressions.Length; i++)
			{
				expression = expression | alternationExpressions[i];
			}

			// Assert that the resulting elements equal the already flattened elements
			Assert.Collection(expression,
				flattenedElements.Select<GrammarElement, Action<GrammarElement>>(e => (a => Assert.True(a.Equals(e)))).ToArray());
		}

		public static IEnumerable<object[]> AlternatedExpressions
		{
			get
			{
				return new[]
				{
					new object[]
					{
						new AlternationExpression[]
						{
							new AlternationExpression("a", "b"),
							new AlternationExpression("c", "d")
						},
						new GrammarElement[] // Flattened Elements
						{
							(Terminal)"a",
							(Terminal)"b",
							(Terminal)"c",
							(Terminal)"d"
						}
					},
					new object[]
					{
						new AlternationExpression[]
						{
							new AlternationExpression("d", "b"),
							new AlternationExpression("c", "d"),
							new AlternationExpression("j", "q", "s")
						},
						new GrammarElement[]
						{
							(Terminal)"d",
							(Terminal)"b",
							(Terminal)"c",
							(Terminal)"d",
							(Terminal)"j",
							(Terminal)"q",
							(Terminal)"s",
						}
					},
				};
			}
		}
	}
}
