using System.Collections.Generic;
using System.Linq;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class CuentaContable
    {
        private Cuenta_contable cuenta_contable;

        public CuentaContable()
        {

        }
        public CuentaContable(Cuenta_contable cuenta_contable)
        {
            this.cuenta_contable = cuenta_contable;
        }


        public static List<CuentaContable> convert(List<Cuenta_contable> cuentasContables)
        {
            var cuentas = new List<CuentaContable>();

            foreach (var cuenta in cuentasContables)
            {
                cuentas.Add(new CuentaContable(cuenta));
            }
            return cuentas;
        }

        public int Id
        {
            get { return this.cuenta_contable.id; }
        }

        public string Codigo
        {
            get { return this.cuenta_contable.codigo; }
        }

        
        public string Nombre
        {
            get { return this.cuenta_contable.nombre; }
        }

        public int IdPlan
        {
            get { return this.cuenta_contable.id_plan; }
        }        


    }
}
