using Microsoft.VisualStudio.TextTemplating;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Oct.Tools.Core.T4
{
    public class CustomTextTemplatingEngineHost : ITextTemplatingEngineHost, ITextTemplatingSessionHost
    {
        #region 变量

        private string[] _nameSpaces;

        #endregion

        #region 构造函数

        public CustomTextTemplatingEngineHost(string[] nameSpaces)
        {
            this._nameSpaces = nameSpaces;
        }

        #endregion

        #region ITextTemplatingEngineHost 成员

        internal string templateFileValue;
        public string TemplateFile
        {
            get { return this.templateFileValue; }
        }

        private string _fileExtensionValue = ".txt";
        public string FileExtension
        {
            get { return this._fileExtensionValue; }
        }

        private Encoding _fileEncodingValue = Encoding.UTF8;
        public Encoding FileEncoding
        {
            get { return this._fileEncodingValue; }
        }

        private CompilerErrorCollection _errorsValue;
        public CompilerErrorCollection Errors
        {
            get { return this._errorsValue; }
        }

        public IList<string> StandardAssemblyReferences
        {
            get
            {
                return new string[]
                {
                    typeof(Queryable).Assembly.Location,
                    typeof(XmlDocument).Assembly.Location,
                    typeof(Uri).Assembly.Location
                };
            }
        }

        public IList<string> StandardImports
        {
            get
            {
                return this._nameSpaces;
            }
        }

        public bool LoadIncludeText(string requestFileName, out string content, out string location)
        {
            content = string.Empty;
            location = string.Empty;

            if (File.Exists(requestFileName))
            {
                content = File.ReadAllText(requestFileName);

                return true;
            }
            else
                return false;
        }

        public object GetHostOption(string optionName)
        {
            object returnObject;

            switch (optionName)
            {
                case "CacheAssemblies":
                    returnObject = true;
                    break;

                default:
                    returnObject = null;
                    break;
            }

            return returnObject;
        }

        public string ResolveAssemblyReference(string assemblyReference)
        {
            if (File.Exists(assemblyReference))
                return assemblyReference;

            string candidate = Path.Combine(Path.GetDirectoryName(this.TemplateFile), assemblyReference);

            if (File.Exists(candidate))
                return candidate;

            return string.Empty;
        }

        public Type ResolveDirectiveProcessor(string processorName)
        {
            if (string.Compare(processorName, "XYZ", StringComparison.OrdinalIgnoreCase) == 0)
            {
                //return typeof();
            }

            throw new Exception("Directive Processor not found");
        }

        public string ResolvePath(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException("the file name cannot be null");

            if (File.Exists(fileName))
                return fileName;

            string candidate = Path.Combine(Path.GetDirectoryName(this.TemplateFile), fileName);

            if (File.Exists(candidate))
                return candidate;

            return fileName;
        }

        public string ResolveParameterValue(string directiveId, string processorName, string parameterName)
        {
            if (directiveId == null)
                throw new ArgumentNullException("the directiveId cannot be null");

            if (processorName == null)
                throw new ArgumentNullException("the processorName cannot be null");

            if (parameterName == null)
                throw new ArgumentNullException("the parameterName cannot be null");

            return String.Empty;
        }

        public void SetFileExtension(string extension)
        {
            this._fileExtensionValue = extension;
        }

        public void SetOutputEncoding(Encoding encoding, bool fromOutputDirective)
        {
            this._fileEncodingValue = encoding;
        }

        public void LogErrors(CompilerErrorCollection errors)
        {
            this._errorsValue = errors;
        }

        public AppDomain ProvideTemplatingAppDomain(string content)
        {
            return AppDomain.CreateDomain("Generation App Domain");
        }

        #endregion

        #region ITextTemplatingSessionHost 成员

        public ITextTemplatingSession CreateSession()
        {
            return this.Session;
        }

        public ITextTemplatingSession Session
        {
            get;
            set;
        }

        #endregion
    }
}
