using System.ComponentModel.DataAnnotations;

namespace HotelListing.Api.DTOs.Country;

// 1. DTO para Creación (Clase estándar con validaciones)
public class CreateCountryDto
{
    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(3)]
    public required string ShortName { get; set; }
}
