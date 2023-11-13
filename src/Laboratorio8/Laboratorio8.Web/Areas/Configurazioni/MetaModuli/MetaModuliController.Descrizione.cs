using Laboratorio8.Moduli;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace Laboratorio8.Web.Areas.Configurazioni.MetaModuli
{
    public partial class MetaModuliController
    {
        [HttpGet]
        public virtual async Task<IActionResult> CercaAggiornamentoDescrizioneMetaModulo(Guid idMetaModulo)
        {
            return Ok(await _moduliService.Query(new AggiornamentoDescrizioneMetaModuloQuery
            {
                IdMetaModulo = idMetaModulo
            }));
        }

        [HttpPost]
        public virtual async Task<IActionResult> ModificaDescrizioneMetaModulo([FromBody] ModificaDescrizioneMetaModuloViewModel model)
        {
            try
            {
                await _moduliService.Handle(new ComandoSincronizzato
                {
                    IdMetaModulo = model.IdMetaModulo,
                    Comando = () => _moduliService.Handle(new ModificaDescrizioneMetaModuloCommand
                    {
                        IdMetaModulo = model.IdMetaModulo,
                        Descrizione = model.Descrizione
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
