namespace SecuritysApp.Routes
{
    public static class ApiRoutes
    {
        public static class v1
        {
            public const string Root = "/api";
            public const string Version = "v1";
            public const string Base = Root + "/" + Version;

            public static class Usuario
            {
                public const string ObtenerTodo = Base + "/usuario";
            }
        }
    }
}
