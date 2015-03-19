using System;
using Xunit;

namespace EbnfParser.Tests
{
	public class EbnfParserTests
	{
		[Fact]
		public void Test_ParseTerminal()
		{
			Terminal terminal = "a";

			EbnfParser parser = new EbnfParser(terminal);

			var result = parser.Parse("a");

			Assert.True(result.Success);
		}

	}
}
