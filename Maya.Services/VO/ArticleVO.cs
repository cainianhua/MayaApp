/*
 * Code Generator v1.0
 * 2014-11-07 23:46:09
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Maya.Services.VO 
{
    public class ArticleVO : BaseVO {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string ArticleContent { get; set; }
        public string Tags { get; set; }
		[ScriptIgnore]
		public int SortOrder { get; set; }
		[ScriptIgnore]
		public int DistrictId { get; set; }
		[ScriptIgnore]
		public int CategoryId { get; set; }
    }
}