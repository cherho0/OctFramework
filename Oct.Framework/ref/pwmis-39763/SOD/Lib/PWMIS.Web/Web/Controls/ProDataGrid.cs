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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace PWMIS.Web.Controls
{
    /// <summary>
    /// DataGrid ��ǿ��֧�ֶ�̬�ͻ���ѡ����궯̬���档
    /// ��̫�� 2008.5.6 Ver 1.1
    /// </summary>
    [DefaultProperty("Text"),
        ToolboxData("<{0}:ProDataGrid runat=server ></{0}:ProDataGrid>")]
    public class ProDataGrid : System.Web.UI.WebControls.DataGrid
    {
        private string text;
        private string m_SelectedFieldValue = string.Empty;
        private string m_CssClassRowSelected = string.Empty;
        private string m_CssClassRowMouseMove = string.Empty;
        private string m_ScriptPath = string.Empty;
        private bool m_ClientSelectMode = false;
        private bool m_MorePageSeleced = false;
        private bool m_DefaultSet = true;
        private bool m_ShowCheckColumn = true;
        private bool m_ShowcheckControl = true;
        private string m_ClientSelectedValue = string.Empty;
        private string m_CheckAllText = "ѡ��";
        private string m_CheckItemText = string.Empty;
        private int m_SelectedFromCellIndex = 1;

        private TemplateColumn tm;

        private string SelectValueList = string.Empty;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public ProDataGrid()
        {
            //m_DefaultSet=true;
            this.EnsureChildControls();
        }

        //		private void MyDataGrid_ItemCreated( object sender , System.Web.UI.WebControls.DataGridItemEventArgs e ) 
        //		{ 
        ////			if ( e.Item.ItemType == ListItemType.Pager ) 
        ////			{ 
        ////				Literal msg = new Literal(); 
        ////				msg.Text = "������������滻�ɳ���ʵ�ʷ�ҳ�ؼ���۵Ĵ��롣"; 
        ////				( ( TableCell ) e.Item.Controls[0] ).Controls.Add( msg ); 
        ////			} 
        //			
        //		} 

        /// <summary>
        /// ѡ���������
        /// </summary>
        [Browsable(false)]
        public TemplateColumn CheckColumn
        {
            get
            {
                //				if(tm!=null)
                //					return tm;

                //				if(this.Columns.Count >0)
                //				{
                //					if(this.Columns [0] is TemplateColumn )
                //					{
                //						if(this.Columns [0].HeaderText =="CheckColumn")
                //							tm=this.Columns [0] as TemplateColumn;
                //					}
                //				}
                //				if( tm==null)
                //				{
                //					tm=new TemplateColumn();
                //				
                //					ColumnTemplate ItemTemplate=new ColumnTemplate();
                //					ItemTemplate.IsMoreSelect=m_ClientSelectMode;
                //					tm.ItemTemplate=ItemTemplate;
                //					
                //					ColumnTemplate2 tmHead=new ColumnTemplate2 ();
                //					tmHead.IsMoreSelect =m_ClientSelectMode;
                //					tmHead.CheckAllText =this.CheckHeaderText  ;//"ȫѡ";
                //					tm.HeaderTemplate =tmHead;
                //					tm.HeaderText="CheckColumn";						 
                //					this.Columns.AddAt(0,tm);
                //				}
                return tm;
            }
            set
            {
                tm = value;
            }
        }
        /// <summary>
        /// �ͻ���ѡ���ֵ
        /// </summary>
        [Browsable(false)]
        public string ClientSelectedValue
        {
            get
            {
                return m_ClientSelectedValue;
            }
            set
            {
                m_ClientSelectedValue = value;
            }
        }

        /// <summary>
        /// �ͻ��˶�ѡ��ʱ���Ƿ��¼�ϴ�ѡ���ֵ��ͨ�����ڶ�ҳѡ��
        /// </summary>
        [Description("�ͻ��˶�ѡ��ʱ���Ƿ��¼�ϴ�ѡ���ֵ��ͨ�����ڶ�ҳѡ��"), Bindable(true),
        Category("Behavior"),
        DefaultValue(false)]
        public bool MorePageSeleced
        {
            get
            {
                return m_MorePageSeleced;
            }
            set
            {
                m_MorePageSeleced = value;
            }
        }

        /// <summary>
        /// �ͻ���ѡ��ķ�ʽ,False=��ѡ,True=��ѡ
        /// </summary>
        [Description("�ͻ���ѡ��ķ�ʽ,False=��ѡ,True=��ѡ"), Bindable(true),
        Category("Behavior"),
        DefaultValue(false)
        ]
        public bool ClientSelectMode
        {
            get
            {
                if (ViewState["ClientSelectMode"] != null)
                    m_ClientSelectMode = (bool)ViewState["ClientSelectMode"];
                return m_ClientSelectMode;
            }
            set
            {
                //VS2008 ���Ա����ڹ��캯��֮ǰ�����ã���VS2003 ��ͬ�������ڳ���������ȫ�ֱ����������
                m_ClientSelectMode = value;
                ViewState["ClientSelectMode"] = value;

                if (m_ClientSelectMode)
                {
                    text = "��ѡ״̬";
                }
                else
                {
                    m_MorePageSeleced = false; //��ҳѡ����ڶ�ѡģʽ��Ч
                    text = "��ѡ״̬";
                }
                //SetCheckColumnInfo();

            }
        }

        /// <summary>
        /// �ͻ���ѡ��Ľű��ļ���ַ
        /// </summary>
        [Description("�ͻ���ѡ��Ľű��ļ���ַ"), Bindable(true),
        Category("Behavior"),
        DefaultValue("")]
        public string ScriptPath
        {
            get
            {
                return m_ScriptPath;
            }
            set
            {
                m_ScriptPath = value;
            }
        }

        /// <summary>
        /// ����ѡ��һ��ʱ���û�CSS����
        /// </summary>
        [Description("����ѡ��һ��ʱ���û�CSS����"), Bindable(true),
        Category("Appearance"),
        DefaultValue("")]
        public string CssClassRowSelected
        {
            get
            {
                return m_CssClassRowSelected;
            }
            set
            {
                m_CssClassRowSelected = value;
            }
        }

        /// <summary>
        /// �������������һ��ʱ���û�CSS����
        /// </summary>
        [Description("�������������һ��ʱ���û�CSS����"), Bindable(true),
        Category("Appearance"),
        DefaultValue("")]
        public string CssClassRowMouseMove
        {
            get
            {
                return m_CssClassRowMouseMove;
            }
            set
            {
                m_CssClassRowMouseMove = value;
            }
        }

        /// <summary>
        /// ÿһ��ѡ���ֵ����Ӧ��ĳһ����
        /// </summary>
        [Description("ÿһ��ѡ���ֵ����Ӧ��ĳһ���С����ֵ��Ϊ�գ�������SelectedFromCellIndex ����"), Bindable(true),
        Category("Data"),
        DefaultValue("")]
        public string SelectedFieldValue
        {
            get
            {
                return m_SelectedFieldValue;

            }

            set
            {
                m_SelectedFieldValue = value;
            }
        }

        /// <summary>
        /// ѡ��ֵ���ڵĵ�Ԫ���������С��1��������Ч
        /// </summary>
        [Description("ѡ��ֵ���ڵĵ�Ԫ���������С��1��������Ч"), Bindable(true),
        Category("Data"),
        DefaultValue("1")]
        public int SelectedFromCellIndex
        {
            get
            {
                return m_SelectedFromCellIndex;

            }

            set
            {
                //if(value <this.Columns.Count )
                m_SelectedFromCellIndex = value;
            }
        }


        /// <summary>
        /// ���������ڱ�ʾ�ؼ���ʾ����������
        /// </summary>
        [Description("���������ڱ�ʾ�ؼ���ʾ����������"), Bindable(true),
            Category("Appearance"),
            DefaultValue("")]
        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾѡ����
        /// </summary>
        [Description("�Ƿ���ʾѡ����"), Bindable(true),
        Category("Appearance"),
        DefaultValue(true)]
        public bool ShowCheckColumn
        {
            get
            {
                return m_ShowCheckColumn;
            }

            set
            {
                m_ShowCheckColumn = value;

                if (tm != null)
                    tm.Visible = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾѡ�����е�ѡ��ؼ�
        /// </summary>
        [Description("�Ƿ���ʾѡ�����е�ѡ��ؼ�"), Bindable(true),
        Category("Appearance"),
        DefaultValue(true)]
        public bool ShowcheckControl
        {
            get
            {
                return m_ShowcheckControl;
            }

            set
            {
                m_ShowcheckControl = value;
            }
        }


        /// <summary>
        /// ѡ���б���������
        /// </summary>
        [Description("ѡ���б���������"), Bindable(true),
        Category("Appearance"),
        DefaultValue("")]
        public string CheckHeaderText
        {
            get
            {
                return m_CheckAllText;
            }

            set
            {
                m_CheckAllText = value;
                if (tm != null)
                {
                    ColumnTemplate2 tmHead = (ColumnTemplate2)tm.HeaderTemplate;
                    tmHead.CheckAllText = value;
                }
            }
        }

        /// <summary>
        /// ѡ���е��ı�
        /// </summary>
        [Description("ѡ���е��ı�"), Bindable(true),
        Category("Appearance"),
        DefaultValue("")]
        public string CheckItemText
        {
            get
            {
                return m_CheckItemText;
            }

            set
            {
                m_CheckItemText = value;
            }
        }

        /// <summary>
        /// �Ƿ�Ӧ��Ĭ�ϵ���ʽ�ͽű�����
        /// </summary>
        [Description("�Ƿ�Ӧ��Ĭ�ϵ���ʽ�ͽű�����"), Bindable(true),
        Category("Appearance"),
        DefaultValue(true),
        DesignOnly(true)]
        public bool DefaultSet
        {
            get
            {
                return m_DefaultSet;
            }

            set
            {
                m_DefaultSet = value;
                if (!m_DefaultSet)//���Ĭ������
                {
                    this.CssClass = "";
                    this.AlternatingItemStyle.CssClass = "";
                    this.ItemStyle.CssClass = "";
                    this.HeaderStyle.CssClass = "";
                    this.CssClassRowMouseMove = "";
                    this.CssClassRowSelected = "";
                    //this.ScriptPath ="";
                }
                SetDefaultInfo();
            }
        }

        /// <summary>
        /// �����׸�ģ���У�������ѡ���ܡ�
        /// </summary>
        private void SetCheckColumnInfo()
        {
            if (tm == null)
            {
                tm = new TemplateColumn();

                ColumnTemplate ItemTemplate = new ColumnTemplate();
                ItemTemplate.IsMoreSelect = ClientSelectMode;
                tm.ItemTemplate = ItemTemplate;

                ColumnTemplate2 tmHead = new ColumnTemplate2();
                tmHead.IsMoreSelect = this.ClientSelectMode;// m_ClientSelectMode;
                tmHead.CheckAllText = this.CheckHeaderText;//"ȫѡ";
                tm.HeaderTemplate = tmHead;
                tm.HeaderText = this.CheckHeaderText;
                tm.Visible = this.ShowCheckColumn;
                //tm.HeaderStyle.Width=100;

                if (this.Columns.Count > 0)
                {
                    if (this.Columns[0] is TemplateColumn)
                    {
                        this.Columns.RemoveAt(0);
                    }
                }

                this.Columns.AddAt(0, tm);
            }
            else
            {
                ColumnTemplate ItemTemplate = (ColumnTemplate)tm.ItemTemplate;
                ItemTemplate.IsMoreSelect = this.ClientSelectMode;
                //tm.ItemTemplate=ItemTemplate;

                ColumnTemplate2 tmHead = (ColumnTemplate2)tm.HeaderTemplate;
                tmHead.CheckAllText = this.CheckHeaderText;// "ȫѡ2";
                tmHead.IsMoreSelect = ClientSelectMode;
                tm.HeaderText = this.CheckHeaderText;
                tm.Visible = this.ShowCheckColumn;
            }

        }



        /// <summary>
        /// /Ĭ����ʽ������
        /// </summary>
        private void SetDefaultInfo()
        {
            if (m_DefaultSet)
            {
                //Ĭ����ʽ��
                if (this.CssClass == "")
                    this.CssClass = "dg_table";
                if (this.AlternatingItemStyle.CssClass == "")
                    this.AlternatingItemStyle.CssClass = "dg_alter";
                if (this.ItemStyle.CssClass == "")
                    this.ItemStyle.CssClass = "dg_item";
                if (this.HeaderStyle.CssClass == "")
                    this.HeaderStyle.CssClass = "dg_header";
                if (this.CssClassRowMouseMove == "")
                    this.CssClassRowMouseMove = "Umove";
                if (this.CssClassRowSelected == "")
                    this.CssClassRowSelected = "Uselected";
                if (this.ScriptPath == "")
                    if (this.ClientSelectMode)
                        this.ScriptPath = "multipleTableRow.js";
                    else
                        this.ScriptPath = "singleTableRow.js";


            }
            //			if(m_DefaultSet)
            //			{
            //				this.CssClass ="dg_table";
            //								
            //				this.AlternatingItemStyle .CssClass ="dg_alter"; 
            //								
            //				this.ItemStyle .CssClass ="dg_item";
            //								
            //				this.HeaderStyle.CssClass ="dg_header";
            //								
            //				this.CssClassRowMouseMove ="Umove";
            //								
            //				this.CssClassRowSelected ="Uselected";
            //				if(this.ScriptPath =="")
            //					if(this.ClientSelectMode )
            //						this.ScriptPath ="multipleTableRow.js";
            //					else
            //						this.ScriptPath ="singleTableRow.js";
            //			}

        }

        /// <summary> 
        /// ���˿ؼ����ָ�ָ�������������
        /// </summary>
        /// <param name="output"> Ҫд������ HTML ��д�� </param>
        protected override void Render(HtmlTextWriter output)
        {
            //output.Write(Text);
            //SetDefaultInfo();



            base.Render(output);

            string script = "<script language=\"javascript\">\n " +
                "<!--\n";
            if (ClientSelectMode)
            {
                script += "SetCheckValues();\n";
            }
            //if(m_MorePageSeleced)//�����ҳѡ��
            script += "InitLastSelected('" + m_ClientSelectedValue + "');\n";
            script += "//-->\n</script>\n";
            output.Write(script);

        }

        /// <summary>
        /// ��д��ʼ���¼�
        /// </summary>
        /// <param name="e">�¼�����</param>
        protected override void OnInit(EventArgs e)
        {
            //�˴���������ʱ��Ч�� �� �ؼ� 
            SetCheckColumnInfo();
            SetDefaultInfo();
            base.OnInit(e);
            if(!this.DesignMode )
                this.ClientSelectedValue = this.Page.Request.Form["CID"];
            //			if(m_DefaultSet)
            //			{
            //                //�Զ������Ա����ڿؼ����г�ʼ��ʱ������Ĭ��ֵ
            //				if(this.CssClassRowMouseMove =="")
            //					this.CssClassRowMouseMove ="Umove";
            //				if(this.CssClassRowSelected =="")
            //					this.CssClassRowSelected ="Uselected";
            //				if(this.ScriptPath =="")
            //					this.ScriptPath ="singleTableRow.js";
            //			}
            //			if(this.CheckAllText=="")
            //				this.CheckAllText ="ȫѡ";

            if (ClientSelectMode)
            {
                text = "��ѡ״̬";//this.Columns[0].GetType().ToString ();
                //this.Columns.RemoveAt (1);//����ʱɾ���հ�ģ����
            }
            else
            {
                m_MorePageSeleced = false; //��ҳѡ����ڶ�ѡģʽ��Ч
                text = "��ѡ״̬";

            }


            //this.Columns.RemoveAt (1);//����ʱɾ���հ�ģ����

        }

        /// <summary>
        /// Ԥ���ִ�����Ҫ�����ҳѡ���¼����
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            //�˴��������ʱ��Ч�� 
            //SetCheckColumnInfo();
            base.OnPreRender(e);
            RegisterMyClientScript();


            //
            if (ClientSelectMode)
            {
                string OldSelectValueList = this.Page.Request.Form["SelectValueList"];
                if (SelectValueList != string.Empty)//������������ݰ�
                    SelectValueList = SelectValueList.Remove(SelectValueList.Length - 1, 1);
                else
                    SelectValueList = OldSelectValueList;

                this.Page.ClientScript.RegisterHiddenField("SelectValueList", SelectValueList);//��¼��ѡ��ǰ��ֵ

                string CurrentSelectedValue = this.Page.Request.Form["CID"];//��ǰѡ���ֵ
                string SHValue = this.Page.Request.Form["SHValue"];//�ͻ��˷���ѡ���¼��ı��
                //��ҳѡ����
                if (m_MorePageSeleced)
                {
                    string LastSelectedValues = this.Page.Request.Form["LastSelectedValues"];
                    if (CurrentSelectedValue != null && SHValue == "-1")//�ͻ��˷�����ѡ���¼�
                    {
                        if (LastSelectedValues != "")
                        {
                            //��� LastSelectedValues �е��� ��OldSelectValueList �д��ڣ���ɾ������
                            LastSelectedValues = DeleRepStringList(LastSelectedValues, OldSelectValueList);
                            if (LastSelectedValues != "")
                            {
                                LastSelectedValues = LastSelectedValues.Replace(",,", "");
                                m_ClientSelectedValue = CurrentSelectedValue + "," + LastSelectedValues;
                            }
                            else
                                m_ClientSelectedValue = CurrentSelectedValue;
                        }
                        else
                            m_ClientSelectedValue = CurrentSelectedValue;
                    }
                    else
                    {
                        if (LastSelectedValues != "" && SHValue == "-1")
                        {
                            //��� LastSelectedValues �е��� ��OldSelectValueList �д��ڣ���ɾ������
                            LastSelectedValues = DeleRepStringList(LastSelectedValues, OldSelectValueList);
                            if (LastSelectedValues != "")
                            {
                                LastSelectedValues = LastSelectedValues.Replace(",,", "");
                                m_ClientSelectedValue = LastSelectedValues;
                            }
                            else
                                m_ClientSelectedValue = "";
                        }
                        else
                            m_ClientSelectedValue = LastSelectedValues;

                    }
                    this.Page.ClientScript.RegisterHiddenField("LastSelectedValues", m_ClientSelectedValue);
                }
                else
                {
                    m_ClientSelectedValue = CurrentSelectedValue;
                }

            }

        }

        /// <summary>
        /// ȥ���ַ����е��ظ���
        /// </summary>
        /// <param name="ObjStr">Ŀ���ַ����б����� ��1,2,3��</param>
        /// <param name="SourceStr">Դ�ַ����б����� ��1,2,3��</param>
        /// <returns>����Ŀ�괮</returns>
        private string DeleRepStringList(string ObjStr, string SourceStr)
        {
            string limit = ",";
            string strTemp = string.Empty;
            string[] arrLSV = ObjStr.Split(limit.ToCharArray());
            SourceStr += ",";
            ObjStr += ",";
            for (int i = 0; i < arrLSV.Length; i++)
            {
                strTemp = arrLSV[i] + ",";
                if (strTemp != "," && SourceStr.IndexOf(strTemp) != -1)
                    ObjStr = ObjStr.Replace(strTemp, "");
            }
            return ObjStr;
        }

        //		/// <summary>
        //		/// ��д�����¼�
        //		/// </summary>
        //		/// <param name="e">�¼�����</param>
        //		protected override void OnLoad(EventArgs e)
        //		{
        //			base.OnLoad (e);
        //			
        //			//RegisterMyClientScript();
        //		}

        /// <summary>
        /// ע��ѡ��ű�
        /// </summary>
        private void RegisterMyClientScript()
        {
            this.Page.ClientScript.RegisterHiddenField("SHValue", "");//ע�ᵥѡֵ�ؼ�
            //string SingleScriptPath="singleTableRow.js";
            //ע��ѡ����ʽ�ű�
            string script = "<script language=\"javascript\" src=\"" + m_ScriptPath + "\" type=\"text/Jscript\"></script>\n " +
                "<script language=\"javascript\">\n " +
                "<!--\n" +
                "cssRowSelected=\"" + m_CssClassRowSelected + "\";\n" +
                "cssRowMouseMove=\"" + CssClassRowMouseMove + "\";\n" +
                "//-->\n" +
                "</script>";
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType (),"ClientSelect", script);
        }

        /// <summary>
        /// ��д��������¼�
        /// </summary>
        /// <param name="e">������Ŀ�¼�</param>

        protected override void OnItemDataBound(DataGridItemEventArgs e)
        {
            base.OnItemDataBound(e);
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.SelectedItem)
            {
                //SelectedFieldValue ��ʵ���ؼ��󶨣����� DataBinder.Eval(e.Item.DataItem, "ProductID").ToString();  
                string Value = (m_SelectedFieldValue == "" ? (this.SelectedFromCellIndex >= 1 ? e.Item.Cells[this.SelectedFromCellIndex].Text : "") : m_SelectedFieldValue);
                string argstring = "this,'" + Value + "'";
                e.Item.Attributes.Add("onclick", "myclick(" + argstring + ")");
                e.Item.Attributes.Add("onmouseover", "mymove(this)");
                e.Item.Attributes.Add("onmouseout", "myout(this)");

                //				if(e.Item.Cells[0].HasControls () && e.Item.Cells[0].Controls.Count >0)
                //				{
                //					LiteralControl cb=(LiteralControl)e.Item.Cells[0].Controls [0];// .FindControl ("CID"); 
                //					if(cb!=null)
                //					{
                //						string text=this.CheckItemText ==string.Empty ?(e.Item.ItemIndex+1).ToString ():this.CheckItemText ;
                //						if(m_ClientSelectMode)
                //							cb.Text = GetSelectTableHTML("checkbox",text,m_SelectedFieldValue);// "<input type='checkbox' name='CID' >" +(e.Item.ItemIndex+1).ToString ();
                //						else
                //							cb.Text = GetSelectTableHTML("radio",text,m_SelectedFieldValue);//"<input type='radio' name='CID' >" +(e.Item.ItemIndex+1).ToString ();
                //						//�����
                //						SelectValueList+=m_SelectedFieldValue+",";//��ѡ��ѡ��ֵ�б���û�����ݰ󶨵�ʱ����
                //					
                //					}
                //				}

                string text = this.CheckItemText == string.Empty ? (e.Item.ItemIndex + 1).ToString() : this.CheckItemText;
                if (ClientSelectMode)
                    e.Item.Cells[0].Text = GetSelectTableHTML("checkbox", text, Value);// "<input type='checkbox' name='CID' >" +(e.Item.ItemIndex+1).ToString ();
                else
                    e.Item.Cells[0].Text = GetSelectTableHTML("radio", text, Value);//"<input type='radio' name='CID' >" +(e.Item.ItemIndex+1).ToString ();
                //�����
                SelectValueList += Value + ",";//��ѡ��ѡ��ֵ�б���û�����ݰ󶨵�ʱ����
            }
            if (e.Item.ItemType == ListItemType.Header)
            {
                if (e.Item.Cells[0].HasControls() && e.Item.Cells[0].Controls.Count > 0)
                {
                    LiteralControl cb = (LiteralControl)e.Item.Cells[0].Controls[0];// .FindControl ("CID"); 
                    if (cb != null)
                    {
                        string text = this.CheckHeaderText;
                        if (this.ClientSelectMode)
                            cb.Text = "<input type=\"checkbox\" id=\"CheckAll\" name=\"CheckAll\" value=\"ON\" onclick=\"CheckedAll()\"><label for=\"CheckAll\">" + text + "</label>";
                        else
                            cb.Text = text;

                    }
                }
            }
        }

        //		/// <summary>
        //		/// ��д��������¼�
        //		/// </summary>
        //		/// <param name="e">�������¼�</param>
        //		protected override void OnItemCreated(DataGridItemEventArgs e)
        //		{
        //			base.OnItemCreated (e);
        //			if(e.Item.ItemType ==ListItemType.Header )
        //			{
        //				if(e.Item.Cells[0].HasControls () && e.Item.Cells[0].Controls.Count >0)
        //				{
        //					LiteralControl cb=(LiteralControl)e.Item.Cells[0].Controls [0];// .FindControl ("CID"); 
        //					if(cb!=null)
        //					{
        //						string text=this.CheckHeaderText ;
        //						if(this.ClientSelectMode)
        //							cb.Text = "<input type=\"checkbox\" id=\"CheckAll\" name=\"CheckAll\" value=\"ON\" onclick=\"CheckedAll()\"><label for=\"CheckAll\">"+text+"</label>";
        //						else
        //							cb.Text =text;
        //											
        //					}
        //				}
        //			}
        //		}

        private string GetSelectTableHTML(string typeName, string text, string Value)
        {
            string style = this.ShowcheckControl ? "" : "style='display:none'";
            string sTable = @"<table width=""100%"" >
					<tr><td width=""20""><input type=""@typeName"" name=""CID"" value=""@Value"" @style></td>
						<td >@text</td>
					</tr></table>";
            sTable = sTable.Replace("@typeName", typeName).Replace("@text", text)
                .Replace("@Value", Value).Replace("@style", style);
            return sTable;
        }

    }



    ///  ColumnTemplate ��ITemplate�̳С�
    ///  "InstantiateIn"�����ӿؼ�������˭

    /// <summary>
    /// ��ѡģ����
    /// </summary>
    internal class ColumnTemplate : ITemplate
    {
        //
        private string _type = "checkbox";
        private bool _IsMoreSelect = true;

        /// <summary>
        /// �Ƿ��Ƕ�ѡ
        /// </summary>
        public bool IsMoreSelect
        {
            get { return _IsMoreSelect; }
            set
            {
                _IsMoreSelect = value;
                if (value)
                    _type = "checkbox";
                else
                    _type = "radio";
            }
        }
        /// <summary>
        /// �����ӿؼ�������˭
        /// </summary>
        /// <param name="container">����</param>
        public void InstantiateIn(Control container)
        {

            //			LiteralControl l1=new LiteralControl ("select");
            //			l1.ID ="ItemLit1";
            //			container.Controls.Add(l1);

        }

    }

    /// <summary>
    /// ȫѡģ����
    /// </summary>
    internal class ColumnTemplate2 : ITemplate
    {
        private string _text = "";
        private bool _IsMoreSelect = true;

        /// <summary>
        /// �Ƿ��Ƕ�ѡ
        /// </summary>
        public bool IsMoreSelect
        {
            get { return _IsMoreSelect; }
            set
            {
                _IsMoreSelect = value;
            }
        }
        /// <summary>
        /// ��ѡ�����С�������
        /// </summary>
        public string CheckAllText
        {
            set { _text = value; }
            get { return _text; }
        }
        /// <summary>
        /// �����ӿؼ�������˭
        /// </summary>
        /// <param name="container">��������</param>
        public void InstantiateIn(Control container)
        {
            string ls = _text;
            if (this.IsMoreSelect)
                ls = "<input type=\"checkbox\" id=\"CheckAll\" name=\"CheckAll\" value=\"ON\" onclick=\"CheckedAll()\"><label for=\"CheckAll\">" + _text + "</label>";
            LiteralControl l1 = new LiteralControl(ls);
            container.Controls.Add(l1);

        }

    }

}
