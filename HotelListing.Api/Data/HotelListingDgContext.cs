using Microsoft.EntityFrameworkCore;
namespace HotelListing.Api.Data
{
    public class HotelListingDgContext : DbContext
    {
        public HotelListingDgContext(DbContextOptions<HotelListingDgContext> options) : base(options)
        {
            
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

    }
}
