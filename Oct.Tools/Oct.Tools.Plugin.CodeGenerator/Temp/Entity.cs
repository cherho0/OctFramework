﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本: 11.0.0.0
//  
//     对此文件的更改可能会导致不正确的行为。此外，如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Oct.Tools.Plugin.CodeGenerator.Temp
{
    using System.Linq;
    using System.Text;
    using Oct.Tools.Plugin.CodeGenerator.Entity;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "11.0.0.0")]
    public partial class Entity : EntityBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using Oct.Framework.DB.Base;\r\nusing Oct.Framework.DB.Core;\r\nusing Oct.Framework.D" +
                    "B.Interface;\r\nusing System;\r\nusing System.Collections.Generic;\r\nusing System.Dat" +
                    "a;\r\nusing System.Data.Common;\r\nusing System.Data.SqlClient;\r\nusing System.Text;\r" +
                    "\n\r\nnamespace ");
            
            #line 16 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(dt.NameSpace));
            
            #line default
            #line hidden
            this.Write(".Entities\r\n{\r\n\t[Serializable]\r\n\tpublic partial class ");
            
            #line 19 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(dt.ClassName));
            
            #line default
            #line hidden
            
            #line 19 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(dt.ClassNameExtend));
            
            #line default
            #line hidden
            this.Write(" : BaseEntity<");
            
            #line 19 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(dt.ClassName));
            
            #line default
            #line hidden
            
            #line 19 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(dt.ClassNameExtend));
            
            #line default
            #line hidden
            this.Write(">\r\n\t{ \r\n\t\t#region\t属性\r\n\t\t");
            
            #line 22 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"

			var pkName = string.Empty;
			var pkFileds = dt.FiledList.Where(d => d.IsPk);

			if (pkFileds.Count() > 0)
				pkName = pkFileds.First().Name;	 

			var identifyFileds = dt.FiledList.Where(d => d.IsIdentify);	

			foreach(FiledInfo filed in dt.FiledList) 
			{		
			var nullFlag = filed.CommonType.IsValueType && filed.IsAllowNull ? "?": string.Empty;
			var temp = "_" + filed.Name.Substring(0, 1).ToLower() + filed.Name.Substring(1, filed.Name.Length - 1); 
		
            
            #line default
            #line hidden
            this.Write("\r\n\t\tprivate ");
            
            #line 37 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(filed.CSharpType + nullFlag + " " + temp));
            
            #line default
            #line hidden
            this.Write(";\r\n\r\n\t\t/// <summary>\r\n\t\t/// ");
            
            #line 40 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(filed.GetDisplayName()));
            
            #line default
            #line hidden
            this.Write("\r\n\t\t/// </summary>\r\n\t\tpublic ");
            
            #line 42 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(filed.CSharpType + nullFlag + " " + filed.Name));
            
            #line default
            #line hidden
            this.Write("\r\n\t\t{\r\n\t\t\tget\r\n\t\t\t{\r\n\t\t\t\treturn this.");
            
            #line 46 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(temp));
            
            #line default
            #line hidden
            this.Write(";\r\n\t\t\t}\r\n\t\t\tset\r\n\t\t\t{\r\n\t\t\t\tthis.PropChanged(\"");
            
            #line 50 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(filed.Name));
            
            #line default
            #line hidden
            this.Write("\", this.");
            
            #line 50 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(temp));
            
            #line default
            #line hidden
            this.Write(", value);\r\n\r\n\t\t\t\tthis.");
            
            #line 52 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(temp));
            
            #line default
            #line hidden
            this.Write(" = value;\r\n\t\t\t}\r\n\t\t}\r\n\t\t");
            
            #line 55 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\r\n\t\t#endregion\r\n\r\n\t\t#region 重载\r\n\r\n\t\tpublic override object PkValue\r\n\t\t{\r\n\t\t\tget\r\n" +
                    "\t\t\t{\r\n\t\t\t\treturn this.");
            
            #line 65 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pkName));
            
            #line default
            #line hidden
            this.Write("; \r\n\t\t\t}\r\n\t\t}\r\n\r\n\t\tpublic override string PkName\r\n\t\t{\r\n\t\t\tget\r\n\t\t\t{\r\n\t\t\t\treturn \"" +
                    "");
            
            #line 73 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pkName));
            
            #line default
            #line hidden
            this.Write("\"; \r\n\t\t\t}\r\n\t\t}\r\n\r\n\t\tpublic override bool IsIdentityPk\r\n\t\t{\r\n\t\t\tget \r\n\t\t\t{\r\n\t\t\t\tre" +
                    "turn ");
            
            #line 81 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture((identifyFileds.Count() > 0).ToString().ToLower()));
            
            #line default
            #line hidden
            this.Write("; \r\n\t\t\t}\r\n\t\t}\r\n\r\n\t\t");
            
            #line 85 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
 if (identifyFileds.Count() > 0) { 
            
            #line default
            #line hidden
            this.Write("\t\tpublic override void SetIdentity(object v)\r\n\t\t{\r\n\t\t\tthis.");
            
            #line 88 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(identifyFileds.First().Name));
            
            #line default
            #line hidden
            this.Write(" = int.Parse(v.ToString());\r\n\t\t}\r\n\t\t");
            
            #line 90 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\r\n\t\tprivate Dictionary<string, string> _props;\r\n\r\n\t\tpublic override Dictionary<st" +
                    "ring, string> Props\r\n\t    {\r\n\t        get {\r\n\t\t\t\tif(_props == null)\r\n\t\t\t\t{\r\n\t\t\t\t" +
                    "\t_props = new Dictionary<string, string>();\r\n\t\t\t\t\t");
            
            #line 100 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
