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
        } // Fin del método ObtenerMenu


    }
}
