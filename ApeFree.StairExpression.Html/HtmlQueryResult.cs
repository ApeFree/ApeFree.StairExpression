using HtmlAgilityPack;
using System.Xml.Linq;

namespace ApeFree.StairExpression.Html
{
    public class HtmlQueryResult : QueryResult<HtmlNode>
    {
        internal HtmlQueryResult(Statement statement) : base(statement)
        {
        }

        /// <summary>
        /// 添加结果节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="statementNode"></param>
        internal void AddNode(HtmlNode node, StatementNode statementNode)
        {
            nodes.Add(new QueryResultNode<HtmlNode>(node, statementNode));
        }
    }
}
