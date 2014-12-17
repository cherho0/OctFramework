using System;
using System.ComponentModel.DataAnnotations;

namespace Oct.Framework.TestWeb.Models
{
    [Serializable]
    public partial class TestTsDTO
    {
        #region	属性

        public int Id
        {
            get;
            set;
        }

        [Display(Name = "DD")]
        [Required(ErrorMessage = "DD不能为空！")]
        [StringLength(50, ErrorMessage = "DD不能超过50个字符！")]
        public string DD
        {
            get;
            set;
        }

        #endregion
    }
}