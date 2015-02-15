//ver 4.5 dbmstype auto get;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
//using System.Drawing.Design;
using System.Collections;
using PWMIS.DataProvider.Data;
using PWMIS.DataProvider.Adapter;
using System.Configuration;
using PWMIS.Common;

namespace PWMIS.Web.Controls
{
    /// <summary>
    /// �����¼�ί�ж���
    /// </summary>
    public delegate void ClickEventHandler(object sender, EventArgs e);

    /// <summary>
    /// ���ݰﶨί�ж���
    /// </summary>
    public delegate void DataBoundHandler(object sender, EventArgs e);

    /// <summary>
    /// Web ��ҳ������
    /// ��̫�� 2007.1.10 Ver 1.0��2008.5.8 Ver 1.0.1.2��2008.7.24 Ver 1.0.1.3
    /// Ver 1.0.1 �������ݷ��ʹ���
    /// Ver 1.0.1.1 �Զ��������ļ�����ȫ��Ĭ�����ò����������ҳ��С
    /// Ver 1.0.1.2 ���˿����Զ����÷�ҳ��С�⣬�����������ض��ķ�ҳ��С��
    /// Ver 1.0.1.3 ֧��GridView
    /// </summary>
    [System.Drawing.ToolboxBitmap(typeof(ControlIcon), "DataPageToolBar.bmp")]
    [DefaultProperty("AllCount"),
    DefaultEvent("PageChangeIndex"),
        ToolboxData("<{0}:ProPageToolBar runat=server></{0}:ProPageToolBar>")]
    public class ProPageToolBar : System.Web.UI.WebControls.WebControl, INamingContainer
    {
        #region �ڲ��ؼ�����
        protected Label lblAllCount = new Label();
        protected Label lblCPA = new Label();
        protected LinkButton lnkFirstPage = new LinkButton();
        protected LinkButton lnkPrePage = new LinkButton();
        protected LinkButton lnkNextPage = new LinkButton();
        protected LinkButton lnkLastPage = new LinkButton();
        protected TextBox txtNavePage = new TextBox();
        protected DropDownList dlPageSize = new DropDownList();
        protected LinkButton lnkGo = new LinkButton();
        #endregion
        /// <summary>
        /// δ��ʼ������ֵ
        /// </summary>
        const int UNKNOW_NUM = -999;

        #region �ֲ���������
        private int PageIndex = UNKNOW_NUM;//
        private int _AllCount;
        private int _PageSize;
        private int _CurrentPage;
        private bool hasSetBgColor;

        private bool ChangePageProperty = false;
        private bool _AutoIDB = false;
        private bool _AutoConfig = false;
        private bool _AutoBindData = false;
        private bool _UserChangePageSize = true;
        private bool _ShowEmptyData = true;

        private string text;
        private string _BindControl;
        private string _SQL;
        private string _Where;
        private string _ConnectionString = string.Empty;
        private string _ErrorMessage = string.Empty;
        private DataProvider.Data.CommonDB _DAO;
        private DBMSType _DBMSType = DBMSType.SqlServer;
        private System.Data.IDataParameter[] _Parameters;

        #endregion

        private DataProvider.Data.CommonDB DAO
        {
            get
            {
                if (_DAO == null)
                {
                    CheckAutoConfig();
                    CheckAutoIDB();
                }
                if (_DAO == null)
                    throw new Exception("δʵ�������ݷ������,��ȷ���Ѿ���������ȷ�����ã�");

                //edit dbmstype:
                this.DBMSType = _DAO.CurrentDBMSType;
                return _DAO;
            }
            set
            {
                _DAO = value;
            }

        }

