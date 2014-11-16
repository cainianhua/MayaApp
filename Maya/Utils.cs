using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
	}
}
