using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Facturacion;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;

using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.ModeloExtranet;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Modelo.Negocio.Almacen

{
    public interface IOrdenAlmacen_Logica
    {
        PrincipalOrdenAlmacenData ObtenerDatosParaOrdenAlmacenPrincipal(UserProfileSessionData profileData);
        List<OrdenAlmacenResumen> ObtenerOrdenesAlmacen(DateTime fechaDesde, DateTime fechaHasta, bool porIngresar, bool entregaInmediata, bool entregaDiferida, bool estadoPendiente, bool estadoParcial, bool estadoCompletada, int[] idsAlmacenes, UserProfileSessionData profileData);
        OrdenAlmacen ObtenerOrdenAlmacen(long idOrdenAlmacen);
        OrdenAlmacen ObtenerOrdenAlmacen(long idOrdenAlmacen, bool porIngresar);
        RegistroMovimientoAlmacen ObtenerRegistroMovimientoOrdenAlmacen(long idOrdenAlmacen, bool porIngresar, UserProfileSessionData profileData);
        OperationResult GuardarMovimientoOrdenAlmacen(RegistroMovimientoAlmacen movimientoOrdenAlmacen, UserProfileSessionData sesionUsuario);
        OperationResult InvalidarMovimientoOrdenAlmacen(long idMovimientoOrdenAlmacen, string observacion, UserProfileSessionData sesionUsuario);
    }
}
