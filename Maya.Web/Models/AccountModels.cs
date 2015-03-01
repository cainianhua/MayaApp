using Maya.Services;
using Maya.Services.BO;
using Maya.Services.VO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Maya.Web.Models
{
    public class LoginModel
    {
        [Display(Name = "登录名")]
        [Required(ErrorMessage = "请输入{0}。")]
        public string LoginName { get; set; }

        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "请输入{0}。")]
        public string Password { get; set; }

		[Display(Name = "验证码")]
		[Required(ErrorMessage = "请输入{0}。")]
		[StringLength(4, MinimumLength = 4, ErrorMessage = "验证码长度不匹配")]
		public string ValidationCode { get; set; }

        [Display(Name = "记住我？")]
        public bool RememberMe { get; set; }
    }
}