namespace ApeFree.StairExpression.Demo
{
    partial class HtmlDemoForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HtmlDemoForm));
            this.tbHtml = new System.Windows.Forms.RichTextBox();
            this.tbResult = new System.Windows.Forms.RichTextBox();
            this.cbExpression = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // tbHtml
            // 
            this.tbHtml.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbHtml.Location = new System.Drawing.Point(0, 20);
            this.tbHtml.Name = "tbHtml";
            this.tbHtml.Size = new System.Drawing.Size(636, 565);
            this.tbHtml.TabIndex = 1;
            this.tbHtml.Text = resources.GetString("tbHtml.Text");
            this.tbHtml.WordWrap = false;
            // 
            // tbResult
            // 
            this.tbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbResult.Location = new System.Drawing.Point(636, 20);
            this.tbResult.Name = "tbResult";
            this.tbResult.Size = new System.Drawing.Size(477, 565);
            this.tbResult.TabIndex = 2;
            this.tbResult.Text = "";
            this.tbResult.WordWrap = false;
            // 
            // cbExpression
            // 
            this.cbExpression.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbExpression.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbExpression.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbExpression.FormattingEnabled = true;
            this.cbExpression.Location = new System.Drawing.Point(0, 0);
            this.cbExpression.Name = "cbExpression";
            this.cbExpression.Size = new System.Drawing.Size(1113, 20);
            this.cbExpression.TabIndex = 0;
            this.cbExpression.TextChanged += new System.EventHandler(this.cbExpression_TextChanged);
            this.cbExpression.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbExpression_KeyDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 585);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.tbHtml);
            this.Controls.Add(this.cbExpression);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox tbHtml;
        private System.Windows.Forms.RichTextBox tbResult;
        private System.Windows.Forms.ComboBox cbExpression;
    }
}

