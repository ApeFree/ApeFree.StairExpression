using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApeFree.StairExpression.Utils
{
    public class SymbolSplitter
    {
        private static char[] openParenthesisSymbol = new char[] { '(', '[', '{' };
        private static char[] closeBracketSymbol = new char[] { ')', ']', '}' };
        private static char quotationMark = '"';

        public static IEnumerable<string> Split(char symbol, string source)
        {
            List<StringBuilder> result = new List<StringBuilder>() { new StringBuilder() };

            // 括号层数
            int layer = 0;
            // 是否处于引号中
            bool inQuote = false;

            // 遍历每一个字符
            for (int i = 0; i < source.Length; ++i)
            {
                var c = source[i];

                if (!inQuote && openParenthesisSymbol.Contains(c))
                {
                    layer++;
                }
                else if (!inQuote && closeBracketSymbol.Contains(c))
                {
                    layer--;
                }
                else if (c == quotationMark)
                {
                    inQuote = !inQuote;
                }

                if (layer == 0 && symbol == c && !inQuote)
                {
                    result.Add(new StringBuilder());
                }
                else
                {
                    result.Last().Append(c);
                }
            }

            return result.Select(sb => sb.ToString());
        }
    }
}
