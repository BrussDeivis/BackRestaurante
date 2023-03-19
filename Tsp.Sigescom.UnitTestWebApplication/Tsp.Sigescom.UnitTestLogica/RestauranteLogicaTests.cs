using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.AccesoDatos.Establecimientos;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Sesion;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.TipoCambio;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Almacen;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Establecimientos;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;
using Tsp.Sigescom.Modelo.Negocio.Restaurant;
using Tsp.Sigescom.Modelo.SigesRestaurant;

namespace Tsp.Sigescom.Logica.Tests
{
    [TestClass()]
    public class RestauranteLogicaTests
    {

        private readonly IRestauranteLogica restauranteLogica;
        private readonly IActorNegocioLogica actorLogica;
        private readonly IMaestroLogica maestroLogica;
        private readonly IOperacionLogica operacionLogica;
        private readonly IConceptoLogica conceptoLogica;
        private readonly IEmpleado_Logica empleadoLogica;
        private readonly ICentroDeAtencion_Logica centroDeAtencionLogica;
        private readonly ISede_Logica sedeLogica;
        private readonly IEstablecimiento_Repositorio establecimientoDatos;
        private readonly IInventarioActualRepositorio inventarioActualDatos;





        public RestauranteLogicaTests()
        {
            restauranteLogica = Dependencia.Resolve<IRestauranteLogica>();
            actorLogica = Dependencia.Resolve<IActorNegocioLogica>();
            maestroLogica = Dependencia.Resolve<IMaestroLogica>();
            operacionLogica = Dependencia.Resolve<IOperacionLogica>();
            conceptoLogica = Dependencia.Resolve<IConceptoLogica>();
            empleadoLogica = Dependencia.Resolve<IEmpleado_Logica>();
            centroDeAtencionLogica = Dependencia.Resolve<ICentroDeAtencion_Logica>();
            sedeLogica = Dependencia.Resolve<ISede_Logica>();
            establecimientoDatos = Dependencia.Resolve<IEstablecimiento_Repositorio>();
            inventarioActualDatos = Dependencia.Resolve<IInventarioActualRepositorio>();



        }

