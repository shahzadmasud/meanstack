using System.Threading.Tasks;
using olx.api.Models;
using System.Security.Cryptography ;

namespace olx.api.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context ;
        public AuthRepository ( DataContext context ) {
            this._context = context ;
        }
        public async Task<User> Login(string username, string password)
        {
            // var user = await _context.Users.FindAsync(x => x.username == username ) ;
            // if ( user == null ) {

            // }
            throw new System.NotImplementedException();
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt ;
            CreatePasswordHash (password, out passwordHash, out passwordSalt) ;

            user.PasswordHash = passwordHash ;
            user.PasswordSalt = passwordSalt ;

            await _context.Users.AddAsync(user) ;
            await _context.SaveChangesAsync() ;

            return user ;
        }

        private void CreatePasswordHash ( string password, out byte[] passwordHash, out byte[] passwordSalt ) 
        {
            using ( var hmac = new HMACSHA512() ) 
            {
                passwordSalt = hmac.Key ;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)) ;
            }
        }

        public Task<bool> UserExists(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}