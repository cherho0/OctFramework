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
 * �޸��ߣ�         ʱ�䣺2013-3-1                
 * �޸�˵���������˿ؼ�
 * ========================================================================
*/

//**************************************************************************
//	�� �� ����  
//	��	  ;��  TextBox��չ���ռ�����
//	�� �� �ˣ�  �����
//  �������ڣ�  2006.03.09
//	�� �� �ţ�	V1.1
//	�޸ļ�¼��  ��̫�� 2006.04.25 ��Ӷ����ַ�����������֤
//              ��̫�� 20060608 �޸ģ�����ֻ��״̬�²�����ʽ����������ñ������֣�
//              �涨���е�ֻ����ʽ��Ϊ�� CssReadOnly
//              ȡ����ʽ���ƹ��ܣ����û���ʽ���壻
//              ��̫�� 2008.2.15 ���ӡ����������ԣ������Զ����ݸ��µ�����
//                     2009.12.29 ������֤����
//**************************************************************************
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using PWMIS.Common;
using PWMIS.Web.Validate;
using PWMIS.DataMap;


namespace PWMIS.Web.Controls
{
    /// <summary>
    /// �����ı���ؼ�.
    /// </summary>
    [System.Drawing.ToolboxBitmap(typeof(ControlIcon), "DataTextBox.bmp")]
    public class DataTextBox : TextBox, IDataTextBox, IQueryControl
    {
        //private string _BaseText=null ;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public DataTextBox()
        {
            EnsureChildControls();
        }

        #region �ؼ���֤
        /// <summary>
        /// ִ�з�������֤ʱ����������
        /// </summary>
        [Category("�ؼ���֤"), Description("ִ�з�������֤ʱ����������")]
        public ValidationDataType Type
        {
            get
            {
                if (ViewState["ValidationDataType"] != null)
                    return (ValidationDataType)ViewState["ValidationDataType"];
                return ValidationDataType.String;
            }
            set
            {
                ViewState["ValidationDataType"] = value;
                //ȡ����ʽ���ƹ��ܣ����û���ʽ����
                //				switch(value)
                //				{
                //					case ValidationDataType.Currency:
                //					case ValidationDataType.Double:
                //					case ValidationDataType.Integer:
                //						this.Style.Add("TEXT-ALIGN","right");
                //						break;
                //					default:
                //						this.Style.Remove("TEXT-ALIGN");
                //						break;
                //				}
            }
        }

        /// <summary>
        /// �Ƿ�ͨ����������֤Ĭ��Ϊtrue
        /// </summary>
        [Category("�ؼ���֤"), Description("�Ƿ�ͨ����������֤Ĭ��Ϊtrue")]
        public bool IsValid
        {
            get
            {
                if (!isClientValidation)
                {
                    return Validate();
                }
                else
                    return true;
            }
        }


        private EnumMessageType _messageType;

        /// <summary>
        /// ��ʾ��Ϣ������
        /// </summary>
        [Category("�ؼ���֤"), Description("��ʾ��Ϣ������")]
        [TypeConverter(typeof(EnumConverter))]
        public EnumMessageType MessageType
        {
            get
            {
                return _messageType;
            }
            set
            {
                _messageType = value;
            }
        }

        /// <summary>
        /// ��Ҫ��֤�ĳ����������ͣ�����趨Ϊ���ޡ�����ֹͣ�ؼ���֤��
        /// </summary>
        [Category("�ؼ���֤"), Description("��Ҫ��֤�ĳ����������ͣ�����趨Ϊ���ޡ�����ֹͣ�ؼ���֤��")]
        [TypeConverter(typeof(StandardRegexListConvertor))]
        public string OftenType
        {
            get
            {
                if (ViewState["OftenType"] != null)
                    return ViewState["OftenType"].ToString();
                return "��";
            }
            set
            {
                ViewState["OftenType"] = value;
                if (value == "��")
                {
                    this.RegexString = "";
                    this.isClientValidation = false;
                }
                else
                    this.RegexString = "^" + RegexStatic.GetGenerateRegex()[value].ToString() + "$";
            }
        }

