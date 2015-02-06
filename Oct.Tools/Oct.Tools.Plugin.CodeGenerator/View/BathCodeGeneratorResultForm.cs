using Oct.Tools.Core.Unity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace Oct.Tools.Plugin.CodeGenerator.View
{
    public partial class BathCodeGeneratorResultForm : Form
    {
        #region 构造函数

        public BathCodeGeneratorResultForm(string msg, string outputDirectory, List<string> successTableList, List<string> failureTableList)
        {
            this.InitializeComponent();

            this.labMsg.Text = msg;
            this.jhOutputDirectory.Text = outputDirectory;
            this.groupBox1.Text = string.Format("{0}（{1}）", this.groupBox1.Text, successTableList.Count);
            this.groupBox2.Text = string.Format("{0}（{1}）", this.groupBox2.Text, failureTableList.Count);
            this.lbSuccessTable.Items.AddRange(successTableList.ToArray());
            this.lbFailureTable.Items.AddRange(failureTableList.ToArray());
        }

        #endregion

        #region 事件

        private void jhOutputDirectory_Click(object sender, System.EventArgs e)
        {
            try
            {
                Process.Start("explorer.exe", this.jhOutputDirectory.Text);
            }
            catch (Exception ex)
            {
                MessageUnity.ShowErrorMsg(ex.Message);
            }
        }

        #endregion
    }
}
