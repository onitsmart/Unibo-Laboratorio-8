using System;
using System.Collections.Generic;
using System.Linq;

namespace Laboratorio8.Moduli
{
    public partial class MetaModulo
    {
        private int GeneraProssimoIdPerGruppo()
        {
            var ids = this.MetaGruppi.Select(x => x.Id);

            if (ids.Any() == false)
                return 1;

            return ids.Max() + 1;
        }

        private int GeneraProssimoIdPerCampo(IEnumerable<int> idsMetacampi)
        {
            if (idsMetacampi.Any() == false)
                return 1;

            return idsMetacampi.Max() + 1;
        }

        public void SetTitolo(string titolo, ModuliDbContext context)
        {
            if (this.Titolo != titolo)
            {
                this.Titolo = titolo;
                this.DataModifica = DateTime.Now;

                context.Raise(new MetaModuloModificatoTitolo
                {
                    IdMetaModulo = this.Id
                });
            }
        }

        public void SetDescrizione(string descrizione, ModuliDbContext context)
        {
            if (this.Descrizione != descrizione)
            {
                this.Descrizione = descrizione;
                this.DataModifica = DateTime.Now;

                context.Raise(new MetaModuloModificataDescrizione
                {
                    IdMetaModulo = this.Id
                });
            }
        }

        public void AggiungiMetaGruppo(ModuliDbContext context)
        {
            var metaGruppi = this.MetaGruppi;
            var id = GeneraProssimoIdPerGruppo();

            var nuovoMetaGruppo = new MetaGruppo { Id = id, Ordine = id };
            metaGruppi.Add(nuovoMetaGruppo);

            this.MetaGruppi = metaGruppi;
            this.DataModifica = DateTime.Now;

            context.Raise(new MetaModuloAggiuntoMetaGruppo
            {
                IdMetaModulo = this.Id,
                IdMetaGruppo = nuovoMetaGruppo.Id
            });
        }

        public void EliminaMetaGruppo(int idMetaGruppo, ModuliDbContext context)
        {
            var metaGruppi = this.MetaGruppi;

            var metaGruppo = metaGruppi.Where(x => x.Id == idMetaGruppo).FirstOrDefault();
            if (metaGruppo != null)
            {
                metaGruppi.Remove(metaGruppo);

                this.MetaGruppi = metaGruppi;
                this.DataModifica = DateTime.Now;

                context.Raise(new MetaModuloEliminatoMetaGruppo
                {
                    IdMetaModulo = this.Id,
                    IdMetaGruppo = metaGruppo.Id
                });
            }
        }

        public void SetEtichettaMetaGruppo(int idMetaGruppo, string etichetta, ModuliDbContext context)
        {
            var metaGruppi = this.MetaGruppi;

            var metaGruppo = metaGruppi.Where(x => x.Id == idMetaGruppo).FirstOrDefault();
            if (metaGruppo != null)
            {
                if (metaGruppo.Etichetta != etichetta)
                {
                    metaGruppo.Etichetta = etichetta;

                    this.MetaGruppi = metaGruppi;
                    this.DataModifica = DateTime.Now;

                    context.Raise(new MetaGruppoModificataEtichetta
                    {
                        IdMetaModulo = this.Id,
                        IdMetaGruppo = idMetaGruppo
                    });
                }
            }
            else
            {
                throw new ServicesExceptions("Impossibile modificare il gruppo");
            }
        }

        public void AggiungiMetaCampoInMetaGruppo(int idMetaGruppo, ModuliDbContext context)
        {
            var metaGruppi = this.MetaGruppi;

            var metaGruppo = metaGruppi.Where(x => x.Id == idMetaGruppo).FirstOrDefault();
            if (metaGruppo != null)
            {
                var metaCampi = metaGruppo.MetaCampi;

                var id = GeneraProssimoIdPerCampo(metaGruppo.MetaCampi.Select(x => x.Id));
                var nuovoMetaCampo = new MetaCampo
                {
                    Id = id,
                    Ordine = id,
                    Tipo = TipologiaMetaCampo.TestoBreve,
                    Dimensione = DimensioneCampo.SingolaColonna,
                    Obbligatorio = false
                };

                metaCampi.Add(nuovoMetaCampo);

                metaGruppo.MetaCampi = metaCampi;

                this.MetaGruppi = metaGruppi;
                this.DataModifica = DateTime.Now;

                context.Raise(new MetaGruppoAggiuntoMetaCampo
                {
                    IdMetaModulo = this.Id,
                    IdMetaGruppo = idMetaGruppo,
                    IdMetaCampo = nuovoMetaCampo.Id
                });
            }
        }

        public void EliminaMetaCampoInMetaGruppo(int idMetaGruppo, int idMetaCampo, ModuliDbContext context)
        {
            var metaGruppi = this.MetaGruppi;

            var metaGruppo = metaGruppi.Where(x => x.Id == idMetaGruppo).FirstOrDefault();
            if (metaGruppo != null)
            {
                var metaCampi = metaGruppo.MetaCampi;

                var metaCampo = metaCampi.Where(x => x.Id == idMetaCampo).FirstOrDefault();
                if (metaCampo != null)
                {
                    metaCampi.Remove(metaCampo);

                    metaGruppo.MetaCampi = metaCampi;
                    this.MetaGruppi = metaGruppi;
                    this.DataModifica = DateTime.Now;

                    context.Raise(new MetaGruppoEliminatoMetaCampo
                    {
                        IdMetaModulo = this.Id,
                        IdMetaGruppo = metaGruppo.Id,
                        IdMetaCampo = metaCampo.Id
                    });
                }
            }
        }

