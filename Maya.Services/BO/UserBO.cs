/*
 * Code Generator v1.0
 * 2014-11-07 23:46:10
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maya.Services.DAO;
using Maya.Services.VO;


namespace Maya.Services.BO 
{
    public class UserBO : BaseBO 
    {
        private IUserProvider provider;
        private static UserBO instance;

        private UserBO() {
            provider = (IUserProvider)this.CreateProvider("User");
        }

        public static UserBO GetInstance() {
            if (instance == null) {
                instance = new UserBO();
            }
            return instance;
        }

        public List<UserVO> GetItems() {
            return provider.GetItems();
        }


        public UserVO GetItem( long userId ) {
            return provider.GetItem( userId );
        }

        public void DeleteItem( long userId ) {
            provider.DeleteItem( userId );
        }


        public UserVO GetItemByUserName( string userName ) {
            return provider.GetItemByUserName( userName );
        }

        public void DeleteItemByUserName( string userName ) {
            provider.DeleteItemByUserName( userName );
        }
        public UserVO GetItemByEmail( string email ) {
            return provider.GetItemByEmail( email );
        }

        public void DeleteItemByEmail( string email ) {
            provider.DeleteItemByEmail( email );
        }

        public int SaveOrUpdateItem( UserVO item ) {
            if ( item == null ) return -1;
            return provider.SaveOrUpdateItem( item );

        }
    }
}