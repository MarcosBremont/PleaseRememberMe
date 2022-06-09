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

        public async Task<List<EVerbToBe>> GetVerbToBe()
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/VerbToBeSentencesPRM");
            var listado_verbtobe = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EVerbToBe>>(result);

            return listado_verbtobe;
        } // Fin del método ObtenerVerbos

        public async Task<List<EQuantifiers>> GetQuantifiers()
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/QuantifiersSentencePRM");
            var listado_quantifiers = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EQuantifiers>>(result);

            return listado_quantifiers;
        } // Fin del método ObtenerVerbos

        public async Task<List<EQuestionsWithHow>> GetQuestionsWithHow()
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/QuestionsWithHowPRM");
            var listado_questionswithhow = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EQuestionsWithHow>>(result);

            return listado_questionswithhow;
        } // Fin del método ObtenerVerbos

        public async Task<List<ECategories>> GetCategories()
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/Categories");
            var listado_categories = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ECategories>>(result);

            return listado_categories;
        } // Fin del método ObtenerVerbos


        public async Task<List<EVocabulary>> GetVocabulary()
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/VocabularyPRM");
            var listado_vocabulary = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EVocabulary>>(result);

            return listado_vocabulary;
        } // Fin del método Obtener Vocabularios

        public async Task<List<EVocabularyClothes>> GetVocabularyClothes()
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/VocabularyClothesPRM");
            var listado_vocabulary_Clothes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EVocabularyClothes>>(result);

            return listado_vocabulary_Clothes;
        } //

        public async Task<List<EVocabularyFamily>> GetVocabularyFamily()
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/VocabularyFamilyPRM");
            var listado_vocabulary_Family = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EVocabularyFamily>>(result);

            return listado_vocabulary_Family;
        } //

        public async Task<Result> SendReport(string report)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/SendReport/{report.ToUpper()}");
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
            return response;
        }

        public async Task<List<ESuperlativesSentence>> GetSuperlativesSentences()
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/SuperlativesPRM");
            var listado_SuperlativesSentences = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ESuperlativesSentence>>(result);

            return listado_SuperlativesSentences;
        } // Fin del método ObtenerVerbos

        public async Task<List<ESuperlativesAdjectives>> GetSuperlativesAdjectives()
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/SuperlativesAdjectivesPRM");
            var listado_superlatives_adjectives = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ESuperlativesAdjectives>>(result);

            return listado_superlatives_adjectives;
        } // Fin del método ObtenerVerbos

        public async Task<List<EVideos>> GetVideos()
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/VideosPRM");
            var List_videos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EVideos>>(result);

            return List_videos;
        } // Fin del método ObtenerVerbos

        public async Task<List<EAudios>> GetAudios()
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/AudiosPRM");
            var List_audios = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EAudios>>(result);

            return List_audios;
        } // Fin del método ObtenerVerbos

        public async Task<List<EExercises>> GetExercisesByCategory(string category)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/ExercisesPRM/{category}");
            var listado_exercises = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EExercises>>(result);

            return listado_exercises;
        } // Fin del método ObtenerTablaDePosiciones

    }
}
