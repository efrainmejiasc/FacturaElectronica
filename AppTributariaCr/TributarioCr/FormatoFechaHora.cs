using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TributarioCr
{
    public class FormatoFechaHora
    {
        private const string dateFormat1 = "yyyy-MM-ddTHH:mm:ss+hh:mm";
        private const string dateFormat2 = "yyyy-MM-ddTHH:mm:ss.fffZ";
        private const string dateFormat3 = "dd-MM-yyyy HH:mm:ss";
        private const string dateFormat4 = "yyyy-MM-dd'T'HH:mm:ss.fffzzz";
        private const string dateFormat5 = "dd-MM-yyyy HH:mm:ss";
        private const string dateFormat6 = "{0:yyyy-MM-dd}T{0:HH:mm:ss-ffff}";


        public string GetFechaHora(DateTime fechaHora, string formatoFecha)
        {
            //fechaHora = fechaHora.AddMinutes(-118);
            string fechaHoraString = fechaHora.ToString(); ;
            if (formatoFecha == "dateFormat")
            {
                fechaHoraString = fechaHora.ToString(dateFormat4, DateTimeFormatInfo.InvariantInfo);
            }
            else if (formatoFecha == "dateFormat2")
            {
                fechaHoraString = fechaHora.ToString(dateFormat2);
            }
            else if (formatoFecha == "dateFormat1")
            {
                fechaHoraString = fechaHora.ToString(dateFormat1);
            }
            else if (formatoFecha == "dateFormat6")
            {
                fechaHoraString = string.Format(dateFormat6,fechaHora);
            }
            else
            {
                fechaHoraString = fechaHora.ToString(dateFormat3);
            }

            return fechaHoraString;
        }


        /*   public string GetFechaHora(DateTime fechaHora , string formatoFecha,string NoUsada)
           {
               int n = 118;
               int iteracion = 0;
               int hora = fechaHora.Hour;
               string fechaHoraString = string.Empty;
               string horaOut = string.Empty;
               if (formatoFecha == "dateFormat")
               {
                   fechaHoraString = fechaHora.ToString(dateFormat);
                   horaOut = fechaHoraString.Substring(11, 5);
               }
               else if (formatoFecha == "dateFormat2")
               {
                   fechaHoraString = fechaHora.ToString(dateFormat2);
                   horaOut = fechaHoraString.Substring(11, 5);
               }
               else
               {
                   fechaHoraString = fechaHora.ToString(dateFormat3);
                   horaOut = fechaHoraString.Substring(11, 4);
               }
               int minuto = fechaHora.Minute;
               while (n >= 1)
               {
                   if (iteracion == 0)
                   {
                       if (minuto == 0)
                       {
                           hora = hora - 1;
                       }
                   }
                   else if (iteracion > 0)
                   {
                       hora = hora - 1;
                   }
                   n = RestarMinutos(n, minuto);
                   minuto = 0;
               }
               if (n < 0) { n = n * -1; }
               string horaString = hora.ToString();
               string nString = n.ToString();
               if (hora < 10) { horaString = "0" + horaString; }
               if (n < 10) { nString = "0" + nString; }
               fechaHoraString = fechaHoraString.Replace(horaOut, horaString + ":" + nString);
               return fechaHoraString;
           }
           public int RestarMinutos(int n, int minuto)
           {
               if (minuto == 0)
               {
                   n = n - 60;
               }
               else if (minuto > 0)
               {
                   n = n - minuto;
               }
               return n;
           }*/

    }
}
