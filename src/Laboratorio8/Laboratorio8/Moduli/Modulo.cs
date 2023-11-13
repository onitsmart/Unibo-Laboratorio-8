using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laboratorio8.Moduli
{
    public class Modulo
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? IdMetaModulo { get; set; }
        [ForeignKey("IdMetaModulo")]
        [InverseProperty("Moduli")]
        public MetaModulo MetaModulo { get; set; }

        [StringLength(50)]
        public string Titolo { get; set; }

        [StringLength(250)]
        public string Descrizione { get; set; }

        public DateTime DataCompilazione { get; set; }

        public string ContenutoJson { get; set; }
        [NotMapped]
        public ICollection<Gruppo> Contenuto
        {
            get
            {
                if (this.ContenutoJson == null)
                    return new List<Gruppo>();
                else
                    return (JsonConvert.DeserializeObject<List<Gruppo>>(this.ContenutoJson));
            }
            set
            {
                this.ContenutoJson = JsonConvert.SerializeObject(value);
            }
        }
    }

    public class Gruppo
    {
        public int Id { get; set; }

        public string Etichetta { get; set; }

        public int Ordine { get; set; }

        public string CampiJson { get; set; }
        public IEnumerable<Campo> Campi { get; set; }
    }

    public class Campo
    {
        public int Id { get; set; }

        public string Etichetta { get; set; }

        public TipologiaMetaCampo Tipo { get; set; }

        public bool Obbligatorio { get; set; }

        public int Ordine { get; set; }

        public DimensioneCampo Dimensione { get; set; }

        public string Contenuto { get; set; }
    }
}
