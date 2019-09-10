using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebixLauncher.Models;

namespace WebixLauncher.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        [Route("login")]
        public IActionResult Login(LoginViewModel model)
        {
            //ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = ValidateLogin(model.UserName, model.Password);
                if (result)
                {
                    return RedirectToLocal("/index.html");
                }                
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View("index",model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private bool ValidateLogin(string userName, string password)
        {
            if (userName.Equals("test", StringComparison.InvariantCultureIgnoreCase) && password.Equals("account", StringComparison.InvariantCultureIgnoreCase))
                return true;
            return false;
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
