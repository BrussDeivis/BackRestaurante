﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tsp.Sigescom.Modelo.Entidades
{
   
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SigescomEntities : DbContext
    {
        public SigescomEntities()
            : base("name=SigescomEntities")
        {
    ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 2400;
             Database.Log = s => WriteFile.WriteSQL(s);
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Accion_de_negocio> Accion_de_negocio { get; set; }
        public virtual DbSet<Accion_de_negocio_por_tipo_transaccion> Accion_de_negocio_por_tipo_transaccion { get; set; }
        public virtual DbSet<Accion_por_estado> Accion_por_estado { get; set; }
        public virtual DbSet<Accion_por_rol> Accion_por_rol { get; set; }
        public virtual DbSet<Actor> Actor { get; set; }
        public virtual DbSet<Actor_negocio> Actor_negocio { get; set; }
        public virtual DbSet<Actor_negocio_por_transaccion> Actor_negocio_por_transaccion { get; set; }
        public virtual DbSet<Actor_negocio_rol> Actor_negocio_rol { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Atributo> Atributo { get; set; }
        public virtual DbSet<Atributo_actor> Atributo_actor { get; set; }
        public virtual DbSet<Caracteristica_concepto> Caracteristica_concepto { get; set; }
        public virtual DbSet<Categoria_concepto> Categoria_concepto { get; set; }
        public virtual DbSet<Clase_actor> Clase_actor { get; set; }
        public virtual DbSet<Comprobante> Comprobante { get; set; }
        public virtual DbSet<Concepto_negocio> Concepto_negocio { get; set; }
        public virtual DbSet<Concepto_negocio_rol> Concepto_negocio_rol { get; set; }
        public virtual DbSet<Configuracion> Configuracion { get; set; }
        public virtual DbSet<Cuenta_contable> Cuenta_contable { get; set; }
        public virtual DbSet<Cuota> Cuota { get; set; }
        public virtual DbSet<Detalle_detalle_maestro> Detalle_detalle_maestro { get; set; }
        public virtual DbSet<Detalle_maestro> Detalle_maestro { get; set; }
        public virtual DbSet<Detalle_transaccion> Detalle_transaccion { get; set; }
        public virtual DbSet<Direccion> Direccion { get; set; }
        public virtual DbSet<Documento_identidad> Documento_identidad { get; set; }
        public virtual DbSet<Documento_identidad_por_tipo_actor> Documento_identidad_por_tipo_actor { get; set; }
        public virtual DbSet<Estado_cuota> Estado_cuota { get; set; }
        public virtual DbSet<Estado_legal> Estado_legal { get; set; }
        public virtual DbSet<Estado_transaccion> Estado_transaccion { get; set; }
        public virtual DbSet<Evento_transaccion> Evento_transaccion { get; set; }
        public virtual DbSet<Existencia> Existencia { get; set; }
        public virtual DbSet<Foto> Foto { get; set; }
        public virtual DbSet<Maestro> Maestro { get; set; }
        public virtual DbSet<Mapeo_cuenta_contable_concepto> Mapeo_cuenta_contable_concepto { get; set; }
        public virtual DbSet<Marca_concepto> Marca_concepto { get; set; }
        public virtual DbSet<Menu_aplicacion> Menu_aplicacion { get; set; }
        public virtual DbSet<Modo_pago> Modo_pago { get; set; }
        public virtual DbSet<Pago_cuota> Pago_cuota { get; set; }
        public virtual DbSet<Parametro_actor_negocio> Parametro_actor_negocio { get; set; }
        public virtual DbSet<Parametro_concepto_negocio> Parametro_concepto_negocio { get; set; }
        public virtual DbSet<Parametro_de_configuracion> Parametro_de_configuracion { get; set; }
        public virtual DbSet<Parametro_por_rol> Parametro_por_rol { get; set; }
        public virtual DbSet<Parametro_por_unidad_negocio> Parametro_por_unidad_negocio { get; set; }
        public virtual DbSet<Parametro_transaccion> Parametro_transaccion { get; set; }
        public virtual DbSet<Parentesco> Parentesco { get; set; }
        public virtual DbSet<Periodo> Periodo { get; set; }
        public virtual DbSet<Plan_contable> Plan_contable { get; set; }
        public virtual DbSet<Precio> Precio { get; set; }
        public virtual DbSet<Referencia_detalle_transaccion> Referencia_detalle_transaccion { get; set; }
        public virtual DbSet<Registro_sucesos> Registro_sucesos { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Rol_por_tipo_actor> Rol_por_tipo_actor { get; set; }
        public virtual DbSet<Rol_por_unidad_negocio> Rol_por_unidad_negocio { get; set; }
        public virtual DbSet<Serie_comprobante> Serie_comprobante { get; set; }
        public virtual DbSet<Tipo_actor> Tipo_actor { get; set; }
        public virtual DbSet<Tipo_cambio> Tipo_cambio { get; set; }
        public virtual DbSet<Tipo_parentesco> Tipo_parentesco { get; set; }
        public virtual DbSet<Tipo_transaccion> Tipo_transaccion { get; set; }
        public virtual DbSet<Tipo_transaccion_tipo_comprobante> Tipo_transaccion_tipo_comprobante { get; set; }
        public virtual DbSet<Transaccion> Transaccion { get; set; }
        public virtual DbSet<Traza_pago> Traza_pago { get; set; }
        public virtual DbSet<Turno> Turno { get; set; }
        public virtual DbSet<Ubigeo> Ubigeo { get; set; }
        public virtual DbSet<Valor_caracteristica> Valor_caracteristica { get; set; }
        public virtual DbSet<Valor_caracteristica_concepto> Valor_caracteristica_concepto { get; set; }
        public virtual DbSet<Valor_caracteristica_concepto_negocio> Valor_caracteristica_concepto_negocio { get; set; }
        public virtual DbSet<Valor_detalle_maestro_detalle_transaccion> Valor_detalle_maestro_detalle_transaccion { get; set; }
        public virtual DbSet<Vinculo_Actor_Negocio> Vinculo_Actor_Negocio { get; set; }
        public virtual DbSet<VoucherModel> VoucherModel { get; set; }
        public virtual DbSet<new_table> new_table { get; set; }
    
        public virtual int BuscaValorEnBBDD(string strValorBusqueda)
        {
            var strValorBusquedaParameter = strValorBusqueda != null ?
                new ObjectParameter("StrValorBusqueda", strValorBusqueda) :
                new ObjectParameter("StrValorBusqueda", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("BuscaValorEnBBDD", strValorBusquedaParameter);
        }
    
        public virtual int businessObjectStockAt(Nullable<System.DateTime> atTime, Nullable<int> systemStockTransactionTypeId, Nullable<int> businessObjectId, Nullable<int> subtransactionInternalSubjectId, Nullable<int> activeSubtransactionStateId)
        {
            var atTimeParameter = atTime.HasValue ?
                new ObjectParameter("AtTime", atTime) :
                new ObjectParameter("AtTime", typeof(System.DateTime));
    
            var systemStockTransactionTypeIdParameter = systemStockTransactionTypeId.HasValue ?
                new ObjectParameter("systemStockTransactionTypeId", systemStockTransactionTypeId) :
                new ObjectParameter("systemStockTransactionTypeId", typeof(int));
    
            var businessObjectIdParameter = businessObjectId.HasValue ?
                new ObjectParameter("businessObjectId", businessObjectId) :
                new ObjectParameter("businessObjectId", typeof(int));
    
            var subtransactionInternalSubjectIdParameter = subtransactionInternalSubjectId.HasValue ?
                new ObjectParameter("subtransactionInternalSubjectId", subtransactionInternalSubjectId) :
                new ObjectParameter("subtransactionInternalSubjectId", typeof(int));
    
            var activeSubtransactionStateIdParameter = activeSubtransactionStateId.HasValue ?
                new ObjectParameter("activeSubtransactionStateId", activeSubtransactionStateId) :
                new ObjectParameter("activeSubtransactionStateId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("businessObjectStockAt", atTimeParameter, systemStockTransactionTypeIdParameter, businessObjectIdParameter, subtransactionInternalSubjectIdParameter, activeSubtransactionStateIdParameter);
        }
    
        public virtual int feesInBusinessActionTransactionType(Nullable<int> businessAction, Nullable<bool> businessActionValue, Nullable<System.DateTime> from, Nullable<System.DateTime> to, Nullable<decimal> greaterEqualThan, Nullable<decimal> lessEqualThan)
        {
            var businessActionParameter = businessAction.HasValue ?
                new ObjectParameter("businessAction", businessAction) :
                new ObjectParameter("businessAction", typeof(int));
    
            var businessActionValueParameter = businessActionValue.HasValue ?
                new ObjectParameter("businessActionValue", businessActionValue) :
                new ObjectParameter("businessActionValue", typeof(bool));
    
            var fromParameter = from.HasValue ?
                new ObjectParameter("from", from) :
                new ObjectParameter("from", typeof(System.DateTime));
    
            var toParameter = to.HasValue ?
                new ObjectParameter("to", to) :
                new ObjectParameter("to", typeof(System.DateTime));
    
            var greaterEqualThanParameter = greaterEqualThan.HasValue ?
                new ObjectParameter("greaterEqualThan", greaterEqualThan) :
                new ObjectParameter("greaterEqualThan", typeof(decimal));
    
            var lessEqualThanParameter = lessEqualThan.HasValue ?
                new ObjectParameter("lessEqualThan", lessEqualThan) :
                new ObjectParameter("lessEqualThan", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("feesInBusinessActionTransactionType", businessActionParameter, businessActionValueParameter, fromParameter, toParameter, greaterEqualThanParameter, lessEqualThanParameter);
        }
    
        public virtual int kardexReport(Nullable<System.DateTime> fromTime, Nullable<System.DateTime> toTime, Nullable<int> systemStockTransactionTypeId, Nullable<int> subtransactionInternalSubjectId, Nullable<int> activeSubtransactionStateId)
        {
            var fromTimeParameter = fromTime.HasValue ?
                new ObjectParameter("fromTime", fromTime) :
                new ObjectParameter("fromTime", typeof(System.DateTime));
    
            var toTimeParameter = toTime.HasValue ?
                new ObjectParameter("toTime", toTime) :
                new ObjectParameter("toTime", typeof(System.DateTime));
    
            var systemStockTransactionTypeIdParameter = systemStockTransactionTypeId.HasValue ?
                new ObjectParameter("systemStockTransactionTypeId", systemStockTransactionTypeId) :
                new ObjectParameter("systemStockTransactionTypeId", typeof(int));
    
            var subtransactionInternalSubjectIdParameter = subtransactionInternalSubjectId.HasValue ?
                new ObjectParameter("subtransactionInternalSubjectId", subtransactionInternalSubjectId) :
                new ObjectParameter("subtransactionInternalSubjectId", typeof(int));
    
            var activeSubtransactionStateIdParameter = activeSubtransactionStateId.HasValue ?
                new ObjectParameter("activeSubtransactionStateId", activeSubtransactionStateId) :
                new ObjectParameter("activeSubtransactionStateId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("kardexReport", fromTimeParameter, toTimeParameter, systemStockTransactionTypeIdParameter, subtransactionInternalSubjectIdParameter, activeSubtransactionStateIdParameter);
        }
    
        public virtual int stockAtReport(Nullable<System.DateTime> atTime, Nullable<int> systemStockTransactionTypeId, Nullable<int> subtransactionInternalSubjectId, Nullable<int> activeSubtransactionStateId)
        {
            var atTimeParameter = atTime.HasValue ?
                new ObjectParameter("AtTime", atTime) :
                new ObjectParameter("AtTime", typeof(System.DateTime));
    
            var systemStockTransactionTypeIdParameter = systemStockTransactionTypeId.HasValue ?
                new ObjectParameter("systemStockTransactionTypeId", systemStockTransactionTypeId) :
                new ObjectParameter("systemStockTransactionTypeId", typeof(int));
    
            var subtransactionInternalSubjectIdParameter = subtransactionInternalSubjectId.HasValue ?
                new ObjectParameter("subtransactionInternalSubjectId", subtransactionInternalSubjectId) :
                new ObjectParameter("subtransactionInternalSubjectId", typeof(int));
    
            var activeSubtransactionStateIdParameter = activeSubtransactionStateId.HasValue ?
                new ObjectParameter("activeSubtransactionStateId", activeSubtransactionStateId) :
                new ObjectParameter("activeSubtransactionStateId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("stockAtReport", atTimeParameter, systemStockTransactionTypeIdParameter, subtransactionInternalSubjectIdParameter, activeSubtransactionStateIdParameter);
        }
    
        public virtual ObjectResult<ObtenerConceptosDeNegociosComercialesConStockPrecioPorRol_Result> ObtenerConceptosDeNegociosComercialesConStockPrecioPorRol(Nullable<int> idRol, Nullable<long> idTransaccionInventario, Nullable<int> idActorNegocioPrecio)
        {
            var idRolParameter = idRol.HasValue ?
                new ObjectParameter("IdRol", idRol) :
                new ObjectParameter("IdRol", typeof(int));
    
            var idTransaccionInventarioParameter = idTransaccionInventario.HasValue ?
                new ObjectParameter("IdTransaccionInventario", idTransaccionInventario) :
                new ObjectParameter("IdTransaccionInventario", typeof(long));
    
            var idActorNegocioPrecioParameter = idActorNegocioPrecio.HasValue ?
                new ObjectParameter("IdActorNegocioPrecio", idActorNegocioPrecio) :
                new ObjectParameter("IdActorNegocioPrecio", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ObtenerConceptosDeNegociosComercialesConStockPrecioPorRol_Result>("ObtenerConceptosDeNegociosComercialesConStockPrecioPorRol", idRolParameter, idTransaccionInventarioParameter, idActorNegocioPrecioParameter);
        }
    
        public virtual ObjectResult<ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYBusquedaConcepto_Result> ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYBusquedaConcepto(Nullable<int> idRol, Nullable<long> idTransaccionInventario, Nullable<int> idActorNegocioPrecio, string cadenaBusqueda)
        {
            var idRolParameter = idRol.HasValue ?
                new ObjectParameter("IdRol", idRol) :
                new ObjectParameter("IdRol", typeof(int));
    
            var idTransaccionInventarioParameter = idTransaccionInventario.HasValue ?
                new ObjectParameter("IdTransaccionInventario", idTransaccionInventario) :
                new ObjectParameter("IdTransaccionInventario", typeof(long));
    
            var idActorNegocioPrecioParameter = idActorNegocioPrecio.HasValue ?
                new ObjectParameter("IdActorNegocioPrecio", idActorNegocioPrecio) :
                new ObjectParameter("IdActorNegocioPrecio", typeof(int));
    
            var cadenaBusquedaParameter = cadenaBusqueda != null ?
                new ObjectParameter("CadenaBusqueda", cadenaBusqueda) :
                new ObjectParameter("CadenaBusqueda", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYBusquedaConcepto_Result>("ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYBusquedaConcepto", idRolParameter, idTransaccionInventarioParameter, idActorNegocioPrecioParameter, cadenaBusquedaParameter);
        }
    
        public virtual ObjectResult<ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYBusquedaConceptoYTipoFamilia_Result> ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYBusquedaConceptoYTipoFamilia(Nullable<int> idRol, Nullable<long> idTransaccionInventario, Nullable<int> idActorNegocioPrecio, string cadenaBusqueda, string valorTipoFamilia)
        {
            var idRolParameter = idRol.HasValue ?
                new ObjectParameter("IdRol", idRol) :
                new ObjectParameter("IdRol", typeof(int));
    
            var idTransaccionInventarioParameter = idTransaccionInventario.HasValue ?
                new ObjectParameter("IdTransaccionInventario", idTransaccionInventario) :
                new ObjectParameter("IdTransaccionInventario", typeof(long));
    
            var idActorNegocioPrecioParameter = idActorNegocioPrecio.HasValue ?
                new ObjectParameter("IdActorNegocioPrecio", idActorNegocioPrecio) :
                new ObjectParameter("IdActorNegocioPrecio", typeof(int));
    
            var cadenaBusquedaParameter = cadenaBusqueda != null ?
                new ObjectParameter("CadenaBusqueda", cadenaBusqueda) :
                new ObjectParameter("CadenaBusqueda", typeof(string));
    
            var valorTipoFamiliaParameter = valorTipoFamilia != null ?
                new ObjectParameter("ValorTipoFamilia", valorTipoFamilia) :
                new ObjectParameter("ValorTipoFamilia", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYBusquedaConceptoYTipoFamilia_Result>("ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYBusquedaConceptoYTipoFamilia", idRolParameter, idTransaccionInventarioParameter, idActorNegocioPrecioParameter, cadenaBusquedaParameter, valorTipoFamiliaParameter);
        }
    
        public virtual ObjectResult<ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYFamilia_Result> ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYFamilia(Nullable<int> idRol, Nullable<long> idTransaccionInventario, Nullable<int> idActorNegocioPrecio, Nullable<int> idFamilia)
        {
            var idRolParameter = idRol.HasValue ?
                new ObjectParameter("IdRol", idRol) :
                new ObjectParameter("IdRol", typeof(int));
    
            var idTransaccionInventarioParameter = idTransaccionInventario.HasValue ?
                new ObjectParameter("IdTransaccionInventario", idTransaccionInventario) :
                new ObjectParameter("IdTransaccionInventario", typeof(long));
    
            var idActorNegocioPrecioParameter = idActorNegocioPrecio.HasValue ?
                new ObjectParameter("IdActorNegocioPrecio", idActorNegocioPrecio) :
                new ObjectParameter("IdActorNegocioPrecio", typeof(int));
    
            var idFamiliaParameter = idFamilia.HasValue ?
                new ObjectParameter("IdFamilia", idFamilia) :
                new ObjectParameter("IdFamilia", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYFamilia_Result>("ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYFamilia", idRolParameter, idTransaccionInventarioParameter, idActorNegocioPrecioParameter, idFamiliaParameter);
        }
    
        public virtual ObjectResult<ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYTipoFamilia_Result> ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYTipoFamilia(Nullable<int> idRol, Nullable<long> idTransaccionInventario, Nullable<int> idActorNegocioPrecio, string valorTipoFamilia)
        {
            var idRolParameter = idRol.HasValue ?
                new ObjectParameter("IdRol", idRol) :
                new ObjectParameter("IdRol", typeof(int));
    
            var idTransaccionInventarioParameter = idTransaccionInventario.HasValue ?
                new ObjectParameter("IdTransaccionInventario", idTransaccionInventario) :
                new ObjectParameter("IdTransaccionInventario", typeof(long));
    
            var idActorNegocioPrecioParameter = idActorNegocioPrecio.HasValue ?
                new ObjectParameter("IdActorNegocioPrecio", idActorNegocioPrecio) :
                new ObjectParameter("IdActorNegocioPrecio", typeof(int));
    
            var valorTipoFamiliaParameter = valorTipoFamilia != null ?
                new ObjectParameter("ValorTipoFamilia", valorTipoFamilia) :
                new ObjectParameter("ValorTipoFamilia", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYTipoFamilia_Result>("ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYTipoFamilia", idRolParameter, idTransaccionInventarioParameter, idActorNegocioPrecioParameter, valorTipoFamiliaParameter);
        }
    }
}
