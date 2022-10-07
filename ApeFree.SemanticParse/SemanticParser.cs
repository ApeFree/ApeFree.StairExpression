using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ApeFree.SemanticParse
{
    public class SemanticParser
    {
        public Dictionary<string, string> CodeBlockStartEndTags { get; }
        public List<string> QuotationMarks { get; }
        public SemanticParser()
        {
            CodeBlockStartEndTags = new Dictionary<string, string>();
            CodeBlockStartEndTags.Add("(", ")");
            CodeBlockStartEndTags.Add("{", "}");
            CodeBlockStartEndTags.Add("[", "]");
            CodeBlockStartEndTags.Add("begin", "end");

            QuotationMarks = new List<string>() { 
                "\"","'"
            };
        }

        public CodeDocument Parse(string codeText)
        {
            Queue<CodeBlock> blocks = new Queue<CodeBlock>() ;
            blocks.Enqueue(new CodeDocument());

            Queue<string> expectedEndTags = new Queue<string>() ;

            char? quotationMark = null;

            foreach (char c in codeText)
            {
                StartWith
            }
        }
    }

    /// <summary>
    /// 代码对象
    /// </summary>
    public class CodeObject
    {
        public virtual CodeBlock Parent { get; internal set; }
    }

    /// <summary>
    /// 代码文档
    /// </summary>
    public class CodeDocument : CodeBlock
    {
        public override CodeBlock Parent { get => null; internal set { } }

        public static CodeDocument Load(string codeText)
        {

        }
    }

    /// <summary>
    /// 代码块
    /// </summary>
    public class CodeBlock : CodeObject
    {
        private readonly LinkedList<CodeObject> elements;
        public IEnumerable<CodeObject> Elements => elements.AsEnumerable();

        public CodeBlock()
        {
            elements = new LinkedList<CodeObject>();
        }

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="object"></param>
        public void AddObject(CodeObject @object)
        {
            if (elements.Contains(@object))
            {
                @object.Parent = this;
                elements.AddLast(@object);
            }
        }
    }

    /// <summary>
    /// 代码文本
    /// </summary>
    public class CodeText : CodeObject
    {
        private StringBuilder stringBuilder;
        private readonly string text;
        public string Text => stringBuilder?.ToString() ?? text;
        public CodeText(string text)
        {
            this.text = text ?? string.Empty;
        }

        public CodeText() : this(null) { }

        public void AppendText(string text)
        {
            stringBuilder ??= new StringBuilder(text);
            stringBuilder.Append(text);
        }
    }
}
