using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ApeFree.StairExpression
{
    public class QueryResult<TNode>
    {
        protected readonly List<QueryResultNode<TNode>> nodes;

        /// <summary>
        /// 所有查询到的结果
        /// </summary>
        public ReadOnlyCollection<QueryResultNode<TNode>> Nodes => nodes.AsReadOnly();

        /// <summary>
        /// 本次查询使用的语句
        /// </summary>
        public Statement Statement { get; }

        protected QueryResult(Statement statement)
        {
            nodes = new List<QueryResultNode<TNode>>();
            Statement = statement;
        }

        public class QueryResultNode<TNode>
        {
            /// <summary>
            /// 节点
            /// </summary>
            public TNode Node { get; }

            /// <summary>
            /// 查询语句节点
            /// </summary>
            public StatementNode StatementNode { get; }

            public QueryResultNode(TNode node, StatementNode statementNode)
            {
                Node = node;
                StatementNode = statementNode;
            }

            public override string ToString()
            {
                return Node.ToString();
            }
        }
    }
}
