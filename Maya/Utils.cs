﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Maya
{
	public class Utils
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pwd"></param>
		/// <param name="salt"></param>
		/// <returns></returns>
		public static string EncryptPassword( string pwd, string salt ) {
			return Md5( Md5( pwd + salt ) );
		}
		/// <summary> 
		/// MD5加密 
		/// </summary> 
		/// <param name="str"></param> 
		/// <returns></returns> 
		public static string Md5( string str ) {
			// Create a new instance of the MD5CryptoServiceProvider object. 
			MD5 md5Hasher = MD5.Create();
			// Convert the input string to a byte array and compute the hash. 
			byte[] data = md5Hasher.ComputeHash( Encoding.GetEncoding( "UTF-8" ).GetBytes( str ) );

			//  Create a new Stringbuilder to collect the bytes 
			//  and create a string. 
			StringBuilder sBuilder = new StringBuilder();
			// Loop through each byte of the hashed data 
			// and format each one as a hexadecimal string. 
			for (int i = 0; i < data.Length; i++) {
				sBuilder.Append( data[i].ToString( "x2" ) );
			}
			// Return the hexadecimal string. 
			return sBuilder.ToString();
		}

		/// <summary>
		/// 从url获取返回的数据。
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static string RetrieveWebData( string url ) {
			HttpWebRequest request = WebRequest.Create( url ) as HttpWebRequest;
			//为了兼容BBS同步登录，加上UserAgent。
			string agent = "Mozilla/4.0(compatible;MSIE6.0;)";
			if (( HttpContext.Current != null ) && ( HttpContext.Current.Request != null )) {
				agent = HttpContext.Current.Request.ServerVariables["Http_User_Agent"];
			}
			request.UserAgent = agent;
			CookieContainer cookieContainer = new CookieContainer();
			request.CookieContainer = cookieContainer;

			string str = "";
			using (HttpWebResponse response = request.GetResponse() as HttpWebResponse) {
				foreach (Cookie cookie in cookieContainer.GetCookies( request.RequestUri )) {
					HttpCookie tmpCookie = new HttpCookie( cookie.Name );
					tmpCookie.Domain = cookie.Domain;
					tmpCookie.Path = cookie.Path;
					tmpCookie.Value = cookie.Value;
					tmpCookie.Expires = cookie.Expires;
					HttpContext.Current.Response.Cookies.Add( tmpCookie );
				}
				StreamReader sr = new StreamReader( response.GetResponseStream() );
				str = sr.ReadToEnd();
			}
			return str;
		}
	}
}
