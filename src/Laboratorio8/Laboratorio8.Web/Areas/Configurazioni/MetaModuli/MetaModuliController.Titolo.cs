using Laboratorio8.Moduli;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace Laboratorio8.Web.Areas.Configurazioni.MetaModuli
{
    public partial class MetaModuliController
    {
        [HttpGet]
        public virtual async Task<IActionResult> CercaAggiornamentoTitoloMetaModulo(Guid idMetaModulo)
        {
            return Ok(await _moduliService.Query(new AggiornamentoTitoloMetaModuloQuery
            {
                IdMetaModulo = idMetaModulo
            }));
        }

        [HttpPost]
        public virtual async Task<IActionResult> ModificaTitoloMetaModulo([FromBody] ModificaTitoloMetaModuloViewModel model)
        {
            try
            {
                await _moduliService.Handle(new ComandoSincronizzato
                {
                    IdMetaModulo = model.IdMetaModulo,
                    Comando = () => _moduliService.Handle(new ModificaTitoloMetaModuloCommand
                    {
                        IdMetaModulo = model.IdMetaModulo,
                        Titolo = model.Titolo
                    })
                });

                return Ok();
            }
            catch (ServicesExceptions ex)
            {
                return Problem(ex.ToString(), null, 400, ex.Message, null);
            }
        }
    }
}
