using Bogus;
using PetsAlone.Models;
using PetsAlone.Models.Enums;
using PetsAlone.ServiceContracts;

namespace PetsAlone.Services;

public class DataLoader : IDataLoader
{
    private static readonly List<Pet> _pets;

    // TO DO: Repalce the fake data loader with DbContext
    static DataLoader()
    {
        _pets = GeneratePets();
    }

    public Task<List<Pet>> LoadPetsAsync()
    {
        return Task.FromResult(_pets);
    }

    private static List<Pet> GeneratePets()
    {
        var faker = new Faker<Pet>()
            .RuleFor(p => p.Id, f => f.IndexFaker + 1)
            .RuleFor(p => p.Name, f => f.Name.FirstName())
            .RuleFor(p => p.PetType, f => f.PickRandom<PetType>())
            .RuleFor(p => p.Description, f => f.Lorem.Sentence())
            .RuleFor(p => p.IsMissing, f => true)
            .RuleFor(p => p.DateMissing, f => f.Date.Past(2));

        return faker.Generate(100);
    }
}
