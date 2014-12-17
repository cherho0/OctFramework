using System;
using System.ComponentModel.DataAnnotations;

namespace Oct.Framework.TestWeb.Areas.Premission.Models
{
	[Serializable]
	public partial class CommonMenuInfoDTO
	{ 
		#region	属性
		
		/// <summary>
		/// 
		/// </summary>
		public Guid Id
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
		public bool IsAllowAnonymousAccess
		{
			get;
			set;
		}
		
		/// <summary>
		/// 
		/// </summary>
		public bool IsEnable
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
		
		#endregion	
	}
}