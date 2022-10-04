using ApeFree.StairExpression.Utils;
using System;
using System.Linq;

namespace ApeFree.StairExpression
{
    /// <summary>
    /// 查询条件
    /// </summary>
    public class QueryCondition
    {
        /// <summary>
        /// 所属节点
        /// </summary>
        internal StatementNode Parent { get; }

        /// <summary>
        /// 条件表达式
        /// </summary>
        public string ConditionExpression { get; }

        /// <summary>
        /// 条件键名
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// 条件值
        /// </summary>
        public string Value { get; private set; }

        internal QueryCondition(StatementNode parent, string conditionExpression)
        {
            Parent = parent;
            ConditionExpression = conditionExpression;

            ParseConditionArgs();
        }

        private void ParseConditionArgs()
        {
            var args = SymbolSplitter.Split('=', ConditionExpression).Select(s=>s.Trim());
            if (args.Count() == 2)
            {
                Key = args.ElementAt(0);
                Value = args.ElementAt(1);

                // 如果参数有引号则取出引号
                if (Value.StartsWith("\"") && Value.EndsWith("\""))
                {
                    Value = Value.Substring(1, Value.Length - 2);
                }
            }
            else
            {
                throw new Exception($"查询条件表达式“{ConditionExpression}”格式有误。(位置:{Parent.NodeExpression})");
            }
        }
    }
}
