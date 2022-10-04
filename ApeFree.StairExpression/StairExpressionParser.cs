namespace ApeFree.StairExpression
{
    /// <summary>
    /// Stair表达式解析器
    /// </summary>
    public class StairExpressionParser
    {
        public static Statement Parse(string line)
        {
            return new Statement(line);
        }
    }
}
