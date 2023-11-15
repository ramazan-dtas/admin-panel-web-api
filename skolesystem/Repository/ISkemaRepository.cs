using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
using skolesystem.Data;
using skolesystem.DTOs;
using skolesystem.Models;

public interface ISkemaRepository
{
    Task<IEnumerable<Skema>> GetAll();
    Task<Skema> GetById(int id);
    Task<int> Create(Skema skema);
    Task Update(int id, SkemaCreateDto skemaDto);
    Task Delete(int id);
}


public class SkemaRepository : ISkemaRepository
{
    private readonly SkemaDbContext _context;

    public SkemaRepository(SkemaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Skema>> GetAll()
    {
        return await _context.Skema.ToListAsync();
    }

    public async Task<Skema> GetById(int id)
    {
        return await _context.Skema.FindAsync(id);
    }

    public async Task<int> Create(Skema skema)
    {
        _context.Skema.Add(skema);
        await _context.SaveChangesAsync();
        return skema.schedule_id;
    }

    public async Task Update(int id, SkemaCreateDto skemaDto)
    {
        var skemaToUpdate = await _context.Skema.FindAsync(id);

        if (skemaToUpdate == null)
        {
            throw new ArgumentException("Skema not found");
        }

        // Map properties from DTO to the entity
        skemaToUpdate.subject_id = skemaDto.subject_id;
        skemaToUpdate.day_of_week = skemaDto.day_of_week;
        skemaToUpdate.subject_name = skemaDto.subject_name;
        skemaToUpdate.start_time = skemaDto.start_time;
        skemaToUpdate.end_time = skemaDto.end_time;
        skemaToUpdate.class_id = skemaDto.class_id;

        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var skemaToDelete = await _context.Skema.FindAsync(id);

        if (skemaToDelete == null)
        {
            throw new ArgumentException("Skema not found");
        }

        _context.Skema.Remove(skemaToDelete);
        await _context.SaveChangesAsync();
    }
}

