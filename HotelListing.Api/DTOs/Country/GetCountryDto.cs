using HotelListing.Api.DTOs.Hotel;

namespace HotelListing.Api.DTOs.Country;

// 3. DTO para Lectura de Detalle (Record: Inmutable y rápido)
public record GetCountryDto(
    int Id,
    string Name,
    string ShortName,
    List<GetHotelSlimDto>? Hotels
);
