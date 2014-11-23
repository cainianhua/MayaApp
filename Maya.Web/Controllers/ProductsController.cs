using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maya.Services.VO;
using Maya.Services.BO;
using Maya.Web.Models;
using Maya.Services;

namespace Maya.Web.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
			List<ProductVO> items = ProductBO.GetInstance().GetItems();
			return View( items );
        }

        // GET: Products/Create
        public ActionResult Create(int? id)
        {
			CreateOrEditProductModel item = new CreateOrEditProductModel();
			if (id.HasValue) {
				ProductVO p = ProductBO.GetInstance().GetItem( id.Value );
				if (p != null) {
					item = p.To<CreateOrEditProductModel>();
				}
			}
            return View( item );
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(CreateOrEditProductModel item)
        {
			if (ModelState.IsValid) {
				ProductVO p = new ProductVO();
				p.ProductId = item.ProductId;
				p.Name = item.Name;
				p.Description = item.Description;
				p.Pic = item.Pic;
				p.LinkTo = item.LinkTo;
				p.SortOrder = item.SortOrder;
				p.ActionDate = DateTime.Now;
				p.ActionBy = UserContext.Current.User.UserName;

				try {
					p.ProductId = ProductBO.GetInstance().SaveOrUpdateItem( p );
				}
				catch (Exception ex) {
					ModelState.AddModelError( "", ex.Message );
				}

				if (ModelState.IsValid) {
					return RedirectToAction( "Index" );
				}
			}

			return View( item );
        }

        // POST: Products/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
			ProductBO.GetInstance().DeleteItem( id );
			return RedirectToAction( "Index" );
        }
    }
}
