using ApeFree.StairExpression.Html;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.StairExpression.Demo
{
    public partial class HtmlDemoForm : Form
    {
        HtmlAgilityPack.HtmlDocument doc;
        public HtmlDemoForm()
        {
            InitializeComponent();

            doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(tbHtml.Text);
        }

        private void cbExpression_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var result = HtmlStairActuator.Query(doc, $"{cbExpression.Text}*");

                StringBuilder sb = new StringBuilder($"共查询到[{result.Nodes.Count}]条结果：\r\n\r\n");
                int index = 1;
                foreach (var node in result.Nodes)
                {
                    sb.AppendLine($"---------------- 结果{index++} ----------------");
                    sb.AppendLine(node.Node.OuterHtml.Trim());
                    //sb.AppendLine(node.Node.GetPath());
                    //sb.AppendLine(node.Node.XPath);
                    sb.AppendLine();
                }
                tbResult.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                tbResult.Text = ex.Message;
            }
        }

        private void cbExpression_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Decimal || e.KeyCode == Keys.OemPeriod)
            {
                var result = HtmlStairActuator.Query(doc, cbExpression.Text);
                cbExpression.Items.Clear();

                List<string> nodeNames = new List<string>();
                foreach (var node in result.Nodes)
                {
                    nodeNames.AddRange(node.Node.ChildNodes.Where(n => n.NodeType == HtmlNodeType.Element).Select(n => n.Name));
                }

                if (nodeNames.Any())
                {
                    cbExpression.Items.Clear();

                    var pathArray = nodeNames.Distinct().Select(name => $"{cbExpression.Text}.{name}").ToArray();
                    cbExpression.Items.AddRange(pathArray);
                    cbExpression.DroppedDown = true;
                    cbExpression.SelectionStart = cbExpression.Text.Length;

                    e.SuppressKeyPress = true;
                }
                else
                {
                    tbResult.Text = "未找到任何子集。";
                }
                return;
            }

            if(e.KeyCode == Keys.Enter)
            {
                cbExpression.SelectionStart = cbExpression.Text.Length;
            }
        }
    }
}
