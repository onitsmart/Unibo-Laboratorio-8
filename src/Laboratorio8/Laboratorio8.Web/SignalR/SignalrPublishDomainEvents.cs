using Laboratorio8.Infrastructure;
using Laboratorio8.Moduli;
using Laboratorio8.Web.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Laboratorio8.Web.SignalR
{
    public class SignalrPublishDomainEvents : IPublishDomainEvents
    {
        IHubContext<ConfigurazioneMetaModuliHub, IConfigurazioneMetaModuliEvents> _configurazioneMetaModuliHub;

        public SignalrPublishDomainEvents(
            IHubContext<ConfigurazioneMetaModuliHub, IConfigurazioneMetaModuliEvents> configurazioneMetaModuliHub)
        {
            _configurazioneMetaModuliHub = configurazioneMetaModuliHub;
        }

        public Task Publish(object evnt)
        {
            try
            {
                return ((dynamic)this).When((dynamic)evnt);
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
            {
                return Task.CompletedTask;
            }
        }

        private IConfigurazioneMetaModuliEvents GetConfigurazioneMetaModuloGroup(Guid idMetaModulo)
        {
            return _configurazioneMetaModuliHub.Clients.Group(idMetaModulo.ToString());
        }

        public Task When(MetaModuloModificatoTitolo e)
        {
            return GetConfigurazioneMetaModuloGroup(e.IdMetaModulo).MetaModuloModificatoTitolo();
        }

        public Task When(MetaModuloModificataDescrizione e)
        {
            return GetConfigurazioneMetaModuloGroup(e.IdMetaModulo).MetaModuloModificataDescrizione();
        }

        public Task When(MetaModuloAggiuntoMetaGruppo e)
        {
            return GetConfigurazioneMetaModuloGroup(e.IdMetaModulo).MetaModuloAggiuntoMetaGruppo(e.IdMetaGruppo);
        }

        public Task When(MetaModuloEliminatoMetaGruppo e)
        {
            return GetConfigurazioneMetaModuloGroup(e.IdMetaModulo).MetaModuloEliminatoMetaGruppo(e.IdMetaGruppo);
        }

        public Task When(MetaGruppoModificataEtichetta e)
        {
            return GetConfigurazioneMetaModuloGroup(e.IdMetaModulo).MetaGruppoModificataEtichetta(e.IdMetaGruppo);
        }

        public Task When(MetaGruppoAggiuntoMetaCampo e)
        {
            return GetConfigurazioneMetaModuloGroup(e.IdMetaModulo).MetaGruppoAggiuntoMetaCampo(e.IdMetaGruppo, e.IdMetaCampo);
        }

        public Task When(MetaGruppoEliminatoMetaCampo e)
        {
            return GetConfigurazioneMetaModuloGroup(e.IdMetaModulo).MetaGruppoEliminatoMetaCampo(e.IdMetaGruppo, e.IdMetaCampo);
        }

        public Task When(MetaCampoModificataEtichetta e)
        {
            return GetConfigurazioneMetaModuloGroup(e.IdMetaModulo).MetaCampoModificataEtichetta(e.IdMetaGruppo, e.IdMetaCampo);
        }

        public Task When(MetaCampoModificataTipologia e)
        {
            return GetConfigurazioneMetaModuloGroup(e.IdMetaModulo).MetaCampoModificataTipologia(e.IdMetaGruppo, e.IdMetaCampo);
        }

        public Task When(MetaCampoModificataDimensione e)
        {
            return GetConfigurazioneMetaModuloGroup(e.IdMetaModulo).MetaCampoModificataDimensione(e.IdMetaGruppo, e.IdMetaCampo);
        }

        public Task When(MetaCampoModificataObbligatorieta e)
        {
            return GetConfigurazioneMetaModuloGroup(e.IdMetaModulo).MetaCampoModificataObbligatorieta(e.IdMetaGruppo, e.IdMetaCampo);
        }

        public Task When(AggiuntoUtenteInCollaborazione e)
        {
            return GetConfigurazioneMetaModuloGroup(e.IdMetaModulo).UtenteInCollaborazione(e.Email, e.IdMetaGruppo, e.IdMetaCampo);
        }

        public Task When(RimossoUtenteInCollaborazione e)
        {
            return GetConfigurazioneMetaModuloGroup(e.IdMetaModulo).UtenteAbbandonataCollaborazione(e.Email);
        }
    }
}
