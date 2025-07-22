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
            => context.UserLogins;
    }
}