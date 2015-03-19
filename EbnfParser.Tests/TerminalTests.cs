using System;
using System.IO;
using Xunit;

namespace EbnfParser.Tests
{
	public class TerminalTests
	{
		[Theory]
		[InlineData("a", "b")]
		[InlineData("b", "c")]
		public void Test_TerminalValueIsPopulatedByConstructor(string value, string notMatchingValue)
		{
			Terminal terminal = new Terminal(value);

			Assert.NotNull(terminal);
			Assert.Equal(value, terminal.Value);
			Assert.NotEqual(notMatchingValue, terminal.Value);
		}

		[Theory]
		[InlineData("a")]
		[InlineData("b")]
		public void Test_TerminalsEqualWhenValueIsSame(string value)
		{
			Terminal terminal = new Terminal(value);
			Terminal otherTerminal = new Terminal(value);

			Assert.Equal(otherTerminal, terminal);
			Assert.NotSame(otherTerminal, terminal);
		}

		[Theory]
		[InlineData("a", "b")]
		[InlineData("b", "a")]
		public void Test_TerminalEqualsStringThatIsSameAsValue(string value, string notMatchingValue)
		{
			Terminal terminal = new Terminal(value);

			Assert.Equal(value, terminal);
			Assert.NotEqual(terminal, notMatchingValue);
		}

		[Theory]
		[InlineData("a")]
		[InlineData("b")]
		public void Test_StringCastToTerminalEqualsTerminalConstructedFromString(string value)
		{
			Terminal terminal = new Terminal(value);
			Terminal otherTerminal = value;

			Assert.Equal(terminal, otherTerminal);
			Assert.NotSame(terminal, otherTerminal);
		}

		[Theory]
		[InlineData("a", "b")]
		[InlineData("b", "a")]
		public void Test_AddedTerminalsProduceGrammarExpression(string firstTerminal, string secondTerminal)
		{
			Terminal first = firstTerminal;
			Terminal second = secondTerminal;

			var expression = first + second;

			Assert.Collection(expression,
				e => Assert.Equal(e, first),
				e => Assert.Equal(e, second));
		}

		[Theory]
		[InlineData("a", "b", "c")]
		[InlineData("b", "a", "c")]
		public void Test_AlternatedTerminalsProduceGrammarExpression(string firstTerminal, string secondTerminal, string thirdTerminal)
		{
			Terminal first = firstTerminal;
			Terminal second = secondTerminal;
			Terminal third = thirdTerminal;

			var expression = first | second | third;

			Assert.Collection(expression.Elements,
				a => a.Equals(firstTerminal),
				b => b.Equals(secondTerminal),
				c => c.Equals(thirdTerminal));
		}

		[Theory]
		[InlineData("a")]
		[InlineData("b")]
		[InlineData("c")]
		public void Test_NegatingTerminalProducesRepeatedGrammarExpression(string terminalValue)
		{
			Terminal terminal = terminalValue;

			RepetitionExpression expression = !terminal;

			Assert.NotNull(expression);
			Assert.Equal(terminal, expression.RepeatedValue);
		}

		[Fact]
		public void Test_AdvancedExpressionMapsToAnExpectedExpression()
		{
			AlternationExpression aOrB = (Terminal)"a" | "b";

			Assert.NotNull(aOrB);
			Assert.Collection(aOrB,
				e => Assert.True(e.Equals("a")),
				e => Assert.True(e.Equals("b")));

			RepetitionExpression manyAOrB = !aOrB;

			Assert.NotNull(manyAOrB);
			Assert.NotNull(manyAOrB.RepeatedValue);
			Assert.Same(aOrB, manyAOrB.RepeatedValue);

			ConcatenationExpression aAndBWithCAtEnd = manyAOrB + "c";

			Assert.NotNull(aAndBWithCAtEnd);
			Assert.Collection(aAndBWithCAtEnd,
                e => Assert.Same(manyAOrB, e),
				e => Assert.True(e.Equals("c")));
		}
	}
}
