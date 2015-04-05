using System;
using System.Collections.Generic;
using System.Linq;

namespace EbnfParser
{
	public abstract class ParseNode : IEquatable<ParseNode>
	{
		protected ParseNode(GrammarElement relatedElement, ParseNode[] children)
		{
			if (relatedElement == null) throw new ArgumentNullException("relatedElement");
			RelatedElement = relatedElement;
			Children = children ?? new ParseNode[0];
		}

		/// <summary>
		/// Gets the total length of this node in characters.
		/// </summary>
		public virtual int Length => Children.Sum(c => c.Length);

		/// <summary>
		/// Gets the enumerable list of children contained by this node.
		/// </summary>
		public ParseNode[] Children { get; }

		/// <summary>
		/// Gets the text that this node contains.
		/// </summary>
		public virtual string Text => string.Join("", Children.AsEnumerable());

		/// <summary>
		/// Gets the grammar element that parsed this node.
		/// </summary>
		public GrammarElement RelatedElement { get; }

		public override string ToString()
		{
			return Text;
		}

		public bool Equals(ParseNode other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Children.SequenceEqual(other.Children) && RelatedElement.Equals(other.RelatedElement);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((ParseNode) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (Children.GetHashCode()*397) ^ RelatedElement.GetHashCode();
			}
		}

		public static bool operator ==(ParseNode left, ParseNode right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(ParseNode left, ParseNode right)
		{
			return !Equals(left, right);
		}
	}

	public class RepetitionNode : ParseNode
	{
		public RepetitionNode(RepetitionExpression relatedElement, ParseNode[] children) : base(relatedElement, children)
		{
		}
	}

	public class AlternationNode : ParseNode
	{

		public AlternationNode(AlternationExpression relatedElement, ParseNode result) : base(relatedElement, new[] { result })
		{
			if (result == null) throw new ArgumentNullException("result");
		}

		/// <summary>
		/// Gets the chosen result from the alternation.
		/// </summary>
		public ParseNode Result => Children.First();
	}

	public class ConcatenationNode : ParseNode
	{
		public ConcatenationNode(ConcatenationExpression concatenationExpression, ParseNode[] children) : base(concatenationExpression, children)
		{
		}
	}

	public class TerminalNode : ParseNode
	{
		public TerminalNode(Terminal terminal, string value) : base(terminal, null)
		{
			Text = value;
		}

		public override int Length => Text.Length;

		public override string Text { get; }
	}
}