        public UserProfileSessionData SeleccionarCentroDeAtencion()
        {
            string IdUsuario = "0887fff9-63d2-4b08-91dd-fe7d6fc551a7";
            Empleado_ empleado = empleadoLogica.ObtenerEmpleadoInclusiveRoles(IdUsuario);
            List<CentroDeAtencion> centrosDeAtencionProgramados = centroDeAtencionLogica.ObtenerCentrosDeAtencionProgramados_(empleado.Id);
            //A partir del user logueado, construir el model
            var profile = new UserProfileSessionData() { IdUsuario = IdUsuario, NombreUsuario = "User test", CentrosDeAtencionProgramados = centrosDeAtencionProgramados };
            profile.Empleado = empleado ?? throw new Exception("No existe un empleado para la cuenta de usuario, por lo tanto no se puede realizar la operación");
            profile.SetCentrosDeAtencionProgramados(centrosDeAtencionProgramados);
            profile.CentroDeAtencionSeleccionado = profile.CentrosDeAtencionProgramados.FirstOrDefault();

            var sede = sedeLogica.ObtenerSedeConLogo();
            profile.Sede = sede;
            profile.NombreSede = sede.Nombre;
            var clientePorDefecto = actorLogica.ObtenerClienteGenerico();
            profile.ClientePorDefecto = clientePorDefecto;
            if (profile.CentrosDeAtencionProgramados.Count > 1)
            {
                profile.CentroDeAtencionSeleccionado = profile.CentrosDeAtencionProgramados.SingleOrDefault(cap => cap.Id == profile.IdCentroDeAtencionInicioSesion);
            }

            if (profile.CentroDeAtencionSeleccionado != null)
            {
                profile.EstablecimientoComercialSeleccionado = establecimientoDatos.ObtenerEstablecimientoComercialExtendidoConLogo(profile.CentroDeAtencionSeleccionado.EstablecimientoComercial.Id);
                profile.IdCentroAtencionQueTieneLosPrecios = centroDeAtencionLogica.ObtenerIdCentroDeAtencionParaObtencionDePrecios(profile.CentroDeAtencionSeleccionado, profile.EstablecimientoComercialSeleccionado);
                profile.IdCentroAtencionQueTieneElStockIntegrada = centroDeAtencionLogica.ObtenerIdCentroDeAtencionParaObtencionDeStock(ModoOperacionEnum.PorMostrador, profile.CentroDeAtencionSeleccionado, profile.EstablecimientoComercialSeleccionado);
                profile.IdCentroAtencionQueTieneElStockDosPasos = centroDeAtencionLogica.ObtenerIdCentroDeAtencionParaObtencionDeStock(ModoOperacionEnum.PorMostradorEnDosPasos, profile.CentroDeAtencionSeleccionado, profile.EstablecimientoComercialSeleccionado);
                profile.IdCentroAtencionQueTieneElStockCorporativa = centroDeAtencionLogica.ObtenerIdCentroDeAtencionParaObtencionDeStock(ModoOperacionEnum.Corporativa, profile.CentroDeAtencionSeleccionado, profile.EstablecimientoComercialSeleccionado);
            }
            profile.CostoUnitarioDelIcbper = conceptoLogica.ObtenerCostoUnitarioDelIcbperALaFecha();
            profile.TipoDeCambio = new TipoCambio();
            profile.SetIdAlmacenIdInventarioFisico(inventarioActualDatos.ObtenerIdsInventarioActualPorAlmacen());
            profile.MaestrosFrecuentes = new MaestroSesion
            {
                Moneda = ItemGenerico.Convert(maestroLogica.ObtenerMonedaPorDefecto())
            };
            profile.OperacionSesionContenedora = operacionLogica.ObtenerOperacionSesionContenedora(profile.IdCentroDeAtencionSeleccionado);
            return profile;
        }
        /*
        [TestMethod()]
        public void FacturarAtencionSimpleTest()
        {
            AtencionRestaurante atencion = new AtencionRestaurante
            {
                Id = 477,
                Estado = 14198,
                ImporteAtencion = 54,
                TipoDePago = (int)TipoPago.Simple,
                Mesa = new Mesa
                {
                    Id = 99,
                    IdAmbiente = 24,
                    Nombre = "P33"
                },
                Ordenes = new List<Orden_Atencion>
                {
                    new Orden_Atencion
                    {
                        Id = 478,
                        ImporteOrden = 20,
                        Estado = 14198,
                        Mozo = new ItemGenerico { Id = 16, },
                        DetallesDeOrden = new List<DetalleOrden>{
                            new DetalleOrden
                            {
                                Id = 2141,
                                IdItem = 536,
                                Cantidad = 1,
                                Precio = 10,
                                Importe = 10,
                                Estado = 4,
                            },
                            new DetalleOrden
                            {
                                Id = 2142,
                                IdItem = 536,
                                Cantidad = 1,
                                Precio = 10,
                                Importe = 10,
                                Estado = 4,
                            }
                        }
                    },
                    new Orden_Atencion
                    {
                        Id = 479,
                        ImporteOrden = 34,
                        Estado = 14198,
                        Mozo = new ItemGenerico { Id = 16 },
                        DetallesDeOrden = new List<DetalleOrden>{
                            new DetalleOrden
                            {
                                Id = 2143,
                                IdItem = 539,
                                Cantidad = 1,
                                Precio = 17,
                                Importe = 17,
                                Estado = 4,
                            },
                            new DetalleOrden
                            {
                                Id = 2144,
                                IdItem = 539,
                                Cantidad = 1,
                                Precio = 17,
                                Importe = 17,
                                Estado = 4,
                            }
                        }
                    }
                },
                Comprobantes = new List<DatosVentaIntegrada>
                {
                   new DatosVentaIntegrada
                   {
                        EsVentaModoCaja = false,
                        Orden = new DatosOrdenVenta
                        {
                            AplicarIGVCuandoEsAmazonia = false,
                            Cliente = new ActorComercial_
                            {
                                Id = 101
                            },
                            Comprobante = new ComprobanteDeNegocio_ {
                            Tipo = new ItemGenerico { Id = 27 },
                            Serie = new SerieComprobante_ { Id = 22 },
                            },
                            EsVentaPasada = false,
                            FechaEmision = new DateTime(),
                            DescuentoGlobal = 0,
                            Flete = 0,
                            Icbper = 0,
                            NumeroBolsasDePlastico = 0,
                            Observacion = "Ninguna",
                            Placa = "",
                            UnificarDetalles = false,
                            Total = 20,
                            Detalles = null,
                        },
                        Pago = new DatosPago
                        {
                            ModoDePago = ModoPago.Contado,
                            Inicial = 0,
                            Traza = new TrazaDePago_
                            {
                                Info = new InfoPago { ImporteAPagar = 54 },
                                MedioDePago = new ItemGenerico { Id = 281 }
                            }
                        }
                   },
                },

            };
            SesionRestaurante sesionRestaurante = new SesionRestaurante
            {
                SesionDeUsuario = SeleccionarCentroDeAtencion()
            };
            var resultado = restauranteLogica.ConfirmarFacturacion(atencion, sesionRestaurante);
            Assert.IsTrue(resultado.code_result == OperationResultEnum.Success);
        }
        [TestMethod()]
        public void FacturarAtencionDivididoPorMontoTest()
        {
            AtencionRestaurante atencion = new AtencionRestaurante
            {
                Id = 477,
                Estado = 14198,
                ImporteAtencion = 54,
                TipoDePago = (int)TipoPago.DivididoPorMonto,
                Mesa = new Mesa
                {
                    Id = 99,
                    IdAmbiente = 24,
                    Nombre = "P33"
                },
                Ordenes = new List<Orden_Atencion>
                {
                    new Orden_Atencion
                    {
                        Id = 478,
                        ImporteOrden = 20,
                        Estado = 14198,
                        Mozo = new ItemGenerico { Id = 16, },
                        DetallesDeOrden = new List<DetalleOrden>{
                            new DetalleOrden
                            {
                                Id = 2141,
                                IdItem = 536,
                                Cantidad = 1,
                                Precio = 10,
                                Importe = 10,
                                Estado = 4,
                            },
                            new DetalleOrden
                            {
                                Id = 2142,
                                IdItem = 536,
                                Cantidad = 1,
                                Precio = 10,
                                Importe = 10,
                                Estado = 4,
                            }
                        }
                    },
                    new Orden_Atencion
                    {
                        Id = 479,
                        ImporteOrden = 34,
                        Estado = 14198,
                        Mozo = new ItemGenerico { Id = 16 },
                        DetallesDeOrden = new List<DetalleOrden>{
                            new DetalleOrden
                            {
                                Id = 2143,
                                IdItem = 539,
                                Cantidad = 1,
                                Precio = 17,
                                Importe = 17,
                                Estado = 4,
                            },
                            new DetalleOrden
                            {
                                Id = 2144,
                                IdItem = 539,
                                Cantidad = 1,
                                Precio = 17,
                                Importe = 17,
                                Estado = 4,
                            }
                        }
                    }
                },
                Comprobantes = new List<DatosVentaIntegrada>
                {
                   new DatosVentaIntegrada
                   {
                        EsVentaModoCaja = false,
                        Orden = new DatosOrdenVenta
                        {
                            AplicarIGVCuandoEsAmazonia = false,
                            Cliente = new ActorComercial_
                            {
                                Id = 101
                            },
                            Comprobante = new ComprobanteDeNegocio_ {
                            Tipo = new ItemGenerico { Id = 27 },
                            Serie = new SerieComprobante_ { Id = 22 },
                            },
                            EsVentaPasada = false,
                            FechaEmision = new DateTime(),
                            DescuentoGlobal = 0,
                            Flete = 0,
                            Icbper = 0,
                            NumeroBolsasDePlastico = 0,
                            Observacion = "Ninguna",
                            Placa = "",
                            UnificarDetalles = false,
                            Total = 20,
                            Detalles = null,
                        },
                        Pago = new DatosPago
                        {
                            ModoDePago = ModoPago.Contado,
                            Inicial = 0,
                            Traza = new TrazaDePago_
                            {
                                Info = new InfoPago { ImporteAPagar = 54 },
                                MedioDePago = new ItemGenerico { Id = 281 }
                            }
                        }
                   },
                   new DatosVentaIntegrada
                   {
                        EsVentaModoCaja = false,
                        Orden = new DatosOrdenVenta
                        {
                            AplicarIGVCuandoEsAmazonia = false,
                            Cliente = new ActorComercial_
                            {
                                Id = 128
                            },
                            Comprobante = new ComprobanteDeNegocio_ {
                            Tipo = new ItemGenerico { Id = 27 },
                            Serie = new SerieComprobante_ { Id = 22 },
                            },
                            EsVentaPasada = false,
                            FechaEmision = new DateTime(),
                            DescuentoGlobal = 0,
                            Flete = 0,
                            Icbper = 0,
                            NumeroBolsasDePlastico = 0,
                            Observacion = "Ninguna",
                            Placa = "",
                            UnificarDetalles = false,
                            Total = 34,
                            Detalles = null,
                        },
                        Pago = new DatosPago
                        {
                            ModoDePago = ModoPago.Contado,
                            Inicial = 0,
                            Traza = new TrazaDePago_
                            {
                                Info = new InfoPago { ImporteAPagar = 54 },
                                MedioDePago = new ItemGenerico { Id = 281 }
                            }
                        }
                   },
                },

            };
            SesionRestaurante sesionRestaurante = new SesionRestaurante
            {
                SesionDeUsuario = SeleccionarCentroDeAtencion()
            };
            var resultado = restauranteLogica.ConfirmarFacturacion(atencion, sesionRestaurante);
            Assert.IsTrue(resultado.code_result == OperationResultEnum.Success);
        }
        [TestMethod()]
        public void FacturarAtencionDivididoPorItemTest()
        {
            AtencionRestaurante atencion = new AtencionRestaurante
            {
                Id = 477,
                Estado = 14198,
                ImporteAtencion = 54,
                TipoDePago = (int)TipoPago.DivididoPorItem,
                Mesa = new Mesa
                {
                    Id = 99,
                    IdAmbiente = 24,
                    Nombre = "P33"
                },
                Ordenes = new List<Orden_Atencion>
                {
                    new Orden_Atencion
                    {
                        Id = 478,
                        ImporteOrden = 20,
                        Estado = 14198,
                        Mozo = new ItemGenerico { Id = 16, },
                        DetallesDeOrden = new List<DetalleOrden>{
                            new DetalleOrden
                            {
                                Id = 2141,
                                IdItem = 536,
                                Cantidad = 1,
                                Precio = 10,
                                Importe = 10,
                                Estado = 4,
                            },
                            new DetalleOrden
                            {
                                Id = 2142,
                                IdItem = 536,
                                Cantidad = 1,
                                Precio = 10,
                                Importe = 10,
                                Estado = 4,
                            }
                        }
                    },
                    new Orden_Atencion
                    {
                        Id = 479,
                        ImporteOrden = 34,
                        Estado = 14198,
                        Mozo = new ItemGenerico { Id = 16 },
                        DetallesDeOrden = new List<DetalleOrden>{
                            new DetalleOrden
                            {
                                Id = 2143,
                                IdItem = 539,
                                Cantidad = 1,
                                Precio = 17,
                                Importe = 17,
                                Estado = 4,
                            },
                            new DetalleOrden
                            {
                                Id = 2144,
                                IdItem = 539,
                                Cantidad = 1,
                                Precio = 17,
                                Importe = 17,
                                Estado = 4,
                            }
                        }
                    }
                },
                Comprobantes = new List<DatosVentaIntegrada>
                {
                   new DatosVentaIntegrada
                   {
                        EsVentaModoCaja = false,
                        Orden = new DatosOrdenVenta
                        {
                            AplicarIGVCuandoEsAmazonia = false,
                            Cliente = new ActorComercial_
                            {
                                Id = 101
                            },
                            Comprobante = new ComprobanteDeNegocio_ {
                            Tipo = new ItemGenerico { Id = 27 },
                            Serie = new SerieComprobante_ { Id = 22 },
                            },
                            EsVentaPasada = false,
                            FechaEmision = new DateTime(),
                            DescuentoGlobal = 0,
                            Flete = 0,
                            Icbper = 0,
                            NumeroBolsasDePlastico = 0,
                            Observacion = "Ninguna",
                            Placa = "",
                            UnificarDetalles = false,
                            Total = 27,
                            Detalles = new List<DetalleDeOperacion>
                            {
                                new DetalleDeOperacion
                                {
                                    Producto = new Concepto_Negocio_Comercial{ Id = 536 },
                                    Cantidad = 1,
                                    PrecioUnitario = 10,
                                    Importe = 10
                                },
                                new DetalleDeOperacion
                                {
                                    Producto = new Concepto_Negocio_Comercial{ Id = 539 },
                                    Cantidad = 1,
                                    PrecioUnitario = 17,
                                    Importe = 17
                                }
                            },
                        },
                        Pago = new DatosPago
                        {
                            ModoDePago = ModoPago.Contado,
                            Inicial = 0,
                            Traza = new TrazaDePago_
                            {
                                Info = new InfoPago { ImporteAPagar = 54 },
                                MedioDePago = new ItemGenerico { Id = 281 }
                            }
                        }
                   },
                   new DatosVentaIntegrada
                   {
                        EsVentaModoCaja = false,
                        Orden = new DatosOrdenVenta
                        {
                            AplicarIGVCuandoEsAmazonia = false,
                            Cliente = new ActorComercial_
                            {
                                Id = 128
                            },
                            Comprobante = new ComprobanteDeNegocio_ {
                            Tipo = new ItemGenerico { Id = 27 },
                            Serie = new SerieComprobante_ { Id = 22 },
                            },
                            EsVentaPasada = false,
                            FechaEmision = new DateTime(),
                            DescuentoGlobal = 0,
                            Flete = 0,
                            Icbper = 0,
                            NumeroBolsasDePlastico = 0,
                            Observacion = "Ninguna",
                            Placa = "",
                            UnificarDetalles = false,
                            Total = 34,
                            Detalles = new List<DetalleDeOperacion>
                            {
                                new DetalleDeOperacion
                                {
                                    Producto = new Concepto_Negocio_Comercial{ Id = 536 },
                                    Cantidad = 1,
                                    PrecioUnitario = 10,
                                    Importe = 10
                                },
                                new DetalleDeOperacion
                                {
                                    Producto = new Concepto_Negocio_Comercial{ Id = 539 },
                                    Cantidad = 1,
                                    PrecioUnitario = 17,
                                    Importe = 17
                                }
                            },
                        },
                        Pago = new DatosPago
                        {
                            ModoDePago = ModoPago.Contado,
                            Inicial = 0,
                            Traza = new TrazaDePago_
                            {
                                Info = new InfoPago { ImporteAPagar = 54 },
                                MedioDePago = new ItemGenerico { Id = 281 }
                            }
                        }
                   },
                },

            };
            SesionRestaurante sesionRestaurante = new SesionRestaurante
            {
                SesionDeUsuario = SeleccionarCentroDeAtencion()
            };
            var resultado = restauranteLogica.ConfirmarFacturacion(atencion, sesionRestaurante);
            Assert.IsTrue(resultado.code_result == OperationResultEnum.Success);
        }*/
    }
}