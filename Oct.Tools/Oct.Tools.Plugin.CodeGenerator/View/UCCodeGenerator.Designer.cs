namespace Oct.Tools.Plugin.CodeGenerator.View
{
    partial class UCCodeGenerator
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.gbOpt = new System.Windows.Forms.GroupBox();
            this.tabControlOpt = new System.Windows.Forms.TabControl();
            this.tabPageTable = new System.Windows.Forms.TabPage();
            this.dgvTableInfo = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labFileName = new System.Windows.Forms.Label();
            this.labFileNameExtend = new System.Windows.Forms.Label();
            this.labClassNameExtend = new System.Windows.Forms.Label();
            this.btnCodeGenerator = new System.Windows.Forms.Button();
            this.txtClassName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNameSpace = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPageCode = new System.Windows.Forms.TabPage();
            this.txtCode = new System.Windows.Forms.RichTextBox();
            this.gbOpt.SuspendLayout();
            this.tabControlOpt.SuspendLayout();
            this.tabPageTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableInfo)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabPageCode.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOpt
            // 
            this.gbOpt.Controls.Add(this.tabControlOpt);
            this.gbOpt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbOpt.Location = new System.Drawing.Point(0, 0);
            this.gbOpt.Name = "gbOpt";
            this.gbOpt.Size = new System.Drawing.Size(938, 540);
            this.gbOpt.TabIndex = 13;
            this.gbOpt.TabStop = false;
            this.gbOpt.Text = "代码生成器";
            // 
            // tabControlOpt
            // 
            this.tabControlOpt.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControlOpt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlOpt.Controls.Add(this.tabPageTable);
            this.tabControlOpt.Controls.Add(this.tabPageCode);
            this.tabControlOpt.Location = new System.Drawing.Point(6, 20);
            this.tabControlOpt.Multiline = true;
            this.tabControlOpt.Name = "tabControlOpt";
            this.tabControlOpt.SelectedIndex = 0;
            this.tabControlOpt.Size = new System.Drawing.Size(926, 520);
            this.tabControlOpt.TabIndex = 12;
            // 
            // tabPageTable
            // 
            this.tabPageTable.Controls.Add(this.dgvTableInfo);
            this.tabPageTable.Controls.Add(this.groupBox1);
            this.tabPageTable.Location = new System.Drawing.Point(4, 4);
            this.tabPageTable.Name = "tabPageTable";
            this.tabPageTable.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTable.Size = new System.Drawing.Size(918, 494);
            this.tabPageTable.TabIndex = 0;
            this.tabPageTable.Text = "生成设置";
            this.tabPageTable.UseVisualStyleBackColor = true;
            // 
            // dgvTableInfo
            // 
            this.dgvTableInfo.AllowUserToAddRows = false;
            this.dgvTableInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTableInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTableInfo.Location = new System.Drawing.Point(6, 6);
            this.dgvTableInfo.Name = "dgvTableInfo";
            this.dgvTableInfo.RowTemplate.Height = 23;
            this.dgvTableInfo.Size = new System.Drawing.Size(906, 366);
            this.dgvTableInfo.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.labFileName);
            this.groupBox1.Controls.Add(this.labFileNameExtend);
            this.groupBox1.Controls.Add(this.labClassNameExtend);
            this.groupBox1.Controls.Add(this.btnCodeGenerator);
            this.groupBox1.Controls.Add(this.txtClassName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtNameSpace);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(6, 378);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(906, 110);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "参数";
            // 
            // labFileName
            // 
            this.labFileName.AutoSize = true;
            this.labFileName.Location = new System.Drawing.Point(146, 84);
            this.labFileName.Name = "labFileName";
            this.labFileName.Size = new System.Drawing.Size(0, 12);
            this.labFileName.TabIndex = 13;
            // 
            // labFileNameExtend
            // 
            this.labFileNameExtend.AutoSize = true;
            this.labFileNameExtend.Location = new System.Drawing.Point(152, 84);
            this.labFileNameExtend.Name = "labFileNameExtend";
            this.labFileNameExtend.Size = new System.Drawing.Size(0, 12);
            this.labFileNameExtend.TabIndex = 12;
            // 
            // labClassNameExtend
            // 
            this.labClassNameExtend.AutoSize = true;
            this.labClassNameExtend.Location = new System.Drawing.Point(221, 55);
            this.labClassNameExtend.Name = "labClassNameExtend";
            this.labClassNameExtend.Size = new System.Drawing.Size(0, 12);
            this.labClassNameExtend.TabIndex = 11;
            // 
            // btnCodeGenerator
            // 
            this.btnCodeGenerator.Location = new System.Drawing.Point(65, 79);
            this.btnCodeGenerator.Name = "btnCodeGenerator";
            this.btnCodeGenerator.Size = new System.Drawing.Size(75, 23);
            this.btnCodeGenerator.TabIndex = 10;
            this.btnCodeGenerator.Text = "生成代码";
            this.btnCodeGenerator.UseVisualStyleBackColor = true;
            this.btnCodeGenerator.Click += new System.EventHandler(this.btnCodeGenerator_Click);
            // 
            // txtClassName
            // 
            this.txtClassName.Location = new System.Drawing.Point(65, 52);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(150, 21);
            this.txtClassName.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "类名";
            // 
            // txtNameSpace
            // 
            this.txtNameSpace.Location = new System.Drawing.Point(65, 25);
            this.txtNameSpace.Name = "txtNameSpace";
            this.txtNameSpace.Size = new System.Drawing.Size(150, 21);
            this.txtNameSpace.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "命名空间";
            // 
            // tabPageCode
            // 
            this.tabPageCode.Controls.Add(this.txtCode);
            this.tabPageCode.Location = new System.Drawing.Point(4, 4);
            this.tabPageCode.Name = "tabPageCode";
            this.tabPageCode.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCode.Size = new System.Drawing.Size(918, 494);
            this.tabPageCode.TabIndex = 1;
            this.tabPageCode.Text = "代码查看";
            this.tabPageCode.UseVisualStyleBackColor = true;
            // 
            // txtCode
            // 
            this.txtCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCode.Location = new System.Drawing.Point(3, 3);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(912, 488);
            this.txtCode.TabIndex = 0;
            this.txtCode.Text = "";
            // 
            // UCCodeGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbOpt);
            this.Name = "UCCodeGenerator";
            this.Size = new System.Drawing.Size(938, 540);
            this.Load += new System.EventHandler(this.UCCodeGenerator_Load);
            this.gbOpt.ResumeLayout(false);
            this.tabControlOpt.ResumeLayout(false);
            this.tabPageTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableInfo)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPageCode.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOpt;
        private System.Windows.Forms.TabControl tabControlOpt;
        private System.Windows.Forms.TabPage tabPageTable;
        private System.Windows.Forms.DataGridView dgvTableInfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labFileName;
        private System.Windows.Forms.Label labFileNameExtend;
        private System.Windows.Forms.Label labClassNameExtend;
        private System.Windows.Forms.Button btnCodeGenerator;
        private System.Windows.Forms.TextBox txtClassName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNameSpace;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabPageCode;
        private System.Windows.Forms.RichTextBox txtCode;
    }
}
