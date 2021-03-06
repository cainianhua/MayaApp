/*
 * Code Generator v1.0
 * 2014-11-11 23:08:30
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Maya;

namespace Maya.Services.VO 
{
    public class DistrictVO : BaseVO {
        public int DistrictId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
		[ScriptIgnore]
		public int Lft { get; set; }
		[ScriptIgnore]
		public int Rgt { get; set; }
        public string Lng { get; set; }
        public string Lat { get; set; }
		public int TimeZone { get; set; }
		[ScriptIgnore]
		public int SortOrder { get; set; }

		public DistrictVO() {
			this.SortOrder = 9999;
		}
    }
}