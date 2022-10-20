using ApeFree.StairExpression.Html;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.StairExpression.Demo
{
    public partial class Form1 : Form
    {
        HtmlAgilityPack.HtmlDocument doc;
        public Form1()
        {
            InitializeComponent();

            doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(tbHtml.Text);
        }

        private void tbExpression_TextChanged(object sender, EventArgs e)
        {
            tbResult.Clear();

            try
            {


                var result = HtmlStairActuator.Query(doc, tbExpression.Text);

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
    }
}
