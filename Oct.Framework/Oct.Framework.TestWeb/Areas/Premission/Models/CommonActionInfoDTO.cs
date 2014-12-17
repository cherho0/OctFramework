using System;
using System.ComponentModel.DataAnnotations;
using Oct.Framework.TestWeb.Code;

namespace Oct.Framework.TestWeb.Areas.Premission.Models
{
	[Serializable]
	public partial class CommonActionInfoDTO
	{ 
		#region	属性
		
		/// <summary>
		/// 
		/// </summary>
		[Required(ErrorMessage = "不能为空！")]
		public Guid Id
		{
			get;
			set;
		}
		
		/// <summary>
		/// 分类名称
		/// </summary>
		[Display(Name = "分类名称")]
		[StringLength(50, ErrorMessage = "分类名称不能超过50个字符！")]
		public string CategoryName
		{
			get;
			set;
		}
		
		/// <summary>
		/// 
		/// </summary>
		[Required(ErrorMessage = "不能为空！")]
		[StringLength(50, ErrorMessage = "不能超过50个字符！")]
		public string Name
		{
			get;
			set;
		}
		
		/// <summary>
		/// 
		/// </summary>
		[Required(ErrorMessage = "不能为空！")]
		[StringLength(100, ErrorMessage = "不能超过100个字符！")]
		public string Url
		{
			get;
			set;
		}
		
		/// <summary>
		/// 
		/// </summary>
		[Required(ErrorMessage = "不能为空！")]
		public bool IsEnable
		{
			get;
			set;
		}
		
		/// <summary>
		/// 
		/// </summary>
		[Required(ErrorMessage = "不能为空！")]
		public bool IsVisible
		{
			get;
			set;
		}
		
		/// <summary>
		/// 
		/// </summary>
		[Required(ErrorMessage = "不能为空！")]
		public bool IsLog
		{
			get;
			set;
		}
		
		/// <summary>
		/// //浏览        View =1,        //更新        Update =2,        //删除        Delete =3,        //增加        Add =4,        //导出        Export =5,        //导入        Import = 6,
		/// </summary>
		[Required(ErrorMessage = "//浏览        View =1,        //更新        Update =2,        //删除        Delete =3,        //增加        Add =4,        //导出        Export =5,        //导入        Import = 6,不能为空！")]
        public OperationEnum Operation
		{
			get;
			set;
		}
		
		/// <summary>
		/// 
		/// </summary>
		[Required(ErrorMessage = "不能为空！")]
		public int Sort
		{
			get;
			set;
		}
		
		/// <summary>
		/// 
		/// </summary>
		[Required(ErrorMessage = "不能为空！")]
		public DateTime CreateDate
		{
			get;
			set;
		}
		
		/// <summary>
		/// 
		/// </summary>
		public DateTime ModifyDate
		{
			get;
			set;
		}
		
		/// <summary>
		/// 
		/// </summary>
		[Required(ErrorMessage = "不能为空！")]
		public Guid MenuId
		{
			get;
			set;
		}
		
		#endregion	
	}
}