for (int i = 0; i < dt.FiledList.Count; i++){
					var filed = dt.FiledList[i];
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\t_props.Add( \"");
            
            #line 102 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(filed.Name));
            
            #line default
            #line hidden
            this.Write("\",\"");
            
            #line 102 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(filed.Name));
            
            #line default
            #line hidden
            this.Write("\");\r\n\t\t\t\t\t");
            
            #line 103 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
}
            
            #line default
            #line hidden
            this.Write("\t\t\t\t}\r\n\t\t\t\treturn _props;\t\t\t \r\n\t\t\t }\r\n\t    }\r\n\r\n\t\tpublic override ");
            
            #line 109 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(dt.ClassName));
            
            #line default
            #line hidden
            this.Write(" GetEntityFromDataRow(DataRow row)\r\n\t\t{\r\n\t\t\t");
            
            #line 111 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
		
				var code = new StringBuilder();

				for (int i = 0; i < dt.FiledList.Count; i++)
				{
					var filed = dt.FiledList[i];
					var type = filed.CSharpType.ToLower();		
					var temp = "_" + filed.Name.Substring(0, 1).ToLower() + filed.Name.Substring(1, filed.Name.Length - 1); 
					if (type == "bool")
					{
						code.AppendFormat("{0}if (row.Table.Columns.Contains(\"{1}\") && row[\"{1}\"] != null && row[\"{1}\"].ToString() != \"\")\r\n", i == 0 ? string.Empty : "\t\t\t", filed.Name);	
						code.Append("\t\t\t{\r\n");
						code.AppendFormat("\t\t\t\tif ((row[\"{0}\"].ToString() == \"1\") || (row[\"{0}\"].ToString().ToLower() == \"true\"))\r\n", filed.Name);
						code.Append("\t\t\t\t{\r\n");
						code.AppendFormat("\t\t\t\t\tthis.{0} = true;\r\n", temp);
						code.Append("\t\t\t\t}\r\n");
						code.Append("\t\t\t\telse\r\n");
						code.Append("\t\t\t\t{\r\n");
						code.AppendFormat("\t\t\t\t\tthis.{0} = false;\r\n",temp);
						code.Append("\t\t\t\t}\r\n");	
						code.Append("\t\t\t}\r\n");	
					}
					else if (type == "byte[]")
					{
						code.AppendFormat("{0}if (row.Table.Columns.Contains(\"{1}\") && row[\"{1}\"] != null && row[\"{1}\"].ToString() != \"\")\r\n", i == 0 ? string.Empty : "\t\t\t", filed.Name);	
						code.Append("\t\t\t{\r\n");
						code.AppendFormat("\t\t\t\tthis.{0} = (byte[])row[\"{1}\"];\r\n", temp,filed.Name);	
						code.Append("\t\t\t}\r\n");	
					}
					else if (type == "datetime")
					{
						code.AppendFormat("{0}if (row.Table.Columns.Contains(\"{1}\") && row[\"{1}\"] != null && row[\"{1}\"].ToString() != \"\")\r\n", i == 0 ? string.Empty : "\t\t\t", filed.Name);	
						code.Append("\t\t\t{\r\n");
						code.AppendFormat("\t\t\t\tthis.{0} = DateTime.Parse(row[\"{1}\"].ToString());\r\n",  temp,filed.Name);	
						code.Append("\t\t\t}\r\n");	
					}
					else if (type == "guid")
					{
						code.AppendFormat("{0}if (row.Table.Columns.Contains(\"{1}\") && row[\"{1}\"] != null && row[\"{1}\"].ToString() != \"\")\r\n", i == 0 ? string.Empty : "\t\t\t", filed.Name);	
						code.Append("\t\t\t{\r\n");
						code.AppendFormat("\t\t\t\tthis.{0} = new Guid(row[\"{1}\"].ToString());\r\n",  temp,filed.Name);	
						code.Append("\t\t\t}\r\n");	
					}
					else if (type == "string")
					{
						code.AppendFormat("{0}if (row.Table.Columns.Contains(\"{1}\") && row[\"{1}\"] != null)\r\n", i == 0 ? string.Empty : "\t\t\t", filed.Name);	
						code.Append("\t\t\t{\r\n");
						code.AppendFormat("\t\t\t\tthis.{0} = row[\"{1}\"].ToString();\r\n",  temp,filed.Name);	
						code.Append("\t\t\t}\r\n");	
					}
					else if (type == "decimal" || type == "double" || type == "float" || type == "int" || type == "long")
					{
						code.AppendFormat("{0}if (row.Table.Columns.Contains(\"{1}\") && row[\"{1}\"] != null && row[\"{1}\"].ToString() != \"\")\r\n", i == 0 ? string.Empty : "\t\t\t", filed.Name);	
						code.Append("\t\t\t{\r\n");
						code.AppendFormat("\t\t\t\tthis.{0} = {1}.Parse(row[\"{2}\"].ToString());\r\n", temp, type,filed.Name);	
						code.Append("\t\t\t}\r\n");	
					}
				}
			
            
            #line default
            #line hidden
            
            #line 170 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(code.ToString()));
            
            #line default
            #line hidden
            this.Write("\r\n\t\t\treturn this;\r\n\t\t}\r\n\r\n\t\tpublic override ");
            
            #line 174 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(dt.ClassName));
            
            #line default
            #line hidden
            this.Write(" GetEntityFromDataReader(IDataReader reader)\r\n        {\r\n            for (int i =" +
                    " 0; i < reader.FieldCount; i++)\r\n            {\r\n                var name = reade" +
                    "r.GetName(i);\r\n\t\t\t\t");
            
            #line 179 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"

				for (int i = 0; i < dt.FiledList.Count; i++)
				{
					var filed = dt.FiledList[i];
					var type = filed.CSharpType.ToLower();	
					var temp = "_" + filed.Name.Substring(0, 1).ToLower() + filed.Name.Substring(1, filed.Name.Length - 1); 
					
            
            #line default
            #line hidden
            this.Write("if (name.ToLower() == \"");
            
            #line 186 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(filed.Name.ToLower()));
            
            #line default
            #line hidden
            this.Write("\" && !reader.IsDBNull(i))\r\n{\r\n");
            
            #line 188 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
if(type == "bool")
{
            
            #line default
            #line hidden
            
            #line 190 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(temp));
            
            #line default
            #line hidden
            this.Write(" = reader.GetBoolean(i);\r\n");
            
            #line 191 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
}
            
            #line default
            #line hidden
            
            #line 192 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
if(type == "byte[]")
{
            
            #line default
            #line hidden
            
            #line 194 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(temp));
            
            #line default
            #line hidden
            this.Write(" = (byte[])reader.GetValue(i);\r\n");
            
            #line 195 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
}
            
            #line default
            #line hidden
            
            #line 196 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
if(type == "datetime")
{
            
            #line default
            #line hidden
            
            #line 198 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(temp));
            
            #line default
            #line hidden
            this.Write(" = reader.GetDateTime(i);\r\n");
            
            #line 199 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
}
            
            #line default
            #line hidden
            
            #line 200 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
if(type == "guid")
{
            
            #line default
            #line hidden
            
            #line 202 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(temp));
            
            #line default
            #line hidden
            this.Write(" = reader.GetGuid(i);\r\n");
            
            #line 203 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
}
            
            #line default
            #line hidden
            
            #line 204 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
if(type == "string")
{
            
            #line default
            #line hidden
            
            #line 206 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(temp));
            
            #line default
            #line hidden
            this.Write(" = reader.GetString(i);\r\n");
            
            #line 207 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
}
            
            #line default
            #line hidden
            
            #line 208 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
if(type == "decimal")
{
            
            #line default
            #line hidden
            
            #line 210 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(temp));
            
            #line default
            #line hidden
            this.Write(" = reader.GetDecimal(i);\r\n");
            
            #line 211 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
}
            
            #line default
            #line hidden
            
            #line 212 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
if(type == "double")
{
            
            #line default
            #line hidden
            
            #line 214 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(temp));
            
            #line default
            #line hidden
            this.Write(" = reader.GetDouble(i);\r\n");
            
            #line 215 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
}
            
            #line default
            #line hidden
            
            #line 216 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
if(type == "float")
{
            
            #line default
            #line hidden
            
            #line 218 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(temp));
            
            #line default
            #line hidden
            this.Write(" = reader.GetFloat(i);\r\n");
            
            #line 219 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
}
            
            #line default
            #line hidden
            
            #line 220 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
if(type == "int")
{
            
            #line default
            #line hidden
            
            #line 222 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(temp));
            
            #line default
            #line hidden
            this.Write(" = reader.GetInt32(i);\r\n");
            
            #line 223 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
}
            
            #line default
            #line hidden
            
            #line 224 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
if(type == "long")
{
            
            #line default
            #line hidden
            
            #line 226 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(temp));
            
            #line default
            #line hidden
            this.Write(" = reader.GetInt64(i);\r\n");
            
            #line 227 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
}
            
            #line default
            #line hidden
            this.Write(" continue;\r\n}\r\n");
            
            #line 230 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
}
            
            #line default
            #line hidden
            this.Write("               \r\n}\r\n            return this;\r\n        }\r\n\r\n\t\tpublic override stri" +
                    "ng TableName\r\n\t\t{\r\n\t\t\tget\r\n\t\t\t{\r\n\t\t\t\treturn \"");
            
            #line 239 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(dt.TableName));
            
            #line default
            #line hidden
            this.Write("\";\r\n\t\t\t}\r\n\t\t}\r\n\r\n\t\tpublic override IOctDbCommand GetInsertCmd()\r\n\t\t{\r\n\t\t\t");
            
            #line 245 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
			
				var sql1 = new StringBuilder(string.Format("\r\n\t\t\t\tINSERT INTO {0} (\r\n", dt.TableName));
				var sql2 = new StringBuilder("\r\n\t\t\t\tVALUES (\r\n");
				var paramStr = new StringBuilder("\t\t\tvar parameters = new Dictionary<string, object> {\r\n");	 
			
				var list = dt.FiledList.Where(d => !d.IsIdentify).ToList(); //排除标识列
				var count = list.Count;
			
				for (int i = 0; i < count; i++)
				{
					var isLast = i == count - 1;
					var filed = list[i];
				
					sql1.AppendFormat("\t\t\t\t\t{0}{1}", filed.Name, isLast ? ")" : ",\r\n");
					sql2.AppendFormat("\t\t\t\t\t@{0}{1}", filed.Name, isLast ? ")" : ",\r\n");
				
					paramStr.AppendFormat("\t\t\t\t{0}\"@{1}\", this.{1}{2}{3}", "{", filed.Name, "}", isLast ? "};" : ",\r\n");	 
				}
			
            
            #line default
            #line hidden
            this.Write("var sql = @\"");
            
            #line 264 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sql1.ToString() + sql2.ToString()));
            
            #line default
            #line hidden
            this.Write("\";\r\n\t\t\t\r\n\t\t\tDbCommand cmd = new SqlCommand();\r\n");
            
            #line 267 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(paramStr.ToString()));
            
            #line default
            #line hidden
            this.Write("\r\n\r\n\t\t\treturn new OctDbCommand(sql, parameters);\r\n\t\t}\r\n\r\n\t\tpublic override IOctDb" +
                    "Command GetUpdateCmd(string @where = \"\", IDictionary<string, object> paras = nul" +
                    "l)\r\n\t\t{\r\n\t\t\tvar sb = new StringBuilder();\r\n\t\t\tvar parameters = new Dictionary<st" +
                    "ring, object>();\r\n          \r\n\t\t\tsb.Append(\"update \" + this.TableName + \" set \")" +
                    ";\r\n         \r\n\t\t\tforeach (var changedProp in this.ChangedProps)\r\n\t\t\t{\r\n\t\t\t\tsb.Ap" +
                    "pend(string.Format(\"{0} = @{0},\", changedProp.Key));\r\n\r\n\t\t\t\tparameters.Add(\"@\" +" +
                    " changedProp.Key, changedProp.Value);\r\n\t\t\t}\r\n\r\n\t\t\tvar sql = sb.ToString().Remove" +
                    "(sb.Length - 1);\r\n\t\t\tsql += string.Format(\" where {0} = \'{1}\'\", this.PkName, thi" +
                    "s.PkValue);\r\n\r\n\t\t\tif (!string.IsNullOrEmpty(@where))\r\n\t\t\t\tsql += \" and \" + @wher" +
                    "e;\r\n\r\n\t\t\tif (paras != null)\r\n\t\t\t{\r\n\t\t\t\tforeach (var p in paras)\r\n\t\t\t\t{\r\n\t\t\t\t\tpar" +
                    "ameters.Add(p.Key, p.Value);\r\n\t\t\t\t}\r\n\t\t\t}\r\n\r\n\t\t\treturn new OctDbCommand(sql, par" +
                    "ameters);\r\n\t\t}\r\n\r\n\t\tpublic override string GetDelSQL()\r\n\t\t{\r\n\t\t\treturn string.Fo" +
                    "rmat(\"delete from {0} where {1} = \'{2}\'\", this.TableName, this.PkName, this.PkVa" +
                    "lue);\r\n\t\t}\r\n\r\n\t\tpublic override string GetDelSQL(object v, string @where = \"\")\r\n" +
                    "\t\t{\r\n\t\t\tstring sql = string.Format(\"delete from {0} where 1 = 1 \", this.TableNam" +
                    "e);\r\n         \r\n\t\t\tif (v != null)\r\n\t\t\t\tsql += \"and \" + this.PkName + \" = \'\" + v " +
                    "+ \"\'\";\r\n         \r\n\t\t\tif (!string.IsNullOrEmpty(@where))\r\n\t\t\t\tsql += \"and \" + @w" +
                    "here;\r\n\r\n\t\t\treturn sql;\r\n\t\t}\r\n\r\n\t\tpublic override string GetModelSQL(object v)\r\n" +
                    "\t\t{\r\n\t\t\treturn string.Format(\"select * from {0} where {1} = \'{2}\'\", this.TableNa" +
                    "me, this.PkName, v);\r\n\t\t}\r\n\r\n\t\tpublic override string GetQuerySQL(string @where " +
                    "= \"\")\r\n\t\t{\r\n\t\t\tvar sql = string.Format(\"select * from {0} where 1 = 1 \", this.Ta" +
                    "bleName);\r\n           \r\n\t\t\tif (!string.IsNullOrEmpty(@where))\r\n\t\t\t\tsql += \"and \"" +
                    " + @where;\r\n\r\n\t\t\treturn sql;\r\n\t\t}\r\n\r\n\t\t#endregion\r\n\t}\r\n}");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 1 "D:\Work\Code\OCT_Framework\Oct.Tools\Oct.Tools.Plugin.CodeGenerator\Temp\Entity.tt"

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
    public class EntityBase
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
