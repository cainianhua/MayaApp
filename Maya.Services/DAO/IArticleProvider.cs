/*
 * Code Generator v1.0
 * 2014-11-04 23:02:03
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maya.Services.VO;


namespace Maya.Services.DAO 
{
    public interface IArticleProvider 
    {
        List<ArticleVO> GetItems();

        ArticleVO GetItem( int articleId );

        void DeleteItem( int articleId );

        List<ArticleVO> GetItemsByCategoryId( int categoryId );

        void DeleteItemsByCategoryId( int categoryId );
        List<ArticleVO> GetItemsByDistrictId( int districtId );

        void DeleteItemsByDistrictId( int districtId );

        void SaveOrUpdateItem( ArticleVO item );
    }
}