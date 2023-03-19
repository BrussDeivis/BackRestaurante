using System;
using System.Collections.Generic;
using Tsp.Sigescom.Config;
using System.Linq;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Custom;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico
{

    public class CajaInicializar
    {
        public int Id { get; set; }
        public string Establecimiento { get; set; }
        public string Caja { get; set; }
        public decimal Monto { get; set; }

        public CajaInicializar()
        {

        }

        public static List<CajaInicializar> Convert(List<CentroDeAtencion> cajas)
        {
            List<CajaInicializar> cajasInicializar = new List<CajaInicializar>();
            foreach (var caja in cajas)
            {
                cajasInicializar.Add(new CajaInicializar()
                {
                    Id = caja.Id,
                    Establecimiento = caja.EstablecimientoComercial.NombreComercial,
                    Caja = caja.Nombre,
                    Monto = 0
                });
            }
            return cajasInicializar;
        }
    }
}
