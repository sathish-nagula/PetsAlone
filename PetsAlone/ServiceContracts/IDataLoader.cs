using PetsAlone.Models;

namespace PetsAlone.ServiceContracts;

public interface IDataLoader
{
    Task<List<Pet>> LoadPetsAsync();
}
