using Maya.Services.BO;
using Maya.Services.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Maya
{
    public class UserContext
    {
        private const string KEY_BASE = "MAYA_APP+";
        private const string LOGIN_COOKIE_NAME = "MAYA_USER_TOKEN";
        private UserVO user = null;
        /// <summary>
        /// 一个请求内多次创建对象，获取到的UserVO是一样的
        /// </summary>
        private UserContext()
        {
            long userId = GetUserId();
            if (userId > 0)
            {
                string cacheKey = KEY_BASE + userId.ToString();
                user = GetUserFromContext(cacheKey);

                if (user == null)
                {
                    user = UserBO.GetInstance().GetItem(userId);
                    SaveToContext(user, cacheKey);
                }
            }
        }

        private UserVO GetUserFromContext(string key)
        {
            if (HttpContext.Current == null)
                return null;

            return (UserVO)HttpContext.Current.Items[key];
        }

        private void SaveToContext(object obj, string key)
        {
            if (obj != null && HttpContext.Current != null)
                HttpContext.Current.Items[key] = obj;
        }

        private long GetUserId()
        {
            if (HttpContext.Current.Items["UserIdentity"] != null)
            {
                return (long)HttpContext.Current.Items["UserIdentity"];
            }
            else
            {
                long userId = 0;
                HttpCookie loginCookie = HttpContext.Current.Request.Cookies[LOGIN_COOKIE_NAME];
                if (loginCookie != null)
                {
                    if (!long.TryParse(Crypto.DecryptDesFromWeb(loginCookie.Values["uid"]), out userId))
                    {
                        userId = 0;
                    }
                }

                if (userId > 0)
                {
                    HttpContext.Current.Items["UserIdentity"] = userId;
                }

                return userId;
            }
        }

        /// <summary>
        /// Gets the current context for the user using the cache.
        /// </summary>
        /// <returns></returns>
        public static UserContext Current
        {
            get
            {
                return new UserContext();
            }
        }

        /// <summary>
        /// </summary>
        public UserVO User
        {
            get { return user; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsAuthenticated()
        {
            HttpCookie loginCookie = HttpContext.Current.Request.Cookies[LOGIN_COOKIE_NAME];
            return loginCookie != null;
        }
    }
}
