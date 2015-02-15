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
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using PWMIS.Common;


namespace PWMIS.Web.Controls
{
	/// <summary>
	/// Summary description for BrainDropDownList.
	/// </summary>
    [System.Drawing.ToolboxBitmap(typeof(ControlIcon), "DataDropDownList.bmp")]
    public class DataDropDownList : DropDownList, IDataControl, IQueryControl
	{

		#region IBrainControl ��Ա

		#region ��������
        [Category("Data"), Description("�趨��Ӧ������Դ����ʽ��FullClassName,AssemblyName �������Ҫ��ʵ���࣬�������ø����ԡ�")]
        public string DataProvider { get; set; }

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

		[Category("Data"),Description("�趨�����ݿ��ֶζ�Ӧ��������")]
		public string LinkProperty
		{
			get
			{
				if(ViewState["LinkProperty"]!=null)
					return (string)ViewState["LinkProperty"];
				return "";
			}
			set
			{
				ViewState["LinkProperty"]=value;
			}
		}

		[Category("Data"),Description("�趨�����ݱ��Ӧ�����ݱ���")]
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

		[Category("Data"),Description("�趨��Ӧ�������ֶ�����")]
		public System.TypeCode SysTypeCode
		{
			get
			{
				if(ViewState["SysTypeCode"]!=null)
					return (System.TypeCode)ViewState["SysTypeCode"];
				return new System.TypeCode ();
			}
			set
			{
				ViewState["SysTypeCode"] = value;
			}
		}

		#endregion

		#region �ӿڷ���

		#region ������֤

		public virtual bool Validate()
		{
			if(!IsNull)
			{
				if(this.SelectedValue.Trim()==string.Empty)
				{
					return false;
				}

			}
			return true;
		}

		#endregion
		
		public void SetValue(object obj)
		{
			//2006-03-22 �޸� ��΢
			if(obj != null)
			{
				if(this.Items.FindByValue(obj.ToString()) != null)
				{
					this.SelectedValue = obj.ToString();
				}
			}
			//
//			string Value = "";
//			if(obj==null)
//			{
//				Value = obj.ToString().Trim();
//			}
//			ListItem item=this.Items.FindByValue(Value);
//			if(item!=null)
//				this.SelectedValue=obj.ToString().Trim();
		}
			
		public object GetValue()
		{
			return this.SelectedValue.Trim();
		}
		#endregion

		#region ��������
		[Category("Behavior"),Description("�Ƿ�ֻ��")]
		public bool ReadOnly
		{
			get
            {
                //δѡ������Ϊֻ���������ԣ������������ݿ⡣dth,2008.7.27
                if (this.SelectedIndex == -1)
                    return true ;
				return !this.Enabled;
			}
			set
			{
				this.Enabled=!value;
			}
		}
		#endregion

		#region ��֤����

		[Category("�ؼ���֤"),Description("�趨�뷵��ֵ�Ƿ����Ϊ��")]
		public bool IsNull
		{

			get
			{
				if(ViewState["isNull"]!=null)
					return (bool)ViewState["isNull"];
				return true;
			}
			set
			{
				ViewState["isNull"] = value;

			}
		}

		[Category("�ؼ���֤"),Description("�Ƿ�ͨ����������֤Ĭ��Ϊtrue")]
		public bool IsValid
		{
			get
			{
				return Validate();
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

          
		#endregion
	}
	
}

