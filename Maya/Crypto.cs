namespace Maya
{
	#region Usings
	using System;
	using System.Security.Cryptography;
	using System.Text;
	using System.IO;
	using System.Globalization;
	using System.Web;
	#endregion

	/// <summary>
	/// Summary description for CryptoHelper.
	/// </summary>
	sealed public class Crypto
	{
		#region Fields
		private static byte[] KEY_192 = { 222, 66, 219, 118, 44, 135, 72, 102 };
		private static byte[] IV_192 = { 33, 54, 108, 155, 204, 222, 49, 118 };
		//private static byte[] KEY_192 = { 231, 136, 177, 231, 187, 147, 49, 51 };
		//private static byte[] IV_192 = { 105, 106, 105, 101, 50, 48, 49, 51 };
		private static byte[] KEY_HMAC = {75, 76, 65, 83, 74, 68, 70, 76, 85, 51, 52, 56, 55, 69, 82, 72, 
											 74, 56, 57, 71, 89, 50, 52, 74, 73, 79, 80, 81, 70, 74, 67, 55, 
											 57, 48, 51, 52, 57, 48, 86, 51, 52, 57, 48, 86, 77, 72, 71, 74, 
											 48, 68, 75, 88, 87, 51, 48, 72, 71, 67, 50, 57, 48, 67, 89, 53};
		#endregion

		#region Private Constructor
		private Crypto() { }

		#endregion

		#region Public Methods
		/// <summary>
		/// Encrypts the text using HMAC-SHA1 oneway algorithm.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public static string EncryptHmacSha1 ( string input )
		{
			if ( input == null || input.Length == 0 )
				return String.Empty;

			ASCIIEncoding asciiEncoding = new ASCIIEncoding();

			HMACSHA1 hmacsha1 = new HMACSHA1(KEY_HMAC);
			
			byte[] data   = asciiEncoding.GetBytes( input );
			byte[] digest = hmacsha1.ComputeHash( data );

			return GetAsHexaDecimal( digest );
		}

		

		/// <summary>
		/// Encrypts value and then performs a UrlEncode.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public static string EncryptDesForWeb(string input)
		{	
			if ( input == null || input.Length == 0 )
				return String.Empty;

			return System.Web.HttpUtility.UrlEncode(EncryptDes(input));
		}

		/// <summary>
		/// Performas a UrlDecode and then decrypts the DES.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public static string DecryptDesFromWeb(string input)
		{	
			if ( input == null || input.Length == 0 )
				return String.Empty;

			return DecryptDes(System.Web.HttpUtility.UrlDecode(input));
		}

		/// <summary>
		/// Encrypts the specified input.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public static string EncryptDes(string input)
		{	
			if ( input == null || input.Length == 0 )
				return String.Empty;

			DESCryptoServiceProvider	des = new DESCryptoServiceProvider();			
			MemoryStream				ms = null;
			CryptoStream				encStream = null;
			StreamWriter				sw = null;
			string						result = String.Empty;
				
			try
			{
				ms = new MemoryStream();

				// Create a CryptoStream using the memory stream and the 
				// CSP DES key.  
				encStream = new CryptoStream(ms, des.CreateEncryptor(KEY_192, IV_192) , CryptoStreamMode.Write);

				// Create a StreamWriter to write a string
				// to the stream.
				sw = new StreamWriter(encStream);

				// Write the plaintext to the stream.
				sw.WriteLine(input);

				sw.Flush();
				encStream.FlushFinalBlock();
				ms.Flush();


				result = Convert.ToBase64String(ms.GetBuffer(), 0, Convert.ToInt32(ms.Length, CultureInfo.InvariantCulture));
			}
			finally
			{
				//close objects
				if ( sw != null )
					sw.Close();
				if ( encStream != null )
					encStream.Close();
				if ( ms != null )
					ms.Close();
			}

			// Return the encrypted string
			return result;
		}

		/// <summary>
		/// Decrypts the specified input.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public static string DecryptDes(string input)
		{
			byte[] buffer;
			try { buffer = Convert.FromBase64String(input); }
			catch (System.ArgumentNullException) { return String.Empty; }
				// length is zero, or not an even multiple of four (plus a few other cases)
			catch (System.FormatException) { return String.Empty; }

			DESCryptoServiceProvider des = new DESCryptoServiceProvider();
			MemoryStream ms = null;
			CryptoStream encStream = null;
			StreamReader sr = null;
			string result = String.Empty;

			try
			{
				ms = new MemoryStream(buffer);

				// Create a CryptoStream using the memory stream and the 
				// CSP DES key. 
				encStream = new CryptoStream(ms, des.CreateDecryptor(KEY_192, IV_192), CryptoStreamMode.Read);

				// Create a StreamReader for reading the stream.
				sr = new StreamReader(encStream);

				// Read the stream as a string.
				result = sr.ReadLine();
			}
			finally
			{
				//close objects
				if ( sr != null )
					sr.Close();
				if ( encStream != null )
					encStream.Close();
				if ( ms != null )
					ms.Close();
			}
            
			return result;
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Gets byte array as hexadecimal.
		/// </summary>
		/// <param name="bytes">The bytes.</param>
		/// <returns></returns>
		private static string GetAsHexaDecimal(byte[] bytes) 
		{
			StringBuilder s = new StringBuilder();
			int length = bytes.Length;
			for (int n=0; n < length; n++) 
			{
				s.Append(String.Format(CultureInfo.InvariantCulture, "{0,2:x}", bytes[n]).Replace(" ", "0"));
			}
			return s.ToString();
		}
		#endregion
	}
}
