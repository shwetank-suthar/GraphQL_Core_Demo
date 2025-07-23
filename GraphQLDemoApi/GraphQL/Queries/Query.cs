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
            => context.UserLogins.Take(100);
        
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
    }
}