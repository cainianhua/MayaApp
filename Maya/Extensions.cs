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

		public static T To<T>(this Object o) {
			Type origin = o.GetType();
			Type destination = typeof(T);

			T obj = Activator.CreateInstance<T>();
			foreach (PropertyInfo p1 in origin.GetProperties()) {
				foreach (PropertyInfo p2 in destination.GetProperties()) {
					if (p1.Name == p2.Name) {
						//p2.SetValue( obj, Convert.ChangeType( p1.GetValue( this ), p1.PropertyType ) );
						//p2.SetValue( obj, "abc", null );
						p2.SetValue( obj, p1.GetValue( o ), null );
						break;
					}
				}
			}

			return obj;
		}
	}
}
