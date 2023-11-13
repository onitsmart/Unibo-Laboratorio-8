using Laboratorio8.Infrastructure;
using System;

namespace Laboratorio8.Moduli
{
    public partial class MetaModuloModificato : IEvent
    {
        public Guid IdMetaModulo { get; set; }
    }

    public partial class MetaModuloModificatoTitolo : MetaModuloModificato { }
    public partial class MetaModuloModificataDescrizione : MetaModuloModificato { }

    public partial class MetaGruppoModificato : MetaModuloModificato
    {
        public int IdMetaGruppo { get; set; }
    }

    public partial class MetaModuloAggiuntoMetaGruppo : MetaGruppoModificato { }
    public partial class MetaModuloEliminatoMetaGruppo : MetaGruppoModificato { }
    public partial class MetaGruppoModificataEtichetta : MetaGruppoModificato { }

    public partial class MetaCampoModificato : MetaGruppoModificato
    {
        public int IdMetaCampo { get; set; }
    }

    public partial class MetaGruppoAggiuntoMetaCampo : MetaCampoModificato { }
    public partial class MetaGruppoEliminatoMetaCampo : MetaCampoModificato { }
    public partial class MetaCampoModificataEtichetta : MetaCampoModificato { }
    public partial class MetaCampoModificataTipologia : MetaCampoModificato { }
    public partial class MetaCampoModificataDimensione : MetaCampoModificato { }
    public partial class MetaCampoModificataObbligatorieta : MetaCampoModificato { }

    public partial class AggiuntoUtenteInCollaborazione : MetaModuloModificato
    {
        public int? IdMetaGruppo { get; set; }
        public int? IdMetaCampo { get; set; }
        public string Email { get; set; }
    }

    public partial class RimossoUtenteInCollaborazione : MetaModuloModificato
    {
        public string Email { get; set; }
    }
}