        /// <summary>
        /// �趨�ؼ���֤��������ʽ
        /// </summary>
        [Category("�ؼ���֤"), Description("�趨�ؼ���֤��������ʽ")]
        public string RegexString
        {
            get
            {
                if (ViewState["RegexString"] != null)
                    return (string)ViewState["RegexString"];
                return "";
            }
            set
            {
                ViewState["RegexString"] = value;
            }
        }

        /// <summary>
        /// ��֤������
        /// </summary>
        [Category("�ؼ���֤"), Description("��֤������")]
        public string RegexName
        {
            get
            {
                if (ViewState["RegexName"] != null)
                    return (string)ViewState["RegexName"];
                return "";
            }
            set
            {
                ViewState["RegexName"] = value;

            }
        }

        /// <summary>
        /// �趨�Ƿ����ؼ���ʾ��Ϣ
        /// </summary>
        [Category("�ؼ���֤"), Description("�趨�Ƿ����ؼ���ʾ��Ϣ"), DefaultValue(false)]
        public bool OnClickShowInfo
        {

            get
            {
                if (ViewState["OnClickShowInfo"] != null)
                    return (bool)ViewState["OnClickShowInfo"];
                return false;
            }
            set
            {
                ViewState["OnClickShowInfo"] = value;
            }
        }

        /// <summary>
        /// �趨�ű�·��
        /// </summary>
        [Category("Data"), Description("�趨�ű�·��")]
        public string ScriptPath
        {
            get
            {
                if (ViewState["ScriptPath"] != null)
                    return (string)ViewState["ScriptPath"];
                return Root + "System/WebControls/script.js";
            }
            set
            {
                ViewState["ScriptPath"] = value;

            }
        }

        private string Root
        {
            get
            {
                if (!this.DesignMode && System.Web.HttpContext.Current.Request.ApplicationPath != "/")
                {
                    return System.Web.HttpContext.Current.Request.ApplicationPath + "/";
                }
                else
                {
                    return "/";
                }
            }
        }

        #endregion

        #region �������
        /// <summary>
        /// ��֤ʧ�ܳ��ֵ���Ϣ
        /// </summary>
        [Category("�ؼ���֤"), Description("��֤ʧ�ܳ��ֵ���Ϣ")]
        public string ErrorMessage
        {
            get
            {
                if (ViewState["ErrorMessage"] != null)
                    return (string)ViewState["ErrorMessage"];
                return "";
            }
            set
            {
                ViewState["ErrorMessage"] = value;
            }
        }
        #endregion

        #region ��������
        [Category("Data"), Description("�趨��Ӧ������Դ����ʽ��FullClassName,AssemblyName �������Ҫ��ʵ���࣬�������ø����ԡ�")]
        public string DataProvider { get; set; }

        /// <summary>
        /// �趨��Ӧ�����ݿ��ֶ��Ƿ��������������Զ����ݲ�ѯ�͸��µ�����
        /// </summary>
        [Category("Data"), Description("�趨��Ӧ�����ݿ��ֶ��Ƿ��������������Զ����ݲ�ѯ�͸��µ�����"), DefaultValue(false)]
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

        /// <summary>
        /// �趨��Ӧ�������ֶ�����
        /// </summary>
        [Category("Data"), Description("�趨��Ӧ�������ֶ�����")]
        public System.TypeCode SysTypeCode
        {
            get
            {
                if (ViewState["SysTypeCode"] != null)
                    return (System.TypeCode)ViewState["SysTypeCode"];
                return new System.TypeCode();
            }
            set
            {
                ViewState["SysTypeCode"] = value;
            }
        }

        /// <summary>
        /// ���ݳ��ָ�ʽ
        /// </summary>
        [Category("���"), Description("���ݳ��ָ�ʽ")]
        public string DataFormatString
        {
            get
            {
                if (ViewState["DataFormatString"] != null)
                    return (string)ViewState["DataFormatString"];
                return "";
            }
            set
            {
                ViewState["DataFormatString"] = value;
            }
        }
        #endregion

