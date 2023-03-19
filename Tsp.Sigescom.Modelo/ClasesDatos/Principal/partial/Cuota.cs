using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public partial class Cuota
    {

        public Cuota(string codigo, string codigoUnicoBanco, DateTime fechaEmision, DateTime fechaVencimiento, decimal total, decimal capital, decimal interes, long idTransaccion, bool letraCambio, string comentario, bool cuotaInicial, long idComprobante, int idBanco, bool porCobrar)
        {
            SetData(codigo, codigoUnicoBanco, fechaEmision, fechaVencimiento, total, capital, interes, idTransaccion, letraCambio, comentario, cuotaInicial, idComprobante, idBanco, porCobrar);
            ValidarId(idBanco, idTransaccion, idComprobante);
        }

        public Cuota(string codigo, DateTime fechaEmision, DateTime fechaVencimiento, decimal total, string comentario, bool porCobrar)
        {
            InitSets();
            this.codigo = codigo;
            this.por_cobrar = porCobrar;
            this.fecha_emision = fechaEmision;
            this.fecha_vencimiento = fechaVencimiento;
            this.cuota_inicial = true;
            this.total = total;
            this.capital = total;
            this.interes = 0;
            this.comentario = comentario;
            this.saldo = total;
        }

        public Cuota(string codigo, DateTime fechaEmision, DateTime fechaVencimiento, decimal total, decimal pagoACuenta, string comentario, bool porCobrar)
        {
            InitSets();
            this.codigo = codigo;
            this.por_cobrar = porCobrar;
            this.fecha_emision = fechaEmision;
            this.fecha_vencimiento = fechaVencimiento;
            this.cuota_inicial = true;
            this.total = total;
            this.capital = total;
            this.interes = 0;
            this.pago_a_cuenta = pagoACuenta;
            this.comentario = comentario;
            this.saldo = total - pagoACuenta;
        }

        public Cuota(string codigo, DateTime fechaEmision, DateTime fechaVencimiento, decimal capital, decimal interes, decimal total, string comentario, bool porCobrar, bool esInicial)
        {
            InitSets();
            this.codigo = codigo;
            this.por_cobrar = porCobrar;
            this.fecha_emision = fechaEmision;
            this.fecha_vencimiento = fechaVencimiento;
            this.cuota_inicial = esInicial;
            this.total = total;
            this.capital = capital;
            this.interes = interes;
            this.comentario = esInicial ? "Cuota inicial" : comentario;
            this.saldo = total;
        }

        public void SetPagoACuenta(decimal pagoACuenta)
        {
            this.pago_a_cuenta = pagoACuenta;
            this.saldo = total - pagoACuenta;
        }

        public void SetData(string codigo, string codigoUnicoBanco, DateTime fechaEmision, DateTime fechaVencimiento, decimal total, decimal capital, decimal interes, long idTransaccion, bool letraCambio, string comentario, bool cuotaInicial, long idComprobante, int idBanco, bool porCobrar)
        {
            this.codigo = codigo;
            this.codigo_unico_banco = codigoUnicoBanco;
            this.fecha_emision = fechaEmision;
            this.fecha_vencimiento = fechaVencimiento;
            this.total = total;
            this.capital = capital;
            this.interes = interes;
            this.id_transaccion = idTransaccion;
            this.letra_cambio = letraCambio;
            this.comentario = comentario;
            this.cuota_inicial = cuotaInicial;
            this.id_comprobante = idComprobante;
            this.id_banco = idBanco;
            this.por_cobrar = porCobrar;
            this.saldo = total;

        }
        private void InitSets()
        {
            this.Estado_cuota = new HashSet<Estado_cuota>();
        }
        protected void ValidarId(int idBanco, long idTransaccion, long idComprobante)
        {
            if (idBanco < 1) { throw new IdNoValidoException(idBanco, "banco"); }
            if (idTransaccion < 1) { throw new IdNoValidoException(idTransaccion, "transaccion"); }
            if (idComprobante < 1) { throw new IdNoValidoException(idComprobante, "comprobante"); }
        }
    }
}
