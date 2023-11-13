using Laboratorio8.Moduli;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Laboratorio8.Web.Areas.Configurazioni.MetaModuli
{
    public partial class MetaModuliController
    {
        [HttpGet]
        public virtual async Task<IActionResult> CercaAggiornamentoMetaGruppoInMetaModulo(Guid idMetaModulo, int idMetaGruppo)
        {
            var metaGruppoDTO = await _moduliService.Query(new AggiornamentoMetaGruppoInMetaModuloQuery
            {
                IdMetaModulo = idMetaModulo,
                IdMetaGruppo = idMetaGruppo
            });

            return Ok(new EditViewModel.MetaGruppoViewModel(metaGruppoDTO));
        }

        [HttpPost]
        public virtual async Task<IActionResult> AggiungiMetaGruppoInMetaModulo([FromBody] AggiungiMetaGruppoInMetaModuloViewModel model)
        {
            try
            {
                await _moduliService.Handle(new ComandoSincronizzato
                {
                    IdMetaModulo = model.IdMetaModulo,
                    Comando = () => _moduliService.Handle(new AggiungiMetaGruppoInMetaModuloCommand
                    {
                        IdMetaModulo = model.IdMetaModulo
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
        public virtual async Task<IActionResult> EliminaMetaGruppoInMetaModulo([FromBody] EliminaMetaGruppoInMetaModuloViewModel model)
        {
            try
            {
                await _moduliService.Handle(new ComandoSincronizzato
                {
                    IdMetaModulo = model.IdMetaModulo,
                    Comando = () => _moduliService.Handle(new EliminaMetaGruppoInMetaModuloCommand
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
    }
}
