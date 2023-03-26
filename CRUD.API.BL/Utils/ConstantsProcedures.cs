using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.API.BL.Utils
{
    public static class ConstantsProcedures
    {

        // USER
        public static string USERS_CREATE = "spUsersCreate";
        public static string USERS_DELETE = "spUsersDelete";
        public static string USERS_GET = "spUsersGet";
        public static string USERS_GET_LIST = "spUsersGetList";
        public static string USERS_UPDATE = "spUsersUpdate";
        public static string USERS_UPDATE_PASSWORD = "spUsersUpdatePassword";
        public static string USERS_VALIDATE_IDENTITY = "spUsersValidateIdentity";
        public static string USERS_VALIDATE_PASSWORD = "spUsersValidatePassword";


        // CLIENT
        public static string CLIENTS_CREATE = "spClientsCreate";
        public static string CLIENTS_DELETE = "spClientsDelete";
        public static string CLIENTS_GET = "spClientsGet";
        public static string CLIENTS_GET_LIST = "spClientsGetList";
        public static string CLIENTS_UPDATE = "spClientsUpdate";


        // INVOICE
        public static string INVOICES_CREATE = "spInvocesCreate";
        public static string INVOICES_DELETE = "spInvocesDelete";
        public static string INVOICES_GET = "spInvocesGet";
        public static string INVOICES_GET_LIST = "spInvocesGetList";
        public static string INVOICES_UPDATE = "spInvocesUpdate";

        // PRODUCT
        public static string PRODUCTS_CREATE = "spProductsCreate";
        public static string PRODUCTS_DELETE = "spProductsDelete";
        public static string PRODUCTS_GET = "spProductsGet";
        public static string PRODUCTS_GET_LIST = "spProductsGetList";
        public static string PRODUCTS_UPDATE = "spProductsUpdate";




    }
}
