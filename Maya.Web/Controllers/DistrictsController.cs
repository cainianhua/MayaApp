using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maya.Web.Models;
using Maya.Services.VO;
using Maya.Services.BO;
using Maya.Services;
using Webdiyer.WebControls.Mvc;
using System.ComponentModel;
using Maya.Web.FilterAttributes;

namespace Maya.Web.Controllers
{
	[LoginChecker]
    public class DistrictsController : ControllerBase
    {
        // GET: Districts
        public ActionResult Index([DefaultValue(1)]int page, string s)
        {
			List<DistrictVO> districts = DistrictBO.GetInstance().GetItems(s);
			PagedList<DistrictVO> items = new PagedList<DistrictVO>(districts.Skip((page - 1) * PageSize).Take(PageSize), page, PageSize, districts.Count);

            ViewBag.UrlFormatter = Url.Action("Index", new { page = page, s = "-_s_-" });

            return View(items);
        }

        // GET: Districts/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Districts/Create
        public ActionResult Create(int? id)
        {
            CreateOrUpdateDistrictModel item = new CreateOrUpdateDistrictModel();
			if (id.HasValue) {
				DistrictVO district = DistrictBO.GetInstance().GetItem( id.Value );
				List<DistrictVO> parents = DistrictBO.GetInstance().GetParentItems( id.Value );
				if (district != null) {
					item = district.To<CreateOrUpdateDistrictModel>();
					if (parents.Count > 0) {
						item.ParentId = parents[parents.Count - 1].DistrictId;
					}
				}
			}

            item.TimeZone = 9;

			ViewBag.Districts = new SelectList( DistrictBO.GetInstance().GetAllItems(), "DistrictId", "Name" );

            return View( item );
        }

        // POST: Districts/Create
        [HttpPost]
        public ActionResult Create(CreateOrUpdateDistrictModel item)
        {
            if ( ModelState.IsValid ) {
                DistrictVO d = new DistrictVO();
				d.DistrictId = item.DistrictId;
                d.Name = item.Name;
                d.Description = item.Description;
                d.Lng = item.Lng;
                d.Lat = item.Lat;
                d.TimeZone = item.TimeZone;
                d.ActionDate = DateTime.Now;
                d.ActionBy = UserContext.Current.User.UserName;

                try {
					if (d.DistrictId > 0)
						DistrictBO.GetInstance().UpdateItem( d );
					else
						d.DistrictId = DistrictBO.GetInstance().SaveItem( item.ParentId, d );
                }
                catch(Exception ex) {
                    ModelState.AddModelError( "Name", "保存数据出错，错误是：" + ex.Message );
                }

                if ( ModelState.IsValid ) {
                    return RedirectToAction( "Index" );
                }
            }

            ViewBag.Districts = new SelectList( DistrictBO.GetInstance().GetAllItems(), "DistrictId", "Name" );

            return View( item );
        }

        // POST: Districts/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
			DistrictBO.GetInstance().DeleteItem( id );
			return RedirectToAction( "Index" );
		}
    }
}
