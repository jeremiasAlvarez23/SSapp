using BCrypt.Net;

namespace SecuritysApp.Utils
{
    public static class PasswordHelper
    {
        public static string Encriptar(string clave)
        {
            return BCrypt.Net.BCrypt.HashPassword(clave);
        }

        public static bool Verificar(string clavePlano, string hashGuardado)
        {
            return BCrypt.Net.BCrypt.Verify(clavePlano, hashGuardado);
        }
    }
}
