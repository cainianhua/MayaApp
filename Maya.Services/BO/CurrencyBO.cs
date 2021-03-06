/*
 * Code Generator v1.0
 * 2014-11-24 23:23:51
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maya.Services.DAO;
using Maya.Services.VO;


namespace Maya.Services.BO 
{
    public class CurrencyBO : BaseBO 
    {
        private ICurrencyProvider provider;
        private static CurrencyBO instance;

        private CurrencyBO() {
            provider = (ICurrencyProvider)this.CreateProvider("Currency");
        }

        public static CurrencyBO GetInstance() {
            if (instance == null) {
                instance = new CurrencyBO();
            }
            return instance;
        }

        public List<CurrencyVO> GetItems() {
            return provider.GetItems();
        }


        public CurrencyVO GetItem( int id ) {
            return provider.GetItem( id );
        }

        public void DeleteItem( int id ) {
            provider.DeleteItem( id );
        }



        public int SaveOrUpdateItem( CurrencyVO item ) {
            if ( item == null ) return -1;
            return provider.SaveOrUpdateItem( item );

        }
    }
}