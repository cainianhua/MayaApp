/*
 * Code Generator v1.0
 * 2014-11-11 23:08:31
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;

namespace Maya.Services.BO
{
    public abstract class BaseBO
    {
        private static readonly string type = ConfigurationManager.AppSettings["DataProvider"] ?? "SqlServer";
        private static readonly string path = Assembly.GetExecutingAssembly().FullName.Split(',')[0];
        private static string classPathFormatter = "{0}.DAO.{1}.{2}Provider";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
		protected virtual object CreateProvider( string name ) {
			//Assembly.GetExecutingAssembly().GetName().Name
			return Assembly.Load( path ).CreateInstance( String.Format( classPathFormatter, path, type, name ) );
		}
    }
}
