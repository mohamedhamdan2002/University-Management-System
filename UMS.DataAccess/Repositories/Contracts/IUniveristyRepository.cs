﻿using UMS.DataAccess.Entities.Models;

namespace UMS.DataAccess.Repositories.Contracts
{
    public interface IUniveristyRepository 
    {
        Task<IEnumerable<University>> GetUniversitiesAsync(bool trackChanges, string[]? includes = null);
        Task<University?> GetUniversityAsync(Guid universityId, bool trackChanges, string[]? includes = null);
        void CreateUniversity(University university);
        void DeleteUniversity(University university);
    }
}
