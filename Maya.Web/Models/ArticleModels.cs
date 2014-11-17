using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maya.Web.Models
{
	public class CreateOrEditArticleModel
	{
		[Key]
		public int ArticleId { get; set; }

		[Display(Name = "文章标题")]
		[Required( ErrorMessage = "请输入{0}。")]
		public string Title { get; set; }

		[Display(Name = "文章内容")]
		[Required(ErrorMessage = "请输入{0}。")]
		[AllowHtml]
		public string ArticleContent { get; set; }

		[Display(Name = "排序值")]
		[Required(ErrorMessage = "请输入{0}。")]
		[RegularExpression(@"\d+", ErrorMessage = "{0}格式不正确。")]
		public int SortOrder { get; set; }

		[Display(Name = "所属分类")]
		public int CategoryId { get; set; }

		[Display(Name = "所属地点")]
		public int DistrictId { get; set; }
	}
}