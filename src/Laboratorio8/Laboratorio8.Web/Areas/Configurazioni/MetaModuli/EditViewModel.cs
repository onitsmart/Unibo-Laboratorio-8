using Laboratorio8.Moduli;
using Laboratorio8.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Laboratorio8.Web.Areas.Configurazioni.MetaModuli
{
    [TypeScriptModule("Configurazioni.MetaModuli.Server")]
    public class EditViewModel
    {
        public EditViewModel()
        {
            this.MetaGruppi = new MetaGruppoViewModel[0];
            this.UtentiAttivi = new UtenteViewModel[0];
        }

        public string UrlCompilazione { get; set; }
        public string UrlModificaTitoloMetaModulo { get; set; }
        public string UrlCercaAggiornamentoTitoloMetaModulo { get; set; }
        public string UrlModificaDescrizioneMetaModulo { get; set; }
        public string UrlCercaAggiornamentoDescrizioneMetaModulo { get; set; }

        public string UrlAggiungiMetaGruppoInMetaModulo { get; set; }
        public string UrlCercaAggiornamentoMetaGruppoInMetaModulo { get; set; }
        public string UrlEliminaMetaGruppoInMetaModulo { get; set; }
        public string UrlModificaEtichettaMetaGruppo { get; set; }
        public string UrlCercaAggiornamentoEtichettaMetaGruppo { get; set; }

        public string UrlAggiungiMetaCampoInMetaGruppo { get; set; }
        public string UrlCercaAggiornamentoMetaCampoInMetaGruppo { get; set; }
        public string UrlEliminaMetaCampoInMetaGruppo { get; set; }
        public string UrlModificaEtichettaMetaCampo { get; set; }
        public string UrlCercaAggiornamentoEtichettaMetaCampo { get; set; }
        public string UrlModificaTipologiaMetaCampo { get; set; }
        public string UrlCercaAggiornamentoTipologiaMetaCampo { get; set; }
        public string UrlModificaDimensioneMetaCampo { get; set; }
        public string UrlCercaAggiornamentoDimensioneMetaCampo { get; set; }
        public string UrlModificaObbligatorietaMetaCampo { get; set; }
        public string UrlCercaAggiornamentoObbligatorietaMetaCampo { get; set; }

        public string UrlGeneraGravatarUrl { get; set; }
        public string UrlAggiungiUtenteInCollaborazione { get; set; }
        public string UrlRimuoviUtenteInCollaborazione { get; set; }

        public IEnumerable<SelectListItem> TipologieMetacampiOptions
        {
            get
            {
                return new SelectListItem[]
                {
                    new SelectListItem
                    {
                        Text = TipologiaMetaCampo.TestoBreve.GetDescription(),
                        Value = ((int)TipologiaMetaCampo.TestoBreve).ToString()
                    }
                };
            }
        }
        public IEnumerable<SelectListItem> DimensioniOptions
        {
            get
            {
                return new SelectListItem[]
                {
                    new SelectListItem
                    {
                        Text = DimensioneCampo.SingolaColonna.GetDescription(),
                        Value = ((int)DimensioneCampo.SingolaColonna).ToString()
                    },
                    new SelectListItem
                    {
                        Text = DimensioneCampo.DoppiaColonna.GetDescription(),
                        Value = ((int)DimensioneCampo.DoppiaColonna).ToString()
                    },
                    new SelectListItem
                    {
                        Text = DimensioneCampo.QuadruplaColonna.GetDescription(),
                        Value = ((int)DimensioneCampo.QuadruplaColonna).ToString()
                    }
                };
            }
        }

        public string EmailUtenteCorrente { get; set; }

        public Guid Id { get; set; }

        public string Titolo { get; set; }
        public string Descrizione { get; set; }

        public DateTime DataModifica { get; set; }
        public string DataModificaString { get; set; }

        public IEnumerable<UtenteViewModel> UtentiAttivi { get; set; }
        public IEnumerable<MetaGruppoViewModel> MetaGruppi { get; set; }

        public string ToJson()
        {
            return JsonSerializer.ToJsonCamelCase(this);
        }

        internal void SetModulo(MetaModuloInEditDTO metaModulo)
        {
            if (metaModulo != null)
            {
                this.Id = metaModulo.Id;
                this.Titolo = metaModulo.Titolo;
                this.Descrizione = metaModulo.Descrizione;
                this.DataModifica = metaModulo.DataModifica;
                this.DataModificaString = metaModulo.DataModifica.ToString("dd/MM/yyyy HH:mm");

                this.MetaGruppi = metaModulo.MetaGruppi.Select(x => new MetaGruppoViewModel(x));
            }
        }

        internal void SetUrls(IUrlHelper url)
        {
            UrlCompilazione = url.ActionAbsolute(MVC.Compilazioni.Moduli.Edit(this.Id));

            UrlModificaTitoloMetaModulo = url.Action(MVC.Configurazioni.MetaModuli.ModificaTitoloMetaModulo());
            UrlCercaAggiornamentoTitoloMetaModulo = url.Action(MVC.Configurazioni.MetaModuli.CercaAggiornamentoTitoloMetaModulo());
            UrlModificaDescrizioneMetaModulo = url.Action(MVC.Configurazioni.MetaModuli.ModificaDescrizioneMetaModulo());
            UrlCercaAggiornamentoDescrizioneMetaModulo = url.Action(MVC.Configurazioni.MetaModuli.CercaAggiornamentoDescrizioneMetaModulo());

            UrlAggiungiMetaGruppoInMetaModulo = url.Action(MVC.Configurazioni.MetaModuli.AggiungiMetaGruppoInMetaModulo());
            UrlCercaAggiornamentoMetaGruppoInMetaModulo = url.Action(MVC.Configurazioni.MetaModuli.CercaAggiornamentoMetaGruppoInMetaModulo());
            UrlEliminaMetaGruppoInMetaModulo = url.Action(MVC.Configurazioni.MetaModuli.EliminaMetaGruppoInMetaModulo());
            UrlModificaEtichettaMetaGruppo = url.Action(MVC.Configurazioni.MetaModuli.ModificaEtichettaMetaGruppo());
            UrlCercaAggiornamentoEtichettaMetaGruppo = url.Action(MVC.Configurazioni.MetaModuli.CercaAggiornamentoEtichettaMetaGruppo());

            UrlAggiungiMetaCampoInMetaGruppo = url.Action(MVC.Configurazioni.MetaModuli.AggiungiMetaCampoInMetaGruppo());
            UrlCercaAggiornamentoMetaCampoInMetaGruppo = url.Action(MVC.Configurazioni.MetaModuli.CercaAggiornamentoMetaCampoInMetaGruppo());
            UrlEliminaMetaCampoInMetaGruppo = url.Action(MVC.Configurazioni.MetaModuli.EliminaMetaCampoInMetaGruppo());
            UrlModificaEtichettaMetaCampo = url.Action(MVC.Configurazioni.MetaModuli.ModificaEtichettaMetaCampo());
            UrlCercaAggiornamentoEtichettaMetaCampo = url.Action(MVC.Configurazioni.MetaModuli.CercaAggiornamentoEtichettaMetaCampo());
            UrlModificaTipologiaMetaCampo = url.Action(MVC.Configurazioni.MetaModuli.ModificaTipologiaMetaCampo());
            UrlCercaAggiornamentoTipologiaMetaCampo = url.Action(MVC.Configurazioni.MetaModuli.CercaAggiornamentoTipologiaMetaCampo());
            UrlModificaDimensioneMetaCampo = url.Action(MVC.Configurazioni.MetaModuli.ModificaDimensioneMetaCampo());
            UrlCercaAggiornamentoDimensioneMetaCampo = url.Action(MVC.Configurazioni.MetaModuli.CercaAggiornamentoDimensioneMetaCampo());
            UrlModificaObbligatorietaMetaCampo = url.Action(MVC.Configurazioni.MetaModuli.ModificaObbligatorietaMetaCampo());
            UrlCercaAggiornamentoObbligatorietaMetaCampo = url.Action(MVC.Configurazioni.MetaModuli.CercaAggiornamentoObbligatorietaMetaCampo());

            UrlGeneraGravatarUrl = url.Action(MVC.Configurazioni.MetaModuli.GeneraGravatarUrl());
            UrlAggiungiUtenteInCollaborazione = url.Action(MVC.Configurazioni.MetaModuli.AggiungiUtenteInCollaborazione());
            UrlRimuoviUtenteInCollaborazione = url.Action(MVC.Configurazioni.MetaModuli.RimuoviUtenteInCollaborazione());
        }

        [TypeScriptModule("Configurazioni.MetaModuli.Server")]
        public class MetaGruppoViewModel
        {
            public int Id { get; set; }

            public string Etichetta { get; set; }

            public int Ordine { get; set; }

            public IEnumerable<MetaCampoViewModel> MetaCampi { get; set; }

            public MetaGruppoViewModel()
            {
                this.MetaCampi = new MetaCampoViewModel[0];
            }

            public MetaGruppoViewModel(MetaModuloInEditDTO.MetaGruppoDTO dto)
            {
                Id = dto.Id;
                Etichetta = dto.Etichetta;
                Ordine = dto.Ordine;
                MetaCampi = dto.MetaCampi.Select(x => new MetaCampoViewModel(x));
            }
        }

        [TypeScriptModule("Configurazioni.MetaModuli.Server")]
        public class MetaCampoViewModel
        {
            public int Id { get; set; }

            public string Etichetta { get; set; }

            public string Tipo { get; set; }

            public bool Obbligatorio { get; set; }

            public int Ordine { get; set; }

            public string Dimensione { get; set; }

            public MetaCampoViewModel() { }

            public MetaCampoViewModel(MetaModuloInEditDTO.MetaGruppoDTO.MetaCampoDTO dto)
            {
                Id = dto.Id;
                Etichetta = dto.Etichetta;
                Tipo = ((int)dto.Tipo).ToString();
                Obbligatorio = dto.Obbligatorio;
                Ordine = dto.Ordine;
                Dimensione = ((int)dto.Dimensione).ToString();
            }
        }

        [TypeScriptModule("Configurazioni.MetaModuli.Server")]
        public class UtenteViewModel
        {
            public string Email { get; set; }
            public string GravatarUrl
            {
                get
                {
                    return Email.ToGravatarUrl(ToGravatarUrlExtension.DefaultGravatar.Identicon, null);
                }
            }

            public int IdMetaGruppo { get; set; }
            public int IdMetaCampo { get; set; }
        }
    }

    [TypeScriptModule("Configurazioni.MetaModuli.Server")]
    public class ModificaTitoloMetaModuloViewModel
    {
        public Guid IdMetaModulo { get; set; }
        public string Titolo { get; set; }
    }

    [TypeScriptModule("Configurazioni.MetaModuli.Server")]
    public class ModificaDescrizioneMetaModuloViewModel
    {
        public Guid IdMetaModulo { get; set; }
        public string Descrizione { get; set; }
    }

    [TypeScriptModule("Configurazioni.MetaModuli.Server")]
    public class AggiungiMetaGruppoInMetaModuloViewModel
    {
        public Guid IdMetaModulo { get; set; }
    }

    [TypeScriptModule("Configurazioni.MetaModuli.Server")]
    public class EliminaMetaGruppoInMetaModuloViewModel
    {
        public Guid IdMetaModulo { get; set; }
        public int IdMetaGruppo { get; set; }
    }

    [TypeScriptModule("Configurazioni.MetaModuli.Server")]
    public class ModificaEtichettaMetaGruppoViewModel
    {
        public Guid IdMetaModulo { get; set; }
        public int IdMetaGruppo { get; set; }
        public string Etichetta { get; set; }
    }

    [TypeScriptModule("Configurazioni.MetaModuli.Server")]
    public class AggiungiMetaCampoInMetaGruppoViewModel
    {
        public Guid IdMetaModulo { get; set; }
        public int IdMetaGruppo { get; set; }
    }

    [TypeScriptModule("Configurazioni.MetaModuli.Server")]
    public class EliminaMetaCampoInMetaGruppoViewModel
    {
        public Guid IdMetaModulo { get; set; }
        public int IdMetaGruppo { get; set; }
        public int IdMetaCampo { get; set; }
    }

    [TypeScriptModule("Configurazioni.MetaModuli.Server")]
    public class ModificaEtichettaMetaCampoViewModel
    {
        public Guid IdMetaModulo { get; set; }
        public int IdMetaGruppo { get; set; }
        public int IdMetaCampo { get; set; }
        public string Etichetta { get; set; }
    }

    [TypeScriptModule("Configurazioni.MetaModuli.Server")]
    public class ModificaTipologiaMetaCampoViewModel
    {
        public Guid IdMetaModulo { get; set; }
        public int IdMetaGruppo { get; set; }
        public int IdMetaCampo { get; set; }
        public string Tipologia { get; set; }
    }

    [TypeScriptModule("Configurazioni.MetaModuli.Server")]
    public class ModificaDimensioneMetaCampoViewModel
    {
        public Guid IdMetaModulo { get; set; }
        public int IdMetaGruppo { get; set; }
        public int IdMetaCampo { get; set; }
        public string Dimensione { get; set; }
    }

    [TypeScriptModule("Configurazioni.MetaModuli.Server")]
    public class ModificaObbligatorietaMetaCampoViewModel
    {
        public Guid IdMetaModulo { get; set; }
        public int IdMetaGruppo { get; set; }
        public int IdMetaCampo { get; set; }
        public bool Obbligatorio { get; set; }
    }

    [TypeScriptModule("Configurazioni.MetaModuli.Server")]
    public class AggiungiUtenteInCollaborazioneViewModel
    {
        public string Email { get; set; }
        public Guid IdMetaModulo { get; set; }
        public int? IdMetaGruppo { get; set; }
        public int? IdMetaCampo { get; set; }
    }

    [TypeScriptModule("Configurazioni.MetaModuli.Server")]
    public class RimuoviUtenteInCollaborazioneViewModel
    {
        public string Email { get; set; }
        public Guid IdMetaModulo { get; set; }
    }
}
