using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maya.Services;
using Maya.Services.VO;
using Maya.Services.BO;
using System.Net;
using Maya.Web.FilterAttributes;

namespace Maya.Web.Controllers
{
    public class ServicesController : ControllerBase
	{
		/// <summary>
		/// 接口调用帮助文件信息
		/// </summary>
		/// <returns></returns>
		public ActionResult Index() {
			return View();
		}
		/// <summary>
		/// 文章数据，包括音乐和产品都归到文章里面
		/// </summary>
		/// <param name="type">Category Id或者Sort key(枚举)</param>
		/// <param name="did">位置编号</param>
		/// <returns></returns>
		public ActionResult Articles(SortType type, int did)
        {
			switch (type) {
				case SortType.电流电压:
				case SortType.插座标准:
				case SortType.全球通使用方法:
				case SortType.大使馆资料:
				case SortType.当地紧急电话:
				case SortType.出入卡填写:
					return ArticlesInternal( type, did );
                case SortType.产品专题:
					return ProductsInternal( did );
				case SortType.旅游音乐:
					return MusicsInternal( did );
				default:
					return new HttpStatusCodeResult( HttpStatusCode.NotImplemented );
            }
        }

		/// <summary>
		/// 利率数据，获取from对to的汇率
		/// </summary>
		/// <param name="from">币种一</param>
		/// <param name="to">币种二</param>
		/// <returns></returns>
		public ActionResult Rate(string from, string to) {
			// 汇率数据源：
			// http://download.finance.yahoo.com/d/quotes.csv?s=USDCNY=X&f=sl1d1t1ba&e=.html
			// 返回数据格式：
			// "USDCNY=X",6.1417,"11/24/2014","8:33am",6.1367,6.1467

			string RATE_API_FORMATTER = "http://download.finance.yahoo.com/d/quotes.html?s={0}{1}=X&f=sl1d1t1ba&e=.html";
			string api = string.Format( RATE_API_FORMATTER, from, to );

			string data = Utils.RetrieveWebData( api );

			return Jsonp( new { rate = data.Split( ',' )[1] } );
		}

		/// <summary>
		/// 地理位置信息，设置目的地的时候需要调用这个接口
		/// </summary>
		/// <param name="dn">地点名称</param>
		/// <returns></returns>
		public ActionResult Locations( string dn ) {
			dn = dn.Trim();
			List<DistrictVO> districts = new List<DistrictVO>();
			if (!string.IsNullOrEmpty( dn )) {
				districts = DistrictBO.GetInstance().GetItems( dn );
			}

			return Jsonp( new { suggestions = districts } );
		}

		/// <summary>
		/// 文章详情
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult Detail(int id ) {
			ArticleVO item = ArticleBO.GetInstance().GetItem( id );
			return Jsonp( item );
		}

		public ActionResult Currencies() {
			List<CurrencyVO> items = CurrencyBO.GetInstance().GetItems();
			return Json( items );
		}

		#region [ 私有方法 ]
		/// <summary>
		/// 
		/// </summary>
		/// <param name="districtId"></param>
		/// <returns></returns>
		private JsonResult MusicsInternal( int districtId ) {
			List<MusicVO> musics = MusicBO.GetInstance().GetItemsByDistrictCriteria( districtId );
			return Jsonp( musics );
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="districtId"></param>
		/// <returns></returns>
		private JsonResult ProductsInternal( int districtId ) {
			List<ProductVO> products = ProductBO.GetInstance().GetItemsByDistrictCriteria( districtId );
			return Jsonp( products );
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="type"></param>
		/// <param name="districtId"></param>
		/// <returns></returns>
		[AllowCrossSiteJson]
		private ActionResult ArticlesInternal( SortType type, int districtId ) {
			List<ArticleVO> articles = ArticleBO.GetInstance().GetItems( (int)type, districtId );
			ArticleVO item = new ArticleVO();
			if (articles.Count > 0)
				item = articles.First();

			return PartialView( "_ArticleDetail", item );
		}
		#endregion
	}
}