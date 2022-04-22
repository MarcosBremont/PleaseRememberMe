using Newtonsoft.Json;
using PleaseRememberMe.Entidad;
using PleaseRememberMe.Utilitarios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PleaseRememberMe.Models
{
    public class Metodos
    {
        Herramientas herramientas = new Herramientas();

        public Metodos()
        {
            // constructor
        }

        public async Task<List<EVerbos>> GetListadoVerbos()
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/ObtenerVerbos");
            var listado_verbos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EVerbos>>(result);

            return listado_verbos;
        } // Fin del método ObtenerVerbos
         
        public async Task<List<EPosiciones>> GetListadoDePosiciones()
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/Posiciones");
            var listado_Posiciones = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EPosiciones>>(result);

            return listado_Posiciones;
        } // Fin del método ObtenerTablaDePosiciones



        public async Task<Result> EnterToTheTournament(string nombrePersona, int numeroVerbosCorrectos, string direccion)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/EnterToTheTournament/{nombrePersona.ToUpper()}/{numeroVerbosCorrectos}/{direccion.ToUpper()}");
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
            return response;
        }

        public async Task<Result> SendEmails(string email)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/SendEmails/{email.ToUpper()}");
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
            return response;
        }

        public async Task<List<EWasWereDid>> GetWasWereSentences()
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/WasWereSentencesprm");
            var listado_sentenceswasweredid = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EWasWereDid>>(result);

            return listado_sentenceswasweredid;
        } // Fin del método ObtenerVerbos

        public async Task<List<EMatchSentences>> GetMatchSentences()
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/MatchSentencesPRM");
            var listado_matchSentences = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EMatchSentences>>(result);

            return listado_matchSentences;
        } // Fin del método ObtenerVerbos


        public async Task<List<ECompleteSentences>> GetCompleteSentences()
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/CompleteSentencesPRM");
            var listado_completeSentences = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ECompleteSentences>>(result);

            return listado_completeSentences;
        } // Fin del método ObtenerVerbos

        public async Task<List<EAdjectives>> GetAdjectives()
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/AdjectivesPRM");
            var listado_adjectives = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EAdjectives>>(result);

            return listado_adjectives;
        } // Fin del método ObtenerVerbos


    }
}
