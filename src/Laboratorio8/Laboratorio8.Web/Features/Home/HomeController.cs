using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio8.Web.Features.Home
{
    public partial class HomeController : Controller
    {
        private readonly AppSettings _appSettings;

        public HomeController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public virtual IActionResult Index()
        {
            return RedirectToAction(MVC.Configurazioni.MetaModuli.Index());
        }

        [HttpPost]
        public virtual IActionResult ChangeLanguageTo(string cultureName)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cultureName)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return Redirect(Request.GetTypedHeaders().Referer.ToString());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public virtual async Task<IActionResult> Error()
        {
            var utenteLoggato = false;

            // No login so the email is empty
            return View(new ErrorViewModel
            {
                InviaSegnalazioneUrl = "",
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, //Equivalente a  ${aspnet-TraceIdentifier} in Nlog
                EmailTo = new[] { "assistenzaApplicativo@utilities.it" },
                EmailCC = Array.Empty<string>(),
                EmailCCN = Array.Empty<string>(),
                EmailFrom = utenteLoggato ? "utenteCorrente@dominio.it" : String.Empty,
                EmailOptions = _appSettings.ErrorContactEmails.Select(x => new SelectListItem
                {
                    Text = x,
                    Value = x
                }),
                Subject = "Errore generico del server",
                StandardContent = $"In riferimento alla richiesta con id {Activity.Current?.Id ?? HttpContext.TraceIdentifier}, volevo aggiungere le seguenti informazioni:"
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public virtual IActionResult PageNotFound()
        {
            string originalPath = "unknown";
            if (HttpContext.Items.ContainsKey("originalPath"))
            {
                originalPath = HttpContext.Items["originalPath"] as string;
            }
            //_telemetryClient.TrackEvent("Error.PageNotFound", new Dictionary<string, string>
            //{
            //    ["originalPath"] = originalPath
            //});
            return View();
        }
    }
}
