using Oct.Tools.Core;
using Oct.Tools.Core.Common;
using Oct.Tools.Core.Ioc;
using Oct.Tools.Core.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Oct.Tools.Host
{
    public partial class MainForm : Form, ITaskReportProgress
    {
        #region 变量

        private IEnumerable<IEditorPlugIn> _editorPlugInList { get; set; }

        #endregion

        #region 构造函数

        public MainForm()
        {
            this.InitializeComponent();

            this.IsMdiContainer = true;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载插件
        /// </summary>
        private void LoadEditorPlugInList()
        {
            this._editorPlugInList = ContainerHelper.GetExport<IEditorPlugIn>(Path.Combine(Environment.CurrentDirectory, ConfigurationManager.AppSettings["AppStartPath"]));

            this.labPlugInCount.Text = string.Format("加载插件数：{0}", this._editorPlugInList.Count());

            foreach (var item in this._editorPlugInList.OrderBy(d => d.Order))
            {
                var btn = new ToolStripButton()
                {
                    AutoToolTip = true,
                    //Name = string.Format("btn{0}", item.Order),
                    Size = new Size(0x24, 0x24),
                    Text = item.ToolTipText,
                    ToolTipText = item.ToolTipText
                };

                if (string.IsNullOrEmpty(item.ToolImgUrl))
                    btn.DisplayStyle = ToolStripItemDisplayStyle.Text;
                else
                {
                    try
                    {
                        var img = Image.FromFile(item.ToolImgUrl);

                        btn.DisplayStyle = ToolStripItemDisplayStyle.Image;
                        btn.Image = img;
                        btn.ImageTransparentColor = Color.Magenta;
                    }
                    catch
                    {

                    }
                }

                btn.Click += (s2, e2) =>
                {
                    bool isHave = false;

                    foreach (TabPage tab in this.tabControl1.TabPages)
                    {
                        if (tab.Text == item.ToolTipText)
                        {
                            isHave = true;

                            this.tabControl1.SelectedTab = tab;

                            break;
                        }
                    }

                    if (!isHave)
                    {
                        var uc = item.PlugInUC;
                        uc.Dock = DockStyle.Fill;

                        var tab = new TabPage(item.ToolTipText);
                        tab.Controls.Add(uc);

                        this.tabControl1.SelectedTab = tab;
                        this.tabControl1.TabPages.Add(tab);
                    }
                };

                this.toolBar1.Items.Add(btn);
            }
        }

        #endregion

        #region 事件

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                //InitializeControls
                TaskCenter.Instance.RegistrationHost(this);

                this.LoadEditorPlugInList();

                this.progressBar1.Visible = false;
            }
            catch (Exception ex)
            {
                MessageUnity.ShowErrorMsg(ex.Message);
            }
        }

        #endregion

        #region ITaskReportProgress 成员

        public void ReportProgress(TaskReportProgressArg arg)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                //将完成进度数据传给进度条
                this.progressBar1.Maximum = arg.Count;
                this.progressBar1.Value = arg.Index;
                this.progressBar1.Visible = true;

                this.labReportProgressMsg.Text = arg.Msg;
            }));
        }

        public void TaskComplete()
        {
            this.Invoke(new MethodInvoker(() =>
            {
                this.progressBar1.Visible = false;
                this.labReportProgressMsg.Text = string.Empty;
            }));
        }

        #endregion
    }
}
