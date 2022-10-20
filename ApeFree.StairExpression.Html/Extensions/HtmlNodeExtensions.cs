using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ApeFree.StairExpression.Html
{
    public static class HtmlNodeExtension
    {
        public static IEnumerable<HtmlNode> MatchElementsWithWildcard(this HtmlNode elem, string wildcard)
        {
            return elem.ChildNodes.Where(e =>e.NodeType == HtmlNodeType.Element &&  e.Name.MatchByWildcard(wildcard));
        }

        public static IEnumerable<HtmlAttribute> MatchAttributesWithWildcard(this HtmlNode elem, string wildcard)
        {
            return elem.Attributes.Where(e => e.Name.MatchByWildcard(wildcard));
        }


        /// <summary>
        /// 节点溯源
        /// </summary>
        /// <param name="xobj"></param>
        /// <returns></returns>
        public static IEnumerable<HtmlNode> Tracing(this HtmlNode xobj)
        {
            while (xobj != null)
            {
                yield return xobj;
                xobj = xobj.ParentNode;
            }
        }

        /// <summary>
        /// 获取XML节点的路径
        /// </summary>
        /// <param name="xobj"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string GetPath(this HtmlNode xobj, string separator = ".")
        {
            return xobj.Tracing().Reverse().Select(n =>
            {
                if (n is HtmlNode elem)
                {
                    return elem.Name.ToString();
                }
                else
                {
                    return n.ToString();
                }
            }).Join(separator);
        }

        public static string GetPath(this HtmlAttribute xobj, string separator = ".") => xobj.XPath;
    }
}
