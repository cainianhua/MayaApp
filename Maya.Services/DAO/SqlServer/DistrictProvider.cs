/*
 * Code Generator v1.0
 * 2014-11-11 23:08:31
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Maya.Services.VO;


namespace Maya.Services.DAO.SqlServer 
{
    internal class DistrictProvider : ProviderBase, IDistrictProvider 
    {
        #region SQLs Statements
        private const string SQL_GET_DISTRICTS_ALL = "SELECT * FROM Districts";
        private const string SQL_GET_DISTRICTS_PK = "SELECT * FROM Districts WHERE 1 = 1 AND [DistrictId] = @DistrictId";
        //private const string SQL_DELETE_DISTRICTS_PK = "DELETE FROM Districts WHERE 1 = 1 AND [DistrictId] = @DistrictId";


        private const string PROC_CREATE_DISTRICT = "USP_CREATE_District";
        private const string PROC_UPDATE_DISTRICT = "USP_Update_District";
		private const string PROC_GET_DISTRICTS_PARENT = "USP_Get_Districts_Of_Parent";
		private const string PROC_GET_DISTRICTS_CHILD = "USP_Get_Districts_Of_Parent";
		private const string PROC_DELETE_DISTRICT = "USP_Delete_District";
		private const string PROC_FETCH_DISTRICTS = "USP_Fetch_Districts";
		#endregion

		#region Fields
		private const string FIELD_DISTRICTID = "DistrictId";
        private const string FIELD_NAME = "Name";
        private const string FIELD_DESCRIPTION = "Description";
        private const string FIELD_LFT = "Lft";
        private const string FIELD_RGT = "Rgt";
        private const string FIELD_LNG = "Lng";
        private const string FIELD_LAT = "Lat";
		private const string FIELD_TIMEZONE = "TimeZone";
		private const string FILED_SORTORDER = "SortOrder";

        private const string FIELD_PARENT_ID = "ParentId";
        #endregion

        #region Methods
        public List<DistrictVO> GetItems() {
            return GetSQLResults( SQL_GET_DISTRICTS_ALL, CommandType.Text, null);
        }

		public List<DistrictVO> GetItems(string searchText ) {
			SqlParameter[] p = new SqlParameter[] {
				SqlHelper.MakeInParameter( AT + FIELD_SEARCH_TEXT, SqlDbType.NVarChar, 50, searchText )
			};
			return GetSQLResults( PROC_FETCH_DISTRICTS, CommandType.StoredProcedure, p );
		}

        public DistrictVO GetItem(int districtId) {
            DistrictVO item = null;
            SqlParameter[] p = new SqlParameter[] { 
                SqlHelper.MakeInParameter( AT + FIELD_DISTRICTID, SqlDbType.Int, 4, districtId )
            };
            List<DistrictVO> items = GetSQLResults( SQL_GET_DISTRICTS_PK, CommandType.Text, p);
            if (items.Count > 0) item = items[0];
            return item;
        }

		public List<DistrictVO> GetParentItems(int districtId ) {
			SqlParameter[] p = new SqlParameter[] {
				SqlHelper.MakeInParameter( AT + FIELD_DISTRICTID, SqlDbType.Int, 4, districtId )
			};
			return GetSQLResults( PROC_GET_DISTRICTS_PARENT, CommandType.StoredProcedure, p );
        }

		public List<DistrictVO> GetChildItems( int districtId ) {
			SqlParameter[] p = new SqlParameter[] {
				SqlHelper.MakeInParameter( AT + FIELD_DISTRICTID, SqlDbType.Int, 4, districtId )
			};
			return GetSQLResults( PROC_GET_DISTRICTS_CHILD, CommandType.StoredProcedure, p );
		}

		public void DeleteItem(int districtId) {
            SqlParameter[] p = new SqlParameter[] { 
                SqlHelper.MakeInParameter( AT + FIELD_DISTRICTID, SqlDbType.Int, 4, districtId )
            };
            SqlHelper.ExecuteNonQuery(dbConnectionString, CommandType.StoredProcedure, PROC_DELETE_DISTRICT, p);
        }

        public int SaveItem(int parentId, DistrictVO item )
        {
            SqlParameter[] p = new SqlParameter[] {
                SqlHelper.MakeInParameter( AT + FIELD_PARENT_ID, SqlDbType.Int, 4, parentId ),
                SqlHelper.MakeInParameter( AT + FIELD_NAME, SqlDbType.NVarChar, 50, item.Name ),
                SqlHelper.MakeInParameter( AT + FIELD_DESCRIPTION, SqlDbType.NVarChar, 255, item.Description ),
                //SqlHelper.MakeInParameter( AT + FIELD_LFT, SqlDbType.Int, 4, item.Lft ),
                //SqlHelper.MakeInParameter( AT + FIELD_RGT, SqlDbType.Int, 4, item.Rgt ),
                SqlHelper.MakeInParameter( AT + FIELD_LNG, SqlDbType.VarChar, 50, item.Lng ),
                SqlHelper.MakeInParameter( AT + FIELD_LAT, SqlDbType.VarChar, 50, item.Lat ),
				SqlHelper.MakeInParameter( AT + FIELD_TIMEZONE, SqlDbType.Int, 4, item.TimeZone ),
				SqlHelper.MakeInParameter( AT + FILED_SORTORDER, SqlDbType.Int, 4, item.SortOrder ),
				SqlHelper.MakeInParameter( AT + FIELD_ACTION_DATE, SqlDbType.DateTime, 8, item.ActionDate == DateTime.MinValue ? DateTime.Now : item.ActionDate ),
                SqlHelper.MakeInParameter( AT + FIELD_ACTION_BY, SqlDbType.NVarChar, 50, item.ActionBy ?? String.Empty ),
                SqlHelper.MakeParameter( AT + FIELD_RETURN_VALUE, SqlDbType.Int, 4, ParameterDirection.Output, -1 )
            };
            SqlHelper.ExecuteNonQuery( dbConnectionString, CommandType.StoredProcedure, PROC_CREATE_DISTRICT, p );
            return Convert.ToInt32( p[p.Length - 1].Value );
        }

        public int UpdateItem( DistrictVO item )
        {
            SqlParameter[] p = new SqlParameter[] {
                SqlHelper.MakeInParameter( AT + FIELD_DISTRICTID, SqlDbType.Int, 4, item.DistrictId ),
                SqlHelper.MakeInParameter( AT + FIELD_NAME, SqlDbType.NVarChar, 50, item.Name ),
                SqlHelper.MakeInParameter( AT + FIELD_DESCRIPTION, SqlDbType.NVarChar, 255, item.Description ),
                //SqlHelper.MakeInParameter( AT + FIELD_LFT, SqlDbType.Int, 4, item.Lft ),
                //SqlHelper.MakeInParameter( AT + FIELD_RGT, SqlDbType.Int, 4, item.Rgt ),
                SqlHelper.MakeInParameter( AT + FIELD_LNG, SqlDbType.VarChar, 50, item.Lng ),
                SqlHelper.MakeInParameter( AT + FIELD_LAT, SqlDbType.VarChar, 50, item.Lat ),
				SqlHelper.MakeInParameter( AT + FIELD_TIMEZONE, SqlDbType.Int, 4, item.TimeZone ),
				SqlHelper.MakeInParameter( AT + FILED_SORTORDER, SqlDbType.Int, 4, item.SortOrder ),
				SqlHelper.MakeInParameter( AT + FIELD_ACTION_DATE, SqlDbType.DateTime, 8, item.ActionDate == DateTime.MinValue ? DateTime.Now : item.ActionDate ),
                SqlHelper.MakeInParameter( AT + FIELD_ACTION_BY, SqlDbType.NVarChar, 50, item.ActionBy ?? String.Empty ),
                SqlHelper.MakeParameter( AT + FIELD_RETURN_VALUE, SqlDbType.Int, 4, ParameterDirection.Output, -1 )
            };
            SqlHelper.ExecuteNonQuery( dbConnectionString, CommandType.StoredProcedure, PROC_UPDATE_DISTRICT, p );
            return Convert.ToInt32( p[p.Length - 1].Value );
        }

        #endregion

        #region Helpers
        private List<DistrictVO> GetSQLResults(string sql, CommandType cmdType, SqlParameter[] p) {
            List<DistrictVO> entities = new List<DistrictVO>();
            IDataReader reader = null;

            try {
                reader = SqlHelper.ExecuteReader( dbConnectionString, cmdType, sql, p );
                while (reader.Read()) {
                    entities.Add( LoadDistrict( reader ) );
                }
            }
            finally {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }
            
            return entities;
        }

        private DistrictVO LoadDistrict(IDataReader reader) {
            DistrictVO item = new DistrictVO();
            SetBaseProperties(reader, item);

            item.DistrictId = ReadInt32( reader, FIELD_DISTRICTID );
            item.Name = ReadString( reader, FIELD_NAME );
            item.Description = ReadString( reader, FIELD_DESCRIPTION );
            item.Lft = ReadInt32( reader, FIELD_LFT );
            item.Rgt = ReadInt32( reader, FIELD_RGT );
            item.Lng = ReadString( reader, FIELD_LNG );
            item.Lat = ReadString( reader, FIELD_LAT );
			item.TimeZone = ReadInt32( reader, FIELD_TIMEZONE );
			item.SortOrder = ReadInt32( reader, FILED_SORTORDER );
            return item;
        }
        #endregion
    }
}