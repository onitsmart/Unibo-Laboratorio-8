using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laboratorio8.Moduli
{
    public partial class MetaModulo
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(50)]
        public string Titolo { get; set; }

        [StringLength(250)]
        public string Descrizione { get; set; }

        public DateTime DataCreazione { get; set; }

        public DateTime DataModifica { get; set; }

        public string MetaContenutoJson { get; set; }
        [NotMapped]
        public ICollection<MetaGruppo> MetaGruppi
        {
            get
            {
                if (this.MetaContenutoJson == null)
                    return new List<MetaGruppo>();
                else
                    return (JsonConvert.DeserializeObject<ICollection<MetaGruppo>>(this.MetaContenutoJson));
            }
            set
            {
                this.MetaContenutoJson = JsonConvert.SerializeObject(value);
            }
        }

        public IEnumerable<Modulo> Moduli { get; set; }

        public MetaModulo()
        {
            Moduli = new List<Modulo>();
            MetaGruppi = new List<MetaGruppo>();
        }
    }

    public partial class MetaGruppo
    {
        public int Id { get; set; }

        public string Etichetta { get; set; }

        public int Ordine { get; set; }

        public ICollection<MetaCampo> MetaCampi { get; set; }

        public MetaGruppo()
        {
            MetaCampi = new List<MetaCampo>();
        }
    }

    public partial class MetaCampo
    {
        public int Id { get; set; }

        public string Etichetta { get; set; }

        public TipologiaMetaCampo Tipo { get; set; }

        public bool Obbligatorio { get; set; }

        public int Ordine { get; set; }

        public DimensioneCampo Dimensione { get; set; }
    }

    public enum TipologiaMetaCampo
    {
        [Description("Testo breve")]
        TestoBreve = 0
    }

    public enum DimensioneCampo
    {
        [Description("Singola colonna")]
        SingolaColonna = 1,
        [Description("Doppia colonna")]
        DoppiaColonna = 2,
        [Description("Quadrupla colonna")]
        QuadruplaColonna = 4
    }
}
