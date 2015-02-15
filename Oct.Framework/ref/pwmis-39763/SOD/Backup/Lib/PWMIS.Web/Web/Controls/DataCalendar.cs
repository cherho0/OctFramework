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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using PWMIS.Common;

namespace PWMIS.Web.Controls
{
	/// <summary>
	/// Calendar ��ժҪ˵����2008.7.26
	/// </summary>
    [System.Drawing.ToolboxBitmap(typeof(ControlIcon), "DataCalendar.bmp")]
    [ToolboxData("<{0}:DataCalendar runat=server></{0}:DataCalendar>")]
    public class DataCalendar : System.Web.UI.WebControls.WebControl, IDataControl, IQueryControl,INamingContainer
	{

		private TextBox objDateBox;

//		private RegularExpressionValidator REV;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
		public DataCalendar()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ��д����

		/// <summary>
		/// ���˿ؼ����ָ�ָ�������������
		/// </summary>
		/// <param name="output"> Ҫд������ HTML ��д�� </param>onclick="selectDate('txtEndDate')" alt="����ѡ������" src="in4_05.gif" align="absBottom"
		protected override void Render(HtmlTextWriter writer)
		{
			this.EnsureChildControls();
			objDateBox.RenderControl(writer);
			
			writer.AddAttribute(HtmlTextWriterAttribute.Align,"absBottom");
			writer.AddAttribute(HtmlTextWriterAttribute.Src,this.ScriptPath+"in4_05.gif");
			writer.AddAttribute("alt","����ѡ������");
			writer.AddAttribute(HtmlTextWriterAttribute.Onclick,"selectDate('"+objDateBox.ClientID+"')");
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
			
		}

        /// <summary>
        /// �ؼ����ӿؼ�����
        /// </summary>
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		protected override void CreateChildControls()
		{
			Controls.Clear();

			objDateBox = new TextBox();

			objDateBox.ID = base.ID + "_DateBox";
			//Ĭ���ı���ֻ�����ڶ�̬�����ӿؼ���ʱ���޷����ѡ���ֵ������Ĭ�ϸ�Ϊ��ֻ��
            //
			//objDateBox.ReadOnly=true;
			//objDateBox.Text = " ";
			
			objDateBox.EnableViewState=true;
			//�ı��������������ڸ�ʽ��֤ ��̫�� 2008.2.20
			objDateBox.Attributes.Add ("onblur","if(this.value!='')TestDate(this);");
			Controls.Add(objDateBox);
//			RegularExpressionValidator REV = new RegularExpressionValidator();
//			REV.ErrorMessage = "���ˣ�";
//			REV.ControlToValidate = objDateBox.ID;
//			REV.ValidationExpression = @"^[_a-z0-9]+@([_a-z0-9]+\.)+[a-z0-9]{2,3}$";
//			Controls.Add(REV);

		}

		protected virtual void RegisterClientScript() 
		{
			string versionInfo = System.Reflection.Assembly.GetAssembly(this.GetType()).FullName;
			int start = versionInfo.IndexOf("Version=")+8;
			int end = versionInfo.IndexOf(",",start);
			versionInfo = versionInfo.Substring(start,end-start);
			string info = @"
<!--
 ********************************************
 * Calendar " + versionInfo + @"
 * by myj,dth
 ********************************************
-->";
			//����ע�����ű�
			//Page.RegisterClientScriptBlock(base.ID + "_Info",info);
			Page.ClientScript.RegisterClientScriptBlock(this.GetType(),versionInfo + "_Info",info);

			string selDate = @"
<script language=""JavaScript"">
function TestDate(obj)
{
	//�ؼ�ֻ����������֤
	if(obj.readOnly) return true;
	var ex=/(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0[13578]|1[02])-(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)-(0[1-9]|[12][0-9]|30))|(02-(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))-02-29)/;
	var r=ex.test(obj.value);
	if(r)
	{
		var sd=obj.value.split(""-"")[2];
		if(sd.length!=2)
			r=false;
		else
			if(sd>""31"")
				r=false;
		
	}
	if(!r) {
		alert(""���ڻ������ڸ�ʽ��Ч����ȷ��ʽ��YYYY-MM-DD��"");
		obj.focus();
		obj.select();
	}
	return r;
}


