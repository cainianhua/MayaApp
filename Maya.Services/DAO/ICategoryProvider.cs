/*
 * Code Generator v1.0
 * 2014-11-04 23:02:05
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maya.Services.VO;


namespace Maya.Services.DAO 
{
    public interface ICategoryProvider 
    {
        List<CategoryVO> GetItems();

        CategoryVO GetItem( int categoryId );

        void DeleteItem( int categoryId );


        void SaveOrUpdateItem( CategoryVO item );
    }
}