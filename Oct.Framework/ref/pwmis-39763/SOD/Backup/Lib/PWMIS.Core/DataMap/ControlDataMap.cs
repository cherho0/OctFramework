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
 * �޸��ߣ�         ʱ�䣺                
 * �޸�˵����
 * ========================================================================
*/
using System;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Reflection;
using PWMIS.Common;
using System.Collections.Generic;
using PWMIS.DataMap.Entity;

namespace PWMIS.DataMap
{


    /// <summary>
    /// BrainControl ��ժҪ˵����
    /// </summary>
    public class ControlDataMap
    {
        public ControlDataMap()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region �������
        /// <summary>
        /// ��DataTable ��䵽���ݿؼ���
        /// </summary>
        /// <param name="dtTmp">���ݱ�</param>
        /// <param name="controls">���ݿؼ�</param>
        public void FillData(DataTable dtTmp, ICollection controls)
        {
            //����DataTable
            foreach (object control in controls)
            {
                if (control is IDataControl)
                {
                    IDataControl brainControl = control as IDataControl;
                    if (dtTmp.TableName == brainControl.LinkObject
                        && dtTmp.Columns.Contains(brainControl.LinkProperty))
                    {
                        brainControl.SetValue(dtTmp.Rows[0][brainControl.LinkProperty]);
                    }
                }
            }
        }
        /// <summary>
        /// �����ݼ� ��䵽���ݿؼ���
        /// </summary>
        /// <param name="objData">���ݼ�</param>
        /// <param name="controls">���ݿؼ�</param>
        public void FillData(DataSet objData, ICollection controls)
        {
            //2������������DataSet

            foreach (DataTable dtTmp in objData.Tables)
            {
                FillData(dtTmp, controls);
            }
        }
        /// <summary>
        /// ������������䵽���ݿؼ��ϣ�Ҫ��falg=true,������ʹ�������2������
        /// </summary>
        /// <param name="objData">���ݶ��󣬿�����ʵ����</param>
        /// <param name="controls">���ݿؼ�</param>
        /// <param name="flag">���</param>
        public  void FillData(object objData, ICollection controls, bool flag)
        {
            if (!flag)
                return;
            //����ʵ�����
            Type type = objData.GetType();
            EntityBase entity = objData as EntityBase;
            
            //Object obj = type.InvokeMember(null, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, null, null, null);
            string typeName = type.Name;
            foreach (object control in controls)
            {
                if (control is IDataControl)
                {
                    IDataControl brainControl = control as IDataControl;
                    if (brainControl.LinkObject == typeName &&  !string.IsNullOrEmpty(brainControl.LinkProperty)) // obj.GetType().Name
                    {
                        if (entity!=null)
                        {
                            object oValue = entity[brainControl.LinkProperty];
                            if (oValue != null || brainControl.SysTypeCode == TypeCode.String)
                            {
                                brainControl.SetValue(oValue);
                                continue; 
                            }
                        }
                        //����PDF.NETʵ�����ʵ�����ԣ����÷�����ֵ
                        object DataObj = type.InvokeMember(brainControl.LinkProperty, BindingFlags.GetProperty, null, objData, null);
                        if (DataObj == null && (brainControl.SysTypeCode == TypeCode.DateTime))
                        {
                            brainControl.SetValue(DBNull.Value);
                            continue;
                        }
                        brainControl.SetValue(DataObj);
                    }
                }

            }
        }

        /// <summary>
        /// ������ݣ���������Դ�������Զ�����ʹ�ú��ַ�ʽ
        /// </summary>
        /// <param name="objData">Ҫ����ṩ������</param>
        /// <param name="controls">���������ݿؼ�</param>
        public void FillData(object objData, ICollection controls)
        {
            if (objData is DataTable)
                FillData(objData as DataTable, controls);
            else if (objData is DataSet)
                FillData(objData as DataSet, controls);
            else
                FillData(objData, controls, true);

        }

        #endregion

