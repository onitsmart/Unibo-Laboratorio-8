declare module Configurazioni.MetaModuli.Server {
	interface metaGruppoViewModel {
		id: number;
		etichetta: string;
		ordine: number;
		metaCampi: Configurazioni.MetaModuli.Server.metaCampoViewModel[];
	}
	interface metaCampoViewModel {
		id: number;
		etichetta: string;
		tipo: string;
		obbligatorio: boolean;
		ordine: number;
		dimensione: string;
	}
	interface utenteViewModel {
		email: string;
		gravatarUrl: string;
		idMetaGruppo: number;
		idMetaCampo: number;
	}
	interface editViewModel {
		urlCompilazione: string;
		urlModificaTitoloMetaModulo: string;
		urlCercaAggiornamentoTitoloMetaModulo: string;
		urlModificaDescrizioneMetaModulo: string;
		urlCercaAggiornamentoDescrizioneMetaModulo: string;
		urlAggiungiMetaGruppoInMetaModulo: string;
		urlCercaAggiornamentoMetaGruppoInMetaModulo: string;
		urlEliminaMetaGruppoInMetaModulo: string;
		urlModificaEtichettaMetaGruppo: string;
		urlCercaAggiornamentoEtichettaMetaGruppo: string;
		urlAggiungiMetaCampoInMetaGruppo: string;
		urlCercaAggiornamentoMetaCampoInMetaGruppo: string;
		urlEliminaMetaCampoInMetaGruppo: string;
		urlModificaEtichettaMetaCampo: string;
		urlCercaAggiornamentoEtichettaMetaCampo: string;
		urlModificaTipologiaMetaCampo: string;
		urlCercaAggiornamentoTipologiaMetaCampo: string;
		urlModificaDimensioneMetaCampo: string;
		urlCercaAggiornamentoDimensioneMetaCampo: string;
		urlModificaObbligatorietaMetaCampo: string;
		urlCercaAggiornamentoObbligatorietaMetaCampo: string;
		urlGeneraGravatarUrl: string;
		urlAggiungiUtenteInCollaborazione: string;
		urlRimuoviUtenteInCollaborazione: string;
		tipologieMetacampiOptions: any[];
		dimensioniOptions: any[];
		emailUtenteCorrente: string;
		id: any;
		titolo: string;
		descrizione: string;
		dataModifica: Date;
		dataModificaString: string;
		utentiAttivi: Configurazioni.MetaModuli.Server.utenteViewModel[];
		metaGruppi: Configurazioni.MetaModuli.Server.metaGruppoViewModel[];
	}
	interface modificaTitoloMetaModuloViewModel {
		idMetaModulo: any;
		titolo: string;
	}
	interface modificaDescrizioneMetaModuloViewModel {
		idMetaModulo: any;
		descrizione: string;
	}
	interface aggiungiMetaGruppoInMetaModuloViewModel {
		idMetaModulo: any;
	}
	interface eliminaMetaGruppoInMetaModuloViewModel {
		idMetaModulo: any;
		idMetaGruppo: number;
	}
	interface modificaEtichettaMetaGruppoViewModel {
		idMetaModulo: any;
		idMetaGruppo: number;
		etichetta: string;
	}
	interface aggiungiMetaCampoInMetaGruppoViewModel {
		idMetaModulo: any;
		idMetaGruppo: number;
	}
	interface eliminaMetaCampoInMetaGruppoViewModel {
		idMetaModulo: any;
		idMetaGruppo: number;
		idMetaCampo: number;
	}
	interface modificaEtichettaMetaCampoViewModel {
		idMetaModulo: any;
		idMetaGruppo: number;
		idMetaCampo: number;
		etichetta: string;
	}
	interface modificaTipologiaMetaCampoViewModel {
		idMetaModulo: any;
		idMetaGruppo: number;
		idMetaCampo: number;
		tipologia: string;
	}
	interface modificaDimensioneMetaCampoViewModel {
		idMetaModulo: any;
		idMetaGruppo: number;
		idMetaCampo: number;
		dimensione: string;
	}
	interface modificaObbligatorietaMetaCampoViewModel {
		idMetaModulo: any;
		idMetaGruppo: number;
		idMetaCampo: number;
		obbligatorio: boolean;
	}
	interface aggiungiUtenteInCollaborazioneViewModel {
		email: string;
		idMetaModulo: any;
		idMetaGruppo?: number;
		idMetaCampo?: number;
	}
	interface rimuoviUtenteInCollaborazioneViewModel {
		email: string;
		idMetaModulo: any;
	}
}
