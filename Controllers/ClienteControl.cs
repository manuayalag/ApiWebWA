//ClienteControl.cs
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;
using WebApiWA.Models;

namespace ApiWebWA.Controllers
{
    public class ClienteControl : ControllerBase
    {
        //RECIBIMOS LOS DATOS DE VALIDACION VIA GET
        [HttpGet]
        //DENTRO DE LA RUTA webhook
        [Route("webhook")]
        //RECIBIMOS LOS PARAMETROS QUE NOS ENVIA WHATSAPP PARA VALIDAR NUESTRA URL
        public string Webhook(
            [FromQuery(Name = "hub.mode")] string mode,
            [FromQuery(Name = "hub.challenge")] string challenge,
            [FromQuery(Name = "hub.verify_token")] string verify_token
        )
        {
            //SI EL TOKEN ES hola (O EL QUE COLOQUEMOS EN FACEBOOK)
            if (verify_token.Equals("qaz123wsx"))
            {
                return challenge;
            }
            else
            {
                return "";
            }
        }
        //RECIBIMOS LOS DATOS DE VIA POST
        [HttpPost]
        //DENTRO DE LA RUTA webhook
        [Route("webhook")]
        //RECIBIMOS LOS DATOS Y LOS GUARDAMOS EN EL MODELO WebHookResponseModel
        public dynamic datos([FromBody] WebHookResponseModel entry)
        {
            //ESTRAEMOS EL MENSAJE RECIBIDO
            string mensaje_recibido = entry.entry[0].changes[0].value.messages[0].text.body;
            //ESTRAEMOS EL ID UNICO DEL MENSAJE
            string id_wa = entry.entry[0].changes[0].value.messages[0].id;
            //ESTRAEMOS EL NUMERO DE TELEFONO DEL CUAL RECIBIMOS EL MENSAJE
            string telefono_wa = entry.entry[0].changes[0].value.messages[0].from;
            //INICIALIZAMOS LA CONEXION A LA BD
            Datos dat = new Datos();
            //INSERTAMOS LOS DATOS RECIBIDOS
            dat.insertar(mensaje_recibido, id_wa, telefono_wa);
            //SI NO HAY ERROR RETORNAMOS UN OK
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            return response;


        }

    }
}