using Laboratorio8.Moduli;
using Laboratorio8.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Laboratorio8.Web.Areas.Configurazioni.MetaModuli
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            Moduli = Array.Empty<ModuloInElencoViewModel>();
        }

        [Display(Name = "Cerca")]
        public string Filtro { get; set; }

        public IEnumerable<ModuloInElencoViewModel> Moduli { get; set; }
        public int TotalItems { get; set; }

        internal void SetModuli(IEnumerable<MetaModuloInElencoDTO> metaModuli)
        {
            Moduli = metaModuli.Select(x => new ModuloInElencoViewModel(x)).ToArray();
        }

        public MetaModuliElencoQuery ToMetaModuliElencoQuery()
        {
            return new MetaModuliElencoQuery
            {
                Filtro = Filtro
            };
        }

        public string ToJson()
        {
            return JsonSerializer.ToJsonCamelCase(this);
        }
    }

    public class ModuloInElencoViewModel
    {
        public ModuloInElencoViewModel(MetaModuloInElencoDTO modulo)
        {
            this.Id = modulo.Id;
            this.Titolo = modulo.Titolo;
            this.Descrizione = modulo.Descrizione;
            this.DataModifica = modulo.DataModifica;
            this.DataModificaString = modulo.DataModifica.ToString("dd/MM/yyyy HH:mm");
        }

        public Guid Id { get; set; }

        public string Titolo { get; set; }
        public string Descrizione { get; set; }

        public DateTime DataModifica { get; set; }
        public string DataModificaString { get; set; }
    }
}
