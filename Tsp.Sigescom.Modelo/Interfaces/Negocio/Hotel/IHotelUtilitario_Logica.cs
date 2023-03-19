using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Facturacion;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.ModeloExtranet;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.PlainModel;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.Report;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Modelo.Interfaces.Negocio
{
    public interface IHotelUtilitario_Logica
    {
        int ObtenerNumeroNoches(DateTime fechaInicio, DateTime fechaFin);
    }
}
