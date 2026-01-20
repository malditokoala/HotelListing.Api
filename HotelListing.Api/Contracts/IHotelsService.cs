using HotelListing.Api.DTOs.Hotel;

namespace HotelListing.Api.Contracts;

public interface IHotelsService
{
    Task<GetHotelDto> CreateHotelAsync(CreateHotelDto hotelDto);
    Task DeleteHotelAsync(int id);
    Task<GetHotelDto?> GetHotelAsync(int id);
    Task<List<GetHotelDto>> GetHotelsAsync();
    Task<bool> HotelExistsAsync(int id);
    Task<bool> HotelExistsAsync(string name);
    Task UpdateHotelAsync(int id, UpdateHotelDto hotelDto);
}
