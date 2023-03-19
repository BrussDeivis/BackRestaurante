using System;
using System.Collections.Generic;
using System.Data.Entity;

using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Almacen;

namespace Tsp.Sigescom.AccesoDatos.Almacen

{

    public partial class MaestroAlmacen_Datos: IMaestrosAlmacen_Repositorio
    {

        public int[] ObtenerIdsConceptosComerciales()
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                return _db.Concepto_negocio_rol
                        .Include(cnr => cnr.Rol)
                        .Include(cnr => cnr.Concepto_negocio)
                        .Include(cnr => cnr.Concepto_negocio.Detalle_maestro)
                        .Include(cnr => cnr.Concepto_negocio.Detalle_maestro4)
                        .Where(cnr => cnr.id_rol == ConceptoSettings.Default.IdRolMercaderia).ToList()
                        .Select(cnr => cnr.Concepto_negocio)
                        .Where(cnr => cnr.es_vigente).Select(cn => cn.id).ToArray();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int[] ObtenerIdsAlmacenes()
        {
            SigescomEntities _db = new SigescomEntities();

            return _db.Actor_negocio.Where(an => an.id_rol == ActorSettings.Default.IdRolAlmacen).Select(an => an.Actor).SelectMany(a => a.Actor_negocio).Where(an => an.id_rol == ActorSettings.Default.IdRolEntidadInterna).Select(an => an.id).ToArray();
        }


        
       
        public decimal ObtenerStockMinimo(int idConcepto)
        {
            SigescomEntities _db = new SigescomEntities();
            return _db.Concepto_negocio.Single(c => c.id == idConcepto).stock_minimo;
        }

        public IEnumerable<ItemGenerico> ObtenerConceptosNegocioComercializablesBienes()
        {
            SigescomEntities _db = new SigescomEntities();
            return _db.Concepto_negocio_rol.Where(cnr => cnr.id_rol== ConceptoSettings.Default.IdRolMercaderia).Select(cnr=> cnr.Concepto_negocio).Where(cn => cn.Detalle_maestro4.valor == "1").Select(cn=> new ItemGenerico() {Id= cn.id, Nombre= cn.nombre });
        }
        public IEnumerable<Familia_Concepto_Comercial> ObtenerFamiliasBienes()
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                return _db.Concepto_negocio_rol.Where(cnr => cnr.id_rol == ConceptoSettings.Default.IdRolMercaderia).Select(cnr => cnr.Concepto_negocio).Select(cn => cn.Detalle_maestro4).Distinct().Where(dm => dm.valor == "1")
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