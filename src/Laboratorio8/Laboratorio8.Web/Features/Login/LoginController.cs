using Laboratorio8.Web.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Laboratorio8.Web.Features.Login
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    [Alerts]
    [ModelStateToTempData]
    public partial class LoginController : Controller
    {
        IStringLocalizer<SharedResource> _sharedLocalizer;

        public LoginController(IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _sharedLocalizer = sharedLocalizer;
        }

        public virtual ActionResult AutorizzaERedirectDopoLogin(string email, string returnUrl, bool rememberMe)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, email),
                    new Claim(ClaimTypes.Email, email)
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                ExpiresUtc = (rememberMe) ? DateTimeOffset.UtcNow.AddMonths(3) : (DateTimeOffset?)null,
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                IsPersistent = rememberMe,
                // Whether the authentication session is persisted across 
                // multiple requests. Required when setting the 
                // ExpireTimeSpan option of CookieAuthenticationOptions 
                // set with AddCookie. Also required when setting 
                // ExpiresUtc.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            });

            return RedirectDopoLogin(returnUrl);
        }

        public virtual ActionResult RedirectDopoLogin(string returnUrl)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl) && !string.Equals(returnUrl, "/"))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(MVC.Configurazioni.MetaModuli.Index());
        }

        [HttpGet]
        public virtual IActionResult Index(string returnUrl)
        {
            var model = new LoginViewModel() { ReturnUrl = returnUrl };

            if (HttpContext.User != null && HttpContext.User.Identity.IsAuthenticated)
                return RedirectDopoLogin(returnUrl);

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Index(LoginViewModel model)
        {
            model.ValidaEmail(ModelState);

            if (ModelState.IsValid)
            {
                return AutorizzaERedirectDopoLogin(model.Email, model.ReturnUrl, model.RememberMe);
            }

            if (ModelState.IsValid == false)
                Alerts.AddError(this, _sharedLocalizer["Errore di login"]);

            return RedirectToAction(MVC.Login.Index());
        }

        [HttpPost]
        public virtual IActionResult Logout()
        {
            HttpContext.SignOutAsync();

            Alerts.AddSuccess(this, "Utente scollegato correttamente");
            return RedirectToAction(MVC.Login.Index());
        }
    }
}