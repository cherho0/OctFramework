using Oct.Tools.Core.Unity;
using Oct.Tools.Plugin.CodeGenerator.Bll;
using Oct.Tools.Plugin.CodeGenerator.Section;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Oct.Tools.Plugin.CodeGenerator.View
{
    public partial class UCTempList : UserControl
    {
        #region 变量

        public EventHandler<TempElement> SelectedTempEvent;

        public EventHandler<List<TempElement>> SelectedTempsEvent;

        #endregion

        #region 属性

        /// <summary>
        /// 是否显示复选框
        /// </summary>
        [Category("自定义")]
        [Description("是否显示复选框")]
        public bool IsShowCheckBoxes
        {
            set
            {
                this.tvTemp.CheckBoxes = value;

                this.tvTemp.ExpandAll();
            }
        }

        /// <summary>
        /// 获取选中的模板集合
        /// </summary>
        public List<TempElement> SelectedTemps
        {
            get
            {
                var nodeList = new List<TreeNode>();

                this.CollectionNodesByChecked(this.tvTemp.Nodes[0], nodeList);

                var tempList = nodeList.Select(d => SectionMgr.GetTempElement(d.Text)).ToList();

                return tempList;
            }
        }

        #endregion

        #region 构造函数

        public UCTempList()
        {
            this.InitializeComponent();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 绑定模板文件
        /// </summary>
        public void BindTemp()
        {
            //Section
            SectionMgr.LoadSection();

            this.tvTemp.Nodes.Clear();

            var dir = new DirectoryInfo(TableBll.TempDirectoryPath);
            var dirNode = this.tvTemp.Nodes.Insert(0, "模板");

            this.BindChildNode(dir, dirNode);

            dirNode.ExpandAll();
        }

        private void BindChildNode(DirectoryInfo dir, TreeNode dirNode)
        {
            dirNode.ImageIndex = 0;

            //遍历文件
            foreach (var file in dir.GetFiles("*.tt"))
            {
                var temp = SectionMgr.GetTempElement(file.Name);

                if (temp == null)
                    continue;

                temp.Path = file.FullName;

                var fileNode = new TreeNode(temp.Name);
                fileNode.Tag = temp;
                fileNode.ImageIndex = fileNode.SelectedImageIndex = 1;

                dirNode.Nodes.Add(fileNode);
            }

            //遍历子文件夹
            foreach (var childDir in dir.GetDirectories())
            {
                var childDirNode = new TreeNode(childDir.Name);

                dirNode.Nodes.Add(childDirNode);

                this.BindChildNode(childDir, childDirNode);
            }
        }

        /// <summary>
        /// 设置父节点的复选框的选中状态
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="isChecked"></param>
        private void SetParentNodeCheckedState(TreeNode treeNode, bool isChecked)
        {
            treeNode.Checked = isChecked;

            var parentNode = treeNode.Parent;

            if (parentNode == null)
                return;

            parentNode.Checked = isChecked;

            if (parentNode.Parent != null)
                this.SetParentNodeCheckedState(parentNode.Parent, isChecked);
        }

        /// <summary>
        /// 设置子节点的复选框的选中状态
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="isChecked"></param>
        private void SetChildNodeCheckedState(TreeNode treeNode, bool isChecked)
        {
            var childNodes = treeNode.Nodes;

            if (childNodes.Count > 0)
            {
                foreach (TreeNode childNode in childNodes)
                {
                    childNode.Checked = isChecked;

                    this.SetChildNodeCheckedState(childNode, isChecked);
                }
            }
        }

        /// <summary>
        /// 收集选中的节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodes"></param>
        private void CollectionNodesByChecked(TreeNode node, List<TreeNode> nodes)
        {
            foreach (TreeNode childNode in node.Nodes)
            {
                if (childNode.Checked && childNode.Tag != null)
                    nodes.Add(childNode);

                this.CollectionNodesByChecked(childNode, nodes);
            }
        }

        #endregion

        #region 事件

        private void UCTempList_Load(object sender, EventArgs e)
        {

        }

        private void tvTemp_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag == null)
                return;

            var temp = SectionMgr.GetTempElement(e.Node.Text);

            if (temp == null)
                MessageUnity.ShowWarningMsg(string.Format("{0} 获取不到对应的配置信息！", e.Node.Text));
            else
            {
                if (this.SelectedTempEvent != null)
                    this.SelectedTempEvent(sender, temp);
            }
        }

        private void tvTemp_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse)
            {
                if (e.Node.Checked)
                    this.SetChildNodeCheckedState(e.Node, true);
                else
                {
                    this.SetChildNodeCheckedState(e.Node, false);

                    if (e.Node.Parent != null)
                        this.SetParentNodeCheckedState(e.Node.Parent, false);
                }

                if (this.SelectedTempsEvent != null)
                    this.SelectedTempsEvent(sender, this.SelectedTemps);
            }
        }

        #endregion
    }
}
