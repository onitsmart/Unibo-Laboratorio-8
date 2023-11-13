var Configurazioni;
(function (Configurazioni) {
    var MetaModuli;
    (function (MetaModuli) {
        class editVueModel {
            constructor(hub, model) {
                this.hub = hub;
                this.model = model;
                //#region titolo meta modulo
                this.aggiornaTitoloMetaModulo = async (titolo) => {
                    this.model.titolo = titolo;
                };
                this.modificaTitoloMetaModulo = async () => {
                    const cmd = { idMetaModulo: this.model.id, titolo: this.model.titolo };
                    try {
                        await utilities.postJson(this.model.urlModificaTitoloMetaModulo, cmd);
                    }
                    catch (e) {
                        utilities.alertError(e);
                    }
                };
                //#endregion
                //#region descrizione meta modulo
                this.aggiornaDescrizioneMetaModulo = async (descrizione) => {
                    this.model.descrizione = descrizione;
                };
                this.modificaDescrizioneMetaModulo = async () => {
                    const cmd = { idMetaModulo: this.model.id, descrizione: this.model.descrizione };
                    try {
                        await utilities.postJson(this.model.urlModificaDescrizioneMetaModulo, cmd);
                    }
                    catch (e) {
                        utilities.alertError(e);
                    }
                };
                //#endregion
                //#region meta gruppi
                this.aggiornaAggiuntoMetaGruppo = async (metaGruppo) => {
                    this.model.metaGruppi.push(metaGruppo);
                };
                this.aggiungiMetaGruppo = async () => {
                    const cmd = { idMetaModulo: this.model.id };
                    try {
                        await utilities.postJson(this.model.urlAggiungiMetaGruppoInMetaModulo, cmd);
                    }
                    catch (e) {
                        utilities.alertError(e);
                    }
                };
                this.aggiornaEliminatoMetaGruppo = async (idMetaGruppo) => {
                    var indexMetaGruppo = this.model.metaGruppi.findIndex(x => x.id == idMetaGruppo);
                    if (indexMetaGruppo > -1)
                        this.model.metaGruppi.splice(indexMetaGruppo, 1);
                };
                this.eliminaMetaGruppo = async (idMetaGruppo) => {
                    const cmd = { idMetaModulo: this.model.id, idMetaGruppo: idMetaGruppo };
                    try {
                        await utilities.postJson(this.model.urlEliminaMetaGruppoInMetaModulo, cmd);
                    }
                    catch (e) {
                        utilities.alertError(e);
                    }
                };
                //#endregion
                //#region etichetta meta gruppo
                this.aggiornaEtichettaMetaGruppo = async (idMetaGruppo, etichetta) => {
                    var metaGruppo = this.model.metaGruppi.find(x => x.id == idMetaGruppo);
                    if (metaGruppo)
                        metaGruppo.etichetta = etichetta;
                };
                this.modificaEtichettaMetaGruppo = async (idMetaGruppo) => {
                    let metaGruppo = this.model.metaGruppi.find(x => x.id == idMetaGruppo);
                    if (metaGruppo) {
                        const cmd = { idMetaModulo: this.model.id, idMetaGruppo: idMetaGruppo, etichetta: metaGruppo.etichetta };
                        try {
                            await utilities.postJson(this.model.urlModificaEtichettaMetaGruppo, cmd);
                        }
                        catch (e) {
                            utilities.alertError(e);
                        }
                    }
                };
                //#endregion
                //#region meta campi
                this.aggiornaAggiuntoMetaCampo = async (idMetaGruppo, metaCampo) => {
                    var metaGruppo = this.model.metaGruppi.find(x => x.id == idMetaGruppo);
                    if (metaGruppo)
                        metaGruppo.metaCampi.push(metaCampo);
                };
                this.aggiungiMetaCampo = async (idMetaGruppo) => {
                    const cmd = { idMetaModulo: this.model.id, idMetaGruppo: idMetaGruppo };
                    try {
                        await utilities.postJson(this.model.urlAggiungiMetaCampoInMetaGruppo, cmd);
                    }
                    catch (e) {
                        utilities.alertError(e);
                    }
                };
                this.aggiornaEliminatoMetaCampo = async (idMetaGruppo, idMetaCampo) => {
                    var metaGruppo = this.model.metaGruppi.find(x => x.id == idMetaGruppo);
                    if (metaGruppo) {
                        var indexMetaCampo = metaGruppo.metaCampi.findIndex(x => x.id == idMetaCampo);
                        if (indexMetaCampo > -1)
                            metaGruppo.metaCampi.splice(indexMetaCampo, 1);
                    }
                };
                this.eliminaMetaCampo = async (idMetaGruppo, idMetaCampo) => {
                    const cmd = { idMetaModulo: this.model.id, idMetaGruppo: idMetaGruppo, idMetaCampo: idMetaCampo };
                    try {
                        await utilities.postJson(this.model.urlEliminaMetaCampoInMetaGruppo, cmd);
                    }
                    catch (e) {
                        utilities.alertError(e);
                    }
                };
                //#endregion
                //#region etichetta meta campo
                this.aggiornaEtichettaMetaCampo = async (idMetaGruppo, idMetaCampo, etichetta) => {
                    var metaGruppo = this.model.metaGruppi.find(x => x.id == idMetaGruppo);
                    if (metaGruppo) {
                        var metaCampo = metaGruppo.metaCampi.find(x => x.id == idMetaCampo);
                        if (metaCampo)
                            metaCampo.etichetta = etichetta;
                    }
                };
                this.modificaEtichettaMetaCampo = async (idMetaGruppo, idMetaCampo) => {
                    let metaGruppo = this.model.metaGruppi.find(x => x.id == idMetaGruppo);
                    if (metaGruppo) {
                        let metaCampo = metaGruppo.metaCampi.find(x => x.id == idMetaCampo);
                        if (metaCampo) {
                            const cmd = { idMetaModulo: this.model.id, idMetaGruppo: idMetaGruppo, idMetaCampo: idMetaCampo, etichetta: metaCampo.etichetta };
                            try {
                                await utilities.postJson(this.model.urlModificaEtichettaMetaCampo, cmd);
                            }
                            catch (e) {
                                utilities.alertError(e);
                            }
                        }
                    }
                };
                //#endregion
                //#region tipologia meta campo
                this.aggiornaTipologiaMetaCampo = async (idMetaGruppo, idMetaCampo, tipologia) => {
                    var metaGruppo = this.model.metaGruppi.find(x => x.id == idMetaGruppo);
                    if (metaGruppo) {
                        var metaCampo = metaGruppo.metaCampi.find(x => x.id == idMetaCampo);
                        if (metaCampo)
                            metaCampo.tipo = tipologia;
                    }
                };
                this.modificaTipologiaMetaCampo = async (idMetaGruppo, idMetaCampo) => {
                    let metaGruppo = this.model.metaGruppi.find(x => x.id == idMetaGruppo);
                    if (metaGruppo) {
                        let metaCampo = metaGruppo.metaCampi.find(x => x.id == idMetaCampo);
                        if (metaCampo) {
                            const cmd = { idMetaModulo: this.model.id, idMetaGruppo: idMetaGruppo, idMetaCampo: idMetaCampo, tipologia: metaCampo.tipo };
                            try {
                                await utilities.postJson(this.model.urlModificaTipologiaMetaCampo, cmd);
                            }
                            catch (e) {
                                utilities.alertError(e);
                            }
                        }
                    }
                };
                //#endregion
                //#region dimensione meta campo
                this.aggiornaDimensioneMetaCampo = async (idMetaGruppo, idMetaCampo, dimensione) => {
                    var metaGruppo = this.model.metaGruppi.find(x => x.id == idMetaGruppo);
                    if (metaGruppo) {
                        var metaCampo = metaGruppo.metaCampi.find(x => x.id == idMetaCampo);
                        if (metaCampo)
                            metaCampo.dimensione = dimensione;
                    }
                };
                this.modificaDimensioneMetaCampo = async (idMetaGruppo, idMetaCampo) => {
                    let metaGruppo = this.model.metaGruppi.find(x => x.id == idMetaGruppo);
                    if (metaGruppo) {
                        let metaCampo = metaGruppo.metaCampi.find(x => x.id == idMetaCampo);
                        if (metaCampo) {
                            const cmd = { idMetaModulo: this.model.id, idMetaGruppo: idMetaGruppo, idMetaCampo: idMetaCampo, dimensione: metaCampo.dimensione };
                            try {
                                await utilities.postJson(this.model.urlModificaDimensioneMetaCampo, cmd);
                            }
                            catch (e) {
                                utilities.alertError(e);
                            }
                        }
                    }
                };
                //#endregion
                //#region obbligatorieta meta campo
                this.aggiornaObbligatorietaMetaCampo = async (idMetaGruppo, idMetaCampo, obbligatorio) => {
                    var metaGruppo = this.model.metaGruppi.find(x => x.id == idMetaGruppo);
                    if (metaGruppo) {
                        var metaCampo = metaGruppo.metaCampi.find(x => x.id == idMetaCampo);
                        if (metaCampo)
                            metaCampo.obbligatorio = obbligatorio;
                    }
                };
                this.modificaObbligatorietaMetaCampo = async (idMetaGruppo, idMetaCampo) => {
                    let metaGruppo = this.model.metaGruppi.find(x => x.id == idMetaGruppo);
                    if (metaGruppo) {
                        let metaCampo = metaGruppo.metaCampi.find(x => x.id == idMetaCampo);
                        if (metaCampo) {
                            const cmd = { idMetaModulo: this.model.id, idMetaGruppo: idMetaGruppo, idMetaCampo: idMetaCampo, obbligatorio: metaCampo.obbligatorio };
                            try {
                                await utilities.postJson(this.model.urlModificaObbligatorietaMetaCampo, cmd);
                            }
                            catch (e) {
                                utilities.alertError(e);
                            }
                        }
                    }
                };
                //#endregion
                //#region utenti attvi
                this.aggiornaAggiuntoUtenteAttivo = async (utente) => {
                    if (utente.email != this.model.emailUtenteCorrente) {
                        var utenteAttivo = this.model.utentiAttivi.find(x => x.email == utente.email);
                        if (utenteAttivo == null) {
                            utenteAttivo = utente;
                            this.model.utentiAttivi.push(utente);
                        }
                        utenteAttivo.idMetaGruppo = utente.idMetaGruppo;
                        utenteAttivo.idMetaCampo = utente.idMetaCampo;
                    }
                };
                this.aggiornaRimossoUtenteAttivo = async (email) => {
                    var utenteIndex = this.model.utentiAttivi.findIndex(x => x.email == email);
                    if (utenteIndex > -1)
                        this.model.utentiAttivi.splice(utenteIndex, 1);
                };
                this.aggiungiUtenteInCollaborazione = async (idMetaGruppo, idMetaCampo) => {
                    const cmd = { idMetaModulo: this.model.id, idMetaGruppo: idMetaGruppo, idMetaCampo: idMetaCampo, email: this.model.emailUtenteCorrente };
                    try {
                        await utilities.postJson(this.model.urlAggiungiUtenteInCollaborazione, cmd);
                    }
                    catch (e) {
                        utilities.alertError(e);
                    }
                };
                this.rimuoviUtenteInCollaborazione = async () => {
                    const cmd = { idMetaModulo: this.model.id, email: this.model.emailUtenteCorrente };
                    try {
                        await utilities.postJson(this.model.urlRimuoviUtenteInCollaborazione, cmd);
                    }
                    catch (e) {
                        utilities.alertError(e);
                    }
                };
                if (this.hub) {
                    this.hub.on("MetaModuloModificatoTitolo", async () => {
                        var url = utilities.composeRelativeUrlWithParams(this.model.urlCercaAggiornamentoTitoloMetaModulo, "idMetaModulo", this.model.id);
                        const dtoAggiornamento = await utilities.getJsonT(url);
                        this.aggiornaTitoloMetaModulo(dtoAggiornamento);
                    });
                    this.hub.on("MetaModuloModificataDescrizione", async () => {
                        var url = utilities.composeRelativeUrlWithParams(this.model.urlCercaAggiornamentoDescrizioneMetaModulo, "idMetaModulo", this.model.id);
                        const dtoAggiornamento = await utilities.getJsonT(url);
                        this.aggiornaDescrizioneMetaModulo(dtoAggiornamento);
                    });
                    this.hub.on("MetaModuloAggiuntoMetaGruppo", async (idMetaGruppo) => {
                        var url = utilities.composeRelativeUrlWithParams(this.model.urlCercaAggiornamentoMetaGruppoInMetaModulo, "idMetaModulo", this.model.id, "idMetaGruppo", idMetaGruppo);
                        const dtoAggiornamento = await utilities.getJsonT(url);
                        this.aggiornaAggiuntoMetaGruppo(dtoAggiornamento);
                    });
                    this.hub.on("MetaModuloEliminatoMetaGruppo", async (idMetaGruppo) => {
                        this.aggiornaEliminatoMetaGruppo(idMetaGruppo);
                    });
                    this.hub.on("MetaGruppoModificataEtichetta", async (idMetaGruppo) => {
                        var url = utilities.composeRelativeUrlWithParams(this.model.urlCercaAggiornamentoEtichettaMetaGruppo, "idMetaModulo", this.model.id, "idMetaGruppo", idMetaGruppo);
                        const dtoAggiornamento = await utilities.getJsonT(url);
                        this.aggiornaEtichettaMetaGruppo(idMetaGruppo, dtoAggiornamento);
                    });
                    this.hub.on("MetaGruppoAggiuntoMetaCampo", async (idMetaGruppo, idMetaCampo) => {
                        var url = utilities.composeRelativeUrlWithParams(this.model.urlCercaAggiornamentoMetaCampoInMetaGruppo, "idMetaModulo", this.model.id, "idMetaGruppo", idMetaGruppo, "idMetaCampo", idMetaCampo);
                        const dtoAggiornamento = await utilities.getJsonT(url);
                        this.aggiornaAggiuntoMetaCampo(idMetaGruppo, dtoAggiornamento);
                    });
                    this.hub.on("MetaGruppoEliminatoMetaCampo", async (idMetaGruppo, idMetaCampo) => {
                        this.aggiornaEliminatoMetaCampo(idMetaGruppo, idMetaCampo);
                    });
                    this.hub.on("MetaCampoModificataEtichetta", async (idMetaGruppo, idMetaCampo) => {
                        var url = utilities.composeRelativeUrlWithParams(this.model.urlCercaAggiornamentoEtichettaMetaCampo, "idMetaModulo", this.model.id, "idMetaGruppo", idMetaGruppo, "idMetaCampo", idMetaCampo);
                        const dtoAggiornamento = await utilities.getJsonT(url);
                        this.aggiornaEtichettaMetaCampo(idMetaGruppo, idMetaCampo, dtoAggiornamento);
                    });
                    this.hub.on("MetaCampoModificataTipologia", async (idMetaGruppo, idMetaCampo) => {
                        var url = utilities.composeRelativeUrlWithParams(this.model.urlCercaAggiornamentoTipologiaMetaCampo, "idMetaModulo", this.model.id, "idMetaGruppo", idMetaGruppo, "idMetaCampo", idMetaCampo);
                        const dtoAggiornamento = await utilities.getJsonT(url);
                        this.aggiornaTipologiaMetaCampo(idMetaGruppo, idMetaCampo, dtoAggiornamento);
                    });
                    this.hub.on("MetaCampoModificataDimensione", async (idMetaGruppo, idMetaCampo) => {
                        var url = utilities.composeRelativeUrlWithParams(this.model.urlCercaAggiornamentoDimensioneMetaCampo, "idMetaModulo", this.model.id, "idMetaGruppo", idMetaGruppo, "idMetaCampo", idMetaCampo);
                        const dtoAggiornamento = await utilities.getJsonT(url);
                        this.aggiornaDimensioneMetaCampo(idMetaGruppo, idMetaCampo, dtoAggiornamento);
                    });
                    this.hub.on("MetaCampoModificataObbligatorieta", async (idMetaGruppo, idMetaCampo) => {
                        var url = utilities.composeRelativeUrlWithParams(this.model.urlCercaAggiornamentoObbligatorietaMetaCampo, "idMetaModulo", this.model.id, "idMetaGruppo", idMetaGruppo, "idMetaCampo", idMetaCampo);
                        const dtoAggiornamento = await utilities.getJsonT(url);
                        this.aggiornaObbligatorietaMetaCampo(idMetaGruppo, idMetaCampo, dtoAggiornamento);
                    });
                    this.hub.on("UtenteInCollaborazione", async (email, idMetaGruppo, idMetaCampo) => {
                        var url = utilities.composeRelativeUrlWithParams(this.model.urlGeneraGravatarUrl, "email", email);
                        const gravatarUrl = await utilities.getJsonT(url);
                        this.aggiornaAggiuntoUtenteAttivo({
                            email: email,
                            gravatarUrl: gravatarUrl,
                            idMetaGruppo: idMetaGruppo,
                            idMetaCampo: idMetaCampo
                        });
                    });
                    this.hub.on("UtenteAbbandonataCollaborazione", async (email) => {
                        this.aggiornaRimossoUtenteAttivo(email);
                    });
                }
            }
        }
        MetaModuli.editVueModel = editVueModel;
    })(MetaModuli = Configurazioni.MetaModuli || (Configurazioni.MetaModuli = {}));
})(Configurazioni || (Configurazioni = {}));
//# sourceMappingURL=Edit.js.map