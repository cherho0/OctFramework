using Oct.Tools.Core.Unity;
using Oct.Tools.Plugin.CodeGenerator.Bll;
using Oct.Tools.Plugin.CodeGenerator.Entity;
using Oct.Tools.Plugin.CodeGenerator.Section;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Oct.Tools.Plugin.CodeGenerator.View
{
    public partial class UCCodeGenerator : UserControl, IUCCommunication
    {
        #region 变量

        private CodeBaseInfo _codeBaseInfo = null;
        private TempElement _selectedTemp = null;

        #endregion

        #region 构造函数

        public UCCodeGenerator()
        {
            this.InitializeComponent();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 绑定表信息
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="isCompositeQueryDt"></param>
        /// <param name="sql"></param>
        public void BindTableInfo(DataTable dt, bool isCompositeQueryDt, string sql = "")
        {
            this.tabControlOpt.SelectedTab = this.tabPageTable;
            this.dgvTableInfo.DataSource = dt;

            if (isCompositeQueryDt)
            {
                this._codeBaseInfo = TableBll.GetCodeBaseInfoByDBTable(dt, true);
                this._codeBaseInfo.Sql = sql;

                this.txtClassName.Text = string.Empty;
            }
            else
            {
                this._codeBaseInfo = TableBll.GetCodeBaseInfoByDBTable(dt);

                this.txtClassName.Text = TableBll.GetClassName(dt.TableName);
            }
        }

        #endregion

        #region 事件

        private void UCCodeGenerator_Load(object sender, EventArgs e)
        {
            //InitializeControls
            this.txtNameSpace.Text = TableBll.DefaultNameSpace;
            this.labFileName.Visible = this.labFileNameExtend.Visible = false;
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCodeGenerator_Click(object sender, EventArgs e)
        {
            try
            {
                var msg = this.txtCode.Text = string.Empty;

                if (this._selectedTemp == null)
                    msg = "请选择模板！";
                else if (string.IsNullOrEmpty(this.txtNameSpace.Text))
                {
                    this.txtNameSpace.Focus();

                    msg = "请输入命名空间！";
                }
                else if (string.IsNullOrEmpty(this.txtClassName.Text) && string.IsNullOrEmpty(this.labClassNameExtend.Text))
                {
                    this.txtClassName.Focus();

                    msg = "请输入类名！";
                }

                if (!string.IsNullOrEmpty(msg))
                {
                    MessageUnity.ShowWarningMsg(msg);

                    return;
                }

                this._codeBaseInfo.NameSpace = this.txtNameSpace.Text;
                this._codeBaseInfo.ClassName = this.txtClassName.Text;
                this._codeBaseInfo.ClassNameExtend = this.labClassNameExtend.Text;

                var output = TableBll.CodeGenerator(this._codeBaseInfo, this._selectedTemp.Path);

                if (output.StartsWith("error："))
                    MessageUnity.ShowErrorMsg(output);

                this.txtCode.Text = output;

                this.tabControlOpt.SelectedTab = this.tabPageCode;
            }
            catch (Exception ex)
            {
                MessageUnity.ShowErrorMsg(ex.Message);
            }
        }

        #endregion

        #region IUCCommunication 成员

        public void SaveFile()
        {
            string code = this.txtCode.Text;

            if (string.IsNullOrEmpty(code))
                return;

            var fileName = string.IsNullOrEmpty(this.labFileName.Text) ? this.txtClassName.Text : this.labFileName.Text;
            var sfd = new SaveFileDialog()
            {
                InitialDirectory = Application.ExecutablePath,
                FileName = string.Format("{0}{1}{2}", fileName, this.labClassNameExtend.Text, this.labFileNameExtend.Text),
                Filter = "All Files(*.*)|*.*",
                OverwritePrompt = true
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, code, Encoding.UTF8);

                MessageUnity.ShowMsg("保存成功！");
            }
        }

        public void SetTempElement(TempElement temp)
        {
            this._selectedTemp = temp;

            this.labClassNameExtend.Text = this._selectedTemp.ClassNameExtend;
            this.labFileName.Text = this._selectedTemp.FileName;
            this.labFileNameExtend.Text = this._selectedTemp.FileNameExtend;
        }

        public void SetTempElements(List<TempElement> temps)
        {

        }

        #endregion
    }
}
