using System;
using System.ComponentModel.DataAnnotations;

namespace Oct
{
	[Serializable]
	public partial class UserInfoDTO
	{ 
		#region	属性
		
		/// <summary>
		/// Id
		/// </summary>
		[Display(Name = "Id")]
		[Required(ErrorMessage = "Id不能为空！")]
		public Guid PkId
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
		public string Name
		{
			get;
			set;
		}
		
		/// <summary>
		/// 性别
		/// </summary>
		[Display(Name = "性别")]
		public int Sex
		{
			get;
			set;
		}
		
		/// <summary>
		/// 手机
		/// </summary>
		[Display(Name = "手机")]
		[StringLength(20, ErrorMessage = "手机不能超过20个字符！")]
		public string Mobile
		{
			get;
			set;
		}
		
		/// <summary>
		/// 工资
		/// </summary>
		[Display(Name = "工资")]
		public decimal Salary
		{
			get;
			set;
		}
		
		/// <summary>
		/// 创建时间
		/// </summary>
		[Display(Name = "创建时间")]
		[Required(ErrorMessage = "创建时间不能为空！")]
		public DateTime CreateTime
		{
			get;
			set;
		}
		
		/// <summary>
		/// 修改时间
		/// </summary>
		[Display(Name = "修改时间")]
		public DateTime UpdateTime
		{
			get;
			set;
		}
		
		/// <summary>
		/// BigintFiled
		/// </summary>
		[Display(Name = "BigintFiled")]
		public long BigintFiled
		{
			get;
			set;
		}
		
		/// <summary>
		/// BinaryFiled
		/// </summary>
		[Display(Name = "BinaryFiled")]
		public byte[] BinaryFiled
		{
			get;
			set;
		}
		
		/// <summary>
		/// BitFiled
		/// </summary>
		[Display(Name = "BitFiled")]
		public bool BitFiled
		{
			get;
			set;
		}
		
		/// <summary>
		/// CharFiled
		/// </summary>
		[Display(Name = "CharFiled")]
		[Required(ErrorMessage = "CharFiled不能为空！")]
		[StringLength(10, ErrorMessage = "CharFiled不能超过10个字符！")]
		public string CharFiled
		{
			get;
			set;
		}
		
		/// <summary>
		/// FloatFiled
		/// </summary>
		[Display(Name = "FloatFiled")]
		public double FloatFiled
		{
			get;
			set;
		}
		
		/// <summary>
		/// ImageFiled
		/// </summary>
		[Display(Name = "ImageFiled")]
		public byte[] ImageFiled
		{
			get;
			set;
		}
		
		/// <summary>
		/// NumericFiled
		/// </summary>
		[Display(Name = "NumericFiled")]
		[Required(ErrorMessage = "NumericFiled不能为空！")]
		public decimal NumericFiled
		{
			get;
			set;
		}
		
		/// <summary>
		/// TimestampFiled
		/// </summary>
		[Display(Name = "TimestampFiled")]
		public byte[] TimestampFiled
		{
			get;
			set;
		}
		
		/// <summary>
		/// TinyintFiled
		/// </summary>
		[Display(Name = "TinyintFiled")]
		public byte TinyintFiled
		{
			get;
			set;
		}
		
		#endregion	
	}
}