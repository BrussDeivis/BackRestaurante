using System;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Actores;
using Tsp.Sigescom.Modelo.Negocio.Actores;

namespace Tsp.Sigescom.Logica.Core.Actores
{
    public partial class ValidacionActorNegocio_Logica: IValidacionActorNegocio_Logica
    {

        private readonly IActor_Repositorio _actorRepositorio;

        public ValidacionActorNegocio_Logica(IActor_Repositorio actorRepositorio)
        {
            _actorRepositorio = actorRepositorio;
        }

        public bool ValidarDNI(string dni)
        {
            return dni.Trim().Length == 8;
        }
        public void ValidarExistenciaDeDocumento(int idTipoDocumentoIdentidad, string numeroDocumentoIdentidad)
        {
            bool validacion = _actorRepositorio.ExisteDocumento(idTipoDocumentoIdentidad, numeroDocumentoIdentidad);
            if (validacion)
            {
                throw new Exception("Ya existe este numero de documento: " + numeroDocumentoIdentidad + ", ingrese otro");
            }
        }
        public bool ValidarRUC(string ruc)
        {
            if (ruc.Length != 11)
            {
                return false;
            }
            int dig01 = Convert.ToInt32(ruc.Substring(0, 1)) * 5;
            int dig02 = Convert.ToInt32(ruc.Substring(1, 1)) * 4;
            int dig03 = Convert.ToInt32(ruc.Substring(2, 1)) * 3;
            int dig04 = Convert.ToInt32(ruc.Substring(3, 1)) * 2;
            int dig05 = Convert.ToInt32(ruc.Substring(4, 1)) * 7;
            int dig06 = Convert.ToInt32(ruc.Substring(5, 1)) * 6;
            int dig07 = Convert.ToInt32(ruc.Substring(6, 1)) * 5;
            int dig08 = Convert.ToInt32(ruc.Substring(7, 1)) * 4;
            int dig09 = Convert.ToInt32(ruc.Substring(8, 1)) * 3;
            int dig10 = Convert.ToInt32(ruc.Substring(9, 1)) * 2;
            int dig11 = Convert.ToInt32(ruc.Substring(10, 1));

            int suma = dig01 + dig02 + dig03 + dig04 + dig05 + dig06 + dig07 + dig08 + dig09 + dig10;
            int residuo = suma % 11;
            int resta = 11 - residuo;

            int digChk = 0;
            if (resta == 10)
            {
                digChk = 0;
            }
            else if (resta == 11)
            {
                digChk = 1;
            }
            else
            {
                digChk = resta;
            }

            if (dig11 == digChk)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ValidarClienteYRUC(int idCliente)
        {
            Cliente cliente = new Cliente(_actorRepositorio.ObtenerActorDeNegocio(idCliente));
            return cliente.IdTipoDocumentoIdentidad == ActorSettings.Default.IdTipoDocumentoIdentidadRuc;
        }
        public bool ValidarProveedorYRUC(int idProveedor)
        {
            Proveedor proveedor = new Proveedor(_actorRepositorio.ObtenerActorDeNegocio(idProveedor));
            return proveedor.IdTipoDocumentoIdentidad == ActorSettings.Default.IdTipoDocumentoIdentidadRuc;
        }
        public void ValidarDocumentoIdentidad(string documentoIdentidad, ItemGenerico tipoDocumentoIdentidad)
        {
            if (tipoDocumentoIdentidad.Id == ActorSettings.Default.IdTipoDocumentoIdentidadDni)
            {
                if (!ValidarDNI(documentoIdentidad)) throw new Exception("DNI no válido");
            }
            else if (tipoDocumentoIdentidad.Id == ActorSettings.Default.IdTipoDocumentoIdentidadRuc)
            {
                if (!ValidarRUC(documentoIdentidad)) throw new Exception("RUC incorrecto");
            }
        }
        public void ValidarExisteActorConElMismoDocumentoYDistintoId(int id, string NumeroDeDocumento)
        {
            if (_actorRepositorio.ExisteActorConElMismoDocumentoYDistintoId(id, NumeroDeDocumento))
            {
                throw new LogicaException("Ya existe un documento de identidad perteneciente a otro actor");
            }
        }
        public void ValidarExisteActorComercialConElMismoDocumentoVigente(int idRol, string NumeroDeDocumento, int idActorComercial)
        {
            if (idActorComercial > 0)
            {
                if (_actorRepositorio.ExisteActorComercialConElMismoDocumentoVigente(idActorComercial, idRol, NumeroDeDocumento))
                {
                    throw new LogicaException("Ya existe un documento de identidad perteneciente a otro actor comercial");
                }
            }
            else
            {
                if (_actorRepositorio.ExisteActorComercialConElMismoDocumentoVigente(idRol, NumeroDeDocumento))
                {
                    throw new LogicaException("Ya existe un documento de identidad perteneciente a otro actor comercial");
                }
            }

        }
        public void ValidarOperaciondesDeActorComercial(int idRol, int id, string numeroDeDocumento)
        {
            var actorComercial = _actorRepositorio.ObtenerActorComercial(id);
            if (actorComercial.NumeroDocumentoIdentidad != numeroDeDocumento)
            {
                if (_actorRepositorio.ActorParticipaEnTransacciones(id))
                {
                    throw new LogicaException("No se puede cambiar el actor del actor comercial debido a que tiene operaciones realizadas");
                }
            }
        }
    }
}
