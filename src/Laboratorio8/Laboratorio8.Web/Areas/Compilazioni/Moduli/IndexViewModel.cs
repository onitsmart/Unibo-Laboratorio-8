using Laboratorio8.Moduli;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Laboratorio8.Web.Areas.Compilazioni.Moduli
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            Moduli = Array.Empty<ModuloCompilatoInElencoViewModel>();
        }

        public Guid IdMetaModulo { get; set; }
        public string Titolo { get; set; }
        public string Descrizione { get; set; }

        public IEnumerable<ModuloCompilatoInElencoViewModel> Moduli { get; set; }
        public int TotalItems { get; set; }

        internal void SetMetaModulo(MetaModuloPerElencoModuliDTO metaModulo)
        {
            this.IdMetaModulo = metaModulo.Id;
            this.Titolo = metaModulo.Titolo;
            this.Descrizione = metaModulo.Descrizione;
        }

        internal void SetModuli(IEnumerable<ModuloInElencoDTO> moduli)
        {
            Moduli = moduli.Select(x => new ModuloCompilatoInElencoViewModel(x)).ToArray();
        }
    }

    public class ModuloCompilatoInElencoViewModel
    {
        public ModuloCompilatoInElencoViewModel(ModuloInElencoDTO modulo)
        {
            this.Id = modulo.Id;
            this.DataCompilazione = modulo.DataCompilazione;
            this.DataCompilazioneString = modulo.DataCompilazione.ToString("dd/MM/yyyy HH:mm");
        }

        public Guid Id { get; set; }

        public DateTime DataCompilazione { get; set; }
        public string DataCompilazioneString { get; set; }
    }
}
