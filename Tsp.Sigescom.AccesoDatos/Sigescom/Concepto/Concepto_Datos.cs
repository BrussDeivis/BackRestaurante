using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Concepto;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio.Conceptos;

namespace Tsp.Sigescom.AccesoDatos.Conceptos

{
    public partial class Concepto_Datos: IConcepto_Repositorio
    {
        public IEnumerable<Concepto> ObtenerConceptos(int[] idsConceptos)
        {
            SigescomEntities _db = new SigescomEntities();
            return _db.Concepto_negocio.Where(cn => idsConceptos.Contains(cn.id)).Select(cn=> new Concepto() {Id= cn.id, CodigoBarra= cn.codigo, Nombre= cn.nombre, Familia= cn.Detalle_maestro4.nombre, UnidadMedida= cn.Detalle_maestro.codigo, StockMinimo= cn.stock_minimo });
        }

        public IEnumerable<Concepto_negocio> ObtenerConceptosComercialesPorFamilia(int idFamilia)
        {
            try
            {
                var idRol = ConceptoSettings.Default.IdRolMercaderia;
                SigescomEntities _db = new SigescomEntities();
                return _db.Concepto_negocio_rol.Include(cnr => cnr.Rol)
                    .Where(cnr => cnr.id_rol == idRol && cnr.Concepto_negocio.id_concepto_basico == idFamilia)
                    .Select(cnr => cnr.Concepto_negocio);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<int> ObtenerIdsDeConceptosComercialesPorFamilia(int idFamilia)
        {
            try
            {
                var idRol = ConceptoSettings.Default.IdRolMercaderia;
                SigescomEntities _db = new SigescomEntities();
                return _db.Concepto_negocio_rol.Include(cnr => cnr.Rol)
                    .Where(cnr => cnr.id_rol == idRol && cnr.Concepto_negocio.id_concepto_basico == idFamilia)
                    .Select(cnr => cnr.Concepto_negocio.id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}