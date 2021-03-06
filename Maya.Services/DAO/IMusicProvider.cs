/*
 * Code Generator v1.0
 * 2014-11-07 23:46:08
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maya.Services.VO;


namespace Maya.Services.DAO 
{
    public interface IMusicProvider 
    {
        List<MusicVO> GetItems();

        MusicVO GetItem( int musicId );

        void DeleteItem( int musicId );

        List<MusicVO> GetItemsByDistrictId( int districtId );

        void DeleteItemsByDistrictId( int districtId );

        int SaveOrUpdateItem( MusicVO item );

		List<MusicVO> GetItemsByDistrictCriteria( int districtId );
    }
}