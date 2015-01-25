using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maya.Services.VO;
using Maya.Services.BO;
using Maya.Web.Models;
using Maya.Services;
using Webdiyer.WebControls.Mvc;
using System.ComponentModel;

namespace Maya.Web.Controllers
{
    public class ArticlesController : ControllerBase
    {
        // GET: Articles
        public ActionResult Index([DefaultValue(1)] int page, int? did)
        {
			List<ArticleVO> articles = ArticleBO.GetInstance().GetItems(did);
            var items = new PagedList<ArticleVO>(articles.Skip((page - 1) * PageSize).Take(PageSize), page, PageSize, articles.Count);

            List<SelectListItem> selectItems = new List<SelectListItem>();
            DistrictBO.GetInstance().GetItems().ForEach(item =>
            {
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

        // GET: Articles/Create
        public ActionResult Create(int? id)
        {
			CreateOrEditArticleModel item = new CreateOrEditArticleModel();
			if (id.HasValue) {
				ArticleVO a = ArticleBO.GetInstance().GetItem( id.Value );
				if(a != null) {
					item = a.To<CreateOrEditArticleModel>();
				}
			}

			ViewBag.Districts = new SelectList( DistrictBO.GetInstance().GetItems(), "DistrictId", "Name" );
			ViewBag.Categories = new SelectList( CategoryBO.GetInstance().GetItems(), "CategoryId", "Name" );

			return View( item );
        }

        // POST: Articles/Create
        [HttpPost]
        public ActionResult Create(CreateOrEditArticleModel item)
        {
			if (ModelState.IsValid) {
				ArticleVO article = item.To<ArticleVO>();
				article.ActionDate = DateTime.Now;
				article.ActionBy = UserContext.Current.User.UserName;

				try {
					ArticleBO.GetInstance().SaveOrUpdateItem( article );
				}
				catch(Exception ex) {
					ModelState.AddModelError( "", ex.Message );
				}

				if (ModelState.IsValid) {
					return RedirectToAction( "Index" );
				}
			}

			ViewBag.Districts = new SelectList( DistrictBO.GetInstance().GetItems(), "DistrictId", "Name" );
			ViewBag.Categories = new SelectList( CategoryBO.GetInstance().GetItems(), "CategoryId", "Name" );

			return View( item );
        }

        // POST: Articles/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
			ArticleBO.GetInstance().DeleteItem( id );
			return RedirectToAction( "Index" );
		}
	}
}
