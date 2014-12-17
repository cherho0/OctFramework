using System.Collections.Generic;
using System.Windows.Forms;

namespace Oct.Tools.Plugin.CodeGenerator.View
{
    public partial class BathCodeGeneratorResult : Form
    {
        #region 构造函数

        public BathCodeGeneratorResult(string msg, List<string> successTableList, List<string> failureTableList)
        {
            this.InitializeComponent();

            this.labMsg.Text = msg;
            this.groupBox1.Text = string.Format("{0}（{1}）", this.groupBox1.Text, successTableList.Count);
            this.groupBox2.Text = string.Format("{0}（{1}）", this.groupBox2.Text, failureTableList.Count);
            this.lbSuccessTable.Items.AddRange(successTableList.ToArray());
            this.lbFailureTable.Items.AddRange(failureTableList.ToArray());
        }

        #endregion
    }
}
