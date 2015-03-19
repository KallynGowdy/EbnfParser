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


		public ParseResult Parse(string input)
		{
			if (input == null) throw new ArgumentNullException("input");
			return Parse(new StringReader(input));
		}

		public ParseResult Parse(TextReader input)
		{
			if (input == null) throw new ArgumentNullException("input");
			return Root.Parse(input);
		}
	}

	public class ParseResult
	{
		/// <summary>
		/// Gets whether the parser was able to successfully parse the input.
		/// </summary>
		public bool Success { get; }
		
		/// <summary>
		/// Gets the root node that is a part of the parse tree.
		/// </summary>
		public ParseNode RootNode { get; }

		/// <summary>
		/// 
		/// </summary>
		public string[] Errors { get; }

	}
}
