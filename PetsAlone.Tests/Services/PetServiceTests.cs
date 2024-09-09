using Moq;
using PetsAlone.Models;
using PetsAlone.Models.Enums;
using PetsAlone.ServiceContracts;
using PetsAlone.Services;
using System.Data;

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

}
