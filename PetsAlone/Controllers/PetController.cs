using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsAlone.Models;
using PetsAlone.ServiceContracts;

namespace PetsAlone.Controllers;

[Authorize]
public class PetController : Controller
{
    private readonly IPetService _petService;

    public PetController(IPetService petService)
    {
        _petService = petService;
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Pet pet)
    {
        if (ModelState.IsValid)
        {
            await _petService.AddPetAsync(pet);
            return RedirectToAction("Index", "Home");
        }

        return View(pet);
    }
}
