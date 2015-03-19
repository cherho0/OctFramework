using EnvDTE;
using EnvDTE80;
using EnvDTE90;
using System.IO;
using System.Linq;

namespace Oct.Tools.Core.AddIn
{
    public static class AddInProvider
    {
        public static DTE2 Application;

        public static Solution3 GetSolution3()
        {
            return Application.Solution as Solution3;
        }

        public static string GetSolutionName()
        {
            var filename = GetSolution3().FileName;
            var last = filename.LastIndexOf("\\");
            var path = filename.Substring(last + 2);
            return path;
        }

        public static Project GetProject(string name)
        {
            var pros = GetSolution3().Projects.Cast<Project>();
            foreach (var project in pros)
            {
                if (project.Name == name)
                {
                    return project;
                }
            }
            return null;
        }

        public static void CreateProject(string name)
        {
            if (GetProject(name) != null)
            {
                return;
            }
            var filename = GetSolution3().FileName;
            var last = filename.LastIndexOf("\\");
            var path = filename.Remove(last);
            string classLibProjTemplatePath =
    GetSolution3().GetProjectTemplate("ClassLibrary.zip", "CSharp");
            string domainProjName = name;
            GetSolution3().AddFromTemplate(classLibProjTemplatePath, Path.Combine(path, domainProjName),
                  domainProjName, false);
            var pro = GetProject(domainProjName);
            var cls1 = pro.GetItem("Class1.cs");
            if (cls1 != null)
            {
                cls1.Delete();
            }
        }

        public static ProjectItem GetItem(this Project pro, string name)
        {
            foreach (ProjectItem projectItem in pro.ProjectItems)
            {
                var dname = projectItem.Name;
                if (dname == name)
                {
                    return projectItem;
                }
            }
            return null;
        }

        public static ProjectItem GetItem(this ProjectItem pro, string name)
        {
            foreach (ProjectItem projectItem in pro.ProjectItems)
            {
                var dname = projectItem.Name;
                if (dname == name)
                {
                    return projectItem;
                }
            }
            return null;
        }

        public static void CreateClass(this Project pro, string directory, string name, string content)
        {
            if (pro == null)
            {
                return;
            }
            var add = false;
            foreach (ProjectItem projectItem in pro.ProjectItems)
            {
                var dname = projectItem.Name;
                if (dname == directory)
                {
                    var item = projectItem.GetItem(name + ".cs");
                    if (item == null)
                    {
                        string templatePath =
                            GetSolution3().GetProjectItemTemplate("Class.zip", "CSharp");
                        projectItem.ProjectItems.AddFromTemplate(templatePath, name + ".cs");
                        TextSelection txtSel = (TextSelection)Application.ActiveDocument.Selection;
                        txtSel.SelectAll();
                        txtSel.Delete();
                        txtSel.Insert(content);
                        add = true;
                    }
                    else
                    {
                        item.Open();
                        TextSelection txtSel = (TextSelection)Application.ActiveDocument.Selection;
                        txtSel.SelectAll();
                        txtSel.Delete();
                        txtSel.Insert(content);
                        add = true;
                    }

                }
            }

            if (!add)
            {
                pro.ProjectItems.AddFolder(directory);
                var projectItem = pro.GetItem(directory);
                string templatePath =
       GetSolution3().GetProjectItemTemplate("Class.zip", "CSharp");
                projectItem.ProjectItems.AddFromTemplate(templatePath, name + ".cs");
                TextSelection txtSel = (TextSelection)Application.ActiveDocument.Selection;

                txtSel.SelectAll();
                txtSel.Delete();
                txtSel.Insert(content);
                add = true;
            }
        }


    }
}
