using Moq;
using PetsAlone.Models;
using PetsAlone.Models.Enums;
using PetsAlone.ServiceContracts;
using PetsAlone.Services;

namespace PetsAlone.Tests.Services;

[TestFixture]
public class PetServiceTests
{
    private Mock<IDataLoader> _dataLoaderMock;
    private PetService _petService;

    [SetUp]
    public void SetUp()
    {
        _dataLoaderMock = new Mock<IDataLoader>();
        _petService = new PetService(_dataLoaderMock.Object);
    }

    [Test]
    public async Task GetPetsAsync_ReturnsFilteredAndSortedPets()
    {
        var pets = new List<Pet>
        {
            new Pet { Id = 1, Name = "Missing Cat 1", PetType = PetType.Cat, DateMissing = new DateTime(2024, 09, 09) },
            new Pet { Id = 2, Name = "Missing Dog 2", PetType = PetType.Dog, DateMissing = new DateTime(2024, 08, 31) }
        };

        _dataLoaderMock.Setup(dl => dl.LoadPetsAsync()).ReturnsAsync(pets);

        _petService = new PetService(_dataLoaderMock.Object);

        var result = await _petService.GetPetsAsync("Dog", "name_asc", 1, 10);

        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result.First().Name, Is.EqualTo("Missing Dog 2"));
    }


    [Test]
    public async Task GetTotalPetsCountAsync_ReturnsCorrectCount()
    {
        var pets = new List<Pet>
        {
            new Pet { Id = 1, Name = "Cat 1", PetType = PetType.Cat },
            new Pet { Id = 2, Name = "Dog 1", PetType = PetType.Dog }
        };
        _dataLoaderMock.Setup(dl => dl.LoadPetsAsync()).ReturnsAsync(pets);
        _petService = new PetService(_dataLoaderMock.Object);

        var count = await _petService.GetTotalPetsCountAsync("Dog");

        Assert.That(count, Is.EqualTo(1));
    }

    [Test]
    public async Task AddPetAsync_AddsPetToList()
    {
        var newPet = new Pet { Name = "Missing Cat 1", PetType = PetType.Cat };
        var existingPets = new List<Pet>();
        _dataLoaderMock.Setup(dl => dl.LoadPetsAsync()).ReturnsAsync(existingPets);
        _petService = new PetService(_dataLoaderMock.Object);

        await _petService.AddPetAsync(newPet);
        var pets = await _petService.GetPetsAsync(null, null, 1, 10);

        Assert.That(pets.Count, Is.EqualTo(1));
        Assert.That(pets.First().Name, Is.EqualTo("Missing Cat 1"));
    }

    [TearDown]
    public void TearDown()
    {
        _dataLoaderMock.Reset();
    }

}
