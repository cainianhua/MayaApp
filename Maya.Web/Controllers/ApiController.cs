using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maya.Web.FilterAttributes;

namespace Maya.Web.Controllers
{
	[LoginChecker]
    public class ApiController : Controller
    {
        // GET: Api
        public ActionResult FileUpload()
        {
			HttpPostedFileBase file = Request.Files[0];

			if (Request.Files.Count == 0 || file.ContentLength == 0 || file.FileName.Length == 0) {
				return Json( new { code = -1, message = "no file" } );
			}
			else if (!CheckFileType( file.InputStream.GetMimeValue() )) {
				return Json( new { code = -2, message = "File type is not supported" } );
			}
			else if (file.ContentLength > ( AllowedMaxImageSize * 1024 )) {
				return Json( new { code = -3, message = "File is too large" } );
			}
			string currDateString = DateTime.Today.ToString( "yyyyMMdd" );
			string fileNameForServer = String.Format( "{0}{1}", Guid.NewGuid().ToString().Replace( "-", "" ), Path.GetExtension( file.FileName ) );
			//string FileNameForErrorMessage = MakeFileNameForErrorMessage( file.FileName );

			//string filePathForClient = String.Format( UploadFileViewUrl, currDateString, "Images", fileNameForServer );
            string savePath = String.Format(UploadFilePath, currDateString, "Images", fileNameForServer);

			string localPath = Server.MapPath( savePath );
  
			// create directory if need.
            if (!Directory.Exists(Path.GetDirectoryName(localPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(localPath));
            }
			// save file to server.
			try {
                file.SaveAs(localPath);
			}
			catch (Exception ex) {
				return Json( new { code = -5, message = "Save file occurs a problem, and error is: " + ex.Message } );
			}

            return Json(new { code = 1, message = "success", url = String.Format(UploadFileViewUrl, Url.Content(savePath)), fileName = fileNameForServer });
		}

		#region 属性
		/// <summary>
		/// 
		/// </summary>
		private int AllowedMaxImageSize {
			get {
				return int.Parse( ConfigurationManager.AppSettings["AllowedMaxImageSize"] );
			}
		}
		/// <summary>
		/// 
		/// </summary>
		private int AllowedMaxAudioSize {
			get {
				return int.Parse( ConfigurationManager.AppSettings["AllowedMaxAudioSize"] );
			}
		}
		/// <summary>
		/// 
		/// </summary>
		private string UploadFilePath {
			get {
				return ConfigurationManager.AppSettings["UploadFilePath"];
			}
		}
		/// <summary>
		/// 
		/// </summary>
		private string UploadFileViewUrl {
			get {
				return ConfigurationManager.AppSettings["UploadFileViewUrl"];
			}
		}
		#endregion

		#region 私有方法
		/// <summary>
		/// 
		/// </summary>
		/// <param name="contentType"></param>
		/// <returns></returns>
		private bool CheckFileType( string contentType ) {
			bool retVal = false;
			retVal = (
				contentType == "image/jpeg" ||
				contentType == "image/pjpeg" ||
				contentType == "image/gif" ||
				contentType == "image/png" ||
				contentType == "image/x-png"
			);
			return retVal;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="contentType"></param>
		/// <returns></returns>
		private bool CheckAudioFileType( string contentType ) {
			bool retVal = false;

			retVal = (
				contentType == "audio/mp3" ||
				contentType == "audio/mpeg"
			);
			return retVal;
		}
		#endregion
	}
}