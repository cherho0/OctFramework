/*
 * ========================================================================
 * Copyright(c) 2006-2010 PWMIS, All Rights Reserved.
 * Welcom use the PDF.NET (PWMIS Data Process Framework).
 * See more information,Please goto http://www.pwmis.com/sqlmap 
 * ========================================================================
 * ���������
 * 
 * ���ߣ���̫��     ʱ�䣺2008-10-12
 * �汾��V3.0
 * 
 * �޸��ߣ�         ʱ�䣺2010-4-13                
 * �޸�˵����       ������ʱ�鿴ִ�е�SQL���� CommandText
 * ========================================================================
*/
using System;
using System.IO ;
using System.Data ;
using PWMIS.Core;

namespace PWMIS.DataProvider.Data
{
	/// <summary>
	/// ���������־2008.7.18 �����̴߳���,2011.5.9 ����ִ��ʱ���¼
	/// </summary>
	public class CommandLog
	{
		//��־���
        private static  string _dataLogFile;
        /// <summary>
        /// ��ȡ����������־�ļ���·�������Դ�Web���·��
        /// </summary>
        public static string DataLogFile
        {
            get {
                if (string.IsNullOrEmpty(_dataLogFile))
                {
                    _dataLogFile = System.Configuration.ConfigurationSettings.AppSettings["DataLogFile"];
                    if (!string.IsNullOrEmpty(_dataLogFile))
                    {
                        CommonUtil.ReplaceWebRootPath(ref _dataLogFile);
                        string temp = System.Configuration.ConfigurationSettings.AppSettings["SaveCommandLog"];
                        if(temp != null)
                            _saveCommandLog = temp.ToUpper() == "TRUE";
                    }
                }
                return _dataLogFile;
            }
            set { _dataLogFile = value; }
        }

        private static bool _saveCommandLog;
        /// <summary>
        /// �Ƿ��¼��־�ļ�
        /// </summary>
        public  static  bool SaveCommandLog
        {
            get {
                string temp = DataLogFile;//�����ȵ����£��Լ���_saveCommandLog
                return _saveCommandLog;
            }
            set { _saveCommandLog = value; }
        }

        private static long _logExecutedTime = -1;
        /// <summary>
        /// ��Ҫ��¼��ʱ�䣬ֻ�и�ֵ����0���¼���в�ѯ������ֻ��¼���ڸ�ʱ��Ĳ�ѯ����λ���롣
        /// </summary>
        public static long LogExecutedTime
        {
            get {
                if (_logExecutedTime ==-1)
                {
                    string temp = System.Configuration.ConfigurationSettings.AppSettings["LogExecutedTime"];
                    if(string.IsNullOrEmpty (temp ))
                        _logExecutedTime =0;
                    else
                        long.TryParse(temp, out _logExecutedTime);
                }
                return _logExecutedTime;
            }
            set { _logExecutedTime = value; }
        }

		private static CommandLog _Instance;
        private static object lockObj = new object();
        private System.Diagnostics.Stopwatch watch = null;
        

