namespace Common.Enums
{
    public class Enums
    {
        public enum TypePermission
        {
            Usuarios = 1,
            Roles = 2,
            Permisos = 3,
        }

        public enum Permission
        {
            //Usuarios
            CrearUsuarios = 1,
            ActualizarUsuarios = 2,
            EliminarUsuarios = 3,
            ConsultarUsuarios = 4,

            //Roles
            ActualizarRoles = 5,
            ConsultarRoles = 6,

            //Permisos
            ActualizarPermisos = 7,
            ConsultarPermisos = 8,
            DenegarPermisos = 9,

        }

        public enum RolUser
        {
            Administrador = 1,
            Estandar = 2,
        }

        public enum TypeFile
        {
            Image = 1,
            PdfWord = 2,
        }
    }
}
