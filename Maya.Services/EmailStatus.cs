using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maya.Services
{
	public enum EmailStatus
	{
		[Description("默认")]
		NotSet,
		[Description("已验证")]
		Verified,
		[Description("未验证")]
		Unverified,
	}
}
