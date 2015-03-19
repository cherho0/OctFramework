namespace CodeGen
{
    partial class MainFrom
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
            this.button1 = new System.Windows.Forms.Button();
            this.txtPro = new System.Windows.Forms.TextBox();
            this.txtCls = new System.Windows.Forms.TextBox();
            this.btnCC = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(346, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "创建项目";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtPro
            // 
            this.txtPro.Location = new System.Drawing.Point(124, 63);
            this.txtPro.Name = "txtPro";
            this.txtPro.Size = new System.Drawing.Size(204, 21);
            this.txtPro.TabIndex = 1;
            // 
            // txtCls
            // 
            this.txtCls.Location = new System.Drawing.Point(124, 115);
            this.txtCls.Name = "txtCls";
            this.txtCls.Size = new System.Drawing.Size(204, 21);
            this.txtCls.TabIndex = 3;
            // 
            // btnCC
            // 
            this.btnCC.Location = new System.Drawing.Point(346, 113);
            this.btnCC.Name = "btnCC";
            this.btnCC.Size = new System.Drawing.Size(75, 23);
            this.btnCC.TabIndex = 2;
            this.btnCC.Text = "创建类";
            this.btnCC.UseVisualStyleBackColor = true;
            this.btnCC.Click += new System.EventHandler(this.btnCC_Click);
            // 
            // MainFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 460);
            this.Controls.Add(this.txtCls);
            this.Controls.Add(this.btnCC);
            this.Controls.Add(this.txtPro);
            this.Controls.Add(this.button1);
            this.Name = "MainFrom";
            this.Text = "代码生成";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtPro;
        private System.Windows.Forms.TextBox txtCls;
        private System.Windows.Forms.Button btnCC;
    }
}