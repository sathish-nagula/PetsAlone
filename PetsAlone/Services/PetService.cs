using PetsAlone.Models;
using PetsAlone.Models.Enums;
using PetsAlone.ServiceContracts;

namespace PetsAlone.Services;

public class PetService : IPetService
{
    private readonly IDataLoader _dataLoader;
    private static List<Pet> _pets;

    public PetService(IDataLoader dataLoader)
    {
        _dataLoader = dataLoader;
        LoadPetsAsync();
    }

    private async Task LoadPetsAsync()
    {
        _pets = await _dataLoader.LoadPetsAsync();
    }


    public async Task<int> GetTotalPetsCountAsync(string? typeFilter)
    {
        if (_pets == null || !_pets.Any()) await LoadPetsAsync();

        var petsQuery = _pets.AsQueryable();

        if (!string.IsNullOrEmpty(typeFilter) && Enum.TryParse(typeFilter, out PetType petType))
        {
            petsQuery = petsQuery.Where(p => p.PetType == petType);
        }

        return await Task.FromResult(petsQuery.Count());
    }

    public Task<List<Pet>> GetPetsAsync(string typeFilter, string sortOrder, int page = 1, int pageSize = 10)
    {
        throw new NotImplementedException();
    }

    public Task AddPetAsync(Pet pet)
    {
        throw new NotImplementedException();
    }
}
