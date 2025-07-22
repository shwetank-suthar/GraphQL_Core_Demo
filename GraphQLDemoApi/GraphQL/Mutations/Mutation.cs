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

    public UserLogin CreateUserLogin(CreateUserLoginInput input, [Service] WebLineIndiaBackup15nov2024Context context)
    {
        var user = new UserLogin
        {
            Username = input.Username,
            Password = input.Password,
            Email = input.Email,
            Name = input.Name,
            PhoneNumber = input.PhoneNumber
        };

        context.UserLogins.Add(user);
        context.SaveChanges();
        return user;
    }

    public UserLogin? UpdateUserLogin(UpdateUserLoginInput input, [Service] WebLineIndiaBackup15nov2024Context context)
    {
        var user = context.UserLogins.FirstOrDefault(u => u.UserId == input.UserId);
        if (user == null) return null;

        if (input.Password != null) user.Password = input.Password;
        if (input.Email != null) user.Email = input.Email;
        if (input.Name != null) user.Name = input.Name;
        if (input.PhoneNumber != null) user.PhoneNumber = input.PhoneNumber;

        context.SaveChanges();
        return user;
    }
}
