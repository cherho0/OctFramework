﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本: 11.0.0.0
//  
//     对此文件的更改可能会导致不正确的行为。此外，如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Oct.Tools.Host.Res.Temp.View
{
    using System.Linq;
    using System.Text;
    using Oct.Tools.Plugin.CodeGenerator.Entity;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\project\Oct.Frame\Oct.Tools\Oct.Tools.Host\Res\Temp\View\Edit.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "11.0.0.0")]
    public partial class Edit : EditBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write(@"@{
    ViewBag.Title = ""Edit"";
    Layout = ""~/Views/Shared/_Modal.cshtml"";
}

@section PluginsJS {
    <script src=""~/assets/global/plugins/jquery-validation/js/jquery.validate.min.js""></script>
    <script src=""~/assets/global/plugins/jquery-validation/js/additional-methods.min.js""></script>
}

@using ");
            
            #line 16 "D:\project\Oct.Frame\Oct.Tools\Oct.Tools.Host\Res\Temp\View\Edit.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(dt.NameSpace));
            
            #line default
            #line hidden
            this.Write(".Models;\r\n\r\n@model ");
            
            #line 18 "D:\project\Oct.Frame\Oct.Tools\Oct.Tools.Host\Res\Temp\View\Edit.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(dt.ClassName));
            
            #line default
            #line hidden
            this.Write("DTO\r\n\r\n<div class=\"container\">\r\n    @using (Html.BeginForm(\"Edit\", \"");
            
            #line 21 "D:\project\Oct.Frame\Oct.Tools\Oct.Tools.Host\Res\Temp\View\Edit.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(dt.ClassName));
            
            #line default
            #line hidden
            this.Write("\", FormMethod.Post, new { @class = \"J_FormValidate\" }))\r\n    {\r\n        <div clas" +
                    "s=\"form form-horizontal\">\r\n");
            
            #line 24 "D:\project\Oct.Frame\Oct.Tools\Oct.Tools.Host\Res\Temp\View\Edit.tt"

	foreach(FiledInfo filed in dt.FiledList) 
	{		

            
            #line default
            #line hidden
            
            #line 28 "D:\project\Oct.Frame\Oct.Tools\Oct.Tools.Host\Res\Temp\View\Edit.tt"
 if (filed.IsPk) { 
            
            #line default
            #line hidden
            this.Write("            @Html.HiddenFor(d => d.");
            
            #line 29 "D:\project\Oct.Frame\Oct.Tools\Oct.Tools.Host\Res\Temp\View\Edit.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(filed.Name));
            
            #line default
            #line hidden
            this.Write(")\t\t \r\n\r\n");
            
            #line 31 "D:\project\Oct.Frame\Oct.Tools\Oct.Tools.Host\Res\Temp\View\Edit.tt"
 } else { 
            
            #line default
            #line hidden
            this.Write("            <div class=\"form-group\">\r\n\t\t\t");
            
            #line 33 "D:\project\Oct.Frame\Oct.Tools\Oct.Tools.Host\Res\Temp\View\Edit.tt"
 if (filed.IsAllowNull) { 
            
            #line default
            #line hidden
            this.Write("\t\r\n                <label class=\"col-xs-3 control-label\"><i class=\"fa fa-asterisk" +
                    " required\"></i>\r\n\t\t\t\t");
            
            #line 35 "D:\project\Oct.Frame\Oct.Tools\Oct.Tools.Host\Res\Temp\View\Edit.tt"
 } else { 
            
            #line default
            #line hidden
            this.Write("\t\t\t\t <label class=\"col-xs-3 control-label\">\r\n\t\t\t\t ");
            
            #line 37 "D:\project\Oct.Frame\Oct.Tools\Oct.Tools.Host\Res\Temp\View\Edit.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\t\t\t\t");
            
            #line 38 "D:\project\Oct.Frame\Oct.Tools\Oct.Tools.Host\Res\Temp\View\Edit.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(string.IsNullOrEmpty(filed.Description) ? filed.Name : filed.Description));
            
            #line default
            #line hidden
            this.Write("</label>\r\n                <div class=\"col-xs-5\">\r\n");
            
            #line 40 "D:\project\Oct.Frame\Oct.Tools\Oct.Tools.Host\Res\Temp\View\Edit.tt"
 if (filed.IsAllowNull) { 
            
            #line default
            #line hidden
            this.Write("\t \r\n                    @Html.TextBoxFor(p => p.");
            
            #line 41 "D:\project\Oct.Frame\Oct.Tools\Oct.Tools.Host\Res\Temp\View\Edit.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(filed.Name));
            
            #line default
            #line hidden
            this.Write(", new { @class = \"form-control\" })\r\n");
            
            #line 42 "D:\project\Oct.Frame\Oct.Tools\Oct.Tools.Host\Res\Temp\View\Edit.tt"
 } else { 
            
            #line default
            #line hidden
            this.Write("                    @Html.TextBoxFor(p => p.");
            
            #line 43 "D:\project\Oct.Frame\Oct.Tools\Oct.Tools.Host\Res\Temp\View\Edit.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(filed.Name));
            
            #line default
            #line hidden
            this.Write(", new { @class = \"form-control required\" })\r\n");
            
            #line 44 "D:\project\Oct.Frame\Oct.Tools\Oct.Tools.Host\Res\Temp\View\Edit.tt"
 } 
            
            #line default
            #line hidden
            this.Write("                </div>\r\n                <label class=\"col-xs-4 help-inline J_Vali" +
                    "dateMsg\"></label>\r\n            </div>\r\n");
            
            #line 48 "D:\project\Oct.Frame\Oct.Tools\Oct.Tools.Host\Res\Temp\View\Edit.tt"
 } 
            
            #line default
            #line hidden
            
            #line 49 "D:\project\Oct.Frame\Oct.Tools\Oct.Tools.Host\Res\Temp\View\Edit.tt"
 } 
            
            #line default
            #line hidden
            this.Write(@"
            <div class=""form-group"">
                <div class=""col-xs-9 col-xs-offset-3"">
                    <button type=""submit"" value=""编辑"" class=""btn blue""><i class=""fa fa-plus""></i>&nbsp;编辑</button>
					 <a href=""/User/Home"" class=""btn default J_CloseModal""><i class=""fa fa-undo""></i>&nbsp;返  回</a>
                </div>
            </div>
        </div>
    }
</div>
");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 1 "D:\project\Oct.Frame\Oct.Tools\Oct.Tools.Host\Res\Temp\View\Edit.tt"

private global::Oct.Tools.Plugin.CodeGenerator.Entity.CodeBaseInfo _dtField;

/// <summary>
/// Access the dt parameter of the template.
/// </summary>
private global::Oct.Tools.Plugin.CodeGenerator.Entity.CodeBaseInfo dt
{
    get
    {
        return this._dtField;
    }
}


/// <summary>
/// Initialize the template
/// </summary>
public virtual void Initialize()
{
    if ((this.Errors.HasErrors == false))
    {
bool dtValueAcquired = false;
if (this.Session.ContainsKey("dt"))
{
    if ((typeof(global::Oct.Tools.Plugin.CodeGenerator.Entity.CodeBaseInfo).IsAssignableFrom(this.Session["dt"].GetType()) == false))
    {
        this.Error("参数“dt”的类型“Oct.Tools.Plugin.CodeGenerator.Entity.CodeBaseInfo”与传递到模板的数据的类型不匹配。");
    }
    else
    {
        this._dtField = ((global::Oct.Tools.Plugin.CodeGenerator.Entity.CodeBaseInfo)(this.Session["dt"]));
        dtValueAcquired = true;
    }
}
if ((dtValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("dt");
    if ((data != null))
    {
        if ((typeof(global::Oct.Tools.Plugin.CodeGenerator.Entity.CodeBaseInfo).IsAssignableFrom(data.GetType()) == false))
        {
            this.Error("参数“dt”的类型“Oct.Tools.Plugin.CodeGenerator.Entity.CodeBaseInfo”与传递到模板的数据的类型不匹配。");
        }
        else
        {
            this._dtField = ((global::Oct.Tools.Plugin.CodeGenerator.Entity.CodeBaseInfo)(data));
        }
    }
}


    }
}


        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "11.0.0.0")]
    public class EditBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}