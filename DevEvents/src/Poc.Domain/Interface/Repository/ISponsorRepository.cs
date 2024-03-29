﻿using Poc.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poc.Domain.Interface.Repository
{
    public interface ISponsorRepository
    {
        Task<IEnumerable<SponsorDto>> GetAll();

        Task<int> AddAsync(SponsorDto dto);

        Task<string> RemoveAsync(int id);

        Task<SponsorDto> SponsorExists(int id);

        bool NameSponsorExists(string name); 
    }
}