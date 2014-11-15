using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maya
{
	public enum FormAction
	{
		[Description("新增")]
		Create = 1,
		[Description("修改")]
		Edit
	}
}
