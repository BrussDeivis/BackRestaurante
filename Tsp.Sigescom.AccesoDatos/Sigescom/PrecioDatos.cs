using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.AccesoDatos.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;

namespace Tsp.Sigescom.AccesoDatos
{
    public class PrecioDatos : RepositorioBase, IPrecioRepositorio
    {
        public OperationResult CrearPrecio(Precio precio)
        {
            try
            {
                //agrega el nuevo precio
                _db.Precio.Add(precio);
                return Save();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult ResolverPrecios(List<Precio> nuevosPrecios, List<Precio> preciosCaducos)
        {
            try
            {
                Precio dbPrecio = null;
                foreach (var nuevoPrecio in nuevosPrecios)
                {
                    //Conseguimos el precio vigente para el mismo producto, tarifa, tipo precio. Si no existe obtendremos null
                    dbPrecio = _db.Precio.SingleOrDefault(p => p.id_actor_negocio == nuevoPrecio.id_actor_negocio && p.id_concepto_negocio == nuevoPrecio.id_concepto_negocio && p.id_tarifa_d == nuevoPrecio.id_tarifa_d && p.id_tipo == nuevoPrecio.id_tipo && p.es_vigente == true);
                    //En caso de existir un precio vigente, se caducara a la fecha de inicio del nuevo precio
                    ResolverConflictoPrecio(nuevoPrecio, dbPrecio);
                    _db.Precio.Add(nuevoPrecio);
                }
                foreach (var precioCaduco in preciosCaducos)
                {
                    dbPrecio = _db.Precio.SingleOrDefault(p => p.id == precioCaduco.id);
                    //Actualizar el precio de base de datos
                    _db.Entry(dbPrecio).CurrentValues.SetValues(precioCaduco);
                }
                return Save();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public OperationResult establecerPrecios(List<Precio> precios)
        {
            try
            {
                Precio dbPrecio = null;

                foreach (var nuevoPrecio in precios)
                {
                    //Conseguimos el precio vigente para el mismo producto, tarifa, tipo precio. Si no existe obtendremos null
                    dbPrecio = _db.Precio.SingleOrDefault(p => p.id_actor_negocio == nuevoPrecio.id_actor_negocio && p.id_concepto_negocio == nuevoPrecio.id_concepto_negocio && p.id_tarifa_d == nuevoPrecio.id_tarifa_d && p.id_tipo == nuevoPrecio.id_tipo && p.es_vigente == true);
                    //En caso de existir un precio vigente, se caducara a la fecha de inicio del nuevo precio
                    ResolverConflictoPrecio(nuevoPrecio, dbPrecio);
                    _db.Precio.Add(nuevoPrecio);
                }
                return Save();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void ResolverConflictoPrecio(Precio nuevoPrecio, Precio precioExistente)
        {
            if (precioExistente != null)//si encuentra lo caduca
            {
                precioExistente.es_vigente = false;
                precioExistente.fecha_fin = nuevoPrecio.fecha_inicio;
                precioExistente.fecha_modificacion = nuevoPrecio.fecha_modificacion;
            }
        }
         
        public OperationResult EstablecerPrecio(Precio precio)
        {
            try
            {
                OperationResult result;
                if(precio.id == 0)
                {
                    result = CrearPrecio(precio);
                }
                else
                {
                    //Obtener el precio con el concepto, centro de atencion, tipo y tarifa vigente
                    Precio dbPrecio = _db.Precio.SingleOrDefault(p => p.id_concepto_negocio == precio.id_concepto_negocio && p.id_actor_negocio == precio.id_actor_negocio && p.id_tarifa_d == precio.id_tarifa_d && p.id_tipo == precio.id_tipo && p.es_vigente);
                    //Verificar si el monto del nuevo precio es el mismo al del precio de bd, si es el caso solo actualizar el precio
                    if (precio.valor == dbPrecio.valor)
                    {
                        result = ActualizarPrecio(precio);
                    }
                    else
                    {
                        //Resolver el conflicto si es que hay un precio registrado en su lugar (Se cadurara el precio que existe)
                        ResolverConflictoPrecio(precio, dbPrecio);
                        result = CrearPrecio(precio);
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public OperationResult ActualizarPrecio(Precio precio)
        {
            try
            {
                //Obtener el precio existente desde base de datos
                Precio dbPrecio = _db.Precio.SingleOrDefault(p => p.id == precio.id);
                //Actualizar el precio de base de datos
                _db.Entry(dbPrecio).CurrentValues.SetValues(precio);
                return Save();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public IEnumerable<Precio> obtenerPreciosVigentes()
        {
            try
            {
                return _db.Precio.Include(p => p.Concepto_negocio).Include(p => p.Detalle_maestro3).Where(p => p.es_vigente == true).OrderByDescending(p => p.id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Precio_Concepto> ObtenerPrecios(int idTipo)
        {
            try
            {
                return _db.Precio.Where(p => p.id_tipo == idTipo).OrderByDescending(p => p.id)
                    .Select(p => new Precio_Concepto()
                    {
                        Id = p.id,
                        IdConcepto = p.id_concepto_negocio,
                        Concepto = p.Concepto_negocio1.nombre,
                        IdTarifa = p.id_tarifa_d,
                        Tarifa = p.Detalle_maestro3.nombre,
                        Precio = p.valor,
                        FechaDesde = p.fecha_inicio,
                        FechaHasta = p.fecha_fin,
                        EsVigente = p.es_vigente
                    }
                    );
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Precio obtenerPrecio(int idPrecio)
        {
            try
            {
                return _db.Precio.Include(p => p.Concepto_negocio).Include(p => p.Detalle_maestro3).Single(p => p.id == idPrecio);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Precio> ObtenerPreciosVigentesDeUnConceptoNegocio(int idTipo, int idConceptoNegocio)
        {
            try
            {
                return _db.Precio.Where(p => (p.es_vigente == true) && (p.id_concepto_negocio == idConceptoNegocio)).OrderByDescending(p => p.id);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Precio ObtenerPrecioVigente(int idConceptoNegocio, int idTarifa)
        {
            try
            {
                return _db.Precio.SingleOrDefault(p => p.id_concepto_negocio== idConceptoNegocio && p.es_vigente && p.id_tarifa_d == idTarifa);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Precio> ObtenerPreciosVigentes(int[] idsConceptosNegocio, int idTarifa)
        {
            try
            {
                return _db.Precio.Where(p => idsConceptosNegocio.Contains(p.id_concepto_negocio) && p.es_vigente && p.id_tarifa_d== idTarifa);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
