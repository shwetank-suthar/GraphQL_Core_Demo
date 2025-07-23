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

    public async Task<UserLogin> UpdateUserLogin(
        int userId,
        string? username,
        string? email,
        string? phoneNumber,
        string? name,
        [Service] WebLineIndiaBackup15nov2024Context context)
    {
        var user = await context.UserLogins.FindAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        user.Username = username ?? user.Username;
        user.Email = email ?? user.Email;
        user.PhoneNumber = phoneNumber ?? user.PhoneNumber;
        user.Name = name ?? user.Name;

        await context.SaveChangesAsync();
        return user;
    }

    public async Task<UserLogin?> UpdateUserLoginAsync(
        int userId,
        string? username,
        string? email,
        string? phoneNumber,
        string? name,
        [Service] WebLineIndiaBackup15nov2024Context context)
    {
        var user = await context.UserLogins.FindAsync(userId);
        if (user == null) return null;

        user.Username = username ?? user.Username;
        user.Email = email ?? user.Email;
        user.PhoneNumber = phoneNumber ?? user.PhoneNumber;
        user.Name = name ?? user.Name;

        await context.SaveChangesAsync();
        return user;
    }
}
