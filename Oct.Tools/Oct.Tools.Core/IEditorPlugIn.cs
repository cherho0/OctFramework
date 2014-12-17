using System.Windows.Forms;

namespace Oct.Tools.Core
{
    public interface IEditorPlugIn
    {
        /// <summary>
        /// 排序字段
        /// </summary>
        int Order { get; }

        /// <summary>
        /// 工具栏按钮的图标路径
        /// </summary>
        string ToolImgUrl { get; }

        /// <summary>
        /// 工具栏按钮的提示文本
        /// </summary>
        string ToolTipText { get; }

        /// <summary>
        /// 插件用户控件
        /// </summary>
        UserControl PlugInUC { get; }
    }
}
