using Laboratorio8.Moduli;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace Laboratorio8.Web.Areas.Configurazioni.MetaModuli
{
    public partial class MetaModuliController
    {
        [HttpGet]
        public virtual async Task<IActionResult> CercaAggiornamentoEtichettaMetaGruppo(Guid idMetaModulo, int idMetaGruppo)
        {
            return Ok(await _moduliService.Query(new AggiornamentoEtichettaMetaGruppoQuery
            {
                IdMetaModulo = idMetaModulo,
                IdMetaGruppo = idMetaGruppo
            }));
        }

        [HttpPost]
        public virtual async Task<IActionResult> ModificaEtichettaMetaGruppo([FromBody] ModificaEtichettaMetaGruppoViewModel model)
        {
            try
            {
                await _moduliService.Handle(new ComandoSincronizzato
                {
                    IdMetaModulo = model.IdMetaModulo,
                    Comando = () => _moduliService.Handle(new ModificaEtichettaMetaGruppoCommand
                    {
                        IdMetaModulo = model.IdMetaModulo,
                        IdMetaGruppo = model.IdMetaGruppo,
                        Etichetta = model.Etichetta
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
