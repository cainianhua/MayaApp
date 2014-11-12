using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maya.Web.Models;
using Maya.Services.VO;
using Maya.Services.BO;

namespace Maya.Web.Controllers
{
    public class DistrictsController : ControllerBase
    {
        // GET: Districts
        public ActionResult Index()
        {
            return View();
        }

        // GET: Districts/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Districts/Create
        public ActionResult Create()
        {
            CreateDistrictModel item = new CreateDistrictModel();

            ViewBag.Districts = new SelectList( DistrictBO.GetInstance().GetItems(), "DistrictId", "Name" );

            return View( item );
        }

        // POST: Districts/Create
        [HttpPost]
        public ActionResult Create(CreateDistrictModel item)
        {
            if ( ModelState.IsValid ) {
                DistrictVO d = new DistrictVO();
                d.Name = item.Name;
                d.Description = item.Description;
                d.Lng = item.Lng;
                d.Lat = item.Lat;
                d.ActionDate = DateTime.Now;
                d.ActionBy = UserContext.Current.User.UserName;

                try {
                    d.DistrictId = DistrictBO.GetInstance().SaveItem( item.ParentId, d );
                }
                catch(Exception ex) {
                    ModelState.AddModelError( "Name", "保存数据出错，错误是：" + ex.Message );
                }

                if ( ModelState.IsValid ) {
                    return RedirectToAction( "Index" );
                }
            }

            ViewBag.Districts = new SelectList( DistrictBO.GetInstance().GetItems(), "DistrictId", "Name" );

            return View( item );
        }

        // GET: Districts/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Districts/Edit/5
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

        // GET: Districts/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Districts/Delete/5
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
