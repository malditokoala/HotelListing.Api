namespace HotelListing.Api.DTOs.Country;

// 4. DTO para Listados (Record: Ligero para listas grandes)
public record GetCountriesDto(
    int Id,
    string Name,
    string ShortName
);