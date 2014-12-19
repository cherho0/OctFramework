namespace Oct.Framework.MQClientDemo
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtip = new System.Windows.Forms.TextBox();
            this.txtport = new System.Windows.Forms.TextBox();
            this.btnConn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtTop = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtSeat = new System.Windows.Forms.TextBox();
            this.btnAsync = new System.Windows.Forms.Button();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtip
            // 
            this.txtip.Location = new System.Drawing.Point(26, 33);
            this.txtip.Name = "txtip";
            this.txtip.Size = new System.Drawing.Size(86, 21);
            this.txtip.TabIndex = 0;
            this.txtip.Text = "127.0.0.1";
            this.txtip.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtport
            // 
            this.txtport.Location = new System.Drawing.Point(139, 33);
            this.txtport.Name = "txtport";
            this.txtport.Size = new System.Drawing.Size(76, 21);
            this.txtport.TabIndex = 1;
            this.txtport.Text = "5555";
            // 
            // btnConn
            // 
            this.btnConn.Location = new System.Drawing.Point(305, 82);
            this.btnConn.Name = "btnConn";
            this.btnConn.Size = new System.Drawing.Size(75, 23);
            this.btnConn.TabIndex = 2;
            this.btnConn.Text = "连接为订阅者";
            this.btnConn.UseVisualStyleBackColor = true;
            this.btnConn.Click += new System.EventHandler(this.btnConn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(122, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = ":";
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(26, 111);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(259, 83);
            this.txtContent.TabIndex = 4;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(302, 111);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 83);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "响应模式发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtTop
            // 
            this.txtTop.Location = new System.Drawing.Point(98, 84);
            this.txtTop.Name = "txtTop";
            this.txtTop.Size = new System.Drawing.Size(187, 21);
            this.txtTop.TabIndex = 6;
            this.txtTop.Text = "5555";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "主题:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(302, 267);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "占座";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(121, 269);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(161, 21);
            this.txtName.TabIndex = 9;
            this.txtName.Text = "张三";
            // 
            // txtSeat
            // 
            this.txtSeat.Location = new System.Drawing.Point(26, 269);
            this.txtSeat.Name = "txtSeat";
            this.txtSeat.Size = new System.Drawing.Size(86, 21);
            this.txtSeat.TabIndex = 10;
            this.txtSeat.Text = "1";
            // 
            // btnAsync
            // 
            this.btnAsync.Location = new System.Drawing.Point(302, 310);
            this.btnAsync.Name = "btnAsync";
            this.btnAsync.Size = new System.Drawing.Size(75, 67);
            this.btnAsync.TabIndex = 11;
            this.btnAsync.Text = "并发占座";
            this.btnAsync.UseVisualStyleBackColor = true;
            this.btnAsync.Click += new System.EventHandler(this.btnAsync_Click);
            // 
            // txtCount
            // 
            this.txtCount.Location = new System.Drawing.Point(181, 341);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(100, 21);
            this.txtCount.TabIndex = 12;
            this.txtCount.Text = "5";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(96, 344);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "并发数量：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 398);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.btnAsync);
            this.Controls.Add(this.txtSeat);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTop);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConn);
            this.Controls.Add(this.txtport);
            this.Controls.Add(this.txtip);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtip;
        private System.Windows.Forms.TextBox txtport;
        private System.Windows.Forms.Button btnConn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtTop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtSeat;
        private System.Windows.Forms.Button btnAsync;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Label label3;
    }
}

