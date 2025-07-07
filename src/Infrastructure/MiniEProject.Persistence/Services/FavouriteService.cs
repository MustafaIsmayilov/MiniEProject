using System;
using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniEProject.Application.Abstracts.Services;
using MiniEProject.Application.DTOs.FavouriteDtos;
using MiniEProject.Application.Shared.Responses;
using MiniEProject.Domain.Entities;
using MiniEProject.Persistence.Contexts;

namespace MiniEProject.Persistence.Services;

public class FavouriteService : IFavouriteService
{
    private readonly MiniEProjectDbContext _context;
    private readonly IMapper _mapper;

    public FavouriteService(MiniEProjectDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BaseResponse<FavouriteGetDto>> CreateAsync(FavouriteCreateDto dto)
    {
        var favourite = _mapper.Map<Favourite>(dto);

        await _context.Favourites.AddAsync(favourite);
        await _context.SaveChangesAsync();

        favourite = await _context.Favourites
            .Include(f => f.Product)
            .FirstOrDefaultAsync(f => f.Id == favourite.Id);

        var resultDto = _mapper.Map<FavouriteGetDto>(favourite!);
        return new BaseResponse<FavouriteGetDto>("Favourite created successfully", resultDto, HttpStatusCode.OK);
    }

    
    public async Task<BaseResponse<string>> DeleteAsync(Guid id)
    {
        var favourite = await _context.Favourites.FindAsync(id);
        if (favourite == null)
            return new BaseResponse<string>("Favourite not found", null, HttpStatusCode.NotFound);

        _context.Favourites.Remove(favourite);
        await _context.SaveChangesAsync();

        return new BaseResponse<string>("Favourite deleted successfully", null, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<FavouriteGetDto>>> GetAllAsync()
    {
        var favourites = await _context.Favourites
            .Include(f => f.Product)
            .ToListAsync();

        var dtos = _mapper.Map<List<FavouriteGetDto>>(favourites);
        return new BaseResponse<List<FavouriteGetDto>>("Success", dtos, HttpStatusCode.OK);
    }
}

