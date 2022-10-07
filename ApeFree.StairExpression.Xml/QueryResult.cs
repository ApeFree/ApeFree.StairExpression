using System.Xml.Linq;

namespace ApeFree.StairExpression.Xml
{
    public class XmlQueryResult : QueryResult<XObject>
    {
        internal XmlQueryResult(Statement statement) : base(statement)
        {
        }

        /// <summary>
        /// 添加结果节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="statementNode"></param>
        internal void AddNode(XObject node, StatementNode statementNode)
        {
            nodes.Add(new QueryResultNode<XObject>(node, statementNode));
        }
    }
}
