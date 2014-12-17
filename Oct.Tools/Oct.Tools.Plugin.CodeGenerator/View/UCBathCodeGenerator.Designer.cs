namespace Oct.Tools.Plugin.CodeGenerator.View
{
    partial class UCBathCodeGenerator
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
            this.lbSourceTable = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panBtnMove = new System.Windows.Forms.Panel();
            this.btnSignleToRight = new System.Windows.Forms.Button();
            this.btnAllToLeft = new System.Windows.Forms.Button();
            this.btnSignleToLeft = new System.Windows.Forms.Button();
            this.btnAllToRight = new System.Windows.Forms.Button();
            this.lbTargetTable = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labSelectedTempsCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnSelectOutputDirectory = new System.Windows.Forms.Button();
            this.txtOutputDirectory = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labTargetTableCount = new System.Windows.Forms.Label();
            this.txtNameSpace = new System.Windows.Forms.TextBox();
            this.bgw = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.panBtnMove.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbSourceTable
            // 
            this.lbSourceTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbSourceTable.FormattingEnabled = true;
            this.lbSourceTable.ItemHeight = 12;
            this.lbSourceTable.Location = new System.Drawing.Point(3, 20);
            this.lbSourceTable.Name = "lbSourceTable";
            this.lbSourceTable.Size = new System.Drawing.Size(180, 244);
            this.lbSourceTable.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.panBtnMove);
            this.groupBox1.Controls.Add(this.lbTargetTable);
            this.groupBox1.Controls.Add(this.lbSourceTable);
            this.groupBox1.Location = new System.Drawing.Point(0, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(703, 282);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择表";
            // 
            // panBtnMove
            // 
            this.panBtnMove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panBtnMove.Controls.Add(this.btnSignleToRight);
            this.panBtnMove.Controls.Add(this.btnAllToLeft);
            this.panBtnMove.Controls.Add(this.btnSignleToLeft);
            this.panBtnMove.Controls.Add(this.btnAllToRight);
            this.panBtnMove.Location = new System.Drawing.Point(189, 20);
            this.panBtnMove.Name = "panBtnMove";
            this.panBtnMove.Size = new System.Drawing.Size(82, 244);
            this.panBtnMove.TabIndex = 15;
            // 
            // btnSignleToRight
            // 
            this.btnSignleToRight.Location = new System.Drawing.Point(3, 50);
            this.btnSignleToRight.Name = "btnSignleToRight";
            this.btnSignleToRight.Size = new System.Drawing.Size(75, 23);
            this.btnSignleToRight.TabIndex = 13;
            this.btnSignleToRight.Text = ">";
            this.btnSignleToRight.UseVisualStyleBackColor = true;
            this.btnSignleToRight.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // btnAllToLeft
            // 
            this.btnAllToLeft.Location = new System.Drawing.Point(3, 170);
            this.btnAllToLeft.Name = "btnAllToLeft";
            this.btnAllToLeft.Size = new System.Drawing.Size(75, 23);
            this.btnAllToLeft.TabIndex = 12;
            this.btnAllToLeft.Text = "<<";
            this.btnAllToLeft.UseVisualStyleBackColor = true;
            this.btnAllToLeft.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // btnSignleToLeft
            // 
            this.btnSignleToLeft.Location = new System.Drawing.Point(3, 141);
            this.btnSignleToLeft.Name = "btnSignleToLeft";
            this.btnSignleToLeft.Size = new System.Drawing.Size(75, 23);
            this.btnSignleToLeft.TabIndex = 14;
            this.btnSignleToLeft.Text = "<";
            this.btnSignleToLeft.UseVisualStyleBackColor = true;
            this.btnSignleToLeft.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // btnAllToRight
            // 
            this.btnAllToRight.Location = new System.Drawing.Point(3, 79);
            this.btnAllToRight.Name = "btnAllToRight";
            this.btnAllToRight.Size = new System.Drawing.Size(75, 23);
            this.btnAllToRight.TabIndex = 11;
            this.btnAllToRight.Text = ">>";
            this.btnAllToRight.UseVisualStyleBackColor = true;
            this.btnAllToRight.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // lbTargetTable
            // 
            this.lbTargetTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbTargetTable.FormattingEnabled = true;
            this.lbTargetTable.ItemHeight = 12;
            this.lbTargetTable.Location = new System.Drawing.Point(277, 20);
            this.lbTargetTable.Name = "lbTargetTable";
            this.lbTargetTable.Size = new System.Drawing.Size(180, 244);
            this.lbTargetTable.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.labSelectedTempsCount);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnExport);
            this.groupBox2.Controls.Add(this.btnSelectOutputDirectory);
            this.groupBox2.Controls.Add(this.txtOutputDirectory);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.labTargetTableCount);
            this.groupBox2.Controls.Add(this.txtNameSpace);
            this.groupBox2.Location = new System.Drawing.Point(0, 291);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(703, 162);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "参数";
            // 
            // labSelectedTempsCount
            // 
            this.labSelectedTempsCount.AutoSize = true;
            this.labSelectedTempsCount.Location = new System.Drawing.Point(86, 51);
            this.labSelectedTempsCount.Name = "labSelectedTempsCount";
            this.labSelectedTempsCount.Size = new System.Drawing.Size(11, 12);
            this.labSelectedTempsCount.TabIndex = 23;
            this.labSelectedTempsCount.Text = "-";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 22;
            this.label4.Text = "已选择模板";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "命名空间";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(88, 127);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 16;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnSelectOutputDirectory
            // 
            this.btnSelectOutputDirectory.Location = new System.Drawing.Point(382, 98);
            this.btnSelectOutputDirectory.Name = "btnSelectOutputDirectory";
            this.btnSelectOutputDirectory.Size = new System.Drawing.Size(75, 23);
            this.btnSelectOutputDirectory.TabIndex = 15;
            this.btnSelectOutputDirectory.Text = "选择...";
            this.btnSelectOutputDirectory.UseVisualStyleBackColor = true;
            this.btnSelectOutputDirectory.Click += new System.EventHandler(this.btnSelectOutputDirectory_Click);
            // 
            // txtOutputDirectory
            // 
            this.txtOutputDirectory.Location = new System.Drawing.Point(88, 100);
            this.txtOutputDirectory.Name = "txtOutputDirectory";
            this.txtOutputDirectory.ReadOnly = true;
            this.txtOutputDirectory.Size = new System.Drawing.Size(288, 21);
            this.txtOutputDirectory.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "输出目录";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "已选择表";
            // 
            // labTargetTableCount
            // 
            this.labTargetTableCount.AutoSize = true;
            this.labTargetTableCount.Location = new System.Drawing.Point(86, 27);
            this.labTargetTableCount.Name = "labTargetTableCount";
            this.labTargetTableCount.Size = new System.Drawing.Size(11, 12);
            this.labTargetTableCount.TabIndex = 9;
            this.labTargetTableCount.Text = "-";
            // 
            // txtNameSpace
            // 
            this.txtNameSpace.Location = new System.Drawing.Point(88, 73);
            this.txtNameSpace.Name = "txtNameSpace";
            this.txtNameSpace.Size = new System.Drawing.Size(150, 21);
            this.txtNameSpace.TabIndex = 7;
            // 
            // UCBathCodeGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "UCBathCodeGenerator";
            this.Size = new System.Drawing.Size(706, 456);
            this.Load += new System.EventHandler(this.BathCodeGeneratorForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.panBtnMove.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbSourceTable;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtNameSpace;
        private System.Windows.Forms.ListBox lbTargetTable;
        private System.Windows.Forms.Button btnAllToRight;
        private System.Windows.Forms.Button btnAllToLeft;
        private System.Windows.Forms.Button btnSignleToRight;
        private System.Windows.Forms.Button btnSignleToLeft;
        private System.Windows.Forms.Label labTargetTableCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOutputDirectory;
        private System.Windows.Forms.Button btnSelectOutputDirectory;
        private System.Windows.Forms.Button btnExport;
        private System.ComponentModel.BackgroundWorker bgw;
        private System.Windows.Forms.Panel panBtnMove;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labSelectedTempsCount;
        private System.Windows.Forms.Label label4;
    }
}