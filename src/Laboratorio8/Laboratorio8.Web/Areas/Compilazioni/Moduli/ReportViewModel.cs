using Laboratorio8.Moduli;
using System.Linq;

namespace Laboratorio8.Web.Areas.Compilazioni.Moduli
{
    public class ReportViewModel : ModuloViewModel
    {
        public ReportViewModel()
        {
            Readonly = true;
        }

        public bool PrintPreview { get; set; }

        internal void SetModel(DettaglioModuloCompilatoDTO modulo)
        {
            if (modulo != null)
            {
                IdMetaModulo = modulo.Id;
                Titolo = modulo.Titolo;
                Descrizione = modulo.Descrizione;
                DataModifica = modulo.DataCompilazione;
                DataModificaString = modulo.DataCompilazione.ToString("dd/MM/yyyy HH:mm");

                Gruppi = modulo.Gruppi.Select(x => new GruppoViewModel(x)).ToList();
            }
        }
    }
}
