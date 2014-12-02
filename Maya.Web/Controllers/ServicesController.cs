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
				case SortType.DLDY:
				case SortType.CZBZ:
				case SortType.QQTSYFF:
				case SortType.DSGZL:
				case SortType.DDJJDH:
				case SortType.CRJKTX:
				case SortType.JCXX:
					return ArticlesInternal( type, did );
                case SortType.CPZT:
					return ProductsInternal( did );
				case SortType.LYYY:
					return MusicsInternal( did );
				case SortType.SSHL:
					return RateInternal( did );
				case SortType.HBDH:
					return ExchangeInternal( did );
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
		private ActionResult ProductsInternal( int districtId ) {
			List<ProductVO> products = ProductBO.GetInstance().GetItemsByDistrictCriteria( districtId );

			return PartialView( "_Products", products );
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

			ViewBag.SortKey = type.ToString().ToLower();
			ViewBag.SortName = type.GetDescription();

			return PartialView( "_ArticleDetail", item );
		}

		private ActionResult RateInternal(int districtId ) {
			ViewBag.Rates = new SelectList( CurrencyBO.GetInstance().GetItems(), "Code", "Name" );

			// 这里可以根据districtId获取到对应的货币类型，然后在dropdownlist里面选中

			return PartialView( "_Rate" );
		}

		private ActionResult ExchangeInternal(int districtId ) {
			ViewBag.Rates = new SelectList( CurrencyBO.GetInstance().GetItems(), "Code", "Name" );
			return PartialView( "_Exchange" );
		}
		#endregion
	}
}