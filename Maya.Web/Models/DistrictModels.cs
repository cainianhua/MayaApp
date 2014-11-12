﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Maya.Web.Models
{
    public class CreateDistrictModel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "父节点")]
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
    }
}