using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Exceptions
{

    public class IdNoValidoException : Exception
    {
        public IdNoValidoException(int id, string nombreEntidad) : base("El valor " + id + " para la entidad " + nombreEntidad + " no es un Id Valido")
        {

        }
        public IdNoValidoException(long id, string nombreEntidad) : base("El valor " + id + " para la entidad " + nombreEntidad + " no es un Id Valido")
        {
        }
    }

    public class SinPermisoException : Exception
    {
        public SinPermisoException(string mensaje) : base(mensaje)
        {

        }
    }

    public class EstadoNoValidoException : Exception
    {
        public EstadoNoValidoException(string mensaje) : base(mensaje)
        {

        }
    }

    public class NoHayDocumentosPorEnviarException : Exception
    {
        public NoHayDocumentosPorEnviarException(string mensaje) : base(mensaje)
        {

        }
    }

    public class AdvertenciaException : Exception
    {
        public AdvertenciaException (string mensaje) : base(mensaje)
        {

        }
    }

    public class LogicaException : Exception
    {
        public LogicaException() : base()
        {

        }
        public LogicaException(string mensaje) : base(mensaje)
        {

        }

        public LogicaException(string mensaje, Exception e) : base(mensaje, e)
        {

        }
    }

    public class ModeloException : Exception
    {
        public ModeloException(string mensaje) : base(mensaje)
        {

        }

        public ModeloException(string mensaje, Exception e) : base(mensaje, e)
        {

        }
    }
    public class ControllerException : Exception
    {
        public ControllerException(string mensaje) : base(mensaje)
        {

        }

        public ControllerException(string mensaje, Exception e) : base(mensaje, e)
        {

        }
    }
    public  class DatosException : Exception
    {
        public DatosException(string mensaje) : base(mensaje)
        {

        }

        public DatosException(string mensaje, Exception e) : base(mensaje,e)
        {

        }
    }

    public class IndicadorExcepcion
    {
        public bool HayExcepcion { get; set; }
        public string RegistroExcepcion { get; set; }

        public IndicadorExcepcion(bool hayExcepcion, string registroExcepcion)
        {
            HayExcepcion = hayExcepcion;
            RegistroExcepcion = registroExcepcion;
        }

        public IndicadorExcepcion(string registroExcepcion)
        {
            RegistroExcepcion = registroExcepcion;
        }

        public IndicadorExcepcion()
        {

        }
    }
}
