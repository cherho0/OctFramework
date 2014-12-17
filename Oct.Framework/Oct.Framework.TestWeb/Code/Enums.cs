using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Oct.Framework.TestWeb.Code
{
    /// <summary>
    /// 状态
    /// </summary>
    public enum Status
    {
        //启用
        [Description("启用")]
        Enable = 1,
        //停用
        [Description("停用")]
        Stop = 2,
        //新建
        [Description("新建")]
        New = 3,
        [Description("删除")]
        Del = 4,

    }

    /// <summary>
    /// 操作枚举
    /// </summary>
    public enum OperationEnum
    {
        //浏览
        [Description("浏览")]
        View = 1,
        //更新
        [Description("编辑")]
        Update = 2,
        //删除
        [Description("删除")]
        Delete = 3,
        //增加
        [Description("增加")]
        Add = 4,
        //导出
        [Description("导出")]
        Export = 5,
        //导入
        [Description("导入")]
        Import = 6,
    }

}