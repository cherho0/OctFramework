using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oct.Tools.Core.Common
{
    /// <summary>
    /// 任务参数类
    /// </summary>
    public class TaskArg
    {
        #region 属性

        /// <summary>
        /// 开始方法
        /// </summary>
        public Func<object> BeginAction
        {
            get;
            set;
        }

        /// <summary>
        /// 结束方法
        /// </summary>
        public Action<object> EndAction
        {
            get;
            set;
        }

        #endregion
    }

    /// <summary>
    /// 任务进度报告参数类
    /// </summary>
    public class TaskReportProgressArg
    {
        #region 属性

        /// <summary>
        /// 索引
        /// </summary>
        public int Index
        {
            get;
            set;
        }

        /// <summary>
        /// 记录数
        /// </summary>
        public int Count
        {
            get;
            set;
        }

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg
        {
            get;
            set;
        }

        #endregion
    }

    /// <summary>
    /// 任务进度报告接口
    /// </summary>
    public interface ITaskReportProgress
    {
        #region 方法

        /// <summary>
        /// 进度报告
        /// </summary>
        /// <param name="arg"></param>
        void ReportProgress(TaskReportProgressArg arg);

        /// <summary>
        /// 任务完成
        /// </summary>
        void TaskComplete();

        #endregion
    }

    /// <summary>
    /// 任务中心
    /// </summary>
    public class TaskCenter : SingleTon<TaskCenter>
    {
        #region 变量

        /// <summary>
        /// 存储任务参数类的FIFO集合
        /// </summary>
        private Queue<TaskArg> _taskArgQueue = null;

        /// <summary>
        /// 实现 任务进度报告接口 的宿主
        /// </summary>
        private ITaskReportProgress _host = null;

        #endregion

        #region 方法

        private void CheckInfo()
        {
            if (this._host == null)
                throw new Exception("没有实现 任务进度报告接口 的宿主");
        }

        /// <summary>
        /// 注册宿主，用于接收任务进度报告
        /// </summary>
        /// <param name="host"></param>
        public void RegistrationHost(ITaskReportProgress host)
        {
            this._taskArgQueue = new Queue<TaskArg>();

            this._host = host;
        }

        /// <summary>
        /// 添加任务参数
        /// </summary>
        /// <param name="taskArg"></param>
        public void AddTask(TaskArg taskArg)
        {
            this._taskArgQueue.Enqueue(taskArg);
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        public void ExecuteTasks()
        {
            this.CheckInfo();

            if (this._taskArgQueue.Count == 0)
                return;

            var index = 0;
            var tasks = new List<Task>();

            foreach (var taskArg in this._taskArgQueue)
            {
                Task task = null;
                Action ac = () =>
                {
                    var result = taskArg.BeginAction();

                    taskArg.EndAction(result);

                    this._host.TaskComplete();
                };

                if (index == 0)
                    task = new Task(() => { ac(); });
                else
                    task = tasks[index - 1].ContinueWith((t) => { ac(); });

                tasks.Add(task);

                index++;
            }

            this._taskArgQueue.Clear();

            tasks[0].Start();
        }

        /// <summary>
        /// 进度报告
        /// </summary>
        /// <param name="arg"></param>
        public void ReportProgress(TaskReportProgressArg arg)
        {
            this.CheckInfo();

            this._host.ReportProgress(arg);
        }

        #endregion
    }
}
