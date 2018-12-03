using System;
using System.Threading.Tasks;
using eGlobalMartNg.api.Models;
using Microsoft.EntityFrameworkCore;

namespace eGlobalMartNg.api.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _db;
        public AuthRepository(DataContext context)
        {
            _db = context;

        }
        public async Task<User> Login(string userName, string passoWord)
        {
           var user=await _db.User.FirstOrDefaultAsync(x => x.Username == userName);
           if (user==null) return null;
           if (!varified(passoWord,user.PasswordHash,user.PasswordSalt)) return null;

           return user;
        }

        private bool varified(string passoWord, byte[] passwordHash, byte[] passwordSalt)
        {
             using(var hmac =new System.Security.Cryptography.HMACSHA512(passwordSalt) ){
                    var hash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passoWord));
                    for(int i=0;i<hash.Length;i++){
                        if (hash[i]!=passwordHash[i]) return false;
                    }
                   }
                return true;
        }

        public async Task<User> Register(User user, string passoWord)
        {
            byte[] hash, salt;
            CreatHash(passoWord, out hash, out salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            await _db.User.AddAsync(user);
            await _db.SaveChangesAsync();
            return user;
        }

        private void CreatHash(string passoWord, out byte[] hash, out byte[] salt)
        {
            using(var hmac =new System.Security.Cryptography.HMACSHA512() ){
                    salt=hmac.Key;
                    hash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passoWord));
                }
         } 

        public async Task<bool> UserExist(string userName)
        {
            if (await _db.User.AnyAsync(x=>x.Username == userName)) return true;

            return false;

        }
    }
}