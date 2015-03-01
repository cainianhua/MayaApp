using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Maya.Security.ValidationCode;

namespace Maya.Security
{
	public interface IValidateCodeProvider
	{
		/// <summary>
		/// 获取图形验证码
		/// </summary>
		/// <returns></returns>
		byte[] GetValidationCode();
		/// <summary>
		/// 校验验证码
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		bool CheckValidationCode( string input );
	}

	public class ValidationCodeProvider : IValidateCodeProvider
	{
		private const string VC_COOKIE_NAME = "MAYA_ADMIN_CONTEXT_VC";
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public byte[] GetValidationCode() {
			var validateCodeType = new ValidateCode_Style10();
			string code = "6666";
			byte[] codeBytes = validateCodeType.CreateImage( out code );

			HttpCookie codeCookie = new HttpCookie( VC_COOKIE_NAME );
			codeCookie.Value = Utils.Md5( code.ToLower() );
			codeCookie.Expires = DateTime.Now.AddMinutes( 15 );	// 15分钟失效

			HttpContext.Current.Response.Cookies.Add( codeCookie );

			return codeBytes;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public bool CheckValidationCode( string input ) {
			HttpCookie codeCookie = HttpContext.Current.Request.Cookies[VC_COOKIE_NAME];
			if ( codeCookie == null ) {
				return false;
			}

			if(string.IsNullOrWhiteSpace(input)) {
				return false;
			}

			input = Utils.Md5( input.ToLower() );

			return input.Equals( codeCookie.Value );
		}
	}
}
