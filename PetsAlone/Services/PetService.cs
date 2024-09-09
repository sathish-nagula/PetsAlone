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

    public async Task<List<Pet>> GetPetsAsync(string? typeFilter, string? sortOrder, int pageNumber, int pageSize)
    {
        if (_pets == null) await LoadPetsAsync();
        var petsQuery = _pets.AsQueryable();

        if (!string.IsNullOrEmpty(typeFilter) && Enum.TryParse(typeFilter, out PetType petType))
        {
            petsQuery = petsQuery.Where(p => p.PetType == petType);
        }

        switch (sortOrder)
        {
            case "name_asc":
                petsQuery = petsQuery.OrderBy(p => p.Name);
                break;
            case "name_desc":
                petsQuery = petsQuery.OrderByDescending(p => p.Name);
                break;
            default:
                petsQuery = petsQuery.OrderByDescending(p => p.DateMissing);
                break;
        }

        return await Task.FromResult(petsQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList());
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

    public async Task AddPetAsync(Pet pet)
    {
        if (_pets == null || !_pets.Any()) await LoadPetsAsync();

        pet.Id = _pets.Any() ? _pets.Max(p => p.Id) + 1 : 1;
        pet.DateMissing = DateTime.Now;
        _pets.Add(pet);

        await Task.CompletedTask;
    }
}
