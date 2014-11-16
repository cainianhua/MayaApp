using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maya.Services;
using Maya.Services.BO;
using Maya.Services.VO;
using Maya.Web.FilterAttributes;
using Maya.Web.Models;

namespace Maya.Web.Controllers
{
	[LoginChecker]
    public class UsersController : ControllerBase
    {
        // GET: Users
        public ActionResult Index()
        {
			List<UserVO> items = UserBO.GetInstance().GetItems();

            return View( items );
        }

        // GET: Users/Create
        public ActionResult Create()
        {
			UserRegisterModel item = new UserRegisterModel();
            return View( item );
        }

        // POST: Users/Create
        [HttpPost]
        public ActionResult Create(UserRegisterModel item)
        {
			if (ModelState.IsValid) {
				UserVO u = new UserVO();
				u.UserName = item.UserName;
				u.Email = item.Email;
				u.PasswordSalt = new Random().Next( 10000, 99999 ).ToString();
				u.Password = Utils.EncryptPassword( item.Password, u.PasswordSalt );
				u.ActionDate = DateTime.Now;
				u.ActionBy = UserContext.Current.User.UserName;

				try {
					u.UserId = UserBO.GetInstance().SaveOrUpdateItem( u );
				}
				catch(Exception ex) {
					ModelState.AddModelError( "", ex.Message );
				}

				if (ModelState.IsValid) {
					return RedirectToAction( "Index" );
				}
			}

			return View( item );
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Users/Edit/5
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

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
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
    }
}
