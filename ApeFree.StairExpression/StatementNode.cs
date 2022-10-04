using ApeFree.StairExpression.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace ApeFree.StairExpression
{
    /// <summary>
    /// 语句节点
    /// </summary>
    public class StatementNode
    {
        /// <summary>
        /// 所属语句
        /// </summary>
        internal Statement Parent { get; }

        /// <summary>
        /// 节点序号
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// 是否为属性节点
        /// </summary>
        public bool IsAttribute { get; }

        /// <summary>
        /// 是否为元素节点
        /// </summary>
        public bool IsElement => !IsAttribute;

        /// <summary>
        /// 节点表达式
        /// </summary>
        public string NodeExpression { get; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; private set; }

        /// <summary>
        /// 查询条件集合
        /// </summary>
        public List<QueryCondition> QueryConditions { get; private set; }

        internal StatementNode(Statement parent, string expression, int hierarchy)
        {
            Parent = parent;
            NodeExpression = expression;
            Index = hierarchy;

            IsAttribute = NodeExpression.StartsWith("$");

            ParseNodeName();
            ParseQueryConditions();

            CheckAttributeNode();
        }

        /// <summary>
        /// 解析节点名称
        /// </summary>
        private void ParseNodeName()
        {
            char[] marks = new[] { '[', '(', '{', '"' };
            NodeName = IsAttribute ? NodeExpression.Substring(1) : NodeExpression;

            var minMarkIndexs = marks.Select(c => NodeName.IndexOf(c)).Where(i => i > -1);
            if (minMarkIndexs.Any())
            {
                NodeName = NodeName.Substring(0, minMarkIndexs.Min());
            }
        }

        /// <summary>
        /// 解析查询条件
        /// </summary>
        private void ParseQueryConditions()
        {
            var regexString = @"(?i)(?<=\[)(.*)(?=\])"; //中括号[]
            Regex r = new Regex(regexString);
            var mc = r.Matches(NodeExpression);

            if (mc.Count > 0)
            {
                QueryConditions = new List<QueryCondition>();
                foreach (Match m in mc)
                {
                    if (string.IsNullOrEmpty(m.Value))
                    {
                        continue;
                    }

                    var conditionsStrings = SymbolSplitter.Split(',', m.Value);
                    foreach (string s in conditionsStrings)
                    {
                        QueryConditions.Add(new QueryCondition(this, s));
                    }
                }
            }
        }

        /// <summary>
        /// 检查属性节点
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void CheckAttributeNode()
        {
            if (IsAttribute)
            {
                // 如果Attribute不是处于最后一位，则报错
                if (Parent.Nodes.Length - 1 != Index)
                {
                    throw new Exception($"属性节点“{NodeExpression}”只能位于语句末尾，不应有下级元素。");
                }

                // 如果Attribute包含查询条件，则报错
                if (QueryConditions?.Any() != null)
                {
                    throw new Exception($"属性节点“{NodeExpression}”不支持使用查询条件。");
                }
            }
        }

        /// <summary>
        /// 获取节点路径
        /// </summary>
        /// <returns></returns>
        public string GetNodePath()
        {
            List<string> nodeNames = new List<string>();

            foreach (var node in Parent.Nodes)
            {
                nodeNames.Add(node.NodeExpression);

                if (node == this)
                {
                    break;
                }
            }

            return string.Join(".", nodeNames.ToArray());
        }
    }
}
