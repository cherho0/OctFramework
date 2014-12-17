using Oct.Tools.Core;
using Oct.Tools.Core.Common;
using Oct.Tools.Core.Unity;
using Oct.Tools.Plugin.CodeGenerator.Bll;
using Oct.Tools.Plugin.CodeGenerator.Entity;
using Oct.Tools.Plugin.CodeGenerator.Properties;
using Oct.Tools.Plugin.CodeGenerator.Section;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Oct.Tools.Plugin.CodeGenerator.View
{
    [ExportAttribute(typeof(IEditorPlugIn))]
    public partial class UCMain : UserControl, IEditorPlugIn, IUCCommunication
    {
        #region 变量

        /// <summary>
        /// 数据库信息列表
        /// </summary>
        private List<DBInfo> _dbInfoList;

        #endregion

        #region 属性

        /// <summary>
        /// 数据库信息列表的配置文件路径
        /// </summary>
        private string DBInfoConfigFilePath
        {
            get
            {
                return Path.Combine(Environment.CurrentDirectory, @"Res\Config\DBInfo.xml");
            }
        }

        #endregion

        #region 构造函数

        public UCMain()
        {
            this.InitializeComponent();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 反序列化 保存数据库信息列表
        /// </summary>
        private void SaveDBInfoList()
        {
            string savePath = Path.Combine(Environment.CurrentDirectory, @"Res\Config");

            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);

            using (var writer = new StreamWriter(this.DBInfoConfigFilePath))
            {
                var xs = new XmlSerializer(typeof(List<DBInfo>));

                xs.Serialize(writer, this._dbInfoList);
            }
        }

        /// <summary>
        /// 绑定节点
        /// </summary>
        /// <param name="dbInfo"></param>
        /// <param name="loadTable"></param>
        private void BindNodes(DBInfo dbInfo, bool loadTable)
        {
            if (!this._dbInfoList.Any(d => d.Key == dbInfo.Key))
                this._dbInfoList.Add(dbInfo);

            var parentNode = this.tvDB.Nodes[dbInfo.Key];

            if (parentNode == null)
            {
                parentNode = this.tvDB.Nodes.Insert(0, dbInfo.Key, dbInfo.Key);
                parentNode.ContextMenuStrip = this.parentTreeNodeMenuItem;
                parentNode.ImageIndex = 0;
                parentNode.Tag = dbInfo;
            }

            if (loadTable)
            {
                var taskArg1 = new TaskArg();
                taskArg1.BeginAction = () =>
                {
                    var nodes = new List<TreeNode>();

                    dbInfo.TableList = TableBll.GetDBTableNameList(dbInfo.ConnectionString);

                    for (int i = 0; i < dbInfo.TableList.Count; i++)
                    {
                        var item = dbInfo.TableList[i];

                        var childNode = new TreeNode(item);
                        childNode.ContextMenuStrip = this.chideTreeNodeMenuItem;
                        childNode.ImageIndex = childNode.SelectedImageIndex = 1;

                        nodes.Add(childNode);

                        Thread.Sleep(10);

                        TaskCenter.Instance.ReportProgress(
                            new TaskReportProgressArg
                            {
                                Index = i + 1,
                                Count = dbInfo.TableList.Count,
                                Msg = item
                            });
                    }

                    return nodes;
                };
                taskArg1.EndAction = (result) =>
                {
                    this.Invoke(new MethodInvoker(() =>
                    {
                        parentNode.Nodes.Clear();
                        parentNode.Nodes.AddRange((result as List<TreeNode>).ToArray());

                        parentNode.ExpandAll();
                    }));
                };

                TaskCenter.Instance.AddTask(taskArg1);
                TaskCenter.Instance.ExecuteTasks();
            }
        }

        private void AddUserControl(UserControl uc)
        {
            uc.Dock = DockStyle.Fill;

            this.panUC.Controls.Clear();
            this.panUC.Controls.Add(uc);
        }

        /// <summary>
        /// 获取 控件之间进行通信的接口 实现
        /// </summary>
        /// <returns></returns>
        private IUCCommunication GetUCCommunicationImp()
        {
            if (this.panUC.Controls.Count == 0)
                return this;

            var uc = this.panUC.Controls[0];
            var imp = uc as IUCCommunication;

            return imp ?? this;
        }

        #endregion

        #region 事件

        private void UCMain_Load(object sender, EventArgs e)
        {
            try
            {
                //InitializeControls
                this.tempList.BindTemp();
                this.tempList.SelectedTempEvent = (s1, e1) =>
                {
                    this.GetUCCommunicationImp().SetTempElement(e1);
                };
                this.tempList.SelectedTempsEvent = (s1, e1) =>
                {
                    this.GetUCCommunicationImp().SetTempElements(e1);
                };

                //LoadDBInfoList
                FileInfo file = new FileInfo(this.DBInfoConfigFilePath);

                if (file.Exists)
                {
                    using (var reader = new StreamReader(file.FullName))
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(List<DBInfo>));

                        this._dbInfoList = (List<DBInfo>)xs.Deserialize(reader);
                    }
                }
                else
                    this._dbInfoList = new List<DBInfo>();

                //BindNodes
                if (this._dbInfoList.Count > 0)
                {
                    foreach (var item in this._dbInfoList)
                    {
                        this.BindNodes(item, false);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageUnity.ShowErrorMsg(ex.Message);
            }
        }

        /// <summary>
        /// 菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var menuItem = ((ToolStripMenuItem)sender);

                switch (menuItem.Text)
                {
                    case "连接":
                        var form = new DBLoginForm();
                        var dialog = form.ShowDialog();

                        if (dialog == DialogResult.OK)
                        {
                            this.BindNodes(form.DBInfo, true);

                            this.SaveDBInfoList();
                        }
                        break;

                    case "保存":
                        this.GetUCCommunicationImp().SaveFile();
                        break;

                    case "加载模板":
                        var ofd = new OpenFileDialog() { Filter = "文本模板(*.tt)|*.tt" };
                        var result = ofd.ShowDialog();

                        if (result == DialogResult.OK)
                        {
                            File.Copy(ofd.FileName, Path.Combine(TableBll.TempDirectoryPath, ofd.SafeFileName), true);

                            MessageUnity.ShowMsg("加载成功！");

                            this.tempList.BindTemp();
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageUnity.ShowErrorMsg(ex.Message);
            }
        }

        /// <summary>
        /// 树状控件菜单展开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeNodeMenuItem_Opening(object sender, CancelEventArgs e)
        {
            var dbInfo = (DBInfo)(this.tvDB.SelectedNode.Parent ?? this.tvDB.SelectedNode).Tag;

            this.menuBathCodeGenerator.Enabled = dbInfo.TableList.Count > 0;
        }

        /// <summary>
        /// 树状控件菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeNodeMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var menuItem = ((ToolStripMenuItem)sender);
                var dbInfo = (DBInfo)(this.tvDB.SelectedNode.Parent ?? this.tvDB.SelectedNode).Tag;
                var ucCG = new UCCodeGenerator();
                var ucBCG = new UCBathCodeGenerator(dbInfo);
                var dt = new DataTable();

                switch (menuItem.Text)
                {
                    case "连接":
                        this.BindNodes(dbInfo, true);
                        break;

                    case "注销":
                        var parentNode = this.tvDB.Nodes[dbInfo.Key];

                        if (parentNode != null)
                        {
                            this.tvDB.Nodes.Remove(parentNode);

                            this._dbInfoList.Remove(dbInfo);
                        }

                        this.SaveDBInfoList();
                        break;

                    case "复合查询":
                        this.tempList.IsShowCheckBoxes = false;

                        var queryForm = new CompositeQueryForm(dbInfo);

                        if (queryForm.ShowDialog() == DialogResult.OK)
                        {
                            dt = queryForm.Table;

                            ucCG.BindTableInfo(dt, true, queryForm.Sql);

                            this.AddUserControl(ucCG);
                        }
                        break;

                    case "单表代码生成器":
                        this.tempList.IsShowCheckBoxes = false;

                        dt = TableBll.GetDBTableInfo(dbInfo.ConnectionString, this.tvDB.SelectedNode.Text);

                        ucCG.BindTableInfo(dt, false);

                        this.AddUserControl(ucCG);
                        break;

                    case "批量生成代码":
                        this.tempList.IsShowCheckBoxes = true;

                        this.AddUserControl(ucBCG);
                        break;

                    case "生成权限相关表":
                        if (MessageUnity.ShowQuestionMsg("确定执行该操作吗？") == DialogResult.No)
                            return;

                        var sqls = new string[] { 
                            Resources.A_Common_RoleInfo, 
                            Resources.B_Common_MenuInfo,
                            Resources.C_Common_ActionInfo,
                            Resources.D_Common_RoleAction,
                            Resources.E_Common_User,
                            Resources.F_Common_UserRole,
                            Resources.G_AddColumnDescription };
                        var result = TableBll.ExecuteSqls(dbInfo.ConnectionString, sqls);

                        if (result)
                        {
                            MessageUnity.ShowMsg("生成成功！");

                            this.BindNodes(dbInfo, true);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageUnity.ShowErrorMsg(ex.Message);
            }
        }

        #endregion

        #region IEditorPlugIn 成员

        public int Order
        {
            get
            {
                return 1;
            }
        }

        public string ToolImgUrl
        {
            get
            {
                return Path.Combine(Environment.CurrentDirectory, @"Res\Img\editor.png");
            }
        }

        public string ToolTipText
        {
            get
            {
                return "代码生成";
            }
        }

        public UserControl PlugInUC
        {
            get
            {
                return this;
            }
        }

        #endregion

        #region IUCCommunication 成员

        public void SaveFile()
        {

        }

        public void SetTempElement(TempElement temp)
        {

        }

        public void SetTempElements(List<TempElement> temps)
        {

        }

        #endregion
    }
}
