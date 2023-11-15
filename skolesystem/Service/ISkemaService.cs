using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using skolesystem.DTOs;
using skolesystem.Models;

public interface ISkemaService
{
    Task<IEnumerable<Skema>> GetAllSchemata();
    Task<Skema> GetSkemaById(int id);
    Task<int> CreateSkema(Skema skema);
    Task UpdateSkema(int id, SkemaCreateDto skemaDto);
    Task DeleteSkema(int id);
}

public class SkemaService : ISkemaService
{
    private readonly ISkemaRepository _skemaRepository;

    public SkemaService(ISkemaRepository skemaRepository)
    {
        _skemaRepository = skemaRepository;
    }

    public async Task<IEnumerable<Skema>> GetAllSchemata()
    {
        return await _skemaRepository.GetAll();
    }

    public async Task<Skema> GetSkemaById(int id)
    {
        return await _skemaRepository.GetById(id);
    }

    public async Task<int> CreateSkema(Skema skema)
    {
        return await _skemaRepository.Create(skema);
    }

    public async Task UpdateSkema(int id, SkemaCreateDto skemaDto)
    {
        await _skemaRepository.Update(id, skemaDto);
    }

    public async Task DeleteSkema(int id)
    {
        await _skemaRepository.Delete(id);
    }
}
