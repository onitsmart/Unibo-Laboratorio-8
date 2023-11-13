using Laboratorio8.Moduli;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace Laboratorio8.Web.Areas.Compilazioni.Moduli
{
    public class EditViewModel : ModuloViewModel
    {
        public EditViewModel()
        {
            this.Readonly = false;
        }

        internal void SetModel(MetaModuloPerCompilazioneDTO metaModulo)
        {
            if (metaModulo != null)
            {
                this.IdMetaModulo = metaModulo.Id;
                this.Titolo = metaModulo.Titolo;
                this.Descrizione = metaModulo.Descrizione;
                this.DataModifica = metaModulo.DataModifica;
                this.DataModificaString = metaModulo.DataModifica.ToString("dd/MM/yyyy HH:mm");

                this.Gruppi = metaModulo.MetaGruppi.Select(x => new GruppoViewModel(x)).ToList();
            }
        }

        internal void ValidaModello(ModelStateDictionary modelState)
        {
            foreach (var gruppo in this.Gruppi)
            {
                foreach (var campo in gruppo.Campi)
                {
                    if (campo.Obbligatorio && string.IsNullOrWhiteSpace(campo.Contenuto))
                    {
                        var indexOfGruppo = this.Gruppi.IndexOf(gruppo);
                        var indexOfCampo = gruppo.Campi.IndexOf(campo);

                        modelState.AddModelError($"Gruppi[{indexOfGruppo}].Campi[{indexOfCampo}]", "Campo obbligatorio");
                    }
                }
            }
        }

        internal CompilaModuloCommand ToCompilaModuloCommand()
        {
            return new CompilaModuloCommand
            {
                IdMetaModulo = this.IdMetaModulo,
                Gruppi = this.Gruppi.Select(x => new CompilaModuloCommand.Gruppo
                {
                    IdMetaGruppo = x.IdMetaGruppo,
                    Campi = x.Campi.Select(y => new CompilaModuloCommand.Gruppo.Campo
                    {
                        IdMetaCampo = y.IdMetaCampo,
                        Contenuto = y.Contenuto
                    })
                })
            };
        }
    }
}
