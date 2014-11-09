using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Maya.Web.Models
{
    public class CreateDistrictModel
    {
        public int ParentDistrictId { get; set; }
        [Display(Name = "名称")]
        [Required(ErrorMessage = "{0}是必填信息。")]
        public string Name { get; set; }
        [Display(Name = "描述")]
        public string Description { get; set; }
        //public int Lft { get; set; }
        //public int Rgt { get; set; }
        //public int Layer { get; set; }
        [Display(Name = "所在经度")]
        [Required(ErrorMessage = "{0}是必填信息。")]
        public string Lng { get; set; }
        [Display(Name = "所在纬度")]
        [Required(ErrorMessage = "{0}是必填信息。")]
        public string Lat { get; set; }
    }
}