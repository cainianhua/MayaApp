/*
 * Code Generator v1.0
 * 2014-11-07 23:46:09
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

        List<ArticleVO> GetItemsByDistrictId( int districtId );

        void DeleteItemsByDistrictId( int districtId );
        List<ArticleVO> GetItemsByCategoryId( int categoryId );

        void DeleteItemsByCategoryId( int categoryId );

        int SaveOrUpdateItem( ArticleVO item );

		List<ArticleVO> GetItems( int categoryId, int districtId );
    }
}