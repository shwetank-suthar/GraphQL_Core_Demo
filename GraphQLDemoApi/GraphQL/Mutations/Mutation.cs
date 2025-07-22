using GraphQLDemoApi.Models;
using GraphQLDemoApi.Data;

namespace GraphQLDemoApi.GraphQL.Mutations;

public class Mutation
{
    public LoginPayload Login(LoginInput input, [Service] WebLineIndiaBackup15nov2024Context context)
    {
        var user = context.UserLogins
            .FirstOrDefault(u => u.Username == input.Username && u.Password == input.Password);

        if (user == null)
        {
            return new LoginPayload { Success = false, Message = "Invalid credentials" };
        }

        return new LoginPayload { Success = true, Message = "Login successful" };
    }
}