        #region �����ռ�
        public void CollectData(DataTable objData, ICollection controls)
        {
            DataTable dtTmp = objData as DataTable;
            if (dtTmp.Rows.Count == 0)
            {
                DataRow dr = dtTmp.NewRow();
                //
                foreach (DataColumn dataCol in dtTmp.PrimaryKey)
                {
                    dr[dataCol] = System.Guid.NewGuid().ToString("N");// PrimaryKey.GetPrimaryKey();
                }
                dr = GetDateTable(dtTmp.TableName, dr, controls);
                dtTmp.Rows.Add(dr);
            }
            else
            {
                //Ŀǰֻ�������ֻ��һ����¼�����
                DataRow dr = dtTmp.Rows[0];
                dr = GetDateTable(dtTmp.TableName, dr, controls);
            }
        }

        public void CollectData(DataSet objData, ICollection controls)
        {
            //2������������DataSet

            foreach (DataTable dtTmp in (objData as DataSet).Tables)
            {
                CollectData(dtTmp, controls);
            }
        }

        public  void CollectData(object objData, ICollection controls, bool isEntityClass)
        {
            //������Ҫ�ж�
            //if (!isEntityClass)
            //    return;
            //����ʵ�����
            if (objData is EntityBase)
            {
                EntityBase entity = objData as EntityBase;
                string typeName = objData.GetType().Name;
                foreach (object control in controls)
                {
                    if (control is IDataControl)
                    {
                        //�����������
                        IDataControl brainControl = control as IDataControl;
                        if (brainControl.IsValid)
                        {
                            if (string.IsNullOrEmpty( brainControl.LinkProperty ))
                                continue;
                            if (brainControl.LinkObject == typeName || brainControl.LinkObject == entity.TableName)//ʵ�������������ӳ�䷽ʽ���߱�ʽ
                            {
                                object oValue = brainControl.GetValue();
                                //����д���ı����ݣ������£�
                                //if (oValue == DBNull.Value)
                                //{
                                //    entity[brainControl.LinkProperty] = oValue;
                                //    continue;
                                //}
                                //add 2008.7.22
                                if (brainControl.SysTypeCode != TypeCode.String && (oValue == null || oValue.ToString() == ""))
                                    continue;

                                //EditFlag ��̫�� 2006.9.17 ���� System.DBNull.Value ���
                                if (oValue != System.DBNull.Value)
                                {
                                    //֧��ʵ��������Է��ʣ����ݿؼ���д������ edit @ 2014.3.12
                                    try
                                    {
                                        if (brainControl.LinkObject == typeName)//����ӳ�䷿�ĺ�
                                            entity[brainControl.LinkProperty] = oValue;
                                        else // ��ӳ�䷽ʽ
                                            entity.setProperty(brainControl.LinkProperty, oValue);
                                    }
                                    catch (ArgumentException ex)
                                    {
                                        //�����PDF.NET��ʵ�������ԣ�����д��ֵ
                                        string linkProp = brainControl.LinkProperty;
                                        bool flag = false;
                                        foreach (var prop in entity.GetType().GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly  | BindingFlags.Public))
                                        {
                                            if (string.Compare(prop.Name, linkProp, true) == 0 && prop.CanWrite)
                                            {
                                                prop.SetValue(entity, oValue, null);
                                                flag = true;
                                                break;
                                            }
                                        }
                                        if(!flag)
                                        throw new ArgumentOutOfRangeException("�Ѿ������쳣������������δ�ҵ����߲���д��ԭʼ������Ϣ��"+ex.Message);
                                    }
                                }
                            }
                            
                        }
                        else
                        {
                            throw new Exception("�󶨵������ֶ�[" + brainControl.LinkProperty + "]ǰδͨ����������֤��");
                        }


                    }

                }
            }
            else
            {
                Type type = objData.GetType();
                object obj = type.InvokeMember(null, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, null, null, null);
                foreach (object control in controls)
                {
                    if (control is IDataControl)
                    {
                        //�����������
                        IDataControl brainControl = control as IDataControl;
                        if (brainControl.IsValid)
                        {
                            //string cao = obj.GetType().Name;
                            if (brainControl.LinkObject == obj.GetType().Name && brainControl.LinkProperty != "")
                            {
                                object oValue = brainControl.GetValue();
                                //add 2008.7.22
                                if (brainControl.SysTypeCode != TypeCode.String && (oValue == null || oValue.ToString() == ""))
                                    continue;

                                //EditFlag ��̫�� 2006.9.17 ���� System.DBNull.Value ���
                                if (oValue == System.DBNull.Value)
                                {
                                    type.InvokeMember(brainControl.LinkProperty, BindingFlags.SetProperty, Type.DefaultBinder, objData, new object[] { null });
                                    continue;
                                }
                                if (brainControl.SysTypeCode == TypeCode.Empty)
                                    throw new Exception("�ռ����ݿؼ�������ʧ�ܣ�SysTypeCode ���Բ���ΪEmpty ��");
                                type.InvokeMember(brainControl.LinkProperty, BindingFlags.SetProperty, Type.DefaultBinder, objData, new object[] { Convert.ChangeType(oValue, brainControl.SysTypeCode) });
                            }

                        }
                        else
                        {
                            throw new Exception("�󶨵������ֶ�[" + brainControl.LinkProperty + "]ǰδͨ����������֤��");
                        }


                    }

                }
            }
        }

