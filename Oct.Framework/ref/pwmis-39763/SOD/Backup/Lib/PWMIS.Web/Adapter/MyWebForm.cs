/*
 * ========================================================================
 * Copyright(c) 2006-2010 PWMIS, All Rights Reserved.
 * Welcom use the PDF.NET (PWMIS Data Process Framework).
 * See more information,Please goto http://www.pwmis.com/sqlmap 
 * ========================================================================
 * ���������
 * 
 * ���ߣ���̫��     ʱ�䣺2008-10-12
 * �汾��V4.5
 * 
 * �޸��ߣ�         ʱ�䣺2012-11-1                
 * �޸�˵�����ռ����ݵ�ʱ�򣬸Ľ���SQLite��֧��
 * ========================================================================
*/
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using PWMIS.DataProvider.Data;
using PWMIS.Common;
using PWMIS.Web.Controls;
using PWMIS.Common.DataMap;

namespace PWMIS.DataForms.Adapter
{
    ///// <summary>
    ///// �û�ʹ�����ݿؼ����Զ��巽��ί��
    ///// </summary>
    ///// <param name="dataControl"></param>
    //public delegate void UseDataControl(IDataControl dataControl);

    /// <summary>
    /// ����Web�������ݴ����࣬���������ռ������ݳ־û������浽���ݿ⣩�ȷ��������ʹ����������ʹ�ø����м�ľ�̬������
    /// </summary>
    public class MyWebForm : MyDataForm
    {
        //private bool _CheckRowUpdateCount = false;//�Ƿ�����½����Ӱ��������������飬��ô��Ӱ����в�����0���׳�����
        //private CommonDB _dao = null;
        private static MyWebForm _instance = null;
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public MyWebForm()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        

