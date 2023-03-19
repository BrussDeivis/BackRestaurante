using System;
using System.Collections.Generic;
using System.Data.Entity;

using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Venta;

namespace Tsp.Sigescom.AccesoDatos.Venta

{

    public class MaestroVenta_Datos: IMaestrosVenta_Repositorio
    {
        public IEnumerable<Familia_Concepto_Comercial> ObtenerFamilias()
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                return _db.Concepto_negocio_rol.Where(cnr => cnr.id_rol == ConceptoSettings.Default.IdRolMercaderia).Select(cnr => cnr.Concepto_negocio).Select(cn => cn.Detalle_maestro4).Distinct()
                    .Select(dm => new Familia_Concepto_Comercial()
                    {
                        Id = dm.id,
                        Nombre = dm.nombre,
                        Esbien = dm.valor == "1"
                    });
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener detalles de maestro", e);
            }
        }

       
    }
}