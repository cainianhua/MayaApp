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
                
                if (item == null || item.Password != EncryptPassword(model.Password, item.PasswordSalt))
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

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        // GET: Account/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Account/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Account/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        private string EncryptPassword(string pwd, string salt)
        {
            return Md5(Md5(pwd + salt));
        }
        /// <summary> 
        /// MD5加密 
        /// </summary> 
        /// <param name="str"></param> 
        /// <returns></returns> 
        public static string Md5(string str) 
        { 
            // Create a new instance of the MD5CryptoServiceProvider object. 
            MD5 md5Hasher = MD5.Create(); 
            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hasher.ComputeHash(Encoding.GetEncoding("UTF-8").GetBytes(str)); 
            
            //  Create a new Stringbuilder to collect the bytes 
            //  and create a string. 
            StringBuilder sBuilder = new StringBuilder(); 
            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++) 
            { 
                sBuilder.Append(data[i].ToString("x2")); 
            } 
            // Return the hexadecimal string. 
            return sBuilder.ToString(); 
        } 

        #endregion
    }
}
