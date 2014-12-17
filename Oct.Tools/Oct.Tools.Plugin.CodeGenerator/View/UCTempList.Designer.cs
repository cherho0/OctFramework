namespace Oct.Tools.Plugin.CodeGenerator.View
{
    partial class UCTempList
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCTempList));
            this.tvTemp = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // tvTemp
            // 
            this.tvTemp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvTemp.ImageIndex = 0;
            this.tvTemp.ImageList = this.imageList1;
            this.tvTemp.Location = new System.Drawing.Point(0, 0);
            this.tvTemp.Name = "tvTemp";
            this.tvTemp.SelectedImageIndex = 0;
            this.tvTemp.Size = new System.Drawing.Size(170, 248);
            this.tvTemp.TabIndex = 0;
            this.tvTemp.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvTemp_AfterCheck);
            this.tvTemp.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvTemp_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder.png");
            this.imageList1.Images.SetKeyName(1, "file.png");
            // 
            // UCTempList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvTemp);
            this.Name = "UCTempList";
            this.Size = new System.Drawing.Size(170, 248);
            this.Load += new System.EventHandler(this.UCTempList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvTemp;
        private System.Windows.Forms.ImageList imageList1;
    }
}
