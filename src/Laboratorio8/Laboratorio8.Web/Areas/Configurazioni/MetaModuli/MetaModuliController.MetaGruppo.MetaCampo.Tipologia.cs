using Laboratorio8.Moduli;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace Laboratorio8.Web.Areas.Configurazioni.MetaModuli
{
    public partial class MetaModuliController
    {
        [HttpGet]
        public virtual async Task<IActionResult> CercaAggiornamentoTipologiaMetaCampo(Guid idMetaModulo, int idMetaGruppo, int idMetaCampo)
        {
            var tipologia = await _moduliService.Query(new AggiornamentoTipologiaMetaCampoQuery
            {
                IdMetaModulo = idMetaModulo,
                IdMetaGruppo = idMetaGruppo,
                IdMetaCampo = idMetaCampo
            });

            return Ok(((int?)tipologia).ToString());
        }

        [HttpPost]
        public virtual async Task<IActionResult> ModificaTipologiaMetaCampo([FromBody] ModificaTipologiaMetaCampoViewModel model)
        {
            try
            {
                await _moduliService.Handle(new ComandoSincronizzato
                {
                    IdMetaModulo = model.IdMetaModulo,
                    Comando = () => _moduliService.Handle(new ModificaTipologiaMetaCampoCommand
                    {
                        IdMetaModulo = model.IdMetaModulo,
                        IdMetaGruppo = model.IdMetaGruppo,
                        IdMetaCampo = model.IdMetaCampo,
                        Tipologia = (TipologiaMetaCampo)int.Parse(model.Tipologia)
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
