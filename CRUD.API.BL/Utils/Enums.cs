using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.API.BL.Utils
{
    public static class Enums
    {
        public enum Entity
        {
            MODULE = 1,
            ROL = 2,
            DIRECTOR = 3,
            PERMISO = 2,
            USUARIO = 3,
            CATALOGO = 4,
            ARCHIVO = 5,
            FOTO = 6,
            PASSWORD = 7,
            CORREO = 8,
            ASISTENCIA = 9,
            DOCUMENTO = 10,
            TIENDA,
            ACCESOINVITADOS,
            PROYECTO
        }

        public enum FileExtension
        {
            MP4,
            WEBM,
            OGV,
            MOV
        }

        public enum BlobContainer
        {
            ATTACHMENTS = 1,
            DOCUMENTS = 2,
            PHOTOS = 3,
            LOGOS = 4,
            LANDING

        }
    }

}