        /// <summary>
        /// ���������ݿؼ����ռ����ݵ�һ���µĶ����У���ʵ�����
        /// </summary>
        /// <typeparam name="T">���صĶ�������</typeparam>
        /// <param name="controls">�ؼ��б�</param>
        /// <returns>һ���µĶ���</returns>
        public T CollectDataToObject<T>(List<IDataControl> controls) where T : class,new() 
        {
            Type type = typeof (T);
            T objData = new T();

            foreach (IDataControl brainControl in controls)
            {
                if (brainControl.IsValid)
                {
                    //string cao = obj.GetType().Name;
                    if (brainControl.LinkObject == type.Name  && brainControl.LinkProperty != "")
                    {
                        object oValue = brainControl.GetValue();
                        //add 2008.7.22
                        if (brainControl.SysTypeCode != TypeCode.String && (oValue == null || oValue.ToString() == ""))
                            continue;

                        //EditFlag ��̫�� 2006.9.17 ���� System.DBNull.Value ���
                        if (oValue == System.DBNull.Value)
                        {
                            type.InvokeMember(brainControl.LinkProperty, BindingFlags.SetProperty, Type.DefaultBinder, objData, new object[] { null });
                            continue;
                        }

                        type.InvokeMember(brainControl.LinkProperty, BindingFlags.SetProperty, Type.DefaultBinder, objData, new object[] { Convert.ChangeType(oValue, brainControl.SysTypeCode) });
                    }

                }
                else
                {
                    throw new Exception("�󶨵������ֶ�[" + brainControl.LinkProperty + "]ǰδͨ����������֤��");
                }
                    
            }
            return objData;
        }

        private DataRow GetDateTable(string TableName, DataRow dr, ICollection Controls)
        {
            foreach (object control in Controls)
            {
                if (control is IDataControl)
                {
                    IDataControl brainControl = control as IDataControl;
                    if (brainControl.LinkObject == TableName)
                    {
                        if (brainControl.Validate() && !brainControl.IsNull)
                        {
                            dr[brainControl.LinkProperty] = brainControl.GetValue();
                        }
                        else
                        {
                            throw new Exception("�������ʹ���" + brainControl.LinkProperty);
                        }
                    }
                }

            }
            return dr;
        }

        public void CollectData(object objData, ICollection controls)
        {
            if (objData is DataTable)
                CollectData(objData as DataTable, controls);
            else if (objData is DataSet)
                CollectData(objData as DataSet, controls);
            else
                CollectData(objData, controls, true);
        }


        #endregion

