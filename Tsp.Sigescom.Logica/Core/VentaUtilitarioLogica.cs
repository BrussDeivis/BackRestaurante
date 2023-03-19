using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Interfaces.Negocio;

namespace Tsp.Sigescom.Logica.Sigescom
{
    public class VentaUtilitarioLogica : IVentaUtilitarioLogica
    {
        public bool ObtenerCamposEditablesEnVentas(string mascaraDeFormaDeCalculo, ElementoDeCalculoEnVentasEnum orden)
        {
            List<int> mascaraCamposInt = mascaraDeFormaDeCalculo.Select(digito => int.Parse(digito.ToString())).ToList();
            //Retornamos si el valor de mascara es igual a 1
            return Convert.ToBoolean(mascaraCamposInt[(int)orden]);
        }

        public bool ObtenerFormasDeCalculosEnVentas(string mascaraDeFormaDeCalculo, ElementoDeCalculoEnVentasEnum orden, ElementoDeCalculoEnVentasEnum valor)
        {
            List<int> mascaraFormasInt = mascaraDeFormaDeCalculo.Select(digito => int.Parse(digito.ToString())).ToList();
            //Retornamos si el valor de mascara es igual al valor
            return mascaraFormasInt[(int)orden] == (int)valor;
        }

    }
}
