using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Maya
{
	public static class Extensions
	{
		public static string GetDescription(this Enum en ) {
			string description = string.Empty;

			Type type = en.GetType();
			MemberInfo[] memberInfos = type.GetMember( en.ToString() );
			if (memberInfos != null && memberInfos.Length > 0) {
				object[] attributes = memberInfos[0].GetCustomAttributes( typeof(DescriptionAttribute), false );
				if (attributes != null && attributes.Length > 0)
					description = ( (DescriptionAttribute)attributes[0] ).Description;
			}

			return description;
		}
	}
}
