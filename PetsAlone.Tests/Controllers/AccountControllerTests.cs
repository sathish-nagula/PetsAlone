using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PetsAlone.Controllers;

namespace PetsAlone.Tests.Controllers;

[TestFixture]
public class AccountControllerTests
{
    private Mock<IAuthenticationService> _authServiceMock;
    private AccountController _accountController;

    [SetUp]
    public void SetUp()
    {
        _authServiceMock = new Mock<IAuthenticationService>();
        _accountController = new AccountController();
    }

    [Test]
    public void Login_ReturnsView()
    {
        var result = _accountController.Login();

        Assert.That(result, Is.TypeOf<ViewResult>());
    }
}