        /// <summary>
        /// ����Web�������ݴ����� �ľ�̬ʵ��
        /// </summary>
        public static MyWebForm Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MyWebForm();
                return _instance;
            }
        }

        /// <summary>
        /// Web ����ؼ�����ӳ����
        /// </summary>
        public static WebControlDataMap DataMap
        {
            get {
                return new WebControlDataMap();
            }
        }

       
        /// <summary>
        /// ��������ϵ����ܿؼ���ֵ
        /// </summary>
        /// <param name="Controls">����ؼ�����</param>
        public static void ClearIBData(ControlCollection Controls)
        {
            //ʹ������ί��
            UseDataControl clearData = delegate(IDataControl dataControl)
            {
                dataControl.SetValue("");
            };
            DoDataControls(Controls, clearData);

        }

        /// <summary>
        /// ʹ���Զ���ķ�������ؼ�������ÿһ�����ܴ������ݿؼ���ʹ�û����ض�������ؼ����ϡ�
        /// </summary>
        /// <param name="controls">���������ؼ��Ŀؼ�����</param>
        /// <param name="useMethod">�Զ���ķ���</param>
        public static void DoDataControls(ControlCollection controls, UseDataControl useMethod)
        {
            foreach (IDataControl item in GetIBControls(controls))
                useMethod(item);
        }

        /// <summary>
        /// �ӿؼ����ϵ�ÿ��Ԫ�ؼ�����Ԫ����Ѱ�����е��������ݿؼ����������ܿؼ��б�
        /// </summary>
        /// <param name="controls">�ؼ����ϣ���ҳ����������</param>
        /// <returns>���ܿؼ��б�</returns>
        public static List<IDataControl> GetIBControls(ControlCollection controls)
        {
            List<IDataControl> IBControls = new List<IDataControl>();
            findIBControls(IBControls, controls);
            return IBControls;
        }

        #region ҳ�������ռ�

        /// <summary>
        /// Ѱ�����ܿؼ�������ŵ������б���
        /// </summary>
        /// <param name="arrIBs">��ſؼ�������</param>
        /// <param name="controls">ҪѰ�ҵ�ԭ�ؼ�����</param>
        private static void findIBControls(List<IDataControl> arrIBs, ControlCollection controls)
        {
            foreach (Control ctr in controls)
            {
                if (ctr is IDataControl)
                {
                    arrIBs.Add(ctr as IDataControl);
                    //���ݿؼ���������Ŀؼ�������Ǹ��Ͽؼ����������������ڲ��������ݿؼ�
                }
                else if (ctr.HasControls())
                {
                    findIBControls(arrIBs, ctr.Controls);
                }
            }

        }


        /// <summary>
        /// ��ȡѡ���ɾ����ѯ��SQL���
        /// </summary>
        /// <param name="Controls">Ҫ�ռ��Ŀؼ�����</param>
        /// <returns> ArrayList �еĳ�ԱΪ IBCommand ���󣬰��������CRUD SQL</returns>
        public static List<IBCommand> GetSelectAndDeleteCommand(ControlCollection Controls)
        {
            List<IDataControl> IBControls = new List<IDataControl>();
            findIBControls(IBControls, Controls);
            return GetSelectAndDeleteCommand(IBControls);
        }

        /// <summary>
        /// �ռ������е����ܿؼ�����ϳ��ܹ�ֱ���������ݿ����͸��� ��ѯ�� SQL���
        /// һ�������п���ͬʱ������������ݲ���
        /// ����ؼ���������������Ϊֻ������ô�ÿؼ���ֵ������µ����ݿ⣻����ÿؼ���������������Ϊ��������ô������佫����������
        /// ��̫�� 2008.1.15
        /// </summary>
        /// <returns>
        /// ArrayList �еĳ�ԱΪ IBCommand ���󣬰��������CRUD SQL
        ///</returns>
        public static List<IBCommand> GetIBFormData(ControlCollection Controls, CommonDB DB)
        {
            List<IDataControl> IBControls = new List<IDataControl>();
            findIBControls(IBControls, Controls);

            return MyDataForm.GetIBFormDataInner(IBControls, DB);
        }

        #endregion

        #region ��������Լ��־û�����


        /// <summary>
        /// �Զ����´�������
        /// </summary>
        /// <param name="Controls">�ؼ�����</param>
        /// <returns></returns>
        public List<IBCommand> AutoUpdateIBFormData(ControlCollection Controls)
        {
            List<IBCommand> ibCommandList = GetIBFormData(Controls, this.DAO);
            AutoUpdateIBFormDataInner(ibCommandList);
            return ibCommandList;
        }

        /// <summary>
        /// �Զ����º���GUID�������ַ��������Ĵ������ݣ�ע�ÿؼ���������PrimaryKey����
        /// </summary>
        /// <param name="Controls">�ؼ�����</param>
        /// <param name="guidControl">Gudi���ַ��������ؼ�</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        public bool AutoUpdateIBFormData(ControlCollection Controls, IDataControl guidControl)
        {
            List<IBCommand> ibCommandList = GetIBFormData(Controls, this.DAO);
            return AutoUpdateIBFormDataInner(ibCommandList, guidControl);
        }

        /// <summary>
        /// �Զ�������ܴ���ؼ�������
        /// </summary>
        /// <param name="Controls">Ҫ���Ĵ���ؼ�����</param>
        public void AutoSelectIBForm(ControlCollection Controls)
        {
            List<IDataControl> IBControls = new List<IDataControl>();
            findIBControls(IBControls, Controls);

            AutoSelectIBFormInner(IBControls);
        }

        /// <summary>
        /// �����ݼ�DataSet������ݵ����ݿؼ����棬DataSet�еı����Ʊ�������ݿؼ���LinkObjectƥ�䣨�����ִ�Сд��
        /// </summary>
        /// <param name="Controls">Ҫ���Ĵ���ؼ�����</param>
        /// <param name="dsSource">�ṩ����Դ�����ݼ�</param>
        public void AutoSelectIBForm(ControlCollection Controls, DataSet dsSource)
        {
            List<IDataControl> IBControls = new List<IDataControl>();
            findIBControls(IBControls, Controls);

            AutoSelectIBFormInner(IBControls, dsSource);    
        }

        /// <summary>
        /// ��ʵ����������ݵ�ҳ��ؼ�
        /// </summary>
        /// <param name="Controls"></param>
        /// <param name="entity"></param>
        public void AutoSelectIBForm(ControlCollection Controls, IEntity entity)
        {
            List<IDataControl> IBControls = new List<IDataControl>();
            findIBControls(IBControls, Controls);

            AutoSelectIBFormInner(IBControls, entity);

        }


        /// <summary>
        /// �Զ�ɾ�����ܴ���ؼ��ĳ־û�����
        /// </summary>
        /// <param name="Controls">Ҫ����Ĵ���ؼ�����</param>
        /// <returns>������Ӱ��ļ�¼����</returns>
        public int AutoDeleteIBForm(ControlCollection Controls)
        {
            List<IDataControl> IBControls = new List<IDataControl>();
            findIBControls(IBControls, Controls);

            return AutoDeleteIBFormInner(IBControls);
        }

        #endregion
    }
}
