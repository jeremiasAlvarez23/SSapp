using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SecuritysApp.Models.JwtService
{
    public static class JwtService
    {
        public static string GenerarToken(int usuarioId, string email, List<int> sistemaIds, string rol)
        {
            var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");

            if (string.IsNullOrWhiteSpace(secretKey) || secretKey.Length < 32)
            {
                throw new Exception("La clave JWT no está definida o es demasiado corta. Debe tener al menos 32 caracteres.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("UsuarioId", usuarioId.ToString()),
                new Claim("Email", email),
                new Claim("Sistemas", string.Join(",", sistemaIds)),
                new Claim(ClaimTypes.Role, rol) // 🔐 Incluimos el Rol como claim
            };

            var token = new JwtSecurityToken(
                issuer: "SecuritysApp",
                audience: "SecuritysApp",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(4),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
