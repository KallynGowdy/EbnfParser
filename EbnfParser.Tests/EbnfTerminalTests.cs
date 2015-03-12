using System;
using System.IO;
using Xunit;

namespace EbnfParser.Tests
{
	public class EbnfTerminalTests
	{
		[Theory]
		[InlineData("a", "b")]
		[InlineData("b", "c")]
		public void Test_TerminalValueIsPopluatedByConstructor(string value, string notMatchingValue)
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
		public void Test_AddedTerminalsProduceGrammarExpression(string firstTerminal, string secondTerminal)
		{
			Terminal first = firstTerminal;
			Terminal second = secondTerminal;

			var expression = first + second;

			Assert.Equal(expression.Left, (Terminal)firstTerminal);
			Assert.Equal(expression.Right, (Terminal)secondTerminal);
		}

		[Theory]
		[InlineData("a", "b", "c")]
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
	}
}
