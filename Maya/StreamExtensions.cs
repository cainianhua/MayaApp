using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maya
{
	public static class StreamExtensions
	{
		private static Dictionary<string, string> _Mimes;
		/// <summary>
		/// 
		/// </summary>
		static StreamExtensions() {
			_Mimes = new Dictionary<string, string>();
			// 常用图片格式
			_Mimes.Add( "7173", "image/gif" );		// gif
			_Mimes.Add( "255216", "image/jpeg" );	// jpg
			_Mimes.Add( "13780", "image/png" );		// png
			_Mimes.Add( "6677", "image/bmp" );		// bmp
			_Mimes.Add( "7777", "image/tiff" );		// tiff
													// 常用音频格式
													//_Mimes.Add( "7778", "audio/basic" );    // au or snd
													//_Mimes.Add( "255251", "audio/mp3" );     // mp3
													//_Mimes.Add( "7710", "audio/mid" );		// mid or rmi
													//_Mimes.Add( "7711", "audio/x-wav" );	// wav
													//_Mimes.Add( "7712", "audio/x-pn-realaudio" );// ra or ram
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fileclass"></param>
		/// <returns></returns>
		public static string GetMimeValue( this Stream inputStream ) {
			if (inputStream.CanSeek && inputStream.Position != 0) inputStream.Position = 0;

			string fileclass = "";

			if (inputStream.Length > 2) {
				byte[] bytes = new byte[1000];
				// 读取前两位。
				if (inputStream.CanRead) inputStream.Read( bytes, 0, 1000 );
				fileclass = bytes[0].ToString() + bytes[1].ToString();
			}

			if (_Mimes.Keys.Contains( fileclass )) {
				return _Mimes[fileclass];
			}

			return string.Empty;
		}
	}
}
