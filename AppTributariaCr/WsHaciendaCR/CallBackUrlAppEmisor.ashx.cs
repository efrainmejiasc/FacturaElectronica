using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using TributarioCr;

namespace WsHaciendaCR
{
    /// <summary>
    /// Descripción breve de CallBackUrlApp
    /// </summary>
    public class CallBackUrlApp : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string cadena = string.Empty;
            if (context.Request.RequestType.Equals(EngineData.post))
            {
                var stream = new StreamReader(HttpContext.Current.Request.InputStream);
                stream.BaseStream.Seek(0, SeekOrigin.Begin);
                cadena = stream.ReadToEnd();
                this.CallBackUrl(cadena);
            }
        }
        private void CallBackUrl(string jsonValidacion)
        {
            TributarioCr.ConsumidorApi.Validacion validacion = new TributarioCr.ConsumidorApi.Validacion();
            jsonValidacion = jsonValidacion.Replace("-", "");
            validacion = JsonConvert.DeserializeObject<TributarioCr.ConsumidorApi.Validacion>(jsonValidacion);
            EngineDbSqlServer MetodoDb = new EngineDbSqlServer();
            int resultado = MetodoDb.InsertarRespuestaDocEnviado(validacion);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}