        #region �ű����

        protected override void OnLoad(EventArgs e)
        {
            string rootScript = "\r\n<script  type=\"text/javascript\" language=\"javascript\">var RootSitePath='" + Root + "';</" + "script>\r\n";
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "JS", rootScript + "\r\n<script src=\"" + ScriptPath + "\"></script>\r\n");
            base.OnLoad(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            string messageType = "";
            switch (MessageType)
            {
                case EnumMessageType.��:
                    messageType = "tip";
                    break;
                case EnumMessageType.��ʾ��:
                    messageType = "alert";
                    break;
            }
            //����ؼ���ʾ��Ϣ
            if (this.OnClickShowInfo)
            {
                //�������ʾ��ʽʼ���Բ�����ʾ
                this.Attributes.Add("onclick", "DTControl_SetInputBG(this);ShowMessage('����д" + this.RegexName + "',this,'tip');");
                this.Attributes.Add("onblur", "DTControl_CleInputBG(this);DTControl_Hide_TIPDIV();");
            }


            if (this.IsNull && this.OftenType == "��")
            {
                base.OnPreRender(e);
                return;
            }
            else
            {
                if (!this.IsNull)
                {    //����Ϊ��
                    this.Page.ClientScript.RegisterOnSubmitStatement(this.GetType(), this.UniqueID, "if(document.all." + this.ClientID + ".value==''){\r\n ShowMessage('�����Ϊ��!',document.all." + this.ClientID + ",'" + messageType + "');\r\n document.all." + this.ClientID + ".focus();return false;\r\n}\r\n");
                }


                //switch (this.OftenType)
                //{
                //    case "����":
                //        string path = Root + "System/JS/My97DatePicker/WdatePicker.js";
                //        this.Page.ClientScript.RegisterStartupScript(this.GetType (),"JS_calendar", "\r\n<script language='javascript' type='text/javascript' src='"+path+"'></script>\r\n");
                //        this.Attributes.Add("onfocus", "new WdatePicker(this)");

                //        break;

                //}
                if (this.RegexString != "" && this.OnClickShowInfo && !isClientValidation)
                {
                    string RegexString = this.RegexString.Replace(@"\", @"\\");
                    this.Attributes.Add("onchange", "return isCustomRegular(this,'" + RegexString + "','" + this.RegexName + "û����д��ȷ','" + messageType + "');");
                }


            }
            ////
            if (!isClientValidation)//�ؼ���֤�ű�
            {
                string script = @"javascript:var key= (event.keyCode | event.which);if( !(( key>=48 && key<=57)|| key==46 || key==37 || key==39 || key==45 || key==43 || key==8 || key==46  ) ) {try{ event.returnValue = false;event.preventDefault();}catch(ex){} alert('" + this.ErrorMessage + "');}";
                switch (Type)
                {
                    case ValidationDataType.String:
                        //Convert.ToString(this.Text.Trim());
                        //��̫�� 2006.04.25 ��Ӷ����ַ�����������֤
                        if (this.MaxLength > 0 && this.TextMode == TextBoxMode.MultiLine)
                        {
                            string maxlen = this.MaxLength.ToString();
                            this.Attributes.Add("onblur", "javascript:if(this.value.length>" + maxlen + "){this.select();alert('��������ֲ��ܳ��� " + maxlen + " ���ַ���');MaxLenError=true;}else{MaxLenError=false;}");
                        }
                        break;
                    case ValidationDataType.Integer:
                        this.Attributes.Add("onkeypress", script);
                        break;

                    case ValidationDataType.Currency:
                        this.Attributes.Add("onkeypress", script);
                        break;

                    case ValidationDataType.Date:
                        string path = Root + "System/JS/My97DatePicker/WdatePicker.js";
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "JS_calendar", "\r\n<script language='javascript' type='text/javascript' src='" + path + "'></script>\r\n");
                        this.Attributes.Add("onfocus", "new WdatePicker(this)");
                        break;
                    case ValidationDataType.Double:
                        this.Attributes.Add("onkeypress", script);
                        break;
                }
            }
            else//ִ���Զ�����֤������Զ���ű�
            {
                this.RegisterClientScript();
                if (this.ClientValidationFunctionString != "")
                {
                    this.Attributes.Add("onblur", @"if(strCheck_" + base.ID + "(this.value)==false) {this.value = '';alert('" + this.ErrorMessage + "');}");

                }
            }
            base.OnPreRender(e);
        }


