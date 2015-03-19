using Oct.Tools.Core;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows.Forms;

namespace Oct.Tools.Plugin.HtmlGenerator
{
    [Export(typeof(IEditorPlugIn))]
    public partial class Main : UserControl, IEditorPlugIn
    {
        public Main()
        {
            InitializeComponent();
        }

        public int Order
        {
            get
            {
                return 2;
            }
        }

        public string ToolImgUrl
        {
            get
            {
                return Path.Combine(Environment.CurrentDirectory, @"Img\table.png");
            }
        }

        public string ToolTipText
        {
            get
            {
                return "控件生成";
            }
        }

        public UserControl PlugInUC
        {
            get
            {
                return this;
            }
        }

    }
}
