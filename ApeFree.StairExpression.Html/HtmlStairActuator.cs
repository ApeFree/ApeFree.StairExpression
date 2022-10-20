using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ApeFree.StairExpression.Html
{
    public class HtmlStairActuator
    {
        public const string HtmlTextNodeMark = "$TEXT";

        public static HtmlQueryResult Query(HtmlDocument doc, string expression)
        {
            var statement = StairExpressionParser.Parse(expression);

            HtmlQueryResult result = new HtmlQueryResult(statement);
            foreach (var node in doc.DocumentNode.ChildNodes)
            {
                if (node.NodeType == HtmlNodeType.Element)
                {
                    Query(node, statement, 0, ref result);
                }
            }

            return result;
        }

        internal static void Query(HtmlNode elem, Statement statement, int level, ref HtmlQueryResult queryResult)
        {
            var node = statement.Nodes[level];

            try
            {
                if (node.IsElement)
                {
                    IEnumerable<HtmlNode> childElements;

                    if (level == 0)
                    {
                        childElements = new List<HtmlNode> { elem as HtmlNode };
                    }
                    else
                    {
                        childElements = (elem as HtmlNode).MatchElementsWithWildcard(node.NodeName);
                    }

                    if (node.QueryConditions != null && childElements.Any())
                    {
                        childElements = childElements.Where(ce =>
                        {
                            bool result = true;
                            try
                            {

                                // 遍历查询条件
                                foreach (var condition in node.QueryConditions)
                                {
                                    // 查询条件的键值与属性表中信息符合则继续检查，如果有一项不符合则结束循环并返回false
                                    if (condition.Key.Equals(HtmlTextNodeMark, StringComparison.OrdinalIgnoreCase) && ce.InnerText.MatchByWildcard(condition.Value))
                                    {
                                        continue;
                                    }
                                    else if (ce.ChildAttributes(condition.Key).FirstOrDefault()?.Value.MatchByWildcard(condition.Value) == true)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        result = false;
                                        break;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                result = false;
                                throw;
                            }
                            return result;

                        });
                    }

                    // 如果当前是最后一级节点
                    if (statement.Nodes.Last() == node)
                    {
                        foreach (var childElement in childElements)
                        {
                            queryResult.AddNode(childElement, node);
                        }
                    }
                    else
                    {
                        foreach (var childElement in childElements)
                        {
                            Query(childElement, statement, level + 1, ref queryResult);
                        }
                    }
                }
                else if (node.IsAttribute)
                {
                    //var attrs = elem.MatchAttributesWithWildcard(node.NodeName);
                    //foreach (var attr in attrs)
                    //{
                    //    queryResult.AddNode(attr, node);
                    //}
                }
            }
            catch (Exception) { }
        }
    }
}
