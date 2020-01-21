using System.Threading.Tasks;
using olx.api.Models;
using System.Security.Cryptography ;
using Microsoft.EntityFrameworkCore ;
using Microsoft.Extensions.Logging;

namespace olx.api.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context ;
        private readonly ILogger<AuthRepository> _logger;

        public AuthRepository ( DataContext context, ILogger<AuthRepository> logger) {
            this._context = context ;
            this._logger = logger ;
        }
        public async Task<User> Login(string username, string password)
        {
            // foreach(var a in _context.Users)
            // {
            //     _logger.LogInformation(a.Username + ":" + a.Id) ;
            // }

            var user =  await _context.Users.FirstOrDefaultAsync(x => x.Username==username ) ;

            // _logger.LogInformation ( user.ToString() ) ;

            // 1. List
            // 2. Compare
            // 3.  set value in user
            if ( user == null ) {
                return null ;
            }
            if ( !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) 
            {
                return null ;
            }
            return user ;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i=0; i<computedHash.Length;i++)
                {
                    if(computedHash[i] != passwordHash[i])
                    return false;
                }
            }

            return true;
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

        public async Task<bool> UserExists(string username)
        {
            if ( await _context.Users.AnyAsync(x => x.Username == username )) 
            {
                return true ;
            }
            return false;        
        }
    }
}