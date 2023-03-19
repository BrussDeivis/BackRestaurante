using Microsoft.Practices.Unity;

namespace Tsp.Sigescom.Inyeccion
{
    public class Dependencia
    {
        private static readonly IUnityContainer Container;

        static Dependencia()
        {
            Container = new UnityContainer();
            ContenedorInyeccion.ObtenerRegistros(Container);
        }

        public static T Resolve<T>()
        {
            var ret = default(T);

            if (Container.IsRegistered(typeof(T)))
            {
                ret = Container.Resolve<T>();
            }
            return ret;
        }
    }
}

