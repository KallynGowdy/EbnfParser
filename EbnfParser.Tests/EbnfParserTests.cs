using System;
using System.Collections.Generic;
using Xunit;

namespace EbnfParser.Tests
{
	public class EbnfParserTests
	{
		[Theory]
		[InlineData("a", "a", true)]
		[InlineData("keyword", "keyword", true)]
		[InlineData("keyword", "not_keyword", false)]
		[InlineData("keyword", "keywordshouldbeparsed", true)]
		public void Test_ParseTerminal(string inputTerminal, string input, bool succeeds)
		{
			Terminal terminal = inputTerminal;

			EbnfParser parser = new EbnfParser(terminal);

			var result = parser.Parse(input);

			Assert.True(succeeds ? result.IsSuccess : result.IsFailure);
		}

		[Fact]
		public void Test_ParseRepeatedExpression()
		{
			RepetitionExpression expression = new RepetitionExpression((Terminal)"abcdef");

			EbnfParser parser = new EbnfParser(expression);

			var result = parser.Parse("abcdefabcdef");

			var expectedResult = ParseResult.Success(
				new RepetitionNode(
					expression, 
					new []
					{
						new TerminalNode("abcdef", "abcdef"),
						new TerminalNode("abcdef", "abcdef")
					}));

			Assert.True(result.IsSuccess);
			Assert.Equal(expectedResult, result);
		}

	}
}
