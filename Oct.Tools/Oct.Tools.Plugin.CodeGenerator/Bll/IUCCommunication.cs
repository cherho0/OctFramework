using Oct.Tools.Plugin.CodeGenerator.Section;
using System.Collections.Generic;

namespace Oct.Tools.Plugin.CodeGenerator.Bll
{
    /// <summary>
    /// 控件之间进行通信的接口
    /// </summary>
    public interface IUCCommunication
    {
        /// <summary>
        /// 保存文件
        /// </summary>
        void SaveFile();

        /// <summary>
        /// 设置模板节点
        /// </summary>
        /// <param name="temp"></param>
        void SetTempElement(TempElement temp);

        /// <summary>
        /// 设置模板节点集合
        /// </summary>
        /// <param name="temps"></param>
        void SetTempElements(List<TempElement> temps);
    }
}