        #region �������Զ���
        [Bindable(true),
        Category("Appearance"),
        Description("��ҳ˵��"),
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

        private string FontSize
        {
            get
            {
                return this.Font.Size.Unit.ToString();//fontsize;
            }
        }

        /// <summary>
        /// ��ҳ����������ʽ,0-Ĭ�ϣ�1-����ʾ��¼������2-����ʾҳ��ת��3-�Ȳ���ʾ��¼������Ҳ����ʾҳ��ת
        /// </summary>
        [Bindable(true),
        Category("��ҳ����"),
        Description("��ҳ�������ķ�ҳ��ʽ��0-Ĭ�ϣ�1-����ʾ��¼������2-����ʾҳ��ת��3-�Ȳ���ʾ��¼������Ҳ����ʾҳ��ת")
        ]
        public int PageToolBarStyle
        {
            get
            {
                if (ViewState["_PageToolBarStyle"] != null)
                    return (int)ViewState["_PageToolBarStyle"];
                else
                    return 0;
            }
            set
            {
                ViewState["_PageToolBarStyle"] = value;
            }
        }


        #endregion

        #region �ڲ��ؼ���ʽ������
        public string css_linkStyle = "";
        public string css_btnStyle = "";
        public string css_txtStyle = "";
        #endregion

        #region ��ҳ���Զ���

        /// <summary>
        /// ��ǰ����ҳ�룬Ĭ��ֵ1
        /// </summary>
        [Bindable(true),
        Category("��ҳ����"),
        Description("��ǰ����ҳ")
        ]
        public int CurrentPage
        {
            get
            {
                if (ViewState[this.ID + "_CurrentPage"] != null)
                    _CurrentPage = (int)ViewState[this.ID + "_CurrentPage"];
                return _CurrentPage <= 0 ? 1 : _CurrentPage;
            }
            set
            {
                if (value < 0) value = 1;
                _CurrentPage = value;
                ViewState[this.ID + "_CurrentPage"] = value;
                PageIndex = value;
                ChangePageProperty = true;
                this.txtNavePage.Text = value.ToString();
            }
        }
        /// <summary>
        /// ��¼������Ĭ��ֵ0
        /// </summary>
        [Bindable(true),
        Category("��ҳ����"),
        Description("��¼����"),
        DefaultValue(0)]
        public int AllCount
        {
            get
            {
                if (ViewState[this.ID + "_AllCount"] != null)
                    _AllCount = (int)ViewState[this.ID + "_AllCount"];
                return _AllCount;
            }
            set
            {
                if (value < 0 && value != -1) value = 0;
                _AllCount = value;
                ViewState[this.ID + "_AllCount"] = value;
                ChangePageProperty = true;
                this.lblAllCount.Text = value.ToString();
            }
        }
        /// <summary>
        /// ҳ���С��Ĭ��ֵ10������0��ʾ��ϵͳ�Զ���ȡ����ֵ
        /// </summary>
        [Bindable(true),
        Category("��ҳ����"),
        Description("ÿҳ���ҳ��¼��С��Ĭ��ֵ10,����0��ʾ��ϵͳ�Զ���ȡ����ֵ"),
        DefaultValue(10)]
        public int PageSize
        {
            get
            {
                if (ViewState[this.ID + "_PageSize"] != null)
                {
                    _PageSize = (int)ViewState[this.ID + "_PageSize"];
                    return _PageSize <= 0 ? 10 : _PageSize;
                }

                //����Ĭ�Ϸ�ҳ��С
                if (this.AutoConfig && _PageSize == 0)
                {
                    string defaultPageSize = ConfigurationSettings.AppSettings["PageSize"];
                    if (defaultPageSize != null && defaultPageSize != "")
                    {
                        _PageSize = int.Parse(defaultPageSize);
                        return _PageSize;
                    }
                    else
                    {
                        _PageSize = 10;
                    }
                }
                else
                {
                    _PageSize = _PageSize <= 0 ? 10 : _PageSize;
                }
                return _PageSize;
            }
            set
            {
                if (this.AutoConfig && value == 0)
                {
                    string defaultPageSize = ConfigurationSettings.AppSettings["PageSize"];
                    if (defaultPageSize != null && defaultPageSize != "")
                    {
                        _PageSize = int.Parse(defaultPageSize);
                    }
                    else
                    {
                        _PageSize = 10;
                    }
                    value = _PageSize;
                }
                if (value < 0) value = 10;
                _PageSize = value;
                ViewState[this.ID + "_PageSize"] = value;
                ChangePageProperty = true;
            }
        }
        /// <summary>
        /// ҳ��������ֻ��
        /// </summary>
        [Bindable(true),
        Category("��ҳ����"),
        Description("ҳ��������ֻ��"),
        DefaultValue(1)]
        public int PageCount
        {
            get
            {
                int AllPage = AllCount / PageSize;
                if ((AllPage * PageSize) < AllCount) AllPage++;
                if (AllPage <= 0) AllPage = 1;
                return AllPage;
            }

        }

        [
        Category("��ҳ����"),
        Description("�Ƿ������û������ҳ���ʱ��ı��ҳ��С"),
        DefaultValue(true)]
        public bool UserChangePageSize
        {
            get
            {
                if (ViewState[this.ID + "_UserChangePageSize"] != null)
                    _UserChangePageSize = (bool)ViewState[this.ID + "_UserChangePageSize"];
                return _UserChangePageSize;
            }
            set
            {
                _UserChangePageSize = value;
                ViewState[this.ID + "_UserChangePageSize"] = value;
            }
        }
        #endregion

        #region ��ҳ�¼�


        /// <summary>
        /// ҳ��ı��¼�
        /// </summary>
        [Category("��ҳ�¼�"),
        Description("ҳ��ı��¼�")]
        public event ClickEventHandler PageChangeIndex;

        /// <summary>
        /// Ŀ��ؼ�������ݰ�֮ǰ���¼�
        /// </summary>
        [Category("Data"),
        Description("Ŀ��ؼ�������ݰ�֮ǰ���¼�")]
        public event DataBoundHandler DataControlDataBinding;
        /// <summary>
        /// Ŀ��ؼ�������ݰ�����¼�
        /// </summary>
        [Category("Data"),
        Description("Ŀ��ؼ�������ݰ�����¼�")]
        public event DataBoundHandler DataControlDataBound;

        /// <summary>
        /// �ı�ҳ������
        /// </summary>
        /// <param name="e">Ŀ��</param>
        protected void changeIndex(EventArgs e)
        {
            if (PageChangeIndex != null)
            {
                PageChangeIndex(this, e);
            }
            //if(this.Site !=null && ! this.Site.DesignMode  )//������ʱ
            //{
            if (this.AutoBindData)
            {
                if (this.Page.IsPostBack)
                {
                    this.BindResultData();
                }
            }
            //}
        }

        #endregion

        #region �����ķ���

        /// <summary>
        /// ��ȡһ��ʵ����ѯ����
        /// </summary>
        /// <returns></returns>
        public System.Data.IDataParameter GetParameter()
        {
            return DAO.GetParameter();
        }

        /// <summary>
        /// ��ȡһ��ʵ����ѯ����
        /// </summary>
        /// <param name="paraName"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public System.Data.IDataParameter GetParameter(string paraName, object Value)
        {
            return DAO.GetParameter(paraName, Value);
        }

        /// <summary>
        /// �����ṩ�ķ�ҳ��ѯ�Ϳؼ��ṩ�����ݷ�����Ϣ��������Դ��ȡ���ݡ�
        /// </summary>
        /// <returns></returns>
        public object GetDataSource()
        {
            if (this.AllCount == 0)
                this.AllCount = -1;//���⴦����ȡ��¼����Ϊ0ʱ�ļܹ�
            DAO.ConnectionString = this.ConnectionString;
            object result = DAO.ExecuteDataSet(this.SQLbyPaging, System.Data.CommandType.Text, this.Parameters);
            if (this.AllCount == -1)
                this.AllCount = 0;
            if (DAO.ErrorMessage != "")
                throw new Exception(DAO.ErrorMessage + ";SQL=" + this.SQLbyPaging);
            return result;
            //DAO.
        }

        /// <summary>
        /// ��ȡ�������¼����
        /// </summary>
        /// <returns></returns>
        public int GetResultDataCount()
        {
            //����һ����ͳ�Ʋ�������������ڼ������Ѿ����ڵ����⡣
            System.Data.IDataParameter[] countParas = null;
            if (this.Parameters != null && this.Parameters.Length > 0)
            {
                countParas = (System.Data.IDataParameter[])this.Parameters.Clone();
                for (int i = 0; i < countParas.Length; i++)
                {
                    countParas[i] = DAO.GetParameter(countParas[i].ParameterName, countParas[i].Value);
                }
            }

            DAO.ConnectionString = this.ConnectionString;
            object count = DAO.ExecuteScalar(this.SQLbyCount, System.Data.CommandType.Text, countParas);
            if (count != null)
                return Convert.ToInt32(count);//(int)count ��Oracle ����ʧ�ܡ�
            else
                throw new Exception(DAO.ErrorMessage);
        }

        /// <summary>
        /// ������Դ�ķ�ҳ���ݰ󶨵���Ŀ��ؼ��ϣ�֧��GridView
        /// </summary>
        public void BindResultData()
        {
            string BindToControlID = this.BindToControl;
            if (BindToControlID != null && BindToControlID != "")
            {
                if (DataControlDataBinding != null)
                {
                    DataControlDataBinding(this, new EventArgs());
                }
                //����ķ�ʽ������ؼ����û��ؼ��У������Ҳ�����
                //Control ctr= this.Page.FindControl (BindToControlID); 
                Control ctr = FindMyControl(this, BindToControlID);

                if (ctr is GridView)
                {
                    ((GridView)ctr).DataSource = this.GetDataSource();
                    ctr.DataBind();
                }
                else if (ctr is DataGrid)
                {
                    ((DataGrid)ctr).DataSource = this.GetDataSource();
                    ctr.DataBind();
                }
                else if (ctr is DataList)
                {
                    ((DataList)ctr).DataSource = this.GetDataSource();
                    ctr.DataBind();
                }
                else if (ctr is Repeater)
                {
                    ((Repeater)ctr).DataSource = this.GetDataSource();
                    ctr.DataBind();
                }
                else
                {
                    throw new Exception("�ؼ�" + BindToControlID + "��֧�����ݰ󶨣���ȷ����Ŀ��ؼ���DataGrid,DataList,Repeater���ͣ�");
                }
                if (DataControlDataBound != null)
                {
                    DataControlDataBound(this, new EventArgs());
                }
            }
        }
        //      ���û��ؼ��У���Ȼ�Ҳ�����Ŀ��ؼ�
        private Control FindMyControl(Control sourceControl, string objControlID)
        {
            //������Ȳ���
            foreach (Control ctr in sourceControl.Parent.Controls)
            {
                if (ctr.ID == objControlID)
                    return ctr;
            }
            foreach (Control ctr in sourceControl.Parent.Controls)
            {
                Control objCtr = FindMyControl(ctr, objControlID);
                if (objCtr != null)
                    return objCtr;
            }
            return null;
        }

        /// <summary>
        /// ���°����ݺͼ��㱾�β�ѯ�ļ�¼���������趨��ǰҳ���ڵ�һҳ
        /// </summary>
        public void ReBindResultData()
        {
            this.CurrentPage = 1;
            this.AllCount = this.GetResultDataCount();
            this.BindResultData();
        }

        #endregion

        #region ���ݷ�ҳ����
        /// <summary>
        /// ��Ҫ�󶨷�ҳ�Ŀؼ�����DataGrid,DataList,Repeater ��
        /// </summary>
        [DefaultValue(null),
        Category("Data"),
        Description("��Ҫ�󶨷�ҳ�Ŀؼ�����DataGrid,DataList,Repeater ��"),
        TypeConverter(typeof(ControlListIDConverter))]
        public string BindToControl
        {
            get
            {
                return _BindControl;
            }
            set
            {
                _BindControl = value;
            }
        }

        /// <summary>
        /// ���ڷ�ҳ��ѯ��ԭʼ SQL ���
        /// </summary>
        [DefaultValue(null),
        Category("Data"),
        Description("���ڷ�ҳ��ѯ��ԭʼ SQL ���")]
        public string SQL
        {
            get
            {
                if (ViewState[this.ID + "_SQL"] != null)
                    _SQL = (string)ViewState[this.ID + "_SQL"];
                return _SQL;
            }
            set
            {
                _SQL = value;
                ViewState[this.ID + "_SQL"] = value;
            }
        }

        /// <summary>
        /// ��ҳ��ѯ����,������ʱ����� GetParameter���� ��ӳ�Ա��
        /// </summary>
        [DefaultValue(null),
        Category("Data"),
        Description("��ҳ��ѯ����,������ʱ����� GetParameter���� ��ӳ�Ա��")]
        public System.Data.IDataParameter[] Parameters
        {
            get
            {
                if (_Parameters != null)
                    return _Parameters;
                else
                {

                    if (System.Web.HttpContext.Current.Session[this.ID + "_Parameters"] != null)
                    {
                        System.Data.IDataParameter[] p0 = (System.Data.IDataParameter[])System.Web.HttpContext.Current.Session[this.ID + "_Parameters"];
                        System.Data.IDataParameter[] p1 = new System.Data.IDataParameter[p0.Length];
                        for (int i = 0; i < p0.Length; i++)
                        {
                            p1[i] = this.GetParameter(p0[i].ParameterName, p0[i].Value);//�����²���
                        }

                        return p1;
                    }
                }
                return null;

            }
            set
            {
                _Parameters = value;
                System.Web.HttpContext.Current.Session[this.ID + "_Parameters"] = _Parameters;
            }
        }

        /// <summary>
        /// ���ɵ����ڷ�ҳ��ѯ�� SQL ���
        /// </summary>
        [DefaultValue(null),
        Category("Data"),
        Description("���ɵ����ڷ�ҳ��ѯ�� SQL ���")]
        public string SQLbyPaging
        {
            get
            {
                if (this.SQL == null) return "";
                SQLPage.DbmsType = this.DBMSType;
                return SQLPage.MakeSQLStringByPage(this.SQL, this.Where, this.PageSize, this.CurrentPage, this.AllCount);
            }
        }

        /// <summary>
        /// ���ɵ�����ͳ�Ʒ�ҳ��ѯ�ܼ�¼���� SQL ���
        /// </summary>
        [DefaultValue(null),
        Category("Data"),
        Description("���ɵ�����ͳ�Ʒ�ҳ��ѯ�ܼ�¼���� SQL ���")]
        public string SQLbyCount
        {
            get
            {
                if (this.SQL == null) return "";
                SQLPage.DbmsType = this.DBMSType;
                return SQLPage.MakeSQLStringByPage(this.SQL, this.Where, this.PageSize, this.CurrentPage, 0);
            }
        }

        /// <summary>
        /// ָ�����ڷ�ҳ��ѯ��֧�ֵ����ݿ����ϵͳ��������
        /// </summary>
        [DefaultValue(DBMSType.SqlServer),
        Category("Data"),
        Description("ָ�����ڷ�ҳ��ѯ��֧�ֵ����ݿ����ϵͳ��������")]
        [TypeConverter(typeof(EnumConverter))]
        public DBMSType DBMSType
        {
            get
            {
                return _DBMSType;
            }
            set
            {
                _DBMSType = value;

            }
        }

        /// <summary>
        /// �Ƿ��Զ������ݿ�ʵ����������ǣ�������DataProvider ���ݷ��ʿ飬�ܹ�����Ļ�ȡ���������ɽ�����ݼ������δ����ȷ���ã�����������ΪTrue ��
        /// </summary>
        [DefaultValue(false),
        Category("Data"),
        Description("�Ƿ��Զ������ݿ�ʵ����������ǣ�������DataProvider ���ݷ��ʿ飬�ܹ�����Ļ�ȡ���������ɽ�����ݼ������δ����ȷ���ã�����������ΪTrue ��")]
        public bool AutoIDB
        {
            get
            {
                return _AutoIDB;
            }
            set
            {
                _AutoIDB = value;

            }
        }

        private void CheckAutoIDB()
        {
            if (System.Web.HttpContext.Current == null)//   this.Site !=null && this.Site.DesignMode
            {
                return;	//�����ʱ�˳������߼��ж�
            }
            if (_AutoIDB)//����Զ�ʵ�������ݿ���ʶ���
            {
                try
                {
                    _ErrorMessage = "";
                    if (DAO == null)
                        DAO = MyDB.GetDBHelper(this.DBMSType, this.ConnectionString);
                    _AutoIDB = true;
                }
                catch (Exception e)
                {
                    _AutoIDB = false;
                    _ErrorMessage = e.Message;
                }
            }
        }

        /// <summary>
        /// �Ƿ��Զ���Ӧ�ó��������ļ���ȡ���ݷ���������Ϣ��ֻ���Ѿ���ȷ����������Ϣ�ſ��Է���True ��
        /// </summary>
        [DefaultValue(false),
        Category("Data"),
        Description("�Ƿ��Զ���Ӧ�ó��������ļ���ȡ���ݷ��ʺ�����������Ϣ��ֻ���Ѿ���ȷ����������Ϣ�ſ��Է���True ��")]
        public bool AutoConfig
        {
            get
            {
                return _AutoConfig;
            }
            set
            {
                _AutoConfig = value;

            }
        }

        private void CheckAutoConfig()
        {
            if (System.Web.HttpContext.Current == null)//   this.Site !=null && this.Site.DesignMode
            {
                return;	//�����ʱ�˳������߼��ж�
            }
            if (_AutoConfig)
            {
                _ErrorMessage = "";
                string strConn = "";
                //�������ݿ����ϵͳ����
                string strDBMSType = ConfigurationSettings.AppSettings["EngineType"];//ͳһ�� DBMSType ��ȡ

                if (strDBMSType != null && strDBMSType != "")
                {
                    if (System.Enum.IsDefined(typeof(DBMSType), strDBMSType))
                        this.DBMSType = (DBMSType)System.Enum.Parse(typeof(DBMSType), strDBMSType);
                    else
                        _AutoConfig = false;

                    //���������ַ���
                    string ConnStrKey = string.Empty;
                    switch (this.DBMSType)
                    {
                        case DBMSType.Access:
                            ConnStrKey = "OleDbConnectionString";
                            break;
                        case DBMSType.SqlServer:
                            ConnStrKey = "SqlServerConnectionString";
                            break;
                        case DBMSType.Oracle:
                            ConnStrKey = "OracleConnectionString";
                            break;
                        case DBMSType.MySql:
                            ConnStrKey = "OdbcConnectionString";
                            break;
                        case DBMSType.UNKNOWN:
                            ConnStrKey = "OdbcConnectionString";
                            break;
                    }
                    strConn = ConfigurationSettings.AppSettings[ConnStrKey];
                }
                else
                {
                    //δָ���������һ��connectionStrings ���ýڶ�ȡ
                    if (ConfigurationManager.ConnectionStrings.Count > 0)
                    {
                        DAO = MyDB.GetDBHelper();
                        strConn = DAO.ConnectionString;
                    }
                }

                if (strConn == null || strConn == "")
                    _AutoConfig = false;
                else
                    this.ConnectionString = strConn.Replace("~", Context.Request.PhysicalApplicationPath);//�滻���·��

                if (!_AutoConfig)//�����ʱ�����ɴ�����Ϣ����ΪVS2003���ʱ�޷���ȡ������Ϣ
                {
                    _ErrorMessage = "δ����ȷ�������ݷ�����Ϣ�������Ƿ��Ѿ���Ӧ�ó��������ļ��н�������ȷ������";
                    _AutoConfig = false;
                }
                else
                    AutoIDB = _AutoConfig;//�����ȷ���ã���ô�Զ������ݿ���ʶ���ʵ��


            }
        }

        /// <summary>
        /// �Ƿ�������ʱ�Զ��󶨷�ҳ���ݣ������� AutoIDB ���Ե���True
        /// </summary>
        [DefaultValue(false),
        Category("Data"),
        Description("�Ƿ�������ʱ�Զ��󶨷�ҳ���ݣ������� AutoIDB ���Ե���True")]
        public bool AutoBindData
        {
            get
            {
                return _AutoBindData;
            }
            set
            {
                _AutoBindData = value;
            }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        [DefaultValue(""),
        Category("Data"),
        Description("������Ϣ")]
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            //set{ _ErrorMessage=value;}
        }

        /// <summary>
        /// ���ݿ������ַ���
        /// </summary>
        [DefaultValue(""),
        Category("Data"),
        Description("���ݿ������ַ���")]
        public string ConnectionString
        {
            get
            {
                return _ConnectionString;
            }
            set
            {
                _ConnectionString = value;
            }
        }

        /// <summary>
        /// ָ����ҳ��ѯ�ĸ���������ע��򵥲�ѯ�븴�Ӳ�ѯ�������޶���ʽ��
        /// </summary>
        [DefaultValue(""),
        Category("Data"),
        Description("ָ����ҳ��ѯ�ĸ���������ע��򵥲�ѯ�븴�Ӳ�ѯ�������޶���ʽ��")]
        public string Where
        {
            get
            {
                if (ViewState[this.ID + "_Where"] != null)
                    _Where = (string)ViewState[this.ID + "_Where"];
                return _Where;
            }
            set
            {
                _Where = value;
                ViewState[this.ID + "_Where"] = value;
            }
        }

        /// <summary>
        /// �����¼����Ϊ0�����������Ƿ���ʾ���ݼܹ��������Ҫ��ʾ�ܹ�����ô��ִ�����ݰ󶨷�����
        /// </summary>
        [DefaultValue(true),
        Category("Data"),
        Description("�����¼����Ϊ0�����������Ƿ���ʾ���ݼܹ��������Ҫ��ʾ�ܹ�����ô��ִ�����ݰ󶨷�����")]
        public bool ShowEmptyData
        {
            get
            {
                if (ViewState[this.ID + "_ShowEmptyData"] != null)
                    _ShowEmptyData = (bool)ViewState[this.ID + "_ShowEmptyData"];
                return _ShowEmptyData;
            }
            set
            {
                _ShowEmptyData = value;
                ViewState[this.ID + "_ShowEmptyData"] = value;
            }
        }

        #endregion

        #region �������صķ���
        /// <summary> 
        /// ���˿ؼ����ָ�ָ�������������
        /// </summary>
        /// <param name="output"> Ҫд������ HTML ��д�� </param>
        protected override void Render(HtmlTextWriter output)
        {
            if (ChangePageProperty)
            {
                ChangePageProperty = false;
                //this.SetPageInfo ();
            }
            this.SetPageInfo();
            this.ForeColor = this.ForeColor;
            this.EnsureChildControls();

            //�����ͷ��ʽ
            output.Write("<table width='" + this.Width.ToString() + "' height='" + this.Height
                + "' bgcolor='" + ConvertColorFormat(this.BackColor)
                + "' bordercolor='" + ConvertColorFormat(this.BorderColor)
                + "' border='" + this.BorderWidth.ToString()
                + "' style='border-style:" + this.BorderStyle.ToString()
                + ";border-collapse:collapse' cellpadding='0'><tr><td><table width='100%' style='color:" + ConvertColorFormat(this.ForeColor)
                + " ;font-size:" + this.FontSize + "; font-family:" + this.Font.Name + "' class='"
                + this.CssClass + "'><tr><td valign='baseline'>"
                + this.Text + "</td><td valign='baseline'>");
            //��ӿؼ�
            //1-����ʾ��¼������2-����ʾҳ��ת��3-�Ȳ���ʾ��¼������Ҳ����ʾҳ��ת

            int type = this.PageToolBarStyle;

            //1��3������ʾ��¼����
            if (type != 1 && type != 3)
            {
                int currSize = PageSize;
                if (this.PageCount == this.CurrentPage)
                    currSize = this.AllCount - this.PageSize * (this.CurrentPage - 1);

                output.Write(currSize.ToString() + "/");//AllCount-PageSize*(PageNumber-1)
                lblAllCount.RenderControl(output);
                output.Write("����");
            }

            //
            if (UserChangePageSize)
            {
                output.Write("\n");
                dlPageSize.RenderControl(output);
                output.Write("��/ҳ��");
            }
            lblCPA.RenderControl(output);
            output.Write("ҳ</td><td>");
            lnkFirstPage.RenderControl(output);
            output.Write("\n");
            lnkPrePage.RenderControl(output);
            output.Write("\n");
            lnkNextPage.RenderControl(output);
            output.Write("\n");
            lnkLastPage.RenderControl(output);

            //2����3 ����ʾҳ��ת
            if (type != 2 && type != 3)
            {
                output.Write("\n��");
                txtNavePage.RenderControl(output);
                output.Write("ҳ\n");
                lnkGo.RenderControl(output);
            }
            output.Write("</td></tr></table></td></tr></table>");

        }

        /// <summary>
        /// ��д OnLoad �¼�
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //			//�����ﴦ���й����ݰ�����
            //			CheckAutoConfig ();
            //			CheckAutoIDB();


            if (this.Site != null && this.Site.DesignMode)//�����ʱ
            {
                return;
            }
            if (this.AutoBindData)
            {
                if (!this.Page.IsPostBack)
                {
                    this.AllCount = this.GetResultDataCount();
                    //�����¼����Ϊ0�����������Ƿ���ʾ���ݼܹ��������Ҫ��ʾ�ܹ�����ô��ִ�����ݰ󶨷�����
                    if (!this.ShowEmptyData && this.AllCount == 0)
                        return;

                    this.BindResultData();

                    //this.SetPageInfo ();
                }
            }
        }


