using Microsoft.AspNetCore.Mvc;
using PetsAlone.ServiceContracts;

namespace PetsAlone.Controllers;

public class HomeController : Controller
{
    private readonly IPetService _petService;

    public HomeController(IPetService petService)
    {
        _petService = petService;
    }

    public async Task<IActionResult> Index(string typeFilter, string sortOrder, int pageNumber = 1)
    {
        var pageSize = 10;
        var pets = await _petService.GetPetsAsync(typeFilter, sortOrder, pageNumber, pageSize);
        var totalPetsCount = await _petService.GetTotalPetsCountAsync(typeFilter);

        ViewData["TotalPages"] = (int)Math.Ceiling(totalPetsCount / (double)pageSize);
        ViewData["CurrentFilter"] = typeFilter;
        ViewData["CurrentSort"] = sortOrder;

        return View(pets);
    }
}
