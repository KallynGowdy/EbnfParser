using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbnfParser
{
	public class EbnfParser
	{
		/// <summary>
		/// Gets the root grammar element that the parser uses.
		/// </summary>
		public GrammarElement Root { get; }

		public EbnfParser(GrammarElement root)
		{
			this.Root = root;
		}

		/// <summary>
		/// Parses the given input and returns the result of the operation.
		/// </summary>
		/// <param name="input">The string that should be parsed.</param>
		/// <returns></returns>
		public ParseResult Parse(string input)
		{
			if (input == null) throw new ArgumentNullException("input");
			return Root.Parse(input);
		}

		/// <summary>
		/// Parses the given input and returns the result of the operation.
		/// </summary>
		/// <param name="input">The text that should be parsed, surfaced through a text reader.</param>
		/// <returns></returns>
		public ParseResult Parse(TextReader input)
		{
			if (input == null) throw new ArgumentNullException("input");
			return Root.Parse(input.ReadToEnd());
		}
	}
}
