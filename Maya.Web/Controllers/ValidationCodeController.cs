using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maya.Security;

namespace Maya.Web.Controllers
{
    public class ValidationCodeController : ControllerBase
	{
        public ActionResult Index() {
			IValidateCodeProvider provider = new ValidationCodeProvider();

			byte[] bytes = provider.GetValidationCode();

			return File( bytes, @"image/jpeg" );
		}
    }
}