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


        public async Task<List<EClothes>> GetClothes()
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/ClothesPRM");
            var listado_Clothes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EClothes>>(result);

            return listado_Clothes;
        }

        public async Task<List<EPronouns>> GetPronouns()
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/PronounsPRM");
            var listado_pronouns = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EPronouns>>(result);

            return listado_pronouns;
        }

        public async Task<List<ESimplePresent>> GetSimplePresent()
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/SimplePresentPRM");
            var listado_SimplePresent = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ESimplePresent>>(result);

            return listado_SimplePresent;
        }

        public async Task<List<EPrepositionsOfTime>> GetPrepositionsOfTimeSentences()
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/PrepositionsOfTimePRM");
            var listado_PrepositionsOfTime = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EPrepositionsOfTime>>(result);

            return listado_PrepositionsOfTime;
        } // Fin del método ObtenerVerbos

        public async Task<List<EFamily>> GetFamily()
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/FamilyvocabularyPRM");
            var listado_Family = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EFamily>>(result);

            return listado_Family;
        } // Fin del método ObtenerVerbos

        public async Task<List<EAnySome>> GetAnySome()
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/AnySomePRM");
            var listado_AnySome = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EAnySome>>(result);

            return listado_AnySome;
        } // Fin del método ObtenerVerbos

    }
}
