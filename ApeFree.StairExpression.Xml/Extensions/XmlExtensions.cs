using ApeFree.StairExpression.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace System.Xml.Linq
{
    public static class XmlExtension
    {
        /// <summary>
        /// 通过StairExpression语句查询Xml文档中的节点
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static XmlQueryResult FindByStairExpression(this XDocument doc,string expression)
        {
            return XmlStairActuator.Query(doc,expression);
        }
    }
}
