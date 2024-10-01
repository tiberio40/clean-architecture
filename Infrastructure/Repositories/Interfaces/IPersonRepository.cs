﻿using Core.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAllAsync();
        Task<Person> GetByIdAsync(string id);
        Task AddAsync(Person person);
        Task UpdateAsync(string id, Person person);
        Task DeleteAsync(string id);
    }
}
