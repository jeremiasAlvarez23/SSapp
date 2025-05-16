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
            }

            public static class Usuario
            {
                public const string Insertar = Base + "/usuario/insertar";
                public const string ObtenerTodo = Base + "/usuario";
                public const string ObtenerPorFiltro = Base + "/usuario/filtro";
                public const string ObtenerPorId = Base + "/usuario/{id}";
                public const string Editar = Base + "/usuario/editar/{id}";
                public const string Desactivar = Base + "/usuario/desactivar/{id}";
                public const string Activar = Base + "/usuario/activar/{id}";
            }

        }
    }
}
