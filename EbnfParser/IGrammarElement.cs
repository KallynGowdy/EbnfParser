using System;

namespace EbnfParser
{
	/// <summary>
	/// Defines an interface for a EBNF grammar element. (Terminal, NonTerminal, Expression)
	/// </summary>
	public interface IGrammarElement : IEquatable<IGrammarElement>
	{
	}
}