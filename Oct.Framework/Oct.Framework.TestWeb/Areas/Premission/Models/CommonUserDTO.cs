using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Oct.Framework.TestWeb.Areas.Premission.Models
{
	[Serializable]
	public partial class CommonUserDTO
	{ 
		#region	属性
		
		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "Id")]
		public Guid Id
		{
			get;
			set;
		}
		
		/// <summary>
		/// 用户名
		/// </summary>
		[Display(Name = "用户名")]
		[Required(ErrorMessage = "用户名不能为空！")]
		[StringLength(50, ErrorMessage = "用户名不能超过50个字符！")]
		public string Account
		{
			get;
			set;
		}
		
		/// <summary>
		/// 姓名
		/// </summary>
		[Display(Name = "姓名")]
		[Required(ErrorMessage = "姓名不能为空！")]
		[StringLength(50, ErrorMessage = "姓名不能超过50个字符！")]
		public string UserName
		{
			get;
			set;
		}
		
		/// <summary>
		/// 密码
		/// </summary>
		[Display(Name = "密码")]
		public string Password
		{
			get;
			set;
		}
		
		/// <summary>
		/// 状态
		/// </summary>
		[Display(Name = "状态")]
		[Required(ErrorMessage = "状态不能为空！")]
		public int Status
		{
			get;
			set;
		}
		
		/// <summary>
		/// 创建时间
		/// </summary>
		[Display(Name = "创建时间")]
		public DateTime CreateDate
		{
			get;
			set;
		}
		
		/// <summary>
		/// 创建人ID
		/// </summary>
		[Display(Name = "创建人ID")]
		public Guid CreateUserId
		{
			get;
			set;
		}
		
		/// <summary>
		/// 修改人ID
		/// </summary>
		[Display(Name = "修改人ID")]
		public Guid ModifyUserId
		{
			get;
			set;
		}
		
		/// <summary>
		/// 最后修改时间
		/// </summary>
		[Display(Name = "最后修改时间")]
		public DateTime ModifyDate
		{
			get;
			set;
		}
		
		/// <summary>
		/// 电话
		/// </summary>
		[Display(Name = "电话")]
		[StringLength(50, ErrorMessage = "电话不能超过50个字符！")]
		public string Phone
		{
			get;
			set;
		}
		
		/// <summary>
		/// 邮箱
		/// </summary>
		[Display(Name = "邮箱")]
		[StringLength(100, ErrorMessage = "邮箱不能超过100个字符！")]
		public string Email
		{
			get;
			set;
		}
		
		/// <summary>
		/// 传真
		/// </summary>
		[Display(Name = "传真")]
		[StringLength(50, ErrorMessage = "传真不能超过50个字符！")]
		public string Fax
		{
			get;
			set;
		}
		
		/// <summary>
		/// QQ
		/// </summary>
		[Display(Name = "QQ")]
		[StringLength(50, ErrorMessage = "QQ不能超过50个字符！")]
		public string QQ
		{
			get;
			set;
		}
		
		/// <summary>
		/// 地址
		/// </summary>
		[Display(Name = "地址")]
		[StringLength(100, ErrorMessage = "地址不能超过100个字符！")]
		public string Address
		{
			get;
			set;
		}
		
		/// <summary>
		/// 性别
		/// </summary>
		[Display(Name = "性别")]
		public int Gander
		{
			get;
			set;
		}
		
		/// <summary>
		/// 手机
		/// </summary>
		[Display(Name = "手机")]
		[StringLength(50, ErrorMessage = "手机不能超过50个字符！")]
		public string Mobile
		{
			get;
			set;
		}
		
		/// <summary>
		/// 身份证号码
		/// </summary>
		[Display(Name = "身份证号码")]
		[StringLength(50, ErrorMessage = "身份证号码不能超过50个字符！")]
		public string IDNumber
		{
			get;
			set;
		}
		
		/// <summary>
		/// 头像
		/// </summary>
		[Display(Name = "头像")]
		[StringLength(200, ErrorMessage = "头像不能超过200个字符！")]
		public string Avatar
		{
			get;
			set;
		}
		
		/// <summary>
		/// 最后登录时间
		/// </summary>
		[Display(Name = "最后登录时间")]
		public DateTime LastLoginDate
		{
			get;
			set;
		}
		
		/// <summary>
		/// 登录次数
		/// </summary>
		[Display(Name = "登录次数")]
		public int LoginCount
		{
			get;
			set;
		}
		
		#endregion	
	}
}