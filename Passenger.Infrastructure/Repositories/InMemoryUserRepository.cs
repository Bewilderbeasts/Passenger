﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Passenger.Core.Repositories;

public class InMemoryUserRepository : IUserRepository
{

    public static ISet<User> _users = new HashSet<User>();
    
    

    public async Task<User> GetAsync(Guid id)
        => await Task.FromResult(_users.Single(x => x.Id == id));

    public async Task<User> GetAsync(string email)
     => await Task.FromResult(_users.SingleOrDefault(x => x.Email == email.ToLowerInvariant()));

    public async Task<IEnumerable<User>> GetAllAsync()
        => await Task.FromResult(_users);

    public async Task RemoveAsync(Guid id)
    {
        var user = await GetAsync(id);
        _users.Remove(user); 
        await Task.CompletedTask;
    }

    public async Task AddAsync(User user)
    {
       _users.Add(user);
       await Task.CompletedTask;
    }

    public async Task UpdateAsync(User user)
    {
       await Task.CompletedTask;
    }

    public Task<IEnumerable<User>> BrowseAsync()
    {
        throw new NotImplementedException();
    }
}
