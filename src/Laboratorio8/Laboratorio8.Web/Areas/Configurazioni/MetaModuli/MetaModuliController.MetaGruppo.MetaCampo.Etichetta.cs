using Laboratorio8.Moduli;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace Laboratorio8.Web.Areas.Configurazioni.MetaModuli
{
    public partial class MetaModuliController
    {
        [HttpGet]
        public virtual async Task<IActionResult> CercaAggiornamentoEtichettaMetaCampo(Guid idMetaModulo, int idMetaGruppo, int idMetaCampo)
        {
            return Ok(await _moduliService.Query(new AggiornamentoEtichettaMetaCampoQuery
            {
                IdMetaModulo = idMetaModulo,
                IdMetaGruppo = idMetaGruppo,
                IdMetaCampo = idMetaCampo
            }));
        }

        [HttpPost]
        public virtual async Task<IActionResult> ModificaEtichettaMetaCampo([FromBody] ModificaEtichettaMetaCampoViewModel model)
        {
            try
            {
                await _moduliService.Handle(new ComandoSincronizzato
                {
                    IdMetaModulo = model.IdMetaModulo,
                    Comando = () => _moduliService.Handle(new ModificaEtichettaMetaCampoCommand
                    {
                        IdMetaModulo = model.IdMetaModulo,
                        IdMetaGruppo = model.IdMetaGruppo,
                        IdMetaCampo = model.IdMetaCampo,
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
