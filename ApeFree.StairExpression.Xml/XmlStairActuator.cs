using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ApeFree.StairExpression.Xml
{
    public class XmlStairActuator
    {
        public const string XmlTextNodeMark = "$TEXT";

        public static XmlQueryResult Query(XDocument doc, string expression)
        {
            var statement = StairExpressionParser.Parse(expression);

            XmlQueryResult result = new XmlQueryResult(statement);
            Query(doc.Root, statement, 0, ref result);

            return result;
        }

        internal static void Query(XObject elem, Statement statement, int level, ref XmlQueryResult queryResult)
        {
            var node = statement.Nodes[level];

            try
            {
                if (node.IsElement)
                {
                    IEnumerable<XElement> childElements;

                    if (level == 0)
                    {
                        childElements = new List<XElement> { elem as XElement };
                    }
                    else
                    {
                        childElements = (elem as XElement).MatchElementsWithWildcard(node.NodeName);
                    }

                    if (node.QueryConditions != null && childElements.Any())
                    {
                        childElements = childElements.Where(ce =>
                        {
                            bool result = true;

                            // 遍历查询条件
                            foreach (var condition in node.QueryConditions)
                            {
                                // 查询条件的键值与属性表中信息符合则继续检查，如果有一项不符合则结束循环并返回false
                                if (condition.Key.Equals(XmlTextNodeMark, StringComparison.OrdinalIgnoreCase) && ce.Value.MatchByWildcard(condition.Value))
                                {
                                    continue;
                                }
                                else if (ce.Attribute(condition.Key)?.Value.MatchByWildcard(condition.Value) == true)
                                {
                                    continue;
                                }
                                else
                                {
                                    result = false;
                                    break;
                                }
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
                    var attrs = (elem as XElement).MatchAttributesWithWildcard(node.NodeName);
                    foreach (var attr in attrs)
                    {
                        queryResult.AddNode(attr, node);
                    }
                }
            }
            catch (Exception) { }
        }
    }
}
