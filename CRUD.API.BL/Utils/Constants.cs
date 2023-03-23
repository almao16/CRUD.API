using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.API.BL.Utils
{
    public static class Constants
    {
        public static string CLAIM_USER_ID = "idUser";

        public static string CHARACTER_POINT = ".";
        public static string CHARACTER_HYPHEN = "-";

        public static string CONFIG_CRUD_NAME_PRODUCT = "CRUD:NameProduct";
        public static string CONFIG_CRUD_NAME_APPLICATION = "CRUD:NameApplication";
        public static string CONFIG_CRUD_JWT_KEY = "CRUD:JWTKey";

        public static string CONFIG_AZURE_RESOURCE_BLOB = "Azure:ResourceBlob";

        public static string CONFIG_CONNECTION_AZURE_STORAGE = "AzureStorage";

        public static string GUID_INTERNALCOMPANY = "constants:guidInternalCompany";

        public static string MESSAGE_TRANSACTION_SUCCESSFUL = "Transacción exitosa.";

        public static string MESSAGE_ERROR_CREATE = "Error al intentar registrar {0}";
        public static string MESSAGE_ERROR_DUPLICATE = "El item que intenta introducir ya existe {0}";
        public static string MESSAGE_ERROR_UPDATE = "Error al intentar actualizar {0}.";
        public static string MESSAGE_ERROR_DELETE = "Error al intentar eliminar {0}.";
        public static string MESSAGE_ERROR_UPLOAD = "Error al intentar cargar {0}.";
        public static string MESSAGE_ERROR_SEND = "Error al intentar enviar {0}.";
        public static string MESSAGE_ERROR_VERIFY = "Error al intentar verificar {0}.";
        public static string MESSAGE_ERROR_DOWNLOAD = "Error al intentar descargar {0}.";
        public static string MESSAGE_ERROR_PASSWORD = "Contraseña inválida";
        public static string MESSAGE_ERROR_EMAIL = "Correo Electronico invalido";
        public static string MESSAGE_ERROR_NOT_FOUND = "{0} no existe.";
        public static string MESSAGE_ERROR_NOT_VALID = "{0} no válido.";
    }

}
