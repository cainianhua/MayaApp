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
    public class RegisterModel
    {
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "{0}是必填信息。")]
        [StringLength(30, ErrorMessage = "{0}不少于{2}个字符。", MinimumLength = 2)]
        [RegularExpression(@"^[\u4e00-\u9fa5\w]*$", ErrorMessage = "{0}包含非法字符。")]
        [UniqueUserName(ErrorMessage = "{0}已经存在，请重新选择。")]
        public string UserName { get; set; }

        [Display(Name = "电子邮箱")]
        [Required(ErrorMessage = "{0}是必填信息。")]
        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "{0}格式不正确。")]
        [UniqueEmail(ErrorMessage = "{0}已经存在，请重新选择。")]
        public string Email { get; set; }

        [Display(Name = "密码")]
        [Required(ErrorMessage = "请输入{0}。")]
        [StringLength(20, ErrorMessage = "{0}长度必须在{2}与{1}之间。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "确认密码")]
        [Required(ErrorMessage = "请输入{0}。")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "密码和{0}不一致。")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Display(Name = "登录名")]
        [Required(ErrorMessage = "请输入{0}。")]
        public string UserName { get; set; }

        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "请输入{0}。")]
        public string Password { get; set; }

        [Display(Name = "记住我？")]
        public bool RememberMe { get; set; }
    }

    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "请输入{0}。")]
        [DataType(DataType.Password)]
        [Display(Name = "当前密码")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "请输入{0}。")]
        [StringLength(20, ErrorMessage = "{0}长度必须在{2}与{1}之间。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认新密码")]
        [Compare("NewPassword", ErrorMessage = "新密码和{0}不一致。")]
        public string ConfirmPassword { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UniqueUserNameAttribute : ValidationAttribute
    {
        public override Boolean IsValid(Object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString())) return false;

            UserVO item = UserBO.GetInstance().GetItemByUserName(value.ToString());
            return item == null;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UniqueEmailAttribute : ValidationAttribute
    {
        public override Boolean IsValid(Object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString())) return false;

            UserVO item = UserBO.GetInstance().GetItemByEmail(value.ToString());
            return item == null;
        }
    }
}