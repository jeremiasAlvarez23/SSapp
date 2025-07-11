namespace SecuritysApp.Routes
{
    public static class AppRoutes
    {
        public static class v1
        {
            public const string Root = "/api";
            public const string Version = "v1";
            public const string Base = Root + "/" + Version;

            public static class Login
            {
                public const string Post = Base + "/login";
                public const string Logout = Base + "/logout";
            }

            public static class Usuario
            {
                public const string Insertar = Base + "/usuario/insertar";
                public const string ObtenerTodo = Base + "/usuario";
                public const string ObtenerPorFiltro = Base + "/usuario/filtro";
                public const string ObtenerPorId = Base + "/usuario/{id}";
                public const string ObtenerConPaginacion = Base + "/usuario/paginado";
                public const string Editar = Base + "/usuario/editar/{id}";
                public const string Desactivar = Base + "/usuario/desactivar/{id}";
                public const string Activar = Base + "/usuario/activar/{id}";
            }

            public static class Rol
            {
                public const string Insertar = Base + "/rol/insertar";
                public const string ObtenerTodo = Base + "/rol";
                public const string Editar = Base + "/rol/editar/{id}";
                public const string Eliminar = Base + "/rol/desactivar/{id}";
            }

            public static class Perfil
            {
                public const string ObtenerPerfil = Base + "/perfil";
            }

            public static class UsuarioSistema
            {
                public const string Insertar = Base + "/usuariosistema/insertar";
                public const string ObtenerTodo = Base + "/usuariosistema";
                public const string Editar = Base + "/usuariosistema/editar";
                public const string Eliminar = Base + "/usuariosistema/eliminar";
            }

            public static class Sistema
            {
                public const string Insertar = Base + "/sistema/insertar";
                public const string ObtenerTodo = Base + "/sistema";
                public const string ObtenerPorId = Base + "/sistema/{id}";
                public const string ObtenerConPaginacion = Base + "/sistema/paginado";
                public const string Editar = Base + "/sistema/editar/{id}";
                public const string Desactivar = Base + "/sistema/desactivar/{id}";
                public const string Activar = Base + "/sistema/activar/{id}";
            }

            public static class Menu
            {
                public const string Base = AppRoutes.v1.Base + "/menu";

                public const string ObtenerPorUsuario = Base + "/usuario";
                public const string ObtenerTodo = Base + "/todo";
                public const string ObtenerPadres = Base + "/padres";
                public const string ObtenerUltimoOrden = Base + "/ultimo-orden/{padreId}";
                public const string Insertar = Base + "/insertar";
                public const string Editar = Base + "/editar";
                public const string Eliminar = Base + "/eliminar/{id}";
            }


        }
    }
}
