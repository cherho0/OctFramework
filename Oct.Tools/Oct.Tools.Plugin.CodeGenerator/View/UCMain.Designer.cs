namespace Oct.Tools.Plugin.CodeGenerator.View
{
    partial class UCMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCMain));
            this.menu1 = new System.Windows.Forms.MenuStrip();
            this.数据库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.连接ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载模板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parentTreeNodeMenuItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.连接ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.注销ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.复合查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuBathCodeGenerator = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.生成权限相关表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.tvDB = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panUC = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chideTreeNodeMenuItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tempList = new Oct.Tools.Plugin.CodeGenerator.View.UCTempList();
            this.menu1.SuspendLayout();
            this.parentTreeNodeMenuItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.chideTreeNodeMenuItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu1
            // 
            this.menu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.数据库ToolStripMenuItem,
            this.文件ToolStripMenuItem});
            this.menu1.Location = new System.Drawing.Point(0, 0);
            this.menu1.Name = "menu1";
            this.menu1.Size = new System.Drawing.Size(890, 25);
            this.menu1.TabIndex = 1;
            this.menu1.Text = "menuStrip1";
            // 
            // 数据库ToolStripMenuItem
            // 
            this.数据库ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.连接ToolStripMenuItem});
            this.数据库ToolStripMenuItem.Name = "数据库ToolStripMenuItem";
            this.数据库ToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
            this.数据库ToolStripMenuItem.Text = "数据库";
            // 
            // 连接ToolStripMenuItem
            // 
            this.连接ToolStripMenuItem.Name = "连接ToolStripMenuItem";
            this.连接ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.连接ToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.连接ToolStripMenuItem.Text = "连接";
            this.连接ToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.保存ToolStripMenuItem,
            this.加载模板ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("保存ToolStripMenuItem.Image")));
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.保存ToolStripMenuItem.Text = "保存";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // 加载模板ToolStripMenuItem
            // 
            this.加载模板ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("加载模板ToolStripMenuItem.Image")));
            this.加载模板ToolStripMenuItem.Name = "加载模板ToolStripMenuItem";
            this.加载模板ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.加载模板ToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.加载模板ToolStripMenuItem.Text = "加载模板";
            this.加载模板ToolStripMenuItem.Visible = false;
            this.加载模板ToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // parentTreeNodeMenuItem
            // 
            this.parentTreeNodeMenuItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.连接ToolStripMenuItem1,
            this.注销ToolStripMenuItem1,
            this.toolStripSeparator1,
            this.复合查询ToolStripMenuItem,
            this.toolStripSeparator3,
            this.menuBathCodeGenerator,
            this.toolStripSeparator2,
            this.生成权限相关表ToolStripMenuItem});
            this.parentTreeNodeMenuItem.Name = "treeviewMenu1";
            this.parentTreeNodeMenuItem.Size = new System.Drawing.Size(161, 132);
            this.parentTreeNodeMenuItem.Opening += new System.ComponentModel.CancelEventHandler(this.TreeNodeMenuItem_Opening);
            // 
            // 连接ToolStripMenuItem1
            // 
            this.连接ToolStripMenuItem1.Name = "连接ToolStripMenuItem1";
            this.连接ToolStripMenuItem1.Size = new System.Drawing.Size(160, 22);
            this.连接ToolStripMenuItem1.Text = "连接";
            this.连接ToolStripMenuItem1.Click += new System.EventHandler(this.TreeNodeMenuItem_Click);
            // 
            // 注销ToolStripMenuItem1
            // 
            this.注销ToolStripMenuItem1.Name = "注销ToolStripMenuItem1";
            this.注销ToolStripMenuItem1.Size = new System.Drawing.Size(160, 22);
            this.注销ToolStripMenuItem1.Text = "注销";
            this.注销ToolStripMenuItem1.Click += new System.EventHandler(this.TreeNodeMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // 复合查询ToolStripMenuItem
            // 
            this.复合查询ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("复合查询ToolStripMenuItem.Image")));
            this.复合查询ToolStripMenuItem.Name = "复合查询ToolStripMenuItem";
            this.复合查询ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.复合查询ToolStripMenuItem.Text = "复合查询";
            this.复合查询ToolStripMenuItem.Click += new System.EventHandler(this.TreeNodeMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(157, 6);
            // 
            // menuBathCodeGenerator
            // 
            this.menuBathCodeGenerator.Name = "menuBathCodeGenerator";
            this.menuBathCodeGenerator.Size = new System.Drawing.Size(160, 22);
            this.menuBathCodeGenerator.Text = "批量生成代码";
            this.menuBathCodeGenerator.Click += new System.EventHandler(this.TreeNodeMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(157, 6);
            // 
            // 生成权限相关表ToolStripMenuItem
            // 
            this.生成权限相关表ToolStripMenuItem.Name = "生成权限相关表ToolStripMenuItem";
            this.生成权限相关表ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.生成权限相关表ToolStripMenuItem.Text = "生成权限相关表";
            this.生成权限相关表ToolStripMenuItem.Click += new System.EventHandler(this.TreeNodeMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.tvDB);
            this.splitContainer1.Panel1MinSize = 150;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panUC);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2MinSize = 300;
            this.splitContainer1.Size = new System.Drawing.Size(890, 475);
            this.splitContainer1.SplitterDistance = 166;
            this.splitContainer1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "数据库视图";
            // 
            // tvDB
            // 
            this.tvDB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvDB.ImageIndex = 0;
            this.tvDB.ImageList = this.imageList1;
            this.tvDB.Location = new System.Drawing.Point(5, 21);
            this.tvDB.Name = "tvDB";
            this.tvDB.SelectedImageIndex = 0;
            this.tvDB.Size = new System.Drawing.Size(158, 454);
            this.tvDB.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "database.png");
            this.imageList1.Images.SetKeyName(1, "table.png");
            this.imageList1.Images.SetKeyName(2, "save.png");
            this.imageList1.Images.SetKeyName(3, "add.png");
            // 
            // panUC
            // 
            this.panUC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panUC.Location = new System.Drawing.Point(3, 6);
            this.panUC.Name = "panUC";
            this.panUC.Size = new System.Drawing.Size(502, 469);
            this.panUC.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.tempList);
            this.groupBox2.Location = new System.Drawing.Point(511, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(204, 469);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "模板";
            // 
            // chideTreeNodeMenuItem
            // 
            this.chideTreeNodeMenuItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.chideTreeNodeMenuItem.Name = "treeviewMenu1";
            this.chideTreeNodeMenuItem.Size = new System.Drawing.Size(161, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItem1.Text = "单表代码生成器";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.TreeNodeMenuItem_Click);
            // 
            // tempList
            // 
            this.tempList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tempList.Location = new System.Drawing.Point(6, 15);
            this.tempList.Name = "tempList";
            this.tempList.Size = new System.Drawing.Size(192, 452);
            this.tempList.TabIndex = 8;
            // 
            // UCMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menu1);
            this.Name = "UCMain";
            this.Size = new System.Drawing.Size(890, 500);
            this.Load += new System.EventHandler(this.UCMain_Load);
            this.menu1.ResumeLayout(false);
            this.menu1.PerformLayout();
            this.parentTreeNodeMenuItem.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.chideTreeNodeMenuItem.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu1;
        private System.Windows.Forms.ToolStripMenuItem 数据库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 连接ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip parentTreeNodeMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvDB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem 连接ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 注销ToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip chideTreeNodeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 加载模板ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 复合查询ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private UCTempList tempList;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 生成权限相关表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuBathCodeGenerator;
        private System.Windows.Forms.Panel panUC;
    }
}
