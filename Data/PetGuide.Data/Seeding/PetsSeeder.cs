namespace PetGuide.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using PetGuide.Data.Models;

    public class PetsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Pets.Any())
            {
                return;
            }

            var location = new Location()
            {
                District = District.Mladost,
                Street = "Best Street 2",
                AdditionalInfo = "Next to the bus stop",
            };

            await dbContext.Pets.AddAsync(new Pet { Name = "Barky", Location = location, Age = 2, Description = "Best dog in the world!" });

            await dbContext.SaveChangesAsync();
        }
    }
}
