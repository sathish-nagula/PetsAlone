using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PetsAlone.Models;
using PetsAlone.Utils;
using System.Security.Claims;

namespace PetsAlone.Controllers;

public class AccountController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    //[HttpPost]
    //public IActionResult Login(string username, string password)
    //{
    //    if (username == Secrets.userName && password == Secrets.password)
    //    {
    //        var claims = new List<Claim>
    //    {
    //        new Claim(ClaimTypes.Name, username)
    //    };

    //        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

    //        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

    //        return RedirectToAction("Index", "Home");
    //    }

    //    ModelState.AddModelError("", "Invalid username or password");
    //    return View();
    //}

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Replace this with actual authentication logic
            bool loginSuccess = model.Username == Secrets.userName && model.Password == Secrets.password;

            if (loginSuccess)
            {
                // Redirect to the Home page or another area if login succeeds
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Login failed, show error message
                ViewBag.ErrorMessage = "Invalid username or password.";
            }
        }

        // Return the view with model state errors or a general error message
        return View(model);
    }
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}