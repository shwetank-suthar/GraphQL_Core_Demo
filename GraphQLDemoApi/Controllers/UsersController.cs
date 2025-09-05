using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GraphQLDemoApi.Data;
using GraphQLDemoApi.Models;

namespace GraphQLDemoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly WebLineIndiaBackup15nov2024Context _context;

    public UsersController(WebLineIndiaBackup15nov2024Context context)
    {
        _context = context;
    }

    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns>List of all users</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserLogin>>> GetUsers()
    {
        var users = await _context.UserLogins
            .OrderByDescending(x => x.UserId)
            .Take(100)
            .ToListAsync();
        
        return Ok(users);
    }

    /// <summary>
    /// Get user by ID
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>User details</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<UserLogin>> GetUser(int id)
    {
        var user = await _context.UserLogins.FindAsync(id);

        if (user == null)
        {
            return NotFound($"User with ID {id} not found.");
        }

        return Ok(user);
    }

    /// <summary>
    /// Search users by username
    /// </summary>
    /// <param name="username">Username to search for</param>
    /// <returns>List of matching users</returns>
    [HttpGet("search/{username}")]
    public async Task<ActionResult<IEnumerable<UserLogin>>> SearchUsers(string username)
    {
        var users = await _context.UserLogins
            .Where(u => u.Username.Contains(username))
            .ToListAsync();

        return Ok(users);
    }

    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="user">User data</param>
    /// <returns>Created user</returns>
    [HttpPost]
    public async Task<ActionResult<UserLogin>> CreateUser([FromBody] CreateUserRequest user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var newUser = new UserLogin
        {
            Username = user.Username,
            Password = user.Password,
            Email = user.Email,
            Name = user.Name,
            PhoneNumber = user.PhoneNumber
        };

        _context.UserLogins.Add(newUser);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = newUser.UserId }, newUser);
    }

    /// <summary>
    /// Update user information
    /// </summary>
    /// <param name="id">User ID</param>
    /// <param name="user">Updated user data</param>
    /// <returns>Updated user</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRequest user)
    {
        var existingUser = await _context.UserLogins.FindAsync(id);
        if (existingUser == null)
        {
            return NotFound($"User with ID {id} not found.");
        }

        if (!string.IsNullOrEmpty(user.Username))
            existingUser.Username = user.Username;
        if (!string.IsNullOrEmpty(user.Email))
            existingUser.Email = user.Email;
        if (!string.IsNullOrEmpty(user.Name))
            existingUser.Name = user.Name;
        if (!string.IsNullOrEmpty(user.PhoneNumber))
            existingUser.PhoneNumber = user.PhoneNumber;

        await _context.SaveChangesAsync();

        return Ok(existingUser);
    }

    /// <summary>
    /// Delete a user
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>Success message</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.UserLogins.FindAsync(id);
        if (user == null)
        {
            return NotFound($"User with ID {id} not found.");
        }

        _context.UserLogins.Remove(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = $"User with ID {id} deleted successfully." });
    }
}

/// <summary>
/// Request model for creating a new user
/// </summary>
public class CreateUserRequest
{
    /// <summary>
    /// Username for the user
    /// </summary>
    public string Username { get; set; } = string.Empty;
    
    /// <summary>
    /// Password for the user
    /// </summary>
    public string Password { get; set; } = string.Empty;
    
    /// <summary>
    /// Email address
    /// </summary>
    public string? Email { get; set; }
    
    /// <summary>
    /// Full name
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Phone number
    /// </summary>
    public string? PhoneNumber { get; set; }
}

/// <summary>
/// Request model for updating user information
/// </summary>
public class UpdateUserRequest
{
    /// <summary>
    /// Username for the user
    /// </summary>
    public string? Username { get; set; }
    
    /// <summary>
    /// Email address
    /// </summary>
    public string? Email { get; set; }
    
    /// <summary>
    /// Full name
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Phone number
    /// </summary>
    public string? PhoneNumber { get; set; }
}
