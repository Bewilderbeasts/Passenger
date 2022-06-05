using System;
using System.Text.RegularExpressions;
using Passenger.Core.Domain;

public class User
{   
    public static readonly Regex NameRegex = new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$");
    public Guid Id { get; protected set; }
    public string Email { get; protected set; }
    public string Password { get; protected set; }
    public string Salt { get; protected set; }
    public string Username { get; protected set; }
    public string Fullname { get; protected set; }
    public string Role {get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }

    protected User()
    {

    }

    public User(Guid userId, string email, string username, string password, string salt,string role)
    {
        Id = userId;
        Email = email.ToLowerInvariant();
        Password = password;
        Salt = salt;
        Role = role;
        Username = username;
        CreatedAt = DateTime.UtcNow;
    }


    public void SetUserId(string UserId)
    {
        
    }
    public void SetUsername(string username)
    {
        if (!NameRegex.IsMatch(username))
        {
            throw new DomainException(ErrorCodes.InvalidUsername, "Username is invalid.");
        }
        if (String.IsNullOrEmpty(username))
        {
             throw new DomainException(ErrorCodes.InvalidUsername, "Username cannot be empty.");
        }

        Username = username.ToLowerInvariant();
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new Exception("Email is invalid.");
        }
        if (Email == email)
        {
            return;
        }
        Email = email.ToLowerInvariant();
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
             throw new DomainException(ErrorCodes.InvalidPassword, "Password cannot be empty.");
        }
        if (password.Length < 4 )
        {
            throw new DomainException(ErrorCodes.InvalidPassword, "Password must be at least 4 charatcers long.");
        }
        if (password.Length > 16 )
        {
            throw new DomainException(ErrorCodes.InvalidPassword, "Password must have maximum of 16 charatcers.");
        }
        if (Password == password)
        {
            return;
        }
        Password = password;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetRole(string role)
    {
        if (Role == role)
        {
            return;
        }
        Role = role;
        UpdatedAt = DateTime.UtcNow;
    }

}

