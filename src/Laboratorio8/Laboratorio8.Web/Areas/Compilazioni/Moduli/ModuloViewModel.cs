using Laboratorio8.Moduli;
using Laboratorio8.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Laboratorio8.Web.Areas.Compilazioni.Moduli
{
    public class ModuloViewModel
    {
        public ModuloViewModel()
        {
            Gruppi = new List<GruppoViewModel>();
        }

        public bool Readonly { get; set; }

        public Guid IdMetaModulo { get; set; }

        public string Titolo { get; set; }
        public string Descrizione { get; set; }

        public DateTime DataModifica { get; set; }
        public string DataModificaString { get; set; }

        public List<GruppoViewModel> Gruppi { get; set; }

        public string ToJson()
        {
            return JsonSerializer.ToJsonCamelCase(this);
        }

        [TypeScriptModule("Compilazioni.Moduli.Server")]
        public class GruppoViewModel
        {
            public int IdMetaGruppo { get; set; }

            public string Etichetta { get; set; }

            public int Ordine { get; set; }

            public List<CampoViewModel> Campi { get; set; }

            public GruppoViewModel()
            {
                Campi = new List<CampoViewModel>();
            }

            public GruppoViewModel(MetaModuloPerCompilazioneDTO.MetaGruppoDTO dto)
            {
                IdMetaGruppo = dto.Id;
                Etichetta = dto.Etichetta;
                Ordine = dto.Ordine;
                Campi = dto.MetaCampi.Select(x => new CampoViewModel(x)).ToList();
            }

            public GruppoViewModel(DettaglioModuloCompilatoDTO.GruppoDTO dto)
            {
                IdMetaGruppo = dto.Id;
                Etichetta = dto.Etichetta;
                Ordine = dto.Ordine;
                Campi = dto.Campi.Select(x => new CampoViewModel(x)).ToList();
            }
        }

        [TypeScriptModule("Compilazioni.Moduli.Server")]
        public class CampoViewModel
        {
            public int IdMetaCampo { get; set; }

            public string Etichetta { get; set; }

            public TipologiaMetaCampo Tipo { get; set; }

            public bool Obbligatorio { get; set; }

            public int Ordine { get; set; }

            public DimensioneCampo Dimensione { get; set; }

            public string Contenuto { get; set; }

            public CampoViewModel() { }

            public CampoViewModel(MetaModuloPerCompilazioneDTO.MetaGruppoDTO.MetaCampoDTO dto)
            {
                IdMetaCampo = dto.Id;
                Etichetta = dto.Etichetta;
                Tipo = dto.Tipo;
                Obbligatorio = dto.Obbligatorio;
                Ordine = dto.Ordine;
                Dimensione = dto.Dimensione;
            }

            public CampoViewModel(DettaglioModuloCompilatoDTO.GruppoDTO.CampoDTO dto)
            {
                IdMetaCampo = dto.Id;
                Etichetta = dto.Etichetta;
                Tipo = dto.Tipo;
                Obbligatorio = dto.Obbligatorio;
                Ordine = dto.Ordine;
                Dimensione = dto.Dimensione;
                Contenuto = dto.Contenuto;
            }
        }
    }
}
