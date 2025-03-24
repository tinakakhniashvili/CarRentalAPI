using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CarRentalApp.Data;
using CarRentalApp.DTOs;
using CarRentalApp.Interfaces;
using CarRentalApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CarRentalApp.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    public AuthService(IConfiguration configuration, ApplicationDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public async Task<ServiceResponse<int>> Register(UserRegisterDTO registerDTO)
    {
        var response = new ServiceResponse<int>();

        if (await UserExists(registerDTO.PhoneNumber))
        {
            response.Success = false;
            response.Message = "User already exists";
            return response;
        }

        CreatePasswordHash(registerDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = new User()
        {
            FirstName = registerDTO.FirstName,
            LastName = registerDTO.LastName,
            Email = registerDTO.Email,  
            PhoneNumber = registerDTO.PhoneNumber, 
            Role = registerDTO.Role,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        response.Data = user.Id;
        return response;
    }


    public async Task<ServiceResponse<string>> Login(UserLoginDTO loginDTO)
    {
        var response = new ServiceResponse<string>();

        var user = await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == loginDTO.PhoneNumber);

        if (user == null)
        {
            response.Success = false;
            response.Message = "User not found";
            return response;
        }
        else if (!VerifyPasswordHash(loginDTO.Password, user.PasswordHash, user.PasswordSalt))
        {
            response.Success = false;
            response.Message = "Incorrect password";
            return response;
        }
        else
        {
            var result = GenerateTokens(user, loginDTO.StaySignedIn);
            response.Data = result.AccessToken;
        }

        if (loginDTO.StaySignedIn)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        return response;
    }

    #region PrivateMethod
    private async Task<bool> UserExists(string phoneNumber)
    {
        if (await _context.Users.AnyAsync(x => x.PhoneNumber.ToLower() == phoneNumber))
            return true;
        return false;
    }
    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }

    private TokenDTO GenerateTokens(User user, bool staySignedIn)
    {
        string refreshToken = string.Empty;

        if (staySignedIn)
        {
            refreshToken = GenerateRefreshToken(user);
            user.RefreshToken = refreshToken;
            user.RefreshExpirationDate = DateTime.Now.AddDays(2);
        }

        var accessToken = GenerateAccessToken(user);

        return new TokenDTO() { AccessToken = accessToken, RefreshToken = refreshToken };
    }

    
        private string GenerateAccessToken(User user)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
            new Claim(ClaimTypes.Role, user.Role)
        };

        // foreach (var role in user.Roles)
        // {
        //     claims.Add(new Claim(ClaimTypes.Role, role.Name));
        // }

        SymmetricSecurityKey key =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWTOptions:Secret").Value));

        SigningCredentials creadentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = creadentials,
            Issuer = _configuration.GetSection("JWTOptions:Issuer").Value,
            Audience = _configuration.GetSection("JWTOptions:Audience").Value
        };

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

        SecurityToken token = handler.CreateToken(securityTokenDescriptor);

        return handler.WriteToken(token);
    }
    private string GenerateRefreshToken(User user)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
            new Claim(ClaimTypes.Role, user.Role)
        };

        // foreach (var role in user.Roles)
        // {
        //     claims.Add(new Claim(ClaimTypes.Role, role.Name));
        // }

        SymmetricSecurityKey key =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWTOptions:Secret").Value));

        SigningCredentials creadentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(2),
            SigningCredentials = creadentials,
            Issuer = _configuration.GetSection("JWTOptions:JwtOptions:Issuer").Value,
            Audience = _configuration.GetSection("JWTOptions:JwtOptions:Audience").Value
        };

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

        SecurityToken token = handler.CreateToken(securityTokenDescriptor);

        return handler.WriteToken(token);
    }
    #endregion
}