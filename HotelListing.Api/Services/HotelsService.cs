using HotelListing.Api.Contracts;
using HotelListing.Api.Data;
using HotelListing.Api.DTOs.Hotel;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace HotelListing.Api.Services
{
    public class HotelsService(HotelListingDbContext context) : IHotelsService
    {
        public async Task<List<GetHotelsDto>> GetHotelsAsync()
        {
            return await context.Hotels
            .Include(h => h.Country)
            .Select(h => new GetHotelsDto(
                h.Id,
                h.Name,
                h.Address,
                h.Rating,
                h.Country.Id
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
                    h.Country!.Name
                )).FirstOrDefaultAsync();
            return hotel ?? null;
        }

        public async Task<bool> UpdateHotelAsync(int id, UpdateHotelDto hotelDto)
        {
            var hotel = await context.Hotels.FindAsync(id) ?? throw new KeyNotFoundException("Hotel not found");

            hotel.Name = hotelDto.Name;
            hotel.Address = hotelDto.Address;
            hotel.Rating = hotelDto.Rating;
            hotel.CountryId = hotelDto.CountryId;
            context.Hotels.Update(hotel);

            await context.SaveChangesAsync();

            return true;
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
                (await context.Countries.FindAsync(hotel.CountryId))!.Name
            );
        }

        public async Task DeleteHotelAsync(int id)
        {
            var hotel = await context.Hotels.FindAsync(id) ?? throw new KeyNotFoundException("Hotel not found");
            context.Hotels.Remove(hotel);
            await context.SaveChangesAsync();
        }


        public async Task<bool> HotelExistsAsync(int id)
        {
            return await context.Hotels.AnyAsync(e => e.Id == id);
        }
    }
}
