using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maya
{
	public static class DictionaryExtensions
	{
		/// <summary>
		/// 为了实现链式操作
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="dict"></param>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static IDictionary<string, object> AddWithChain( this IDictionary<string, object> dict, string key, object value ) {
			if (!string.IsNullOrEmpty( key ) && value != null && !dict.ContainsKey( key )) {
				dict.Add( new KeyValuePair<string, object>( key, value ) );
			}
			return dict;
		}
	}
}
