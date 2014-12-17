using System;
using System.ComponentModel.DataAnnotations;

namespace Oct.Framework.TestWeb.Areas.Premission.Models
{
	[Serializable]
	public partial class CommonUserRoleDTO
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
		public Guid UserId
		{
			get;
			set;
		}
		
		/// <summary>
		/// 
		/// </summary>
		public Guid RoleId
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