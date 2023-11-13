using Microsoft.Extensions.Logging;
using System;

namespace Laboratorio8.Web.SignalR.Hubs
{
    public interface IConfigurazioneMetaModuliEvents
    {
        public System.Threading.Tasks.Task MetaModuloModificatoTitolo();
        public System.Threading.Tasks.Task MetaModuloModificataDescrizione();

        public System.Threading.Tasks.Task MetaModuloAggiuntoMetaGruppo(int idMetaGruppo);
        public System.Threading.Tasks.Task MetaModuloEliminatoMetaGruppo(int idMetaGruppo);
        public System.Threading.Tasks.Task MetaGruppoModificataEtichetta(int idMetaGruppo);

        public System.Threading.Tasks.Task MetaGruppoAggiuntoMetaCampo(int idMetaGruppo, int idMetaCampo);
        public System.Threading.Tasks.Task MetaGruppoEliminatoMetaCampo(int idMetaGruppo, int idMetaCampo);
        public System.Threading.Tasks.Task MetaCampoModificataEtichetta(int idMetaGruppo, int idMetaCampo);
        public System.Threading.Tasks.Task MetaCampoModificataTipologia(int idMetaGruppo, int idMetaCampo);
        public System.Threading.Tasks.Task MetaCampoModificataDimensione(int idMetaGruppo, int idMetaCampo);
        public System.Threading.Tasks.Task MetaCampoModificataObbligatorieta(int idMetaGruppo, int idMetaCampo);

        public System.Threading.Tasks.Task UtenteInCollaborazione(string email, int? idMetaGruppo, int? idMetaCampo);
        public System.Threading.Tasks.Task UtenteAbbandonataCollaborazione(string email);
    }

    [Microsoft.AspNetCore.Authorization.Authorize]
    public class ConfigurazioneMetaModuliHub : BaseHub<IConfigurazioneMetaModuliEvents, ConfigurazioneMetaModuliHub>
    {
        public ConfigurazioneMetaModuliHub(ILogger<ConfigurazioneMetaModuliHub> logger) : base(logger) { }

        public async System.Threading.Tasks.Task EntraNelGruppo(Guid idMetaModulo)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, idMetaModulo.ToString());
        }
        public async System.Threading.Tasks.Task EsciDalGruppo(Guid idMetaModulo)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, idMetaModulo.ToString());
        }
    }
}