        /// <summary>
        /// ����ű�
        /// </summary>
        protected virtual void RegisterClientScript()
        {
            string versionInfo = System.Reflection.Assembly.GetAssembly(this.GetType()).FullName;
            int start = versionInfo.IndexOf("Version=") + 8;
            int end = versionInfo.IndexOf(",", start);
            versionInfo = versionInfo.Substring(start, end - start);
            string info = @"
<!--
 ********************************************
 * ServerControls " + versionInfo + @"
 ********************************************
-->";

            string ClientFunctionString = @"<SCRIPT language=javascript >
function strCheck_" + base.ID + @"(str)
{
var pattern =/" + ClientValidationFunctionString + @"/;
if(pattern.test(str)) 
{
return true; 
}
  return false;}
</SCRIPT>";

            if (this.ClientValidationFunctionString == "")
            {
                ClientFunctionString = "";
            }
            if (ClientFunctionString != string.Empty)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), base.ID + "_Info", info);
            }


            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), base.ID + "_ValidationFunction", ClientFunctionString);
        }


        #endregion

        #region IBrainControl ��Ա

        #region ��������
        /// <summary>
        /// �趨�����ݿ��ֶζ�Ӧ��������
        /// </summary>
        [Category("Data"), Description("�趨�����ݿ��ֶζ�Ӧ��������")]
        public string LinkProperty
        {
            get
            {
                // TODO:  ��� BrainTextBox.LinkProperty getter ʵ��
                if (ViewState["LinkProperty"] != null)
                    return (string)ViewState["LinkProperty"];
                return "";
            }
            set
            {
                ViewState["LinkProperty"] = value;
                // TODO:  ��� BrainTextBox.LinkProperty setter ʵ��
            }
        }

        /// <summary>
        /// �趨�����ݿ��ֶζ�Ӧ�����ݱ���
        /// </summary>
        [Category("Data"), Description("�趨�����ݿ��ֶζ�Ӧ�����ݱ���")]
        public string LinkObject
        {
            get
            {
                if (ViewState["LinkObject"] != null)
                    return (string)ViewState["LinkObject"];
                return "";
            }
            set
            {
                ViewState["LinkObject"] = value;

            }
        }

        #endregion

        #region ��������

        //�Ƿ�ֻ��
        public override bool ReadOnly
        {
            get
            {
                return base.ReadOnly;
            }
            set
            {
                base.ReadOnly = value;
                if (value)
                    //��̫�� 20060608 �޸ģ�����ֻ��״̬�²�����ʽ����������ñ������֣�����һ�б�ע��
                    //this.BackColor=System.Drawing.Color.FromName("#E0E0E0");
                    this.CssClass = "CssReadOnly";
                else
                    this.BackColor = System.Drawing.Color.Empty;
            }
        }

        //		/// <summary>
        //		/// ��ȡ���������ı�����������˸�ʽ�ַ�������ô��ʾ�ı�Ϊ��ʽ������ı��������ڲ������ʱ����Ȼʹ�ø�ʽ��ǰ���ı�
        //		/// </summary>
        //		public override string Text
        //		{
        //			get
        //			{
        ////				if(_BaseText==null)
        ////				{
        ////					if(ViewState["BaseText"]!=null)
        ////						_BaseText=ViewState["BaseText"].ToString ();
        ////					else
        ////						_BaseText= base.Text;
        ////				}
        ////				return _BaseText;
        //				return base.Text;
        //				
        //			}
        //			set
        //			{
        //				if(DataFormatString!="")
        //					base.Text =String.Format(DataFormatString,value); 
        //				else
        //					base.Text =value;
        ////				_BaseText=value;
        ////				ViewState["BaseText"]=value;
        //			}
        //		}

        #endregion

        #region �ӿڷ���

        //��������
        public void SetValue(object obj)
        {
            DataTextBoxValue dtbv = new DataTextBoxValue(this);
            dtbv.SetValue(obj);
        }

        //�����ռ� 
        //Ϊ��ʱstring ���� ����
        //��������  һ�ɷ���dbnull.value
        //��̫�� 2006.8.23 �޸ģ���������͵�ֵΪ���ַ�������ô����ֵ�޸�Ϊ DBNull.Value ������Ĭ�ϵ� "0"
        public object GetValue()
        {
            DataTextBoxValue dtbv = new DataTextBoxValue(this);
            return dtbv.GetValue();
        }

        #endregion

        #region �ؼ���֤����
        public bool Validate()
        {
            // TODO:  ��� BrainTextBox.Validate ʵ��

            //��������ؼ���֤
            if (!this.isClientValidation)
            {
                if (this.Text.Trim() != "")
                {
                    try
                    {
                        switch (Type)
                        {
                            case ValidationDataType.String:
                                Convert.ToString(this.Text.Trim());
                                break;
                            case ValidationDataType.Integer:
                                Convert.ToInt32(this.Text.Trim());
                                break;

                            case ValidationDataType.Currency:
                                Convert.ToDecimal(this.Text.Trim());
                                break;

                            case ValidationDataType.Date:
                                Convert.ToDateTime(this.Text.Trim());
                                break;
                            case ValidationDataType.Double:
                                Convert.ToDouble(this.Text.Trim());
                                break;

                        }
                        return true;
                        //						if(!this.isNull)//������Ϊ��
                        //						{
                        //							return false;
                        //						}
                        //						else//����Ϊ��
                        //						{
                        //							return true;
                        //						}
                    }
                    catch
                    {
                        return false;//�쳣 �������� ������
                    }
                }
                else
                {
                    //��̫�� 2006.05.8 �޸ģ��������ֵΪ���ڽ����жϣ����沿���ѱ�ע��
                    //return true;
                    if (!this.IsNull)//������Ϊ��
                    {
                        return false;
                    }
                    else//����Ϊ��
                    {
                        return true;
                    }
                }
            }
            else//�������ؼ���֤
            {
                return true;
            }
        }
        #endregion

        #region �Զ�����֤����

        /// <summary>
        /// �趨�Զ�����֤������ʽ
        /// </summary>
        [Category("�Զ�����֤"), Description("�趨�Զ�����֤������ʽ"), DefaultValue("")]
        public string ClientValidationFunctionString
        {
            get
            {
                if (ViewState["ClientValidationFunctionString"] != null)
                    return (string)ViewState["ClientValidationFunctionString"];
                return "";
            }
            set
            {
                ViewState["ClientValidationFunctionString"] = value;
            }
        }

        /// <summary>
        /// �趨�ؼ��Ƿ��ȡ�Զ�����֤(ֹͣ�ؼ���֤)
        /// </summary>
        [Category("�Զ�����֤"), Description("�趨�ؼ��Ƿ��ȡ�Զ�����֤(ֹͣ�ؼ���֤)"), DefaultValue(false)]
        public bool isClientValidation
        {

            get
            {
                if (ViewState["ClientValidation"] != null)
                    return (bool)ViewState["ClientValidation"];
                return false;
            }
            set
            {
                ViewState["ClientValidation"] = value;

            }
        }

        #endregion

        #region �ؼ���֤
        /// <summary>
        /// �ؼ���֤--�趨�ؼ�ֵ�Ƿ����Ϊ��
        /// </summary>
        [Category("�ؼ���֤"), Description("�趨�ؼ�ֵ�Ƿ����Ϊ��"), DefaultValue(true)]
        public bool IsNull
        {

            get
            {
                if (ViewState["isNull"] != null)
                    return (bool)ViewState["isNull"];
                return true;
            }
            set
            {
                ViewState["isNull"] = value;

            }
        }
        #endregion

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
    }
}
