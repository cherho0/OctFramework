using EnvDTE;
using EnvDTE80;
using Oct.Tools.Core.AddIn;
using System;
using System.Windows.Forms;

namespace CodeGen
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainFrom : Form
    {
        //http://www.cnblogs.com/anderslly/archive/2009/03/15/vs-addin-solution-project.html
        private DTE2 _application;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationObject"></param>
        public MainFrom(DTE2 applicationObject)
        {
            var test = System.Configuration.ConfigurationManager.AppSettings["test"];
            InitializeComponent();
            _application = applicationObject;
        }
        //C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\ItemTemplatesCache\CSharp\Code\2052\Interface
        private void button1_Click(object sender, EventArgs e)
        {
            AddInProvider.CreateProject("AddInTest");
        }

        private void btnCC_Click(object sender, EventArgs e)
        {
            var pro = AddInProvider.GetProject("AddInTest");
            pro.CreateClass("AddInTest", "test", "clsaa");
        }

    }
}
