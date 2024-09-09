using Microsoft.AspNetCore.Mvc;
using Moq;
using PetsAlone.Controllers;
using PetsAlone.Models;
using PetsAlone.ServiceContracts;

namespace PetsAlone.Tests.Controllers;

[TestFixture]
public class PetControllerTests
{
    private Mock<IPetService> _petServiceMock;
    private PetController _petController;

    [SetUp]
    public void SetUp()
    {
        _petServiceMock = new Mock<IPetService>();
        _petController = new PetController(_petServiceMock.Object);
    }

    [Test]
    public void Create_Get_ReturnsView()
    {
        var result = _petController.Create();

        Assert.That(result, Is.TypeOf<ViewResult>());
    }

    [Test]
    public async Task Create_Post_ValidModel_RedirectsToHome()
    {
        var pet = new Pet { Name = "My Pet" };

        var result = await _petController.Create(pet);

        _petServiceMock.Verify(ps => ps.AddPetAsync(pet), Times.Once);
        Assert.That(result, Is.TypeOf<RedirectToActionResult>());
        var redirectResult = (RedirectToActionResult)result;
        Assert.That(redirectResult.ActionName, Is.EqualTo("Index"));
        Assert.That(redirectResult.ControllerName, Is.EqualTo("Home"));
    }

    [Test]
    public async Task Create_Post_InvalidModel_ReturnsViewWithModel()
    {
        var pet = new Pet { Name = "" };
        _petController.ModelState.AddModelError("Name", "Required");

        var result = await _petController.Create(pet);

        Assert.That(result, Is.TypeOf<ViewResult>());
        var viewResult = (ViewResult)result;
        Assert.That(viewResult.Model, Is.EqualTo(pet));
    }
}
