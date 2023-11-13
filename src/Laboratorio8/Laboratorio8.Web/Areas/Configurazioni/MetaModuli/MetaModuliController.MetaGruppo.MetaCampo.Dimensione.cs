using Laboratorio8.Moduli;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace Laboratorio8.Web.Areas.Configurazioni.MetaModuli
{
    public partial class MetaModuliController
    {
        [HttpGet]
        public virtual async Task<IActionResult> CercaAggiornamentoDimensioneMetaCampo(Guid idMetaModulo, int idMetaGruppo, int idMetaCampo)
        {
            var dimensione = await _moduliService.Query(new AggiornamentoDimensioneMetaCampoQuery
            {
                IdMetaModulo = idMetaModulo,
                IdMetaGruppo = idMetaGruppo,
                IdMetaCampo = idMetaCampo
            });

            return Ok(((int?)dimensione).ToString());
        }

        [HttpPost]
        public virtual async Task<IActionResult> ModificaDimensioneMetaCampo([FromBody] ModificaDimensioneMetaCampoViewModel model)
        {
            try
            {
                await _moduliService.Handle(new ComandoSincronizzato
                {
                    IdMetaModulo = model.IdMetaModulo,
                    Comando = () => _moduliService.Handle(new ModificaDimensioneMetaCampoCommand
                    {
                        IdMetaModulo = model.IdMetaModulo,
                        IdMetaGruppo = model.IdMetaGruppo,
                        IdMetaCampo = model.IdMetaCampo,
                        Dimensione = (DimensioneCampo)int.Parse(model.Dimensione)
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
