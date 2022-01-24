﻿using System;
using System.Text.RegularExpressions;

public class User
{   
    public static readonly Regex NameRegex = new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$");
    public Guid Id { get; protected set; }
    public string Email { get; protected set; }
    public string Password { get; protected set; }
    public string Salt { get; protected set; }
    public string Username { get; protected set; }
    public string Fullname { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }

    protected User()
    {

    }

    public User(string email, string username, string password, string salt)
    {
        Id = Guid.NewGuid();
        Email = email.ToLowerInvariant();
        Password = password;
        Salt = salt;
        Username = username;
        CreatedAt = DateTime.UtcNow;
    }

    public void SetUsername(string username)
    {
        if (!NameRegex.IsMatch(username))
        {
            throw new Exception("Username is invalid.");
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
            throw new Exception("Password is invalid.");
        }
        if (password.Length < 4 )
        {
            throw new Exception("Password must be at least 4 charatcers long.");
        }
        if (password.Length > 16 )
        {
            throw new Exception("Password must have maximum of 16 charatcers.");
        }
        if (Password == password)
        {
            return;
        }
        Password = password;
        UpdatedAt = DateTime.UtcNow;
    }

}
