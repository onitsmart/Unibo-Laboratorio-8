module Configurazioni.MetaModuli {
    export class editVueModel {
        constructor(public hub: any, public model: Configurazioni.MetaModuli.Server.editViewModel) {
            if (this.hub) {
                this.hub.on("MetaModuloModificatoTitolo", async () => {
                    var url = utilities.composeRelativeUrlWithParams(this.model.urlCercaAggiornamentoTitoloMetaModulo, "idMetaModulo", this.model.id);
                    const dtoAggiornamento = await utilities.getJsonT<string>(url);
                    this.aggiornaTitoloMetaModulo(dtoAggiornamento);
                });

                this.hub.on("MetaModuloModificataDescrizione", async () => {
                    var url = utilities.composeRelativeUrlWithParams(this.model.urlCercaAggiornamentoDescrizioneMetaModulo, "idMetaModulo", this.model.id);
                    const dtoAggiornamento = await utilities.getJsonT<string>(url);
                    this.aggiornaDescrizioneMetaModulo(dtoAggiornamento);
                });

                this.hub.on("MetaModuloAggiuntoMetaGruppo", async (idMetaGruppo: number) => {
                    var url = utilities.composeRelativeUrlWithParams(this.model.urlCercaAggiornamentoMetaGruppoInMetaModulo, "idMetaModulo", this.model.id, "idMetaGruppo", idMetaGruppo);
                    const dtoAggiornamento = await utilities.getJsonT<Configurazioni.MetaModuli.Server.metaGruppoViewModel>(url);
                    this.aggiornaAggiuntoMetaGruppo(dtoAggiornamento);
                });

                this.hub.on("MetaModuloEliminatoMetaGruppo", async (idMetaGruppo: number) => {
                    this.aggiornaEliminatoMetaGruppo(idMetaGruppo);
                });

                this.hub.on("MetaGruppoModificataEtichetta", async (idMetaGruppo: number) => {
                    var url = utilities.composeRelativeUrlWithParams(this.model.urlCercaAggiornamentoEtichettaMetaGruppo, "idMetaModulo", this.model.id, "idMetaGruppo", idMetaGruppo);
                    const dtoAggiornamento = await utilities.getJsonT<string>(url);
                    this.aggiornaEtichettaMetaGruppo(idMetaGruppo, dtoAggiornamento);
                });

                this.hub.on("MetaGruppoAggiuntoMetaCampo", async (idMetaGruppo: number, idMetaCampo: number) => {
                    var url = utilities.composeRelativeUrlWithParams(this.model.urlCercaAggiornamentoMetaCampoInMetaGruppo, "idMetaModulo", this.model.id, "idMetaGruppo", idMetaGruppo, "idMetaCampo", idMetaCampo);
                    const dtoAggiornamento = await utilities.getJsonT<Configurazioni.MetaModuli.Server.metaCampoViewModel>(url);
                    this.aggiornaAggiuntoMetaCampo(idMetaGruppo, dtoAggiornamento);
                });

                this.hub.on("MetaGruppoEliminatoMetaCampo", async (idMetaGruppo: number, idMetaCampo: number) => {
                    this.aggiornaEliminatoMetaCampo(idMetaGruppo, idMetaCampo);
                });

                this.hub.on("MetaCampoModificataEtichetta", async (idMetaGruppo: number, idMetaCampo: number) => {
                    var url = utilities.composeRelativeUrlWithParams(this.model.urlCercaAggiornamentoEtichettaMetaCampo, "idMetaModulo", this.model.id, "idMetaGruppo", idMetaGruppo, "idMetaCampo", idMetaCampo);
                    const dtoAggiornamento = await utilities.getJsonT<string>(url);
                    this.aggiornaEtichettaMetaCampo(idMetaGruppo, idMetaCampo, dtoAggiornamento);
                });

                this.hub.on("MetaCampoModificataTipologia", async (idMetaGruppo: number, idMetaCampo: number) => {
                    var url = utilities.composeRelativeUrlWithParams(this.model.urlCercaAggiornamentoTipologiaMetaCampo, "idMetaModulo", this.model.id, "idMetaGruppo", idMetaGruppo, "idMetaCampo", idMetaCampo);
                    const dtoAggiornamento = await utilities.getJsonT<string>(url);
                    this.aggiornaTipologiaMetaCampo(idMetaGruppo, idMetaCampo, dtoAggiornamento);
                });

                this.hub.on("MetaCampoModificataDimensione", async (idMetaGruppo: number, idMetaCampo: number) => {
                    var url = utilities.composeRelativeUrlWithParams(this.model.urlCercaAggiornamentoDimensioneMetaCampo, "idMetaModulo", this.model.id, "idMetaGruppo", idMetaGruppo, "idMetaCampo", idMetaCampo);
                    const dtoAggiornamento = await utilities.getJsonT<string>(url);
                    this.aggiornaDimensioneMetaCampo(idMetaGruppo, idMetaCampo, dtoAggiornamento);
                });

                this.hub.on("MetaCampoModificataObbligatorieta", async (idMetaGruppo: number, idMetaCampo: number) => {
                    var url = utilities.composeRelativeUrlWithParams(this.model.urlCercaAggiornamentoObbligatorietaMetaCampo, "idMetaModulo", this.model.id, "idMetaGruppo", idMetaGruppo, "idMetaCampo", idMetaCampo);
                    const dtoAggiornamento = await utilities.getJsonT<boolean>(url);
                    this.aggiornaObbligatorietaMetaCampo(idMetaGruppo, idMetaCampo, dtoAggiornamento);
                });

                this.hub.on("UtenteInCollaborazione", async (email: string, idMetaGruppo: any, idMetaCampo: any) => {
                    var url = utilities.composeRelativeUrlWithParams(this.model.urlGeneraGravatarUrl, "email", email);
                    const gravatarUrl = await utilities.getJsonT<string>(url);
                    this.aggiornaAggiuntoUtenteAttivo(<Configurazioni.MetaModuli.Server.utenteViewModel>{
                        email: email,
                        gravatarUrl: gravatarUrl,
                        idMetaGruppo: idMetaGruppo,
                        idMetaCampo: idMetaCampo
                    });
                });

                this.hub.on("UtenteAbbandonataCollaborazione", async (email: string) => {
                    this.aggiornaRimossoUtenteAttivo(email);
                });
            }
        }

        //#region titolo meta modulo
        public aggiornaTitoloMetaModulo = async (titolo: string) => {
            this.model.titolo = titolo;
        }

        public modificaTitoloMetaModulo = async () => {
            const cmd: Configurazioni.MetaModuli.Server.modificaTitoloMetaModuloViewModel = { idMetaModulo: this.model.id, titolo: this.model.titolo };
            try {
                await utilities.postJson(this.model.urlModificaTitoloMetaModulo, cmd);
            } catch (e) {
                utilities.alertError(e);
            }
        }
        //#endregion

        //#region descrizione meta modulo
        public aggiornaDescrizioneMetaModulo = async (descrizione: string) => {
            this.model.descrizione = descrizione;
        }

        public modificaDescrizioneMetaModulo = async () => {
            const cmd: Configurazioni.MetaModuli.Server.modificaDescrizioneMetaModuloViewModel = { idMetaModulo: this.model.id, descrizione: this.model.descrizione };
            try {
                await utilities.postJson(this.model.urlModificaDescrizioneMetaModulo, cmd);
            } catch (e) {
                utilities.alertError(e);
            }
        }
        //#endregion

        //#region meta gruppi
        public aggiornaAggiuntoMetaGruppo = async (metaGruppo: Configurazioni.MetaModuli.Server.metaGruppoViewModel) => {
            this.model.metaGruppi.push(metaGruppo);
        }

        public aggiungiMetaGruppo = async () => {
            const cmd: Configurazioni.MetaModuli.Server.aggiungiMetaGruppoInMetaModuloViewModel = { idMetaModulo: this.model.id };
            try {
                await utilities.postJson(this.model.urlAggiungiMetaGruppoInMetaModulo, cmd);
            } catch (e) {
                utilities.alertError(e);
            }
        }

        public aggiornaEliminatoMetaGruppo = async (idMetaGruppo: number) => {
            var indexMetaGruppo = this.model.metaGruppi.findIndex(x => x.id == idMetaGruppo);

            if (indexMetaGruppo > -1)
                this.model.metaGruppi.splice(indexMetaGruppo, 1);
        }

        public eliminaMetaGruppo = async (idMetaGruppo: number) => {
            const cmd: Configurazioni.MetaModuli.Server.eliminaMetaGruppoInMetaModuloViewModel = { idMetaModulo: this.model.id, idMetaGruppo: idMetaGruppo };
            try {
                await utilities.postJson(this.model.urlEliminaMetaGruppoInMetaModulo, cmd);
            } catch (e) {
                utilities.alertError(e);
            }
        }

        //#endregion

        //#region etichetta meta gruppo
        public aggiornaEtichettaMetaGruppo = async (idMetaGruppo: number, etichetta: string) => {
            var metaGruppo = this.model.metaGruppi.find(x => x.id == idMetaGruppo);

            if (metaGruppo)
                metaGruppo.etichetta = etichetta;
        }

        public modificaEtichettaMetaGruppo = async (idMetaGruppo: number) => {
            let metaGruppo = this.model.metaGruppi.find(x => x.id == idMetaGruppo);

            if (metaGruppo) {
                const cmd: Configurazioni.MetaModuli.Server.modificaEtichettaMetaGruppoViewModel = { idMetaModulo: this.model.id, idMetaGruppo: idMetaGruppo, etichetta: metaGruppo.etichetta };
                try {
                    await utilities.postJson(this.model.urlModificaEtichettaMetaGruppo, cmd);
                } catch (e) {
                    utilities.alertError(e);
                }
            }
        }
        //#endregion

        //#region meta campi
        public aggiornaAggiuntoMetaCampo = async (idMetaGruppo: number, metaCampo: Configurazioni.MetaModuli.Server.metaCampoViewModel) => {
            var metaGruppo = this.model.metaGruppi.find(x => x.id == idMetaGruppo);

            if (metaGruppo)
                metaGruppo.metaCampi.push(metaCampo);
        }

        public aggiungiMetaCampo = async (idMetaGruppo: number) => {
            const cmd: Configurazioni.MetaModuli.Server.aggiungiMetaCampoInMetaGruppoViewModel = { idMetaModulo: this.model.id, idMetaGruppo: idMetaGruppo };
            try {
                await utilities.postJson(this.model.urlAggiungiMetaCampoInMetaGruppo, cmd);
            } catch (e) {
                utilities.alertError(e);
            }
        }

        public aggiornaEliminatoMetaCampo = async (idMetaGruppo: number, idMetaCampo: number) => {
            var metaGruppo = this.model.metaGruppi.find(x => x.id == idMetaGruppo);

            if (metaGruppo) {
                var indexMetaCampo = metaGruppo.metaCampi.findIndex(x => x.id == idMetaCampo);

                if (indexMetaCampo > -1)
                    metaGruppo.metaCampi.splice(indexMetaCampo, 1);
            }
        }

        public eliminaMetaCampo = async (idMetaGruppo: number, idMetaCampo: number) => {
            const cmd: Configurazioni.MetaModuli.Server.eliminaMetaCampoInMetaGruppoViewModel = { idMetaModulo: this.model.id, idMetaGruppo: idMetaGruppo, idMetaCampo: idMetaCampo };
            try {
                await utilities.postJson(this.model.urlEliminaMetaCampoInMetaGruppo, cmd);
            } catch (e) {
                utilities.alertError(e);
            }
        }
        //#endregion

        //#region etichetta meta campo
        public aggiornaEtichettaMetaCampo = async (idMetaGruppo: number, idMetaCampo: number, etichetta: string) => {
            var metaGruppo = this.model.metaGruppi.find(x => x.id == idMetaGruppo);

            if (metaGruppo) {
                var metaCampo = metaGruppo.metaCampi.find(x => x.id == idMetaCampo);

                if (metaCampo)
                    metaCampo.etichetta = etichetta;
            }
        }

        public modificaEtichettaMetaCampo = async (idMetaGruppo: number, idMetaCampo: number) => {
            let metaGruppo = this.model.metaGruppi.find(x => x.id == idMetaGruppo);

            if (metaGruppo) {
                let metaCampo = metaGruppo.metaCampi.find(x => x.id == idMetaCampo);

                if (metaCampo) {
                    const cmd: Configurazioni.MetaModuli.Server.modificaEtichettaMetaCampoViewModel = { idMetaModulo: this.model.id, idMetaGruppo: idMetaGruppo, idMetaCampo: idMetaCampo, etichetta: metaCampo.etichetta };
                    try {
                        await utilities.postJson(this.model.urlModificaEtichettaMetaCampo, cmd);
                    } catch (e) {
                        utilities.alertError(e);
                    }
                }
            }
        }
        //#endregion

        //#region tipologia meta campo
        public aggiornaTipologiaMetaCampo = async (idMetaGruppo: number, idMetaCampo: number, tipologia: string) => {
            var metaGruppo = this.model.metaGruppi.find(x => x.id == idMetaGruppo);

            if (metaGruppo) {
                var metaCampo = metaGruppo.metaCampi.find(x => x.id == idMetaCampo);

                if (metaCampo)
                    metaCampo.tipo = tipologia;
            }
        }

        public modificaTipologiaMetaCampo = async (idMetaGruppo: number, idMetaCampo: number) => {
            let metaGruppo = this.model.metaGruppi.find(x => x.id == idMetaGruppo);

            if (metaGruppo) {
                let metaCampo = metaGruppo.metaCampi.find(x => x.id == idMetaCampo);

                if (metaCampo) {
                    const cmd: Configurazioni.MetaModuli.Server.modificaTipologiaMetaCampoViewModel = { idMetaModulo: this.model.id, idMetaGruppo: idMetaGruppo, idMetaCampo: idMetaCampo, tipologia: metaCampo.tipo };
                    try {
                        await utilities.postJson(this.model.urlModificaTipologiaMetaCampo, cmd);
                    } catch (e) {
                        utilities.alertError(e);
                    }
                }
            }
        }
        //#endregion

        //#region dimensione meta campo
        public aggiornaDimensioneMetaCampo = async (idMetaGruppo: number, idMetaCampo: number, dimensione: string) => {
            var metaGruppo = this.model.metaGruppi.find(x => x.id == idMetaGruppo);

            if (metaGruppo) {
                var metaCampo = metaGruppo.metaCampi.find(x => x.id == idMetaCampo);

                if (metaCampo)
                    metaCampo.dimensione = dimensione;
            }
        }

        public modificaDimensioneMetaCampo = async (idMetaGruppo: number, idMetaCampo: number) => {
            let metaGruppo = this.model.metaGruppi.find(x => x.id == idMetaGruppo);

            if (metaGruppo) {
                let metaCampo = metaGruppo.metaCampi.find(x => x.id == idMetaCampo);

                if (metaCampo) {
                    const cmd: Configurazioni.MetaModuli.Server.modificaDimensioneMetaCampoViewModel = { idMetaModulo: this.model.id, idMetaGruppo: idMetaGruppo, idMetaCampo: idMetaCampo, dimensione: metaCampo.dimensione };
                    try {
                        await utilities.postJson(this.model.urlModificaDimensioneMetaCampo, cmd);
                    } catch (e) {
                        utilities.alertError(e);
                    }
                }
            }
        }
        //#endregion

        //#region obbligatorieta meta campo
        public aggiornaObbligatorietaMetaCampo = async (idMetaGruppo: number, idMetaCampo: number, obbligatorio: boolean) => {
            var metaGruppo = this.model.metaGruppi.find(x => x.id == idMetaGruppo);

            if (metaGruppo) {
                var metaCampo = metaGruppo.metaCampi.find(x => x.id == idMetaCampo);

                if (metaCampo)
                    metaCampo.obbligatorio = obbligatorio;
            }
        }

        public modificaObbligatorietaMetaCampo = async (idMetaGruppo: number, idMetaCampo: number) => {
            let metaGruppo = this.model.metaGruppi.find(x => x.id == idMetaGruppo);

            if (metaGruppo) {
                let metaCampo = metaGruppo.metaCampi.find(x => x.id == idMetaCampo);

                if (metaCampo) {
                    const cmd: Configurazioni.MetaModuli.Server.modificaObbligatorietaMetaCampoViewModel = { idMetaModulo: this.model.id, idMetaGruppo: idMetaGruppo, idMetaCampo: idMetaCampo, obbligatorio: metaCampo.obbligatorio };
                    try {
                        await utilities.postJson(this.model.urlModificaObbligatorietaMetaCampo, cmd);
                    } catch (e) {
                        utilities.alertError(e);
                    }
                }
            }
        }
        //#endregion

        //#region utenti attvi
        public aggiornaAggiuntoUtenteAttivo = async (utente: Configurazioni.MetaModuli.Server.utenteViewModel) => {
            if (utente.email != this.model.emailUtenteCorrente) {
                var utenteAttivo = this.model.utentiAttivi.find(x => x.email == utente.email);

                if (utenteAttivo == null) {
                    utenteAttivo = utente;
                    this.model.utentiAttivi.push(utente);
                }

                utenteAttivo.idMetaGruppo = utente.idMetaGruppo;
                utenteAttivo.idMetaCampo = utente.idMetaCampo;
            }
        }

        public aggiornaRimossoUtenteAttivo = async (email: string) => {
            var utenteIndex = this.model.utentiAttivi.findIndex(x => x.email == email);

            if (utenteIndex > -1)
                this.model.utentiAttivi.splice(utenteIndex, 1);
        }

        public aggiungiUtenteInCollaborazione = async (idMetaGruppo: any, idMetaCampo: any) => {
            const cmd: Configurazioni.MetaModuli.Server.aggiungiUtenteInCollaborazioneViewModel = { idMetaModulo: this.model.id, idMetaGruppo: idMetaGruppo, idMetaCampo: idMetaCampo, email: this.model.emailUtenteCorrente };
            try {
                await utilities.postJson(this.model.urlAggiungiUtenteInCollaborazione, cmd);
            } catch (e) {
                utilities.alertError(e);
            }
        }

        public rimuoviUtenteInCollaborazione = async () => {
            const cmd: Configurazioni.MetaModuli.Server.rimuoviUtenteInCollaborazioneViewModel = { idMetaModulo: this.model.id, email: this.model.emailUtenteCorrente };
            try {
                await utilities.postJson(this.model.urlRimuoviUtenteInCollaborazione, cmd);
            } catch (e) {
                utilities.alertError(e);
            }
        }
        //#endregion
    }
}