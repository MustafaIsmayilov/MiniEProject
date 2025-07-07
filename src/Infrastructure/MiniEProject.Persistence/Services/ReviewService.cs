using AutoMapper;
using System.Net;
using System;
using MiniEProject.Application.Abstracts.Services;
using MiniEProject.Application.DTOs.ReviewDtos;
using MiniEProject.Application.Shared.Responses;
using MiniEProject.Domain.Entities;
using MiniEProject.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
namespace MiniEProject.Persistence.Services;

public class ReviewService : IReviewService
{
    private readonly MiniEProjectDbContext _context;
    private readonly IMapper _mapper;

    public ReviewService(MiniEProjectDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BaseResponse<ReviewGetDto>> CreateAsync(ReviewCreateDto dto)
    {
        var review = _mapper.Map<Review>(dto);
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();

        var resultDto = _mapper.Map<ReviewGetDto>(review);
        return new BaseResponse<ReviewGetDto>("Review created", resultDto, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<ReviewGetDto>> UpdateAsync(Guid id, ReviewUpdateDto dto)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
            return new BaseResponse<ReviewGetDto>("Review not found", null, HttpStatusCode.NotFound);

        _mapper.Map(dto, review);
        await _context.SaveChangesAsync();

        var resultDto = _mapper.Map<ReviewGetDto>(review);
        return new BaseResponse<ReviewGetDto>("Review updated", resultDto, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<string>> DeleteAsync(Guid id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
            return new BaseResponse<string>("Review not found", null, HttpStatusCode.NotFound);

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();

        return new BaseResponse<string>("Review deleted", null, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<ReviewGetDto>> GetByIdAsync(Guid id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
            return new BaseResponse<ReviewGetDto>("Review not found", null, HttpStatusCode.NotFound);

        var dto = _mapper.Map<ReviewGetDto>(review);
        return new BaseResponse<ReviewGetDto>("Success", dto, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<ReviewGetDto>>> GetAllAsync()
    {
        var reviews = await _context.Reviews.ToListAsync();
        var dtos = _mapper.Map<List<ReviewGetDto>>(reviews);
        return new BaseResponse<List<ReviewGetDto>>("Success", dtos, HttpStatusCode.OK);
    }
}

