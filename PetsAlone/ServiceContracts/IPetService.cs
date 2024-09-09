using PetsAlone.Models.Enums;
using PetsAlone.Models;

namespace PetsAlone.ServiceContracts;

public interface IPetService
{
    Task<List<Pet>> GetPetsAsync(string typeFilter, string sortOrder, int page = 1, int pageSize = 10);
    Task AddPetAsync(Pet pet);
}
