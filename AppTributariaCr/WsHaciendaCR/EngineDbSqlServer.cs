using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TributarioCr;

namespace WsHaciendaCR
{
    public class EngineDbSqlServer
    {
        private string cadenaConexion = ConfigurationManager.ConnectionStrings[EngineData.nombreCadenaConexion].ToString();

        public int InsertarRespuestaDocEnviado(TributarioCr.ConsumidorApi.Validacion respuesta)
        {
            SqlConnection Conexion = new SqlConnection(cadenaConexion);
            int resultado = new int();
            try
            {
                using (Conexion)
                {
                    Conexion.Open();
                    SqlCommand command = new SqlCommand("Sp_InsertarRespuestaDocEnviado", Conexion);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CLAVE", respuesta.Clave);
                    command.Parameters.AddWithValue("@FECHA", respuesta.Fecha);
                    command.Parameters.AddWithValue("@INDESTADO", respuesta.IndEstado);
                    command.Parameters.AddWithValue("@RESPUESTAXML", respuesta.RespuestaXml);
                    resultado = command.ExecuteNonQuery();
                    Conexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en metodo InsertarRespuestaDocEnviado", ex);
            }
            return resultado;
        }

        public int InsertarRespuestaDocEnviadoReceptor(TributarioCr.ConsumidorApi.Validacion respuesta)
        {
            SqlConnection Conexion = new SqlConnection(cadenaConexion);
            int resultado = new int();
            try
            {
                using (Conexion)
                {
                    Conexion.Open();
                    SqlCommand command = new SqlCommand("Sp_InsertarRespuestaDocEnviadoReceptor", Conexion);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CLAVE", respuesta.Clave);
                    command.Parameters.AddWithValue("@FECHA", respuesta.Fecha);
                    command.Parameters.AddWithValue("@INDESTADO", respuesta.IndEstado);
                    command.Parameters.AddWithValue("@RESPUESTAXML", respuesta.RespuestaXml);
                    resultado = command.ExecuteNonQuery();
                    Conexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en metodo InsertarRespuestaDocEnviadoReceptor", ex);
            }
            return resultado;
        }
    }
}