/*
 * Code Generator v1.0
 * 2014-11-24 23:23:51
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
    internal class CurrencyProvider : ProviderBase, ICurrencyProvider 
    {
        #region SQLs Statements
        private const string SQL_GET_CURRENCIES_ALL = "SELECT * FROM Currencies";
        private const string SQL_GET_CURRENCIES_PK = "SELECT * FROM Currencies WHERE 1 = 1 AND [Id] = @Id";
        private const string SQL_DELETE_CURRENCIES_PK = "DELETE FROM Currencies WHERE 1 = 1 AND [Id] = @Id";


        private const string PROC_SAVE_CURRENCY = "USP_Save_Currency";
        #endregion

        #region Fields
        private const string FIELD_ID = "Id";
        private const string FIELD_CODE = "Code";
        private const string FIELD_NAME = "Name";
        private const string FIELD_DESCRIPTION = "Description";
        #endregion

        #region Methods
        public List<CurrencyVO> GetItems() {
            return GetSQLResults( SQL_GET_CURRENCIES_ALL, null);
        }

        public CurrencyVO GetItem(int id) {
            CurrencyVO item = null;
            SqlParameter[] p = new SqlParameter[] { 
                SqlHelper.MakeInParameter( AT + FIELD_ID, SqlDbType.Int, 4, id )
            };
            List<CurrencyVO> items = GetSQLResults( SQL_GET_CURRENCIES_PK, p);
            if (items.Count > 0) item = items[0];
            return item;
        }

        public void DeleteItem(int id) {
            SqlParameter[] p = new SqlParameter[] { 
                SqlHelper.MakeInParameter( AT + FIELD_ID, SqlDbType.Int, 4, id )
            };
            SqlHelper.ExecuteNonQuery(dbConnectionString, CommandType.Text, SQL_DELETE_CURRENCIES_PK, p);
        }
        
        public int SaveOrUpdateItem(CurrencyVO item) {
            SqlParameter[] p = new SqlParameter[] {
                SqlHelper.MakeInParameter( AT + FIELD_ID, SqlDbType.Int, 4, item.Id ),
                SqlHelper.MakeInParameter( AT + FIELD_CODE, SqlDbType.Char, 3, item.Code ),
                SqlHelper.MakeInParameter( AT + FIELD_NAME, SqlDbType.NVarChar, 50, item.Name ),
                SqlHelper.MakeInParameter( AT + FIELD_DESCRIPTION, SqlDbType.NVarChar, 255, item.Description ),
                SqlHelper.MakeInParameter( AT + FIELD_ACTION_DATE, SqlDbType.DateTime, 8, item.ActionDate == DateTime.MinValue ? DateTime.Now : item.ActionDate ),
                SqlHelper.MakeInParameter( AT + FIELD_ACTION_BY, SqlDbType.NVarChar, 50, item.ActionBy ?? String.Empty ),
                SqlHelper.MakeParameter( AT + FIELD_RETURN_VALUE, SqlDbType.Int, 4, ParameterDirection.Output, -1 )
            };
            SqlHelper.ExecuteNonQuery( dbConnectionString, CommandType.StoredProcedure, PROC_SAVE_CURRENCY, p );
            return Convert.ToInt32( p[p.Length - 1].Value );
        }
        #endregion

        #region Helpers
        private List<CurrencyVO> GetSQLResults(string sql, SqlParameter[] p) {
            List<CurrencyVO> entities = new List<CurrencyVO>();
            IDataReader reader = null;

            try {
                reader = SqlHelper.ExecuteReader( dbConnectionString, CommandType.Text, sql, p );
                while (reader.Read()) {
                    entities.Add( LoadCurrency( reader ) );
                }
            }
            finally {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }
            
            return entities;
        }

        private CurrencyVO LoadCurrency(IDataReader reader) {
            CurrencyVO item = new CurrencyVO();
            SetBaseProperties(reader, item);

            item.Id = ReadInt32( reader, FIELD_ID );
            item.Code = ReadString( reader, FIELD_CODE );
            item.Name = ReadString( reader, FIELD_NAME );
            item.Description = ReadString( reader, FIELD_DESCRIPTION );
            return item;
        }
        #endregion
    }
}