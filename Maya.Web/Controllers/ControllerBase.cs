using Maya.Services.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maya.Web.Controllers
{
    public abstract class ControllerBase : JsonpController
	{
        //private UserVO _UserVO;
        ///// <summary>
        ///// 
        ///// </summary>
        //protected UserVO CurrentUser
        //{
        //    get
        //    {
        //        if (_UserVO == null)
        //        {
        //            _UserVO = UserContext.Current.User;
        //        }
        //        return _UserVO;
        //    }
        //}
		protected virtual int PageSize {
			get {
				return 10;
			}
		}
    }
}