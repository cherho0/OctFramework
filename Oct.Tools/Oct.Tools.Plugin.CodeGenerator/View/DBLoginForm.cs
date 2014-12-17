using Oct.Tools.Core.Unity;
using Oct.Tools.Plugin.CodeGenerator.Bll;
using Oct.Tools.Plugin.CodeGenerator.Entity;
using System;
using System.Windows.Forms;

namespace Oct.Tools.Plugin.CodeGenerator.View
{
    public partial class DBLoginForm : Form
    {
        #region 属性

        /// <summary>
        /// 数据库信息
        /// </summary>
        public DBInfo DBInfo
        {
            private set;
            get;
        }

        #endregion

        #region 构造函数

        public DBLoginForm()
        {
            this.InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var connectionString = string.Format("User id={0};Password={1};Server={2};database={3};", this.txtAccount.Text, this.txtPassword.Text, this.txtServerName.Text, this.txtDBName.Text);
                var result = TableBll.TextConnection(connectionString);

                if (result)
                {
                    MessageUnity.ShowMsg("登陆成功！");

                    this.DBInfo = new DBInfo();
                    this.DBInfo.Key = string.Format("{0}({1})", this.txtServerName.Text, this.txtDBName.Text);
                    this.DBInfo.ConnectionString = connectionString;

                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                MessageUnity.ShowErrorMsg(string.Format("登陆失败，错误原因：{0}", ex.Message));
            }
        }

        #endregion
    }
}
