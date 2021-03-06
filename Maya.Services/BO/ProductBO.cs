/*
 * Code Generator v1.0
 * 2014-11-07 23:46:07
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



        public int SaveOrUpdateItem( ProductVO item ) {
            if ( item == null ) return -1;
            return provider.SaveOrUpdateItem( item );

        }

		public List<ProductVO> GetItemsByDistrictCriteria( int districtId ) {
			if (districtId <= 0) return new List<ProductVO>();

			return provider.GetItemsByDistrictCriteria( districtId );
		}

        public List<ProductVO> GetItems(int? districtId)
        {
            if (districtId.HasValue) return this.GetItemsByDistrictCriteria(districtId.Value);

            return this.GetItems();
        }
	}
}