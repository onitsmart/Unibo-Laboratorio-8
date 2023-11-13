//import server = Esempi.Matite.Server;
//import common = Esempi.Server;

module Home {
    export class errorVueModel {
        //tipi: Esempi.Server.idDescrizioneViewModel[];

        constructor(public model: Home.Server.errorViewModel) {
            //this.tipi = [];
        }

        public inviaSegnalazione = async () => {
            try {
                await utilities.postJson(this.model.inviaSegnalazioneUrl, this.model);

                utilities.alertSuccess("Operazione riuscita");
            } catch (e) {
                utilities.alertError(e);
            }
        }
    }
}