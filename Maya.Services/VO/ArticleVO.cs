/*
 * Code Generator v1.0
 * 2014-11-04 23:02:03
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maya.Services.VO 
{
    public class ArticleVO : BaseVO {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string ArticleContent { get; set; }
        public string Tags { get; set; }
        public int SortOrder { get; set; }
        public int DistrictId { get; set; }
        public int CategoryId { get; set; }
    }
}