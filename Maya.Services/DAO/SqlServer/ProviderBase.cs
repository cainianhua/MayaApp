/*
 * Code Generator v1.0
 * 2014-11-04 23:02:07
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using Maya.Services.VO;

namespace Maya.Services.DAO.SqlServer
{
    internal class ProviderBase
    {
        protected static readonly string dbConnectionString = ConfigurationManager.ConnectionStrings["defaultConnectionString"].ConnectionString;

        protected const string FIELD_CREATED_DATE = "CreatedDate";
        protected const string FIELD_CREATED_BY = "CreatedBy";
        protected const string FIELD_UPDATED_DATE = "UpdatedDate";
        protected const string FIELD_UPDATED_BY = "UpdatedBy";
        protected const string FIELD_RETURN_VALUE = "ReturnValue";
        //protected const string FIELD_SEARCH_TEXT = "SearchText";
		protected const string FIELD_PAGE_SIZE = "PageSize";
		protected const string FIELD_PAGE_NO = "PageNo";
        protected const string FIELD_TOTAL = "Total";
        protected const string AT = "@";
		// 非数据表字段常量
		protected const string FIELD_ACTION_DATE = "ActionDate";
		protected const string FIELD_ACTION_BY = "ActionBy";

        protected const int TWENTY_FOUR_HOURS = 1440;   // cache duration(minutes)

        protected void SetBaseProperties(IDataReader reader, BaseVO item) {
            item.CreatedDate = ReadDateTime(reader, FIELD_CREATED_DATE);
			item.CreatedBy = ReadString(reader, FIELD_CREATED_BY);
			item.UpdatedDate = ReadDateTime(reader, FIELD_UPDATED_DATE);
			item.UpdatedBy = ReadString(reader, FIELD_UPDATED_BY);
        }

        protected string ReadString(IDataReader reader, string p) {
            return reader[p] == DBNull.Value ? string.Empty : Convert.ToString(reader[p]);
        }

        protected int ReadByte(IDataReader reader, string p) {
            return reader[p] == DBNull.Value ? 0 : Convert.ToByte(reader[p]);
        }

        protected int ReadInt16(IDataReader reader, string p) {
            return reader[p] == DBNull.Value ? 0 : Convert.ToInt16(reader[p]);
        }

        protected int ReadInt32(IDataReader reader, string p) {
            return reader[p] == DBNull.Value ? 0 : Convert.ToInt32(reader[p]);
        }

        protected long ReadInt64(IDataReader reader, string p) {
            return reader[p] == DBNull.Value ? 0 : Convert.ToInt64(reader[p]);
        }

        protected DateTime ReadDateTime(IDataReader reader, string p) {
            return reader[p] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader[p]);
        }

        protected bool ReadBoolean(IDataReader reader, string p) {
            return reader[p] == DBNull.Value ? false : Convert.ToBoolean(reader[p]);
        }

        protected decimal ReadDecimal( IDataReader reader, string p ) {
			return reader[p] == DBNull.Value ? 0 : Convert.ToDecimal( reader[p] );
		}
    }
}