        /// <summary>
        /// ��д CSS ��ʽ��
        /// </summary>
        public override string CssClass
        {
            get
            {
                return base.CssClass;
            }
            set
            {
                base.CssClass = value;
                foreach (System.Web.UI.WebControls.WebControl ctr in this.Controls)
                {
                    ctr.CssClass = value;
                }
            }
        }

        /// <summary>
        /// ��дǰ��ɫ
        /// </summary>
        public override System.Drawing.Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                foreach (System.Web.UI.WebControls.WebControl ctr in this.Controls)
                {
                    ctr.ForeColor = value;
                }
            }
        }

        public override System.Drawing.Color BackColor
        {
            get
            {
                if (!hasSetBgColor)
                    return System.Drawing.Color.White;
                return base.BackColor;
            }
            set
            {
                hasSetBgColor = true;
                base.BackColor = value;
            }
        }

        /// <summary>
        /// �����ӿؼ�
        /// </summary>
        protected override void CreateChildControls()
        {
            //base.CreateChildControls ();
            Controls.Clear();
            Controls.Add(lblAllCount);
            Controls.Add(lblCPA);
            Controls.Add(lnkFirstPage);
            Controls.Add(lnkPrePage);
            Controls.Add(lnkNextPage);
            Controls.Add(lnkLastPage);
            Controls.Add(txtNavePage);
            Controls.Add(lnkGo);

            lblAllCount.Text = this.AllCount.ToString();
            lnkFirstPage.Text = "��ҳ";
            lnkPrePage.Text = "��һҳ";
            lnkNextPage.Text = "��һҳ";
            lnkLastPage.Text = "βҳ";
            txtNavePage.Width = 30;
            lnkGo.Text = "Go";


            if (this.UserChangePageSize)
            {
                Controls.Add(dlPageSize);
                dlPageSize.AutoPostBack = true;
                dlPageSize.Items.Clear();
                for (int i = 5; i <= 50; i += 5)
                {
                    dlPageSize.Items.Add(i.ToString());
                }
                dlPageSize.SelectedValue = this.PageSize.ToString();
                this.dlPageSize.SelectedIndexChanged += new EventHandler(dlPageSize_SelectedIndexChanged);
            }

            this.lnkFirstPage.Click += new System.EventHandler(this.lnkFirstPage_Click);
            this.lnkPrePage.Click += new System.EventHandler(this.lnkPrePage_Click);
            this.lnkNextPage.Click += new System.EventHandler(this.lnkNextPage_Click);
            this.lnkLastPage.Click += new System.EventHandler(this.lnkLastPage_Click);
            this.lnkGo.Click += new System.EventHandler(this.lnkGo_Click);


        }
        #endregion

        #region �ڲ��¼�����

        /// <summary>
        /// RGB��ɫֵ��Html��ɫֵת��
        /// </summary>
        /// <param name="RGBColor"></param>
        /// <returns></returns>
        private string ConvertColorFormat(System.Drawing.Color RGBColor)
        {
            return "RGB(" + RGBColor.R.ToString() + "," + RGBColor.G.ToString() + "," + RGBColor.G.ToString() + ")";
        }

        /// <summary>
        /// ���÷�ҳ״̬��Ϣ
        /// </summary>
        private void SetPageInfo()
        {
            if (PageIndex == UNKNOW_NUM)
                PageIndex = this.CurrentPage;

            if (PageIndex > PageCount) PageIndex = PageCount;
            else if (PageIndex == -1) PageIndex = PageCount;
            else if (PageIndex < 1) PageIndex = 1;

            if (this.AllCount == 0)
                this.lblCPA.Text = "0/0";
            else
                this.lblCPA.Text = PageIndex.ToString() + "/" + PageCount.ToString();
            this.txtNavePage.Text = PageIndex.ToString();
            this.CurrentPage = PageIndex;

            if (this.PageCount == 1) this.lnkGo.Enabled = false;

            if (PageIndex == 0)
            {
                this.lnkFirstPage.Enabled = false;
                this.lnkPrePage.Enabled = false;
                this.lnkLastPage.Enabled = false;
                this.lnkNextPage.Enabled = false;
                //this.btnNavePage .Enabled =false;
                return;

            }

            if (PageIndex == 1)
            {
                this.lnkFirstPage.Enabled = false;
                this.lnkPrePage.Enabled = false;

            }
            else
            {
                this.lnkFirstPage.Enabled = true;
                this.lnkPrePage.Enabled = true;
            }
            if (PageIndex < PageCount)
            {
                this.lnkLastPage.Enabled = true;
                this.lnkNextPage.Enabled = true;

            }
            else
            {
                this.lnkLastPage.Enabled = false;
                this.lnkNextPage.Enabled = false;
            }
        }

        /// <summary>
        /// ��һҳ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkNextPage_Click(object sender, System.EventArgs e)
        {
            PageIndex = this.CurrentPage;
            PageIndex++;
            SetPageInfo();
            this.changeIndex(e);

        }

        /// <summary>
        /// ��һҳ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkPrePage_Click(object sender, System.EventArgs e)
        {
            PageIndex = this.CurrentPage;
            PageIndex--;
            SetPageInfo();
            this.changeIndex(e);
        }

        /// <summary>
        /// βҳ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkLastPage_Click(object sender, System.EventArgs e)
        {
            PageIndex = -1;
            SetPageInfo();
            this.changeIndex(e);
        }

        /// <summary>
        /// ��ҳ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkFirstPage_Click(object sender, System.EventArgs e)
        {
            PageIndex = 1;
            SetPageInfo();
            this.changeIndex(e);
        }


        /// <summary>
        /// ��ʼ������������ʽ
        /// </summary>
        private void InitStyle()
        {
            if (css_linkStyle != "")
                this.lnkFirstPage.Attributes.Add("class", css_linkStyle);
            if (css_linkStyle != "")
                this.lnkNextPage.Attributes.Add("class", css_linkStyle);
            if (css_linkStyle != "")
                this.lnkLastPage.Attributes.Add("class", css_linkStyle);
            if (css_linkStyle != "")
                this.lnkPrePage.Attributes.Add("class", css_linkStyle);
            if (css_linkStyle != "")
                this.lnkGo.Attributes.Add("class", css_linkStyle);
            if (css_txtStyle != "")
                this.txtNavePage.Attributes.Add("class", css_txtStyle);
        }

        /// <summary>
        /// ת��ĳҳ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkGo_Click(object sender, System.EventArgs e)
        {
            try
            {
                PageIndex = Int32.Parse(this.txtNavePage.Text.Trim());
                SetPageInfo();
                this.changeIndex(e);

            }
            catch
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "pageErr", "<script language='javascript'>alert('����д����ҳ�룡');</script>");
            }
        }

        /// <summary>
        /// �ı�ҳ��С
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PageSize = int.Parse(dlPageSize.SelectedValue);
            this.CurrentPage = 1;
            this.PageIndex = this.CurrentPage;
            SetPageInfo();
            this.BindResultData();
        }
        #endregion

        #region ��ȡ�󶨿ؼ��б� ��
        /// <summary>
        /// ��ȡ�󶨿ؼ��б� ��
        /// </summary>
        public class ControlListIDConverter : StringConverter
        {
            /// <summary>
            /// false
            /// </summary>
            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                return false;
            }
            /// <summary>
            /// true
            /// </summary>
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
            /// <summary>
            /// ��ȡ��������ʱ��Ŀ��ؼ���ID
            /// </summary>
            public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                if (context == null)
                    return null;
                ArrayList al = new ArrayList();
                foreach (IComponent ic in context.Container.Components)
                {
                    if (ic is ProPageToolBar)
                        continue;
                    if (ic is DataGrid || ic is Repeater || ic is DataList || ic is GridView)//|| ic is System.Web.UI.WebControls.ListView ������Ҫ3.5���
                    {
                        al.Add(((Control)ic).ID);
                    }
                }
                return new TypeConverter.StandardValuesCollection(al);
            }
        }

        public DBMSType DBMSType1
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public SQLPage SQLPage
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public DataBoundHandler DataBoundHandler
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public ClickEventHandler ClickEventHandler
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        //		public class DBMSConverter:TypeConverter
        //		{
        //			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        //			{
        //				if(sourceType==typeof(string ))
        //				{
        //					return true;
        //				}
        //				return base.CanConvertFrom (context, sourceType);
        //			}
        //
        //			public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        //			{
        //				if( value.GetType() == typeof(string) )
        //				{
        //					DBMSType dbms=DBMSType.UNKNOWN  ;
        //					if(System.Enum.IsDefined (typeof(DBMSType),value) )
        //					{
        //						dbms=(DBMSType)System.Enum.Parse (typeof(DBMSType),value.ToString (),false); 
        //					}
        //					return dbms;
        //				}
        //				else
        //					return base.ConvertFrom(context, culture, value);
        //
        //			}
        //
        //			/// <summary>
        //			/// true
        //			/// </summary>
        //			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        //			{
        //				return true;
        //			}
        //
        //			public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        //			{
        //				if(context==null)
        //					return null;
        //				string[] dbmsTypeNames=System.Enum.GetNames (typeof(DBMSType ));
        //				return new TypeConverter.StandardValuesCollection(dbmsTypeNames);
        // 			}
        //
        //		}

        #endregion


    }
}