        public void SetEtichettaMetaCampo(int idMetaGruppo, int idMetaCampo, string etichetta, ModuliDbContext context)
        {
            var metaGruppi = this.MetaGruppi;

            var metaGruppo = metaGruppi.Where(x => x.Id == idMetaGruppo).FirstOrDefault();
            if (metaGruppo != null)
            {
                var metaCampi = metaGruppo.MetaCampi;

                var metaCampo = metaCampi.Where(x => x.Id == idMetaCampo).FirstOrDefault();
                if (metaCampo != null)
                {
                    if (metaCampo.Etichetta != etichetta)
                    {
                        metaCampo.Etichetta = etichetta;

                        metaGruppo.MetaCampi = metaCampi;
                        this.MetaGruppi = metaGruppi;
                        this.DataModifica = DateTime.Now;

                        context.Raise(new MetaCampoModificataEtichetta
                        {
                            IdMetaModulo = this.Id,
                            IdMetaGruppo = idMetaGruppo,
                            IdMetaCampo = idMetaCampo
                        });
                    }
                }
                else
                {
                    throw new ServicesExceptions("Impossibile modificare il campo");
                }
            }
            else
            {
                throw new ServicesExceptions("Impossibile modificare il gruppo");
            }
        }

        public void SetTipologiaMetaCampo(int idMetaGruppo, int idMetaCampo, TipologiaMetaCampo tipologia, ModuliDbContext context)
        {
            var metaGruppi = this.MetaGruppi;

            var metaGruppo = metaGruppi.Where(x => x.Id == idMetaGruppo).FirstOrDefault();
            if (metaGruppo != null)
            {
                var metaCampi = metaGruppo.MetaCampi;

                var metaCampo = metaCampi.Where(x => x.Id == idMetaCampo).FirstOrDefault();
                if (metaCampo != null)
                {
                    if (metaCampo.Tipo != tipologia)
                    {
                        metaCampo.Tipo = tipologia;

                        metaGruppo.MetaCampi = metaCampi;
                        this.MetaGruppi = metaGruppi;
                        this.DataModifica = DateTime.Now;

                        context.Raise(new MetaCampoModificataTipologia
                        {
                            IdMetaModulo = this.Id,
                            IdMetaGruppo = idMetaGruppo,
                            IdMetaCampo = idMetaCampo
                        });
                    }
                }
                else
                {
                    throw new ServicesExceptions("Impossibile modificare il campo");
                }
            }
            else
            {
                throw new ServicesExceptions("Impossibile modificare il gruppo");
            }
        }

        public void SetDimensioneMetaCampo(int idMetaGruppo, int idMetaCampo, DimensioneCampo dimensione, ModuliDbContext context)
        {
            var metaGruppi = this.MetaGruppi;

            var metaGruppo = metaGruppi.Where(x => x.Id == idMetaGruppo).FirstOrDefault();
            if (metaGruppo != null)
            {
                var metaCampi = metaGruppo.MetaCampi;

                var metaCampo = metaCampi.Where(x => x.Id == idMetaCampo).FirstOrDefault();
                if (metaCampo != null)
                {
                    if (metaCampo.Dimensione != dimensione)
                    {
                        metaCampo.Dimensione = dimensione;

                        metaGruppo.MetaCampi = metaCampi;
                        this.MetaGruppi = metaGruppi;
                        this.DataModifica = DateTime.Now;

                        context.Raise(new MetaCampoModificataDimensione
                        {
                            IdMetaModulo = this.Id,
                            IdMetaGruppo = idMetaGruppo,
                            IdMetaCampo = idMetaCampo
                        });
                    }
                }
                else
                {
                    throw new ServicesExceptions("Impossibile modificare il campo");
                }
            }
            else
            {
                throw new ServicesExceptions("Impossibile modificare il gruppo");
            }
        }

        public void SetObbligatorietaMetaCampo(int idMetaGruppo, int idMetaCampo, bool obbligatorio, ModuliDbContext context)
        {
            var metaGruppi = this.MetaGruppi;

            var metaGruppo = metaGruppi.Where(x => x.Id == idMetaGruppo).FirstOrDefault();
            if (metaGruppo != null)
            {
                var metaCampi = metaGruppo.MetaCampi;

                var metaCampo = metaCampi.Where(x => x.Id == idMetaCampo).FirstOrDefault();
                if (metaCampo != null)
                {
                    if (metaCampo.Obbligatorio != obbligatorio)
                    {
                        metaCampo.Obbligatorio = obbligatorio;

                        metaGruppo.MetaCampi = metaCampi;
                        this.MetaGruppi = metaGruppi;
                        this.DataModifica = DateTime.Now;

                        context.Raise(new MetaCampoModificataObbligatorieta
                        {
                            IdMetaModulo = this.Id,
                            IdMetaGruppo = idMetaGruppo,
                            IdMetaCampo = idMetaCampo
                        });
                    }
                }
                else
                {
                    throw new ServicesExceptions("Impossibile modificare il campo");
                }
            }
            else
            {
                throw new ServicesExceptions("Impossibile modificare il gruppo");
            }
        }
    }
}
