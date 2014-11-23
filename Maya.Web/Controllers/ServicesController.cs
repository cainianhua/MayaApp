using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maya.Services;
using Maya.Services.VO;
using Maya.Services.BO;
using System.Net;

namespace Maya.Web.Controllers
{
    public class ServicesController : Controller
    {
        /// <summary>
		/// 
		/// </summary>
		/// <param name="cid">Category Id</param>
		/// <param name="dn">District name</param>
		/// <returns></returns>
        public ActionResult Index(CategoryType type, string dn)
        {
			switch (type) {
				case CategoryType.电流电压:
				case CategoryType.插座标准:
				case CategoryType.全球通使用方法:
				case CategoryType.大使馆资料:
				case CategoryType.当地紧急电话:
				case CategoryType.出入卡填写:
					return Articles( type, dn );
				case CategoryType.经纬度:
					return LngLat( dn );
                case CategoryType.产品专题:
					return Products( dn );
				case CategoryType.旅游音乐:
					return Musics( dn );
				default:
					return new HttpStatusCodeResult( HttpStatusCode.NotImplemented );
            }
        }

		public JsonResult Rates() {
			return new JsonResult();
		}

		private JsonResult Musics(string dn ) {
			List<MusicVO> musics = MusicBO.GetInstance().GetItemsByDistrictCriteria( dn );
			return Json( from item in musics
						 select new { item.MusicId, item.Name, item.Description, item.LinkTo }, JsonRequestBehavior.AllowGet );
		}

		private JsonResult Products(string dn ) {
			List<ProductVO> products = ProductBO.GetInstance().GetItemsByDistrictCriteria( dn );
			return Json( from item in products
						 select new { item.ProductId, item.Name, item.Pic, item.Description, item.LinkTo }, JsonRequestBehavior.AllowGet );
		}

		private JsonResult Articles( CategoryType type, string dn ) {
			List<ArticleVO> articles = ArticleBO.GetInstance().GetItems( (int)type, dn );
			return Json( from item in articles
						 select new { item.ArticleId, item.Title, item.ArticleContent }, JsonRequestBehavior.AllowGet );
		}

		private JsonResult LngLat( string dn ) {
			List<DistrictVO> districts = DistrictBO.GetInstance().GetItems( dn );
			return Json( from item in districts
								   select new { item.Name, item.Lng, item.Lat }, JsonRequestBehavior.AllowGet );
		}
	}
}