using Laboratorio8.Moduli;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace Laboratorio8.Web.Areas.Configurazioni.MetaModuli
{
    public partial class MetaModuliController
    {
        [HttpGet]
        public virtual async Task<IActionResult> CercaAggiornamentoObbligatorietaMetaCampo(Guid idMetaModulo, int idMetaGruppo, int idMetaCampo)
        {
            return Ok(await _moduliService.Query(new AggiornamentoObbligatorietaMetaCampoQuery
            {
                IdMetaModulo = idMetaModulo,
                IdMetaGruppo = idMetaGruppo,
                IdMetaCampo = idMetaCampo
            }));
        }

        [HttpPost]
        public virtual async Task<IActionResult> ModificaObbligatorietaMetaCampo([FromBody] ModificaObbligatorietaMetaCampoViewModel model)
        {
            try
            {
                await _moduliService.Handle(new ComandoSincronizzato
                {
                    IdMetaModulo = model.IdMetaModulo,
                    Comando = () => _moduliService.Handle(new ModificaObbligatorietaMetaCampoCommand
                    {
                        IdMetaModulo = model.IdMetaModulo,
                        IdMetaGruppo = model.IdMetaGruppo,
                        IdMetaCampo = model.IdMetaCampo,
                        Obbligatorio = model.Obbligatorio
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
