using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TributarioCr
{
    public class ProductorAndProductorProyecto
    {
        public Productor MiProductor { get; set; }

        public ProductorProyecto MiProductorProyecto { get; set; }

        public class Productor
        {
            public int IdProyecto { get; set; }
            public string Nombre { get; set; }
        }

        public class ProductorProyecto
        {
            public int IdProductorProyecto { get; set; }
            public int IdProductor { get; set; }
            public int IdProyecto { get; set; }
            public byte EstatusIntegracion { get; set; }
            public DateTime FechaIntegracion { get; set; }
            public DateTime FechaSalida { get; set; }
            public string RazonSalida { get; set; }
        }

        public ProductorAndProductorProyecto MiModeloSeteado()
        {
            ProductorAndProductorProyecto ProductorSet = new ProductorAndProductorProyecto()
            {
                MiProductor = new ProductorAndProductorProyecto.Productor()
                {
                    IdProyecto = 1,
                    Nombre = "Eleazar",
                },

                MiProductorProyecto = new ProductorAndProductorProyecto.ProductorProyecto()
                {
                    IdProductorProyecto = 1,
                    IdProductor = 2,
                    IdProyecto = 3,
                    EstatusIntegracion = 0,
                    FechaIntegracion = DateTime.Now,
                    FechaSalida = DateTime.Now,
                    RazonSalida = "X razon"
                }
            };
              return ProductorSet;
         }
       
    }
}
