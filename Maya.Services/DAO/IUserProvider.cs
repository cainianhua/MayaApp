/*
 * Code Generator v1.0
 * 2014-11-07 23:46:10
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maya.Services.VO;


namespace Maya.Services.DAO 
{
    public interface IUserProvider 
    {
        List<UserVO> GetItems();

        UserVO GetItem( long userId );

        void DeleteItem( long userId );


        UserVO GetItemByUserName( string userName );

        void DeleteItemByUserName( string userName );
        UserVO GetItemByEmail( string email );

        void DeleteItemByEmail( string email );
        int SaveOrUpdateItem( UserVO item );
    }
}