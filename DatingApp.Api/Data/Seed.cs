using System.Collections.Generic;
using System.Linq;
using DatingApp.Api.Models;
using Newtonsoft.Json;

namespace DatingApp.Api.Data
{
    public static class Seed
    {
        public static void SeedUsers(DataContext dataContext)
        {

            if (!dataContext.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                foreach (var user in users)
                {
                    byte[] passwordHash, passwordSalt;
                    CratePasswordHash("password", out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.Username = user.Username.ToLower();

                    dataContext.Users.Add(user);
                }
                dataContext.SaveChanges();
            }
        }

        private static void CratePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

    }
}