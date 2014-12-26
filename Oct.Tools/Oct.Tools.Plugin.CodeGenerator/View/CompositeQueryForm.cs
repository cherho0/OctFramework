using Oct.Tools.Core.Unity;
using Oct.Tools.Plugin.CodeGenerator.Bll;
using Oct.Tools.Plugin.CodeGenerator.Entity;
using System;
using System.Data;
using System.Windows.Forms;

namespace Oct.Tools.Plugin.CodeGenerator.View
{
    public partial class CompositeQueryForm : Form
    {
        #region 变量

        private DBInfo _dbInfo = null;
        private DataColumnCollection _columns = null;

        #endregion

        #region 属性

        public DataTable Table
        {
            get;
            private set;
        }

        public string Sql
        {
            get;
            private set;
        }

        #endregion

        #region 构造函数

        public CompositeQueryForm(DBInfo dbInfo)
        {
            this.InitializeComponent();

            this._dbInfo = dbInfo;
        }

        #endregion

        #region 事件

        private void CompositeQueryForm_Load(object sender, EventArgs e)
        {
            this.ddlPk.Enabled = this.btnOk.Enabled = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtSql.Text))
                {
                    MessageUnity.ShowWarningMsg("请输入查询语句！");

                    this.txtSql.Focus();

                    return;
                }

                this._columns = TableBll.GetDataByQuery(this._dbInfo.ConnectionString, this.txtSql.Text).Columns;

                this.ddlPk.Items.Clear();

                this.ddlPk.Items.Add(string.Empty);

                for (int i = 0; i < this._columns.Count; i++)
                {
                    this.ddlPk.Items.Add(this._columns[i].ColumnName);
                }

                this.ddlPk.Enabled = this.btnOk.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageUnity.ShowErrorMsg(ex.Message);
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {


                this.Table = new DataTable();
                this.Table.Columns.Add("序号");
                this.Table.Columns.Add("列名");
                this.Table.Columns.Add("数据类型");
                this.Table.Columns.Add("主键");

                for (int i = 0; i < this._columns.Count; i++)
                {
                    this.Table.Rows.Add(new object[] { 
                        i + 1, 
                        this._columns[i].ColumnName, 
                        ConvertUnity.MapCsharpType(this._columns[i].DataType), 
                        this._columns[i].ColumnName == this.ddlPk.Text });
                }

                this.Sql = this.txtSql.Text;

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageUnity.ShowErrorMsg(ex.Message);
            }
        }

        #endregion
    }
}
