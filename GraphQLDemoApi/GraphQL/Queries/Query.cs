using GraphQLDemoApi.Models;
using GraphQLDemoApi.Data;

namespace GraphQLDemoApi.GraphQL.Queries
{
    public class Query
    {
        // Dummy query to test
        public string Hello() => "GraphQL is working!";
        // Query all users
        public IQueryable<UserLogin> GetUserLogins([Service] WebLineIndiaBackup15nov2024Context context)
            => context.UserLogins.OrderByDescending(x => x.UserId).Take(100);

        /// <summary>
        /// Get UserLogin By Username
        /// </summary>
        /// <param name="username"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public UserLogin? GetUserLoginByUsername(string username, [Service] WebLineIndiaBackup15nov2024Context context)
        {
            return context.UserLogins.FirstOrDefault(u => u.Username.ToLower() == username.ToLower());
        }
        
        public async Task<UserLogin> UpdateUserLogin(
            int userId,
            string? username,
            string? email,
            string? phoneNumber,
            string? name,
            [Service] WebLineIndiaBackup15nov2024Context context)
        {
            var user = await context.UserLogins.FindAsync(userId);
            if (user == null) throw new Exception("User not found");

            user.Username = username ?? user.Username;
            user.Email = email ?? user.Email;
            user.PhoneNumber = phoneNumber ?? user.PhoneNumber;
            user.Name = name ?? user.Name;

            await context.SaveChangesAsync();
            return user;
        }

    }
}