using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Maya.Web.Models
{
	public class CreateOrEditMusicModel
	{
		[Key]
		public int MusicId { get; set; }
		[Display(Name = "音乐名称")]
		[Required(ErrorMessage = "{0}为必填字段。")]
		public string Name { get; set; }
		[Display(Name = "音乐描述")]
		public string Description { get; set; }
		[Display(Name = "音乐链接")]
		[Required(ErrorMessage = "{0}为必填字段。")]
		public string LinkTo { get; set; }
		[Display(Name = "排序值")]
		[Required(ErrorMessage = "{0}为必填字段。")]
		[RegularExpression(@"\d+", ErrorMessage = "{0}格式不正确。")]
		public int SortOrder { get; set; }
		[Display(Name = "所属地点")]
		[Required(ErrorMessage = "请选择{0}。")]
		public int DistrictId { get; set; }

		public CreateOrEditMusicModel() {
			this.SortOrder = 9999;
		}
	}
}