    function selectDate(objname)
{
//move window to screen center,by dth,2006.5.23
var height=265;
var width=310;
var top= (screen.height/2 - height/2) ;
var left= (screen.width/2 - width/2);
//��̫�� 2008.4.26 ��Ϊģʽ����Ӧ�����ڵı�ǩ�����ʽ
var objwin=window.showModalDialog('"+this.ScriptPath+@"calendar.htm',window,'dialogHeight='+height+'px;dialogWidth='+width+'px;status=no;toolbar=no;menubar=no;location=no;');
//alert(objwin);
//alert(objname);
document.getElementById(objname).value=objwin;
}
</script>
";
			
			//����ע�����ű�
			//Page.RegisterClientScriptBlock(base.ID + "_Info",selDate);
			Page.ClientScript.RegisterClientScriptBlock(this.GetType (),versionInfo + "_script",selDate);
		}

		protected override void OnPreRender( EventArgs e ) 
		{
			this.RegisterClientScript();
			base.OnPreRender(e);
		}

//
//		[Category("Data"),Description("�趨��Ӧ�������ֶ�����")]
//		public System.TypeCode SysTypeCode
//		{
//			get
//			{
//				return TypeCode.DateTime;
//			}
//		}

		#endregion

		#region IBrainControl ��Ա
        [Category("Data"), Description("�趨��Ӧ������Դ����ʽ��FullClassName,AssemblyName �������Ҫ��ʵ���࣬�������ø����ԡ�")]
        public string DataProvider { get; set; }

        [Description("�Ƿ�ֻ��������Ϊ��"),
        Category("Behavior"),
        DefaultValue(true)
        ]
		public bool IsNull
		{
			get
			{
				// TODO:  ��� Calendar.isNull getter ʵ��
				return true;
			}
			set
			{
				// TODO:  ��� Calendar.isNull setter ʵ��
			}
		}

		#region ��������
		[Category("Data"),Description("�趨��Ӧ�����ݿ��ֶ��Ƿ��������������Զ����ݲ�ѯ�͸��µ�����")]
		public bool PrimaryKey
		{
			get
			{
				if(ViewState["PrimaryKey"]!=null)
					return (bool)ViewState["PrimaryKey"];
				return false;
			}
			set
			{
				ViewState["PrimaryKey"]=value;
			}
		}

		/// <summary>
		/// �趨�����ݿ��ֶζ�Ӧ��������
		/// </summary>
		[Category("Data"),Description("�趨�����ݿ��ֶζ�Ӧ��������")]
		public string LinkProperty
		{
			get
			{
				// TODO:  ��� BrainTextBox.LinkProperty getter ʵ��
				if(ViewState["LinkProperty"]!=null)
					return (string)ViewState["LinkProperty"];
				return "";
			}
			set
			{
				ViewState["LinkProperty"]=value;
				// TODO:  ��� BrainTextBox.LinkProperty setter ʵ��
			}
		}

		/// <summary>
		/// �趨�����ݿ��ֶζ�Ӧ�����ݱ���
		/// </summary>
		[Category("Data"),Description("�趨�����ݿ��ֶζ�Ӧ�����ݱ���")]
		public string LinkObject
		{
			get
			{
				if(ViewState["LinkObject"]!=null)
					return (string)ViewState["LinkObject"];
				return "";
			}
			set
			{
				ViewState["LinkObject"]=value;
				
			}
		}

		#endregion

		[Description("�Ƿ�ֻ�����ڿͻ���ѡ������ֵ"),Bindable(true), 
		Category("Behavior"), 
		DefaultValue(true)
		]
		public bool ReadOnly
		{
			get
			{
				// TODO:  ��� Calendar.ReadOnly getter ʵ��
				this.EnsureChildControls();
				return objDateBox.ReadOnly;
			}
			set
			{
				this.EnsureChildControls();
				objDateBox.ReadOnly = value;
			}
		}

        /// <summary>
        /// �������֤����
        /// </summary>
        /// <returns></returns>
		public bool Validate()
		{
			// TODO:  ��� Calendar.Validate ʵ��
			return true;
		}

        /// <summary>
        /// ��ȡֵ
        /// </summary>
        /// <returns></returns>
		public object GetValue()
		{

			if(this.Text!="")
			{
				try
				{
					return Convert.ToDateTime(this.Text);
				}
				catch
				{
					return DBNull.Value;
				}
			}
			return DBNull.Value;
		}

		public void SetValue(object obj)
		{
			if(obj!=DBNull.Value)
			{
				//��̫�� 2006.7.26 �޸�	���ڸ�ʽת��
				try
				{
					if(DataFormatString!="")
						this.Text=String.Format(DataFormatString,Convert.ToDateTime (obj));
					else
						this.Text=((DateTime)obj).ToString ();
				}
				catch
				{
					this.Text = "";
				}
				
			}
			else
			{
				this.Text = "";
			}
		}

		public string ClientValidationFunctionString
		{
			get
			{
				// TODO:  ��� Calendar.ClientValidationFunctionString getter ʵ��
				return null;
			}
			set
			{
				// TODO:  ��� Calendar.ClientValidationFunctionString setter ʵ��
			}
		}

		public bool isClientValidation
		{
			get
			{
				// TODO:  ��� Calendar.isClientValidation getter ʵ��
				return false;
			}
			set
			{
				// TODO:  ��� Calendar.isClientValidation setter ʵ��
				
			}
		}

		public bool IsValid
		{
			get
			{
				// TODO:  ��� Calendar.IsValid getter ʵ��
				return true;
								
			}
		}

		public System.TypeCode SysTypeCode
		{
			get
			{
				// TODO:  ��� Calendar.Weltop.ServerControls.IBrainControl.SysTypeCode getter ʵ��
				return TypeCode.DateTime;
			}
			set
			{
				// TODO:  ��� Calendar.Weltop.ServerControls.IBrainControl.SysTypeCode setter ʵ��
			}
		}
		#endregion

		#region ��������
		
		/// <summary>
		/// ���ݳ��ָ�ʽ
		/// </summary>
		[Category("���"),Description("�����ı����ݳ��ָ�ʽ"),DefaultValue("{0:yyyy-MM-dd}")]
		public string DataFormatString
		{
			get
			{
				if(ViewState["DataFormatString"]!=null)
					return (string)ViewState["DataFormatString"];
				return "{0:yyyy-MM-dd}";
			}
			set
			{
				ViewState["DataFormatString"]=value.Trim ();
			}
		}

		public string Text
		{
			
			get
			{
				this.EnsureChildControls();
                if (!string.IsNullOrEmpty(this.objDateBox.Text))
                    return this.objDateBox.Text;
                else if (ViewState["Date_Text"] != null)
                    return ViewState["Date_Text"].ToString();
                else
                    return "";
			}
			set
			{
				this.EnsureChildControls();
				//this.objDateBox.Text=value;
				if(DataFormatString!="" && value!=null && value!="")
					this.objDateBox.Text =String.Format(DataFormatString,Convert.ToDateTime (value));
				else
					this.objDateBox.Text =value;//Convert.ToDateTime (value).ToString ("yyyy-MM-dd").Trim();

                ViewState["Date_Text"] = this.objDateBox.Text;
			}
		}

		public string ScriptPath
		{
			
			get
			{
				if(ViewState["ScriptPath"]!=null)
				{
					return ViewState["ScriptPath"].ToString();
				}
				else 
				{
					return "";
				}
			}
			set
			{
				
				if (value != "") 
				{
					if (value.Trim().Substring(value.Length-1,1) != "/") value += "/";
				}
				ViewState["ScriptPath"]=value;
			}
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
    }
}
