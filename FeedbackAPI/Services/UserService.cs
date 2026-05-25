using FeedbackAPI.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FeedbackAPI.DTOs;
using FeedbackAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;

namespace FeedbackAPI.Services;
public class UserService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public UserService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<UserResponseDto> registerAsync(RegisterUserDto dto)
    {
        if(await _context.Users.AnyAsync(u => u.username == dto.username))
        {
            throw new Exception("Este nome de usuário já está em uso");

        }

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.password);

        var user = new User
        {
            username = dto.username,
            password = passwordHash
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserResponseDto
        {
            id = user.id,
            username = user.username
        };

    }
    
    public async Task<string> loginAsync(LoginUserDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.username == dto.username);

        if(user == null || !BCrypt.Net.BCrypt.Verify(dto.password, user.password))
        {
            return null;
        }

        var token = GenerateJwtToken(user);

        return token;
    }

    private string GenerateJwtToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor{
             Subject = new ClaimsIdentity(new[]
             {
                 new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                 new Claim(ClaimTypes.Name, user.username)
             }),

             Expires = DateTime.UtcNow.AddHours(8),
             SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
             )
        };
         var token = tokenHandler.CreateToken(tokenDescriptor);

         return tokenHandler.WriteToken(token);
    }

    public async Task<List<User>> getAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }
}