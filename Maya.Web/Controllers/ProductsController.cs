using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maya.Services.VO;
using Maya.Services.BO;
using Maya.Web.Models;
using Maya.Services;
using System.ComponentModel;
using Webdiyer.WebControls.Mvc;

namespace Maya.Web.Controllers
{
    public class ProductsController : ControllerBase
    {
        protected override int PageSize
        {
            get
            {
                return 5;
            }
        }
        // GET: Products
        public ActionResult Index([DefaultValue(1)] int page, int? did)
        {
            List<ProductVO> products = ProductBO.GetInstance().GetItems(did);
            var items = new PagedList<ProductVO>(products.Skip((page - 1) * PageSize).Take(PageSize), page, PageSize, products.Count);

            List<SelectListItem> selectItems = new List<SelectListItem>();
            DistrictBO.GetInstance().GetItems().ForEach(item => {
                selectItems.Add(new SelectListItem()
                {
                    Text = item.Name,
                    Value = Url.Action("Index", new { page = page, did = item.DistrictId }),
                    Selected = did.HasValue && did.Value == item.DistrictId
                });
            });

            ViewBag.Districts = selectItems;

            return View(items);
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

			ViewBag.Districts = new SelectList( DistrictBO.GetInstance().GetItems(), "DistrictId", "Name" );

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
                p.DistrictId = item.DistrictId;
				p.ActionDate = DateTime.Now;
				p.ActionBy = UserContext.Current.User.UserName;

				try {
					p.ProductId = ProductBO.GetInstance().SaveOrUpdateItem( p );
				}
				catch (Exception ex) {
					ModelState.AddModelError( "Name", ex.Message );
				}

				if (ModelState.IsValid) {
					return RedirectToAction( "Index" );
				}
			}

			ViewBag.Districts = new SelectList( DistrictBO.GetInstance().GetItems(), "DistrictId", "Name" );

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
