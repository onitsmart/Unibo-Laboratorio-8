using Laboratorio8.Moduli;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Laboratorio8.Web.Infrastructure;
using PuppeteerSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http.Headers;

namespace Laboratorio8.Web.Areas.Compilazioni.Moduli
{
    [Area("Compilazioni")]
    public partial class ModuliController : Controller
    {
        AppSettings _appSettings;
        private readonly ModuliService _moduliService;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public ModuliController(ModuliService moduliService, IStringLocalizer<SharedResource> sharedLocalizer, IOptions<AppSettings> appSettings)
        {
            _moduliService = moduliService;
            _sharedLocalizer = sharedLocalizer;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Index(Guid idMetaModulo)
        {
            var model = new IndexViewModel();

            model.SetMetaModulo(await _moduliService.Query(new MetaModuloPerElencoModuliQuery
            {
                Id = idMetaModulo
            }));

            var qry = new ModuliQuery
            {
                IdMetaModulo = idMetaModulo
            };

            var moduliCompilati = await _moduliService.Query(qry);

            model.SetModuli(moduliCompilati);
            model.TotalItems = await _moduliService.Count(qry);

            return View(model);
        }

        [HttpGet]
        public virtual async Task<IActionResult> Report(Guid idModulo, bool printPreview)
        {
            var model = new ReportViewModel()
            {
                PrintPreview = printPreview,
            };

            model.SetModel(await _moduliService.Query(new DettaglioModuloCompilatoQuery
            {
                Id = idModulo,
            }));

            return View(model);
        }

        [HttpGet]
        public virtual async Task<IActionResult> Download(Guid idModulo)
        {
            //var model = new ReportViewModel();

            //model.SetModel(await _moduliService.Query(new DettaglioModuloCompilatoQuery
            //{
            //    Id = idModulo
            //}));

            var fileName = "Riepilogo compilazione.pdf";
            var fileTempName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true, ExecutablePath = _appSettings.ChromeExecutablePath }))
            {
                using (var page = await browser.NewPageAsync())
                {
                    var urlAbsolute = Url.ActionAbsolute(MVC.Compilazioni.Moduli.Report(idModulo, false));
                    await page.SetCookieAsync(Request.Cookies.Select(x => new CookieParam
                    {
                        Name = x.Key,
                        Value = x.Value,
                        Url = urlAbsolute,
                    }).ToArray());

                    await page.GoToAsync(urlAbsolute);
                    await page.PdfAsync(fileTempName, new PdfOptions
                    {
                        PreferCSSPageSize = true,
                    });
                }
                await browser.CloseAsync();
            }

            var fileStream = new FileStream(fileTempName, FileMode.Open, FileAccess.Read, FileShare.None, 4096, FileOptions.DeleteOnClose);
            return File(fileStream, MimeKit.MimeTypes.GetMimeType(fileName), fileName);
        }


        [HttpGet]
        public virtual async Task<IActionResult> Edit(Guid idMetaModulo)
        {
            var model = new EditViewModel();

            var modulo = await _moduliService.Query(new MetaModuloPerCompilazioneQuery { Id = idMetaModulo });
            if (modulo == null) return new ViewResult
            {
                StatusCode = 403,
                ViewName = "ContentForbidden",
                ContentType = new MediaTypeHeaderValue("text/html").ToString()
            };

            model.SetModel(modulo);

            return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Edit(EditViewModel model)
        {
            model.ValidaModello(ModelState);

            if (ModelState.IsValid)
            {
                try
                {
                    var idModulo = await _moduliService.Handle(model.ToCompilaModuloCommand());

                    if (idModulo != null)
                    {
                        Alerts.AddSuccess(this, _sharedLocalizer["Informazioni aggiornate"]);
                        return RedirectToAction(Actions.CompilazioneRiuscita(idModulo.Value));
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("ModelError", e.Message);
                }
            }

            if (ModelState.IsValid == false)
            {
                Alerts.AddError(this, _sharedLocalizer["Errore in aggiornamento"]);
            }

            return RedirectToAction(Actions.Edit(model.IdMetaModulo));
        }

        [HttpGet]
        public virtual IActionResult CompilazioneRiuscita(Guid idModulo)
        {
            return View(idModulo);
        }
    }
}