        /// <summary>
        /// �ռ��ؼ��Ĳ�ѯ�ַ����������Ѿ�Ϊ�ؼ�ָ���˲�ѯ�����ȽϷ��š�
        /// </summary>
        /// <param name="conlObject">��������</param>
        /// <returns>��ѯ�ַ���</returns>
        public static string CollectQueryString(ICollection  Controls)
        {
            string conditin = string.Empty;
            foreach (object  control in Controls)
            {
                if (control is IDataControl && control is IQueryControl)
                {
                    //((IDataControl)control).SetValue("");
                    IDataControl ibC = (IDataControl)control;
                    object Value = ibC.GetValue();
                    //����ؼ�ֵΪ��,��ô����.
                    if (Value == null || Value.ToString() == "")
                        continue;

                    string compareSymbol = ((IQueryControl)ibC).CompareSymbol;
                    string queryFormatString = ((IQueryControl)ibC).QueryFormatString;
                    //Ĭ�ϵıȽϷ���Ϊ ���� "="
                    if (compareSymbol == "") compareSymbol = "=";
                    conditin += " And " + ibC.LinkObject + "." + ibC.LinkProperty + " " + compareSymbol + " ";

                    if (ibC.SysTypeCode == TypeCode.String || ibC.SysTypeCode == TypeCode.DateTime)
                    {
                        string sValue = Value.ToString().Replace("'", "''");
                        if (queryFormatString != "")
                        {
                            sValue = String.Format(queryFormatString, sValue);
                            conditin += sValue.ToString();
                        }
                        else
                        {
                            if (compareSymbol.Trim().ToLower() == "like")
                                conditin += "'%" + sValue + "%'";
                            else
                                conditin += "'" + sValue + "'";
                        }
                    }
                    else
                        conditin += Value.ToString();
                    ////
                    //if (tb.QueryFormatString != "")
                    //{
                    //    conditin += String.Format(tb.QueryFormatString, tb.Text.Replace("'", "''"));
                    //}
                    //else
                    //{
                    //    if (tb.SysTypeCode == TypeCode.String)
                    //        if (tb.CompareSymbol.Trim().ToLower() == "like")
                    //            conditin += "'%" + tb.Text.Replace("'", "''") + "%'";
                    //        else
                    //            conditin += "'" + tb.Text.Replace("'", "''") + "'";
                    //    else
                    //        conditin += Value.ToString();
                    //}
                    //    //�ı�������⴦��
                    //    if (ibC is CBrainTextBox)
                    //    {

                    //        CBrainTextBox tb = control as CBrainTextBox;
                    //        if (tb.QueryFormatString != "")
                    //        {
                    //            conditin += String.Format(tb.QueryFormatString, tb.Text.Replace("'", "''"));
                    //        }
                    //        else
                    //        {
                    //            if (tb.SysTypeCode == TypeCode.String)
                    //                if (tb.CompareSymbol.Trim().ToLower() == "like")
                    //                    conditin += "'%" + tb.Text.Replace("'", "''") + "%'";
                    //                else
                    //                    conditin += "'" + tb.Text.Replace("'", "''") + "'";
                    //            else
                    //                conditin += Value.ToString();
                    //        }
                    //    }
                    //    else if (ibC is WelTop.ControlLibrary.Controls.Calendar)
                    //    {
                    //        //���������ؼ�
                    //        WelTop.ControlLibrary.Controls.Calendar tb = control as WelTop.ControlLibrary.Controls.Calendar;
                    //        if (tb.QueryFormatString != "")
                    //        {
                    //            conditin += String.Format(tb.QueryFormatString, tb.Text);
                    //        }
                    //        else
                    //        {
                    //            if (tb.SysTypeCode == TypeCode.String || tb.SysTypeCode == TypeCode.DateTime)
                    //                conditin += "'" + tb.Text.Replace("'", "''") + "'";
                    //            else
                    //                conditin += Value.ToString();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (ibC.SysTypeCode == TypeCode.String)
                    //            conditin += "'" + Value.ToString().Replace("'", "''") + "'";
                    //        else
                    //            conditin += Value.ToString();
                    //    }
                    //}
                    //else
                    //{
                    //    conditin += CollectQueryString(control);
                    //}
                }
            }
            return conditin;
        }

        

    }

}

