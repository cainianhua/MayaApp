/*
 * Code Generator v1.0
 * 2014-11-07 23:46:08
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
    internal class CategoryProvider : ProviderBase, ICategoryProvider 
    {
        #region SQLs Statements
        private const string SQL_GET_CATEGORIES_ALL = "SELECT * FROM Categories";
        private const string SQL_GET_CATEGORIES_PK = "SELECT * FROM Categories WHERE 1 = 1 AND [CategoryId] = @CategoryId";
        private const string SQL_DELETE_CATEGORIES_PK = "DELETE FROM Categories WHERE 1 = 1 AND [CategoryId] = @CategoryId";


        private const string PROC_SAVE_CATEGORY = "USP_Save_Category";
        #endregion

        #region Fields
        private const string FIELD_CATEGORYID = "CategoryId";
        private const string FIELD_NAME = "Name";
        private const string FIELD_DESCRIPTION = "Description";
        private const string FIELD_SORTORDER = "SortOrder";
        #endregion

        #region Methods
        public List<CategoryVO> GetItems() {
            return GetSQLResults( SQL_GET_CATEGORIES_ALL, null);
        }

        public CategoryVO GetItem(int categoryId) {
            CategoryVO item = null;
            SqlParameter[] p = new SqlParameter[] { 
                SqlHelper.MakeInParameter( AT + FIELD_CATEGORYID, SqlDbType.Int, 4, categoryId )
            };
            List<CategoryVO> items = GetSQLResults( SQL_GET_CATEGORIES_PK, p);
            if (items.Count > 0) item = items[0];
            return item;
        }

        public void DeleteItem(int categoryId) {
            SqlParameter[] p = new SqlParameter[] { 
                SqlHelper.MakeInParameter( AT + FIELD_CATEGORYID, SqlDbType.Int, 4, categoryId )
            };
            SqlHelper.ExecuteNonQuery(dbConnectionString, CommandType.Text, SQL_DELETE_CATEGORIES_PK, p);
        }
        
        public int SaveOrUpdateItem(CategoryVO item) {
            SqlParameter[] p = new SqlParameter[] {
                SqlHelper.MakeInParameter( AT + FIELD_CATEGORYID, SqlDbType.Int, 4, item.CategoryId ),
                SqlHelper.MakeInParameter( AT + FIELD_NAME, SqlDbType.NVarChar, 50, item.Name ),
                SqlHelper.MakeInParameter( AT + FIELD_DESCRIPTION, SqlDbType.NVarChar, 255, item.Description ),
                SqlHelper.MakeInParameter( AT + FIELD_SORTORDER, SqlDbType.Int, 4, item.SortOrder ),
                SqlHelper.MakeInParameter( AT + FIELD_ACTION_DATE, SqlDbType.DateTime, 8, item.ActionDate == DateTime.MinValue ? DateTime.Now : item.ActionDate ),
                SqlHelper.MakeInParameter( AT + FIELD_ACTION_BY, SqlDbType.NVarChar, 50, item.ActionBy ?? String.Empty ),
                SqlHelper.MakeParameter( AT + FIELD_RETURN_VALUE, SqlDbType.Int, 4, ParameterDirection.Output, -1 )
            };
            SqlHelper.ExecuteNonQuery( dbConnectionString, CommandType.StoredProcedure, PROC_SAVE_CATEGORY, p );
            return Convert.ToInt32( p[p.Length - 1].Value );
        }
        #endregion

        #region Helpers
        private List<CategoryVO> GetSQLResults(string sql, SqlParameter[] p) {
            List<CategoryVO> entities = new List<CategoryVO>();
            IDataReader reader = null;

            try {
                reader = SqlHelper.ExecuteReader( dbConnectionString, CommandType.Text, sql, p );
                while (reader.Read()) {
                    entities.Add( LoadCategory( reader ) );
                }
            }
            finally {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }
            
            return entities;
        }

        private CategoryVO LoadCategory(IDataReader reader) {
            CategoryVO item = new CategoryVO();
            SetBaseProperties(reader, item);

            item.CategoryId = ReadInt32( reader, FIELD_CATEGORYID );
            item.Name = ReadString( reader, FIELD_NAME );
            item.Description = ReadString( reader, FIELD_DESCRIPTION );
            item.SortOrder = ReadInt32( reader, FIELD_SORTORDER );
            return item;
        }
        #endregion
    }
}