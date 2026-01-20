using HotelListing.Api.Contracts;
using HotelListing.Api.Data;
using HotelListing.Api.DTOs.Hotel;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace HotelListing.Api.Services
{
    public class HotelsService(HotelListingDbContext context) : IHotelsService
    {
        public async Task<List<GetHotelDto>> GetHotelsAsync()
        {
            return await context.Hotels
            .Include(h => h.Country)
            .Select(h => new GetHotelDto(
                h.Id,
                h.Name,
                h.Address,
                h.Rating,
                h.CountryId,
                h.Country!.Name
            ))
            .ToListAsync();
        }

        public async Task<GetHotelDto?> GetHotelAsync(int id)
        {
            var hotel = await context.Hotels
                .Where(h => h.Id == id)
                .Select(h => new GetHotelDto(
                    h.Id,
                    h.Name,
                    h.Address,
                    h.Rating,
                    h.CountryId,
                    h.Country!.Name
                )).FirstOrDefaultAsync();
            return hotel ?? null;
        }

        public async Task UpdateHotelAsync(int id, UpdateHotelDto hotelDto)
        {
            var hotel = await context.Hotels.FindAsync(id) ?? throw new KeyNotFoundException("Hotel not found");

            hotel.Name = hotelDto.Name;
            hotel.Address = hotelDto.Address;
            hotel.Rating = hotelDto.Rating;
            hotel.CountryId = hotelDto.CountryId;

            context.Entry(hotel).State = EntityState.Modified;
            context.Hotels.Update(hotel);
            await context.SaveChangesAsync();
        }

        public async Task<GetHotelDto> CreateHotelAsync(CreateHotelDto hotelDto)
        {
            var hotel = new Hotel
            {
                Name = hotelDto.Name,
                Address = hotelDto.Address,
                Rating = hotelDto.Rating,
                CountryId = hotelDto.CountryId
            };
            context.Hotels.Add(hotel);
            await context.SaveChangesAsync();
            return new GetHotelDto(
                hotel.Id,
                hotel.Name,
                hotel.Address,
                hotel.Rating,
                hotel.CountryId,
                (await context.Countries.FindAsync(hotel.CountryId))!.Name  // o string.Empty
            );
        }

        public async Task DeleteHotelAsync(int id)
        {
            var hotel = await context.Hotels.Where(q => q.Id == id).ExecuteDeleteAsync();
                
        }


        public async Task<bool> HotelExistsAsync(int id)
        {
            return await context.Hotels.AnyAsync(e => e.Id == id);
        }
        public async Task<bool> HotelExistsAsync(string name)
        {
            return await context.Hotels.AnyAsync(e => e.Name == name);
        }
    }
}
