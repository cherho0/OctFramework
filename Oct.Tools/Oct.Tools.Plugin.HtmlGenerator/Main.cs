using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oct.Tools.Core;

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
                return Path.Combine(Environment.CurrentDirectory, @"Res\Img\table.png");
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
