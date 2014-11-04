/*
 * Code Generator v1.0
 * 2014-11-04 23:02:06
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maya.Services.DAO;
using Maya.Services.VO;


namespace Maya.Services.BO 
{
    public class ProductBO : BaseBO 
    {
        private IProductProvider provider;
        private static ProductBO instance;

        private ProductBO() {
            provider = (IProductProvider)this.CreateProvider("Product");
        }

        public static ProductBO GetInstance() {
            if (instance == null) {
                instance = new ProductBO();
            }
            return instance;
        }

        public List<ProductVO> GetItems() {
            return provider.GetItems();
        }


        public ProductVO GetItem( int productId ) {
            return provider.GetItem( productId );
        }

        public void DeleteItem( int productId ) {
            provider.DeleteItem( productId );
        }



        public void SaveOrUpdateItem( ProductVO item ) {
            if ( item == null ) return;
            provider.SaveOrUpdateItem( item );
            

        }
    }
}