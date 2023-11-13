using Laboratorio8.Moduli;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Laboratorio8.Web.Areas.Configurazioni.MetaModuli
{
    public partial class MetaModuliController
    {
        [HttpGet]
        public virtual async Task<IActionResult> CercaAggiornamentoMetaCampoInMetaGruppo(Guid idMetaModulo, int idMetaGruppo, int idMetaCampo)
        {
            var metaCampoDTO = await _moduliService.Query(new AggiornamentoMetaCampoInMetaGruppoQuery
            {
                IdMetaModulo = idMetaModulo,
                IdMetaGruppo = idMetaGruppo,
                IdMetaCampo = idMetaCampo
            });

            return Ok(new EditViewModel.MetaCampoViewModel(metaCampoDTO));
        }

        [HttpPost]
        public virtual async Task<IActionResult> AggiungiMetaCampoInMetaGruppo([FromBody] AggiungiMetaCampoInMetaGruppoViewModel model)
        {
            try
            {
                await _moduliService.Handle(new ComandoSincronizzato
                {
                    IdMetaModulo = model.IdMetaModulo,
                    Comando = () => _moduliService.Handle(new AggiungiMetaCampoInMetaGruppoCommand
                    {
                        IdMetaModulo = model.IdMetaModulo,
                        IdMetaGruppo = model.IdMetaGruppo
                    })
                });

                return Ok();
            }
            catch (ServicesExceptions ex)
            {
                return Problem(ex.ToString(), null, 400, ex.Message, null);
            }
        }

        [HttpPost]
        public virtual async Task<IActionResult> EliminaMetaCampoInMetaGruppo([FromBody] EliminaMetaCampoInMetaGruppoViewModel model)
        {
            try
            {
                await _moduliService.Handle(new ComandoSincronizzato
                {
                    IdMetaModulo = model.IdMetaModulo,
                    Comando = () => _moduliService.Handle(new EliminaMetaCampoInMetaGruppoCommand
                    {
                        IdMetaModulo = model.IdMetaModulo,
                        IdMetaGruppo = model.IdMetaGruppo,
                        IdMetaCampo = model.IdMetaCampo
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