		/// <summary>
		/// ��ȡ��������
		/// </summary>
		public static CommandLog Instance
		{
			get
			{
                if (_Instance == null)
                {
                    lock (lockObj)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new CommandLog();
                        }
                    }
                }
				return _Instance;
			}
		}

		/// <summary>
		/// Ĭ�Ϲ��캯��
		/// </summary>
		public CommandLog()
		{
            
		}

        /// <summary>
        /// �Ƿ���ִ��ʱ���¼
        /// </summary>
        /// <param name="startStopwatch"></param>
        public CommandLog(bool startStopwatch)
        {
            if (startStopwatch)
            {
                watch = new System.Diagnostics.Stopwatch();
                watch.Start();            
            }
        }

        /// <summary>
        /// ���¿�ʼ��¼ִ��ʱ��
        /// </summary>
        public void ReSet()
        {
            if (watch != null)
                watch.Reset();
        }

        /// <summary>
        /// ��ȡ��ǰִ�е�ʵ��SQL���
        /// </summary>
        public string CommandText
        {
            private set;
            get;
        }
       
		/// <summary>
        /// д������־��ִ��ʱ�䣨��������Ļ���
		/// </summary>
        /// <param name="command">�������</param>
		/// <param name="who">���������Դ����</param>
        /// <param name="elapsedMilliseconds">ִ��ʱ��</param>
		public void WriteLog(IDbCommand command,string who,out long elapsedMilliseconds)
		{
            CommandText = command.CommandText;
            elapsedMilliseconds = 0;
            if (SaveCommandLog)
            {
                if (watch != null)
                {
                    elapsedMilliseconds = watch.ElapsedMilliseconds;
                    if ((LogExecutedTime > 0 && elapsedMilliseconds > LogExecutedTime) || LogExecutedTime == 0)
                    {
                        RecordCommandLog(command, who);
                        WriteLog("Execueted Time(ms):" + elapsedMilliseconds + "\r\n", who);
                    }
                }
                else
                {
                    RecordCommandLog(command, who);
                }
            }
		}

		/// <summary>
		///д����־��Ϣ
		/// </summary>
		/// <param name="msg">��Ϣ</param>
		/// <param name="who">������</param>
		public void WriteLog(string msg,string who)
		{
			if(SaveCommandLog)
				WriteLog ("//"+DateTime.Now.ToString ()+ " @"+who+" ��"+msg+"\r\n");
		}

        /// <summary>
        /// д������־����ʹ�� DataLogFile ���ü����ļ���д�ļ�������SaveCommandLog Ӱ�죬���� DataLogFile δ���û�Ϊ�ա�
        /// </summary>
        /// <param name="command">�������</param>
        /// <param name="errmsg">���������Դ����</param>
        public void WriteErrLog(IDbCommand command, string errmsg)
        {
            if (!string.IsNullOrEmpty(DataLogFile))
            {
                errmsg += ":Error";
                RecordCommandLog(command, errmsg);
            }
        }

		/// <summary>
		/// ��ȡ��־�ı�
		/// </summary>
		/// <returns>��־�ı�</returns>
		public string GetLog()
		{
			StreamReader sr= File.OpenText(DataLogFile );
			string text=sr.ReadToEnd ();
			sr.Close();
			return text;
		}

		/// <summary>
		/// ��¼������Ϣ
		/// </summary>
		/// <param name="command">�������</param>
        /// <param name="who">ִ����</param>
		private void RecordCommandLog(IDbCommand command,string who)
		{
			string temp="//"+DateTime.Now.ToString ()+ " @"+who+" ִ�����\r\nSQL=\""+command.CommandText+"\"\r\n//�������ͣ�"+command.CommandType.ToString ();
			if(command.Transaction !=null)
				temp=temp.Replace ("ִ������","ִ������");
			WriteLog(temp);
			if(command.Parameters.Count >0)
			{
				WriteLog("//"+command.Parameters.Count+"�����������");
				for(int i=0;i<command.Parameters.Count ;i++)
				{
					IDataParameter p=(IDataParameter)command.Parameters[i];
					WriteLog ("Parameter[\""+p.ParameterName+"\"]\t=\t\""+Convert.ToString ( p.Value)+"\"  \t\t\t//DbType=" +p.DbType.ToString ());
				}
			}
			

		}

		/// <summary>
		/// д����־
		/// </summary>
		/// <param name="log"></param>
		private void WriteLog(string log)
		{
            //edit at 2012.10.17 �ĳ������첽д����־�ļ�
            using (FileStream fs = new FileStream(DataLogFile, FileMode.Append, FileAccess.Write, FileShare.Write, 1024, FileOptions.Asynchronous))
            {
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(log + "\r\n");
                IAsyncResult writeResult = fs.BeginWrite(buffer, 0, buffer.Length,
                    (asyncResult) =>
                    {
                        FileStream fStream = (FileStream)asyncResult.AsyncState;
                        fStream.EndWrite(asyncResult);
                        //fs.Close();//������˻ᱨ��
                    },
                    fs);
                //fs.EndWrite(writeResult);//���ַ����첽�𲻵�Ч��
                fs.Flush();
                //fs.Close();//���Բ��ü�
            }

            //lock (lockObj)
            //{

            //    //using (StreamWriter sw = File.AppendText(DataLogFile))
            //    //{

            //    //    sw.WriteLine(log);
            //    //    sw.Flush();
            //    //    sw.Close();
            //    //}
            //}
		}
	}
}
