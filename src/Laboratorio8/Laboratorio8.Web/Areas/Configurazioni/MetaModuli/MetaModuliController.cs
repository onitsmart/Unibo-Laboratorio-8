using Laboratorio8.Infrastructure;
using Laboratorio8.Moduli;
using Laboratorio8.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Laboratorio8.Web.Areas.Configurazioni.MetaModuli
{
    [Area("Configurazioni")]
    public partial class MetaModuliController : AuthenticatedBaseController
    {
        private readonly ModuliService _moduliService;
        private readonly IPublishDomainEvents _publisher;

        public MetaModuliController(ModuliService moduliService, IPublishDomainEvents publisher)
        {
            _moduliService = moduliService;
            _publisher = publisher;
        }

        public virtual async Task<IActionResult> Index()
        {
            var model = new IndexViewModel();

            var qry = model.ToMetaModuliElencoQuery();
            var moduli = await _moduliService.Query(qry);
            model.SetModuli(moduli);
            model.TotalItems = await _moduliService.Count(qry);

            return View(model);
        }

        [HttpGet]
        public virtual async Task<IActionResult> New()
        {
            try
            {
                var idModulo = await _moduliService.Handle(new AggiungiNuovoMetaModuloCommand());

                return RedirectToAction(Actions.Edit(idModulo));
            }
            catch (ServicesExceptions)
            {
                Alerts.AddError(this, "Errore nella creazione del nuovo modulo");
            }

            return RedirectToAction(Actions.Index());
        }

        [HttpGet]
        public virtual async Task<IActionResult> Edit(Guid? id)
        {
            var model = new EditViewModel()
            {
                EmailUtenteCorrente = Identita.EmailUtenteCorrente
            };

            var modulo = await _moduliService.Query(new MetaModuloInEditQuery { Id = id.Value });
            if (modulo == null) return new ViewResult
            {
                StatusCode = 403,
                ViewName = "ContentForbidden",
                ContentType = new MediaTypeHeaderValue("text/html").ToString()
            }; ;
            model.SetModulo(modulo);
            model.SetUrls(Url);

            return View(model);
        }

        [HttpGet]
        public virtual IActionResult GeneraGravatarUrl(string email)
        {
            return Ok(email.ToGravatarUrl(ToGravatarUrlExtension.DefaultGravatar.Identicon, null));
        }

        [HttpPost]
        public async virtual Task<IActionResult> AggiungiUtenteInCollaborazione([FromBody] AggiungiUtenteInCollaborazioneViewModel model)
        {
            await _publisher.Publish(new AggiuntoUtenteInCollaborazione
            {
                Email = model.Email,
                IdMetaModulo = model.IdMetaModulo,
                IdMetaGruppo = model.IdMetaGruppo,
                IdMetaCampo = model.IdMetaCampo
            });

            return Ok();
        }

        [HttpPost]
        public async virtual Task<IActionResult> RimuoviUtenteInCollaborazione([FromBody] RimuoviUtenteInCollaborazioneViewModel model)
        {
            await _publisher.Publish(new RimossoUtenteInCollaborazione
            {
                Email = model.Email
            });

            return Ok();
        }

        [HttpPost]
        public virtual async Task<IActionResult> Elimina([FromForm]Guid idMetaModulo)
        {
            await _moduliService.Handle(new EliminaMetaModuloCommand
            {
                IdMetaModulo = idMetaModulo
            });

            return RedirectToAction(Actions.Index());
        }


    }
}
