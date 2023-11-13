//import server = Esempi.Matite.Server;
//import common = Esempi.Server;
var Home;
(function (Home) {
    class errorVueModel {
        //tipi: Esempi.Server.idDescrizioneViewModel[];
        constructor(model) {
            this.model = model;
            this.inviaSegnalazione = async () => {
                try {
                    await utilities.postJson(this.model.inviaSegnalazioneUrl, this.model);
                    utilities.alertSuccess("Operazione riuscita");
                }
                catch (e) {
                    utilities.alertError(e);
                }
            };
            //this.tipi = [];
        }
    }
    Home.errorVueModel = errorVueModel;
})(Home || (Home = {}));
//# sourceMappingURL=Error.js.map