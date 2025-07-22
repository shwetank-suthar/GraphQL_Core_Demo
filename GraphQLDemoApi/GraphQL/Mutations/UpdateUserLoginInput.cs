namespace GraphQLDemoApi.GraphQL.Mutations;

public class UpdateUserLoginInput
{
    public int UserId { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
}
