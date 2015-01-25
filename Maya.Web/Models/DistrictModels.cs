using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Maya.Web.Models
{
    public class CreateOrUpdateDistrictModel
    {
        [Key]
        public int DistrictId { get; set; }
        [Display(Name = "所属地点")]
        public int ParentId { get; set; }
        [Display(Name = "名称")]
        [Required(ErrorMessage = "{0}是必填信息。")]
        public string Name { get; set; }
        [Display(Name = "描述")]
        public string Description { get; set; }
        [Display(Name = "所在经度")]
        [Required(ErrorMessage = "{0}是必填信息。")]
        public string Lng { get; set; }
        [Display(Name = "所在纬度")]
        [Required(ErrorMessage = "{0}是必填信息。")]
        public string Lat { get; set; }
		[Display(Name = "所在时区")]
		[Required(ErrorMessage = "{0}是必填信息。")]
		[Range(-12, 12, ErrorMessage = "{0}必须在区间[{1}-{2}]内。")]
		public int TimeZone { get; set; }
    }
}