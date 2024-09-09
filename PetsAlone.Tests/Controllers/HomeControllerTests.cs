using Microsoft.AspNetCore.Mvc;
using Moq;
using PetsAlone.Controllers;
using PetsAlone.Models;
using PetsAlone.ServiceContracts;

namespace PetsAlone.Tests.Controllers;

[TestFixture]
public class HomeControllerTests
{
    private Mock<IPetService> _petServiceMock;
    private HomeController _homeController;

    [SetUp]
    public void SetUp()
    {
        _petServiceMock = new Mock<IPetService>();
        _homeController = new HomeController(_petServiceMock.Object);
    }

    [Test]
    public async Task Index_ReturnsViewWithPets()
    {
        _petServiceMock.Setup(ps => ps.GetPetsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new List<Pet> { new Pet { Id = 1, Name = "Missing Pet" } });
        _petServiceMock.Setup(ps => ps.GetTotalPetsCountAsync(It.IsAny<string>()))
            .ReturnsAsync(1);

        var result = await _homeController.Index(null, null, 1);

        var viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        Assert.That(viewResult.Model, Is.TypeOf<List<Pet>>());
        Assert.That(((List<Pet>)viewResult.Model).Count, Is.EqualTo(1));
    }
}
