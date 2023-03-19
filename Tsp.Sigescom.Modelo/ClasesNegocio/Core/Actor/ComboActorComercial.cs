using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class ComboActorComercial 
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public string NumeroDocumentoIdentidad { get; set; }

        public ComboActorComercial()
        {

        }
        public ComboActorComercial(int id)
        {
            this.Id = id;
        }
        public ComboActorComercial(int id, string razonSocial, string numeroDocuemntoIdentidad)
        {
            this.Id = id;
            this.RazonSocial = razonSocial;
            this.NumeroDocumentoIdentidad = numeroDocuemntoIdentidad;
        }

        public static List<ComboActorComercial> Convert(List<Cliente> clientes)
        {
            List<ComboActorComercial> comboGenericoActores = new List<ComboActorComercial>();
            foreach (var cleinte in clientes)
            {
                comboGenericoActores.Add(new ComboActorComercial(cleinte.Id, cleinte.RazonSocial, cleinte.DocumentoIdentidad));
            }
            return comboGenericoActores;
        }

        public static List<ComboActorComercial> Convert(List<Proveedor> proveedores)
        {
            List<ComboActorComercial> comboGenericoActores = new List<ComboActorComercial>();
            foreach (var prooveedor in proveedores)
            {
                comboGenericoActores.Add(new ComboActorComercial(prooveedor.Id, prooveedor.RazonSocial, prooveedor.DocumentoIdentidad));
            }
            return comboGenericoActores;
        }

        public static List<ComboActorComercial> Convert(List<Empleado> empleados)
        {
            List<ComboActorComercial> comboGenericoActores = new List<ComboActorComercial>();
            foreach (var empleado in empleados)
            {
                comboGenericoActores.Add(new ComboActorComercial(empleado.Id, empleado.NombreCompleto, empleado.DocumentoIdentidad));
            }
            return comboGenericoActores;
        }
    }
    
    public class ComboCentroAtencion//item generico
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }

        public ComboCentroAtencion()
        {

        }
        public ComboCentroAtencion(int id, string codigo, string nombre)
        {
            this.Id = id;
            this.Codigo = codigo;
            this.Nombre = nombre;
        }

        public static List<ComboCentroAtencion> Convert(List<CentroDeAtencionExtendido> centrosDeAtencion)
        {
            List<ComboCentroAtencion> comboGenericoActores = new List<ComboCentroAtencion>();
            foreach (var item in centrosDeAtencion)
            {
                comboGenericoActores.Add(new ComboCentroAtencion(item.Id, item.Codigo, item.Codigo + " | " + item.EstablecimientoComercial.NombreInterno + " | " + item.Nombre ));
            }
            return comboGenericoActores;
        }

        public static List<ComboCentroAtencion> Convert(List<Empleado> empleados)
        {
            List<ComboCentroAtencion> comboGenericoActores = new List<ComboCentroAtencion>();
            foreach (var item in empleados)
            {
                comboGenericoActores.Add(new ComboCentroAtencion(item.Id, item.Codigo, item.Codigo + " | " + item.NombreCompleto));
            }
            return comboGenericoActores;
        }
    }
}