namespace Oct.Tools.Plugin.CodeGenerator.View
{
    partial class BathCodeGeneratorResultForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labMsg = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbSuccessTable = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbFailureTable = new System.Windows.Forms.ListBox();
            this.jhOutputDirectory = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labMsg
            // 
            this.labMsg.AutoSize = true;
            this.labMsg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labMsg.ForeColor = System.Drawing.Color.Red;
            this.labMsg.Location = new System.Drawing.Point(9, 9);
            this.labMsg.Name = "labMsg";
            this.labMsg.Size = new System.Drawing.Size(17, 16);
            this.labMsg.TabIndex = 0;
            this.labMsg.Tag = "";
            this.labMsg.Text = "-";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.lbSuccessTable);
            this.groupBox1.Location = new System.Drawing.Point(12, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(249, 360);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "成功";
            // 
            // lbSuccessTable
            // 
            this.lbSuccessTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSuccessTable.FormattingEnabled = true;
            this.lbSuccessTable.ItemHeight = 12;
            this.lbSuccessTable.Location = new System.Drawing.Point(6, 20);
            this.lbSuccessTable.Name = "lbSuccessTable";
            this.lbSuccessTable.Size = new System.Drawing.Size(229, 316);
            this.lbSuccessTable.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lbFailureTable);
            this.groupBox2.Location = new System.Drawing.Point(267, 68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(249, 360);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "失败";
            // 
            // lbFailureTable
            // 
            this.lbFailureTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFailureTable.FormattingEnabled = true;
            this.lbFailureTable.ItemHeight = 12;
            this.lbFailureTable.Location = new System.Drawing.Point(6, 20);
            this.lbFailureTable.Name = "lbFailureTable";
            this.lbFailureTable.Size = new System.Drawing.Size(237, 316);
            this.lbFailureTable.TabIndex = 3;
            // 
            // jhOutputDirectory
            // 
            this.jhOutputDirectory.AutoSize = true;
            this.jhOutputDirectory.Location = new System.Drawing.Point(10, 35);
            this.jhOutputDirectory.Name = "jhOutputDirectory";
            this.jhOutputDirectory.Size = new System.Drawing.Size(11, 12);
            this.jhOutputDirectory.TabIndex = 3;
            this.jhOutputDirectory.TabStop = true;
            this.jhOutputDirectory.Text = "-";
            this.jhOutputDirectory.Click += new System.EventHandler(this.jhOutputDirectory_Click);
            // 
            // BathCodeGeneratorResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 440);
            this.Controls.Add(this.jhOutputDirectory);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labMsg);
            this.Location = new System.Drawing.Point(417, 328);
            this.MinimumSize = new System.Drawing.Size(544, 478);
            this.Name = "BathCodeGeneratorResult";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "批量生成代码结果";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labMsg;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lbSuccessTable;
        private System.Windows.Forms.ListBox lbFailureTable;
        private System.Windows.Forms.LinkLabel jhOutputDirectory;
    }
}