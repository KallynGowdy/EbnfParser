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
			AlternatedExpression alternatedExpression = new AlternatedExpression(firstTerminal, secondTerminal, thirdTerminal);

			Assert.Collection(alternatedExpression,
				a => a.Equals(firstTerminal),
				b => b.Equals(secondTerminal),
				c => c.Equals(thirdTerminal));
		}

		[Theory]
		[MemberData("AlternatedExpressions")]
		public void Test_AlternatedExpressionsAreFlattened(AlternatedExpression[] alternatedExpressions, IGrammarElement[] flattenedElements)
		{
			AlternatedExpression expression = new AlternatedExpression(alternatedExpressions);

			Assert.Collection(expression, flattenedElements.Select<IGrammarElement, Action<IGrammarElement>>(e => (a => Assert.True(a.Equals(e)))).ToArray());
		}

		public static IEnumerable<object[]> AlternatedExpressions
		{
			get
			{
				return new[]
				{
					new object[]
					{
						new AlternatedExpression[]
						{
							new AlternatedExpression("a", "b"),
							new AlternatedExpression("c", "d")
						},
						new IGrammarElement[]
						{
							(Terminal)"a",
							(Terminal)"b",
							(Terminal)"c",
							(Terminal)"d"
						}
					},
					new object[]
					{
						new AlternatedExpression[]
						{
							new AlternatedExpression("d", "b"),
							new AlternatedExpression("c", "d"),
							new AlternatedExpression("j", "q", "s")
						},
						new IGrammarElement[]
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
