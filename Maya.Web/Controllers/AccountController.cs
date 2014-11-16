using System;
using System.Web;
using System.Web.Mvc;
using Maya.Web.Models;
using Maya.Web.FilterAttributes;
using Maya.Services.VO;
using System.Globalization;
using Maya.Services.BO;
using System.Security.Cryptography;
using System.Text;

namespace Maya.Web.Controllers
{
    [LoginChecker]
    public class AccountController : ControllerBase
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            UserVO usr = UserContext.Current.User;
            LoginModel item = new LoginModel();
            if (usr != null) {
                item.LoginName = usr.UserName;
            }

            return View(item);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                UserVO item = UserBO.GetInstance().GetItemByUserName(model.LoginName);
                
                if (item == null || item.Password != Utils.EncryptPassword(model.Password, item.PasswordSalt))
                {
                    ModelState.AddModelError("Password", "用户名或者密码不正确");
                }
                else
                {
                    CreateLoginCookie(item, model.RememberMe);

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Dashboard");
                }
            }
            return View(model);
        }

        #region 私有方法
        /// <summary>
        /// Create cookie
        /// </summary>
        /// <param name="value">cookie value of uid</param>
        private void CreateLoginCookie(UserVO user, bool rememberMe)
        {
            DateTime startDate = new DateTime(1970, 01, 01);
            DateTime endDate = DateTime.Now.AddDays(30);

            HttpCookie cookie = new HttpCookie("MAYA_USER_TOKEN");
            cookie["exp"] = endDate.Subtract(startDate).Days.ToString(CultureInfo.InvariantCulture);
            cookie["uid"] = Crypto.EncryptDesForWeb(user.UserId.ToString());
            cookie["uname"] = HttpUtility.UrlEncode(user.UserName);
            cookie["digest"] = Crypto.EncryptHmacSha1(cookie["exp"] + cookie["uid"] + cookie["uname"]);
            if (rememberMe) cookie.Expires = endDate;
            Response.Cookies.Add(cookie);
        }
        /// <summary>
        /// Delete cookie
        /// </summary>
        private void DeleteLoginCookie()
        {
            HttpCookie hc = new HttpCookie("MAYA_USER_TOKEN");
            hc.Expires = DateTime.Now.AddHours(-24);
            Response.Cookies.Add(hc);
        }
        /// <summary>
        /// Validate if cookie was changed by client user.
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        private bool AuthenticateCookie(HttpCookie cookie)
        {
            return true;
        }

        #endregion
    }
}
