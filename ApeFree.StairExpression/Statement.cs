using ApeFree.StairExpression.Utils;
using System.Linq;

namespace ApeFree.StairExpression
{
    /// <summary>
    /// 查找语句
    /// </summary>
    public class Statement
    {
        /// <summary>
        /// 语句表达式
        /// </summary>
        public string Expression { get; }

        /// <summary>
        /// 语句节点
        /// </summary>
        public StatementNode[] Nodes { get; }

        /// <summary>
        /// 节点个数
        /// </summary>
        public int NodeCount => Nodes?.Length ?? 0;

        internal Statement(string statementLine)
        {
            Expression = statementLine;

            // 通过‘.’切分节点
            var nodeExpressions = SymbolSplitter.Split('.', statementLine).ToArray();

            // 创建节点数组并实例化节点
            Nodes = new StatementNode[nodeExpressions.Length];
            for (int i = 0; i < nodeExpressions.Length; i++)
            {
                Nodes[i] = new StatementNode(this, nodeExpressions[i], i);
            }
        }
    }
}
