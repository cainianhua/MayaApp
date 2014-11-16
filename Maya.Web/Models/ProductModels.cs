using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Maya.Web.Models
{
	public class CreateOrEditProductModel
	{
		[Key]
		public int ProductId { get; set; }
		[Display(Name = "产品名称")]
		[Required(ErrorMessage = "{0}为必填字段。")]
		public string Name { get; set; }
		[Display(Name = "产品描述")]
		public string Description { get; set; }
		[Display(Name = "图片")]
		[Required(ErrorMessage = "{0}为必填字段。")]
		[UIHint("File")]
		public string Pic { get; set; }
		[Display(Name = "产品链接")]
		[Required(ErrorMessage = "{0}为必填字段。")]
		public string LinkTo { get; set; }
		[Display(Name = "排序值")]
		[Required(ErrorMessage = "{0}为必填字段。")]
		[RegularExpression(@"\d+", ErrorMessage = "{0}格式不正确。")]
		public int SortOrder { get; set; }

		public CreateOrEditProductModel() {
			this.SortOrder = 9999;
		}
	}
}