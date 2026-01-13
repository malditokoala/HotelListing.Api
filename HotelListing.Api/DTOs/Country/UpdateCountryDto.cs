using System.ComponentModel.DataAnnotations;

namespace HotelListing.Api.DTOs.Country;

// 2. DTO para Actualización (Casi idéntico al Create, pero separado por seguridad futura)
public class UpdateCountryDto : CreateCountryDto
{
    [Required]
    public required int Id { get; set; }
}
