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
		[ValidateAntiForgeryToken]
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
        public ActionResult Edit(long? id)
        {
			UserEditModel item = new UserEditModel();
			if (id.HasValue)
			{
				UserVO user = UserBO.GetInstance().GetItem(id.Value);
				if (user != null)
				{
					item = user.To<UserEditModel>();
				}
			}

			return View(item);
        }

        // POST: Users/Edit/5
        [HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(UserEditModel item)
        {
			if (ModelState.IsValid) {
				UserVO u = UserBO.GetInstance().GetItem( item.UserId );
				if(u == null) {
					return RedirectToAction( "Index" );
				}

				u.Email = item.Email;

				u.ActionDate = DateTime.Now;
				u.ActionBy = UserContext.Current.User.UserName;

				try {
					u.UserId = UserBO.GetInstance().SaveOrUpdateItem( u );
				}
				catch ( Exception ex ) {
					ModelState.AddModelError( "", ex.Message );
				}

				if ( ModelState.IsValid ) {
					return RedirectToAction( "Index" );
				}
			}

			return View(item);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(long id)
        {
			UserBO.GetInstance().DeleteItem(id);

			return RedirectToAction("Index");
        }
		
		public ActionResult ChangePassword(long id) {
			UserVO u = UserBO.GetInstance().GetItem(id);
			if (u == null) {
				return RedirectToAction("Index");
			}

			ChangePasswordModel item = u.To<ChangePasswordModel>();

			return View(item);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ChangePassword(ChangePasswordModel item) {
			if ( ModelState.IsValid ) {
				UserVO u = UserBO.GetInstance().GetItem( item.UserId );
				if ( u == null )
					return RedirectToAction( "Index" );

				u.Password = Utils.EncryptPassword( item.NewPassword, u.PasswordSalt );

				u.ActionDate = DateTime.Now;
				u.ActionBy = UserContext.Current.User.UserName;

				try {
					u.UserId = UserBO.GetInstance().SaveOrUpdateItem( u );
				}
				catch ( Exception ex ) {
					ModelState.AddModelError( "", ex.Message );
				}

				if ( ModelState.IsValid ) {
					return RedirectToAction( "Index" );
				}
			}

			return View();
		}
    }
}
