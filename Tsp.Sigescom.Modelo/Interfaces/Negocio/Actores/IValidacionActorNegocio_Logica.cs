using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Modelo.Negocio.Actores
{
    public interface IValidacionActorNegocio_Logica
    {

        bool ValidarClienteYRUC(int idCliente);
        bool ValidarProveedorYRUC(int idProveedor);
        bool ValidarRUC(string ruc);
        void ValidarDocumentoIdentidad(string documentoIdentidad, ItemGenerico tipoDocumentoIdentidad);
        void ValidarExisteActorConElMismoDocumentoYDistintoId(int id, string NumeroDeDocumento);
        void ValidarExisteActorComercialConElMismoDocumentoVigente(int idRol, string NumeroDeDocumento, int idActorComercial);
        void ValidarOperaciondesDeActorComercial(int idRol, int id, string numeroDeDocumento);
        void ValidarExistenciaDeDocumento(int idTipoDocumentoIdentidad, string numeroDocumentoIdentidad);


    }
}

