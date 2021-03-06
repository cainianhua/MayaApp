/*
 * Code Generator v1.0
 * 2014-11-11 23:08:31
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maya.Services.VO;


namespace Maya.Services.DAO 
{
    public interface IDistrictProvider 
    {
        List<DistrictVO> GetItems();

        DistrictVO GetItem( int districtId );

		List<DistrictVO> GetParentItems( int districtId );

		List<DistrictVO> GetChildItems( int districtId );

        void DeleteItem( int districtId );


        int UpdateItem( DistrictVO item );
        int SaveItem( int parentId, DistrictVO item );

		List<DistrictVO> GetItems( string searchText );
    }
}