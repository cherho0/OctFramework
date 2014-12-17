using System.Windows.Forms;

namespace Oct.Tools.Core.Unity
{
    public class MessageUnity
    {
        #region 方法

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static DialogResult ShowMsg(string info)
        {
            return MessageBox.Show(info, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 显示错误信息
        /// </summary>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static DialogResult ShowErrorMsg(string errorMsg)
        {
            return MessageBox.Show(errorMsg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 显示提问信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static DialogResult ShowQuestionMsg(string info)
        {
            return MessageBox.Show(info, "确定", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        /// <summary>
        /// 显示警告信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static DialogResult ShowWarningMsg(string info)
        {
            return MessageBox.Show(info, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        #endregion
    }
}
