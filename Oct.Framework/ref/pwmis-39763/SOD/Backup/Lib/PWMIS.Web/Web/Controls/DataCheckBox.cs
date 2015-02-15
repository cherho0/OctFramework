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
 * �޸��ߣ�         ʱ�䣺2013-3-1                
 * �޸�˵���������˿ؼ�
 * ========================================================================
*/
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using PWMIS.Common;
using PWMIS.DataMap;


namespace PWMIS.Web.Controls
{
    /// <summary>
    /// BrainCheckBox ��ժҪ˵����2008.7.6
    /// </summary>
    [System.Drawing.ToolboxBitmap(typeof(ControlIcon), "DataCheckBox.bmp")]
    public class DataCheckBox : CheckBox, IDataCheckBox, IQueryControl
    {
        public DataCheckBox()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }



        #region IBrainControl ��Ա

        #region ��������
        [Category("Data"), Description("�趨��Ӧ������Դ����ʽ��FullClassName,AssemblyName �������Ҫ��ʵ���࣬�������ø����ԡ�")]
        public string DataProvider { get; set; }
        /// <summary>
        /// ָ���Ƿ�������
        /// </summary>
        [Category("Data"), Description("�趨��Ӧ�����ݿ��ֶ��Ƿ��������������Զ����ݲ�ѯ�͸��µ�����")]
        public bool PrimaryKey
        {
            get
            {
                if (ViewState["PrimaryKey"] != null)
                    return (bool)ViewState["PrimaryKey"];
                return false;
            }
            set
            {
                ViewState["PrimaryKey"] = value;
            }
        }

        [Category("Data"), Description("�趨�����ݿ��ֶζ�Ӧ������ֵ")]
        public string Value
        {
            get
            {
                if (ViewState["CheckBoxvalue"] != null)
                    return (string)ViewState["CheckBoxvalue"];
                return "";
            }
            set
            {
                ViewState["CheckBoxvalue"] = value;
            }
        }

        [Category("Data"), Description("�趨�����ݿ��ֶζ�Ӧ��������")]
        public string LinkProperty
        {
            get
            {
                if (ViewState["LinkProperty"] != null)
                    return (string)ViewState["LinkProperty"];
                return "";
            }
            set
            {
                ViewState["LinkProperty"] = value;
            }
        }

        [Category("Data"), Description("�趨�����ݱ��Ӧ�����ݱ���")]
        public string LinkObject
        {
            get
            {
                if (ViewState["LinkObject"] != null)
                    return (string)ViewState["LinkObject"];
                return "";
                // TODO:  ��� BrainDropDownList.LinkObject getter ʵ��
            }
            set
            {
                ViewState["LinkObject"] = value;
                // TODO:  ��� BrainDropDownList.LinkObject setter ʵ��
            }
        }

        #endregion

        #region Ĭ������

        public bool IsValid
        {
            get
            {
                // TODO:  ��� BrainListBox.IsValid getter ʵ��
                return true;
            }
        }
        public System.TypeCode SysTypeCode
        {
            get
            {
                if (ViewState["SysTypeCode"] != null)
                    return (System.TypeCode)ViewState["SysTypeCode"];
                return System.TypeCode.String;
            }
            set
            {
                ViewState["SysTypeCode"] = value;
            }
        }

        public virtual bool Validate()
        {
            // TODO:  ��� BrainListBox.Validate ʵ��
            return true;
        }
        #endregion

        #region ��������

        public bool ReadOnly
        {
            get
            {
                if (!this.Checked)//���δѡ����ô�趨Ϊֻ���������ԡ�
                    return true;
                return !base.Enabled;
            }
            set
            {
                base.Enabled = !value;
            }
        }

        [Category("Data"), Description("�趨�������Ƿ����")]
        public bool IsNull
        {

            get
            {
                // TODO:  ��� BrainTextBox.isClientValidation getter ʵ��
                if (ViewState["isNull"] != null)
                    return (bool)ViewState["isNull"];
                return true;
            }
            set
            {
                // TODO:  ��� BrainTextBox.isClientValidation setter ʵ��
                ViewState["isNull"] = value;

            }
        }
        #endregion

        #region �ӿڷ���

        public void SetValue(object value)
        {
            DataCheckBoxValue dcbv = new DataCheckBoxValue(this);
            dcbv.SetValue(value);
        }

        public object GetValue()
        {
            DataCheckBoxValue dcbv = new DataCheckBoxValue(this);
            return dcbv.GetValue();
        }
        #endregion

        #region IQueryControl ��Ա

        public string CompareSymbol
        {
            get
            {
                if (ViewState["CompareSymbol"] != null)
                    return (string)ViewState["CompareSymbol"];
                return "";
            }
            set
            {
                ViewState["CompareSymbol"] = value;
            }
        }

        public string QueryFormatString
        {
            get
            {
                if (ViewState["QueryFormatString"] != null)
                    return (string)ViewState["QueryFormatString"];
                return "";
            }
            set
            {
                ViewState["QueryFormatString"] = value;
            }
        }

        #endregion


        #endregion
    }
}
