using GestionSeguridadAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace GestionSeguridadAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            // Lógica de autenticación
            var hashedPassword = HashPassword(password); // Asegúrate de que las contraseñas se hashean
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == hashedPassword);

            return user;
        }

        public async Task<bool> RegisterAsync(string adminUsername, string adminPassword, string username, string email, string password)
        {
            var adminUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == adminUsername && u.PasswordHash == adminPassword);

            if (adminUser == null || adminUser.Role != "Admin")
            {
                // Agrega un registro para depurar el problema
                Console.WriteLine("Admin user not found or not authorized.");
                return false;
            }

            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (existingUser != null)
            {
                // Agrega un registro para depurar el problema
                Console.WriteLine("User already exists.");
                return false;
            }

            var user = new User
            {
                Username = username,
                Email = email,
                PasswordHash = HashPassword(password),
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return true;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
