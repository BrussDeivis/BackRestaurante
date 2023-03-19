using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 namespace Tsp.Sigescom.Modelo.Entidades
{

    public partial class __MigrationHistory
    {
		public __MigrationHistory()
		{
		}
		public __MigrationHistory(String MigrationId,String ContextKey,String Model,String ProductVersion)
		{
			setData(MigrationId,ContextKey,Model,ProductVersion);
			
		}
		
		public void setData(String MigrationId,String ContextKey,String Model,String ProductVersion)
		{
			this.MigrationId = MigrationId;
			this.ContextKey = ContextKey;
			this.Model = Model;
			this.ProductVersion = ProductVersion;
			
		}
		
	}

    public partial class Accion_de_negocio
    {
		public Accion_de_negocio()
		{
		}
		public Accion_de_negocio(string nombre,string descripcion)
		{
			setData(nombre,descripcion);
			
		}
		
		public void setData(string nombre,string descripcion)
		{
			this.nombre = nombre;
			this.descripcion = descripcion;
			
		}
		
	}

    public partial class Accion_de_negocio_por_tipo_transaccion
    {
		public Accion_de_negocio_por_tipo_transaccion()
		{
		}
		public Accion_de_negocio_por_tipo_transaccion(int idTipoTransaccion,int idAccionDeNegocio,bool valor)
		{
			setData(idTipoTransaccion,idAccionDeNegocio,valor);
			validarId(idAccionDeNegocio, idTipoTransaccion);
		}
		
		public void setData(int idTipoTransaccion,int idAccionDeNegocio,bool valor)
		{
			this.id_tipo_transaccion = idTipoTransaccion;
			this.id_accion_de_negocio = idAccionDeNegocio;
			this.valor = valor;
			
		}
		protected void validarId(int idAccionDeNegocio, int idTipoTransaccion)
		{
			if(idAccionDeNegocio< 1){ throw new IdNoValidoException(idAccionDeNegocio , "accion de negocio"); }
			if(idTipoTransaccion< 1){ throw new IdNoValidoException(idTipoTransaccion , "tipo transaccion"); }
			
		}
	}

    public partial class Accion_por_estado
    {
		public Accion_por_estado()
		{
		}
		public Accion_por_estado(int idTipoTransaccion,int idEstadoActual,int idAccionPosible,int idSiguienteEstado)
		{
			setData(idTipoTransaccion,idEstadoActual,idAccionPosible,idSiguienteEstado);
			validarId(idEstadoActual, idAccionPosible, idTipoTransaccion);
		}
		
		public void setData(int idTipoTransaccion,int idEstadoActual,int idAccionPosible,int idSiguienteEstado)
		{
			this.id_tipo_transaccion = idTipoTransaccion;
			this.id_estado_actual = idEstadoActual;
			this.id_accion_posible = idAccionPosible;
			this.id_siguiente_estado = idSiguienteEstado;
			
		}
		protected void validarId(int idEstadoActual, int idAccionPosible, int idTipoTransaccion)
		{
			if(idEstadoActual< 1){ throw new IdNoValidoException(idEstadoActual , "estado actual"); }
			if(idAccionPosible< 1){ throw new IdNoValidoException(idAccionPosible , "accion posible"); }
			if(idTipoTransaccion< 1){ throw new IdNoValidoException(idTipoTransaccion , "tipo transaccion"); }
			
		}
	}

    public partial class Accion_por_rol
    {
		public Accion_por_rol()
		{
		}
		public Accion_por_rol(int idTipoTransaccion,int idRol,int idAccionPosible,int idUnidadNegocio)
		{
			setData(idTipoTransaccion,idRol,idAccionPosible,idUnidadNegocio);
			validarId(idAccionPosible, idUnidadNegocio, idRol, idTipoTransaccion);
		}
		
		public void setData(int idTipoTransaccion,int idRol,int idAccionPosible,int idUnidadNegocio)
		{
			this.id_tipo_transaccion = idTipoTransaccion;
			this.id_rol = idRol;
			this.id_accion_posible = idAccionPosible;
			this.id_unidad_negocio = idUnidadNegocio;
			
		}
		protected void validarId(int idAccionPosible, int idUnidadNegocio, int idRol, int idTipoTransaccion)
		{
			if(idAccionPosible< 1){ throw new IdNoValidoException(idAccionPosible , "accion posible"); }
			if(idUnidadNegocio< 1){ throw new IdNoValidoException(idUnidadNegocio , "unidad negocio"); }
			if(idRol< 1){ throw new IdNoValidoException(idRol , "rol"); }
			if(idTipoTransaccion< 1){ throw new IdNoValidoException(idTipoTransaccion , "tipo transaccion"); }
			
		}
	}

    public partial class Actor
    {
		public Actor()
		{
		}
		public Actor(int idDocumentoIdentidad,string numeroDocumentoIdentidad,string primerNombre,string segundoNombre,DateTime fechaNacimiento,string telefono,int idTipoActor,long idFoto,int idClaseActor,int idEstadoLegal,string correo,string tercerNombre,string paginaWeb)
		{
			setData(idDocumentoIdentidad,numeroDocumentoIdentidad,primerNombre,segundoNombre,fechaNacimiento,telefono,idTipoActor,idFoto,idClaseActor,idEstadoLegal,correo,tercerNombre,paginaWeb);
			validarId(idClaseActor, idTipoActor, idEstadoLegal, idFoto, idDocumentoIdentidad);
		}
		
		public void setData(int idDocumentoIdentidad,string numeroDocumentoIdentidad,string primerNombre,string segundoNombre,DateTime fechaNacimiento,string telefono,int idTipoActor,long idFoto,int idClaseActor,int idEstadoLegal,string correo,string tercerNombre,string paginaWeb)
		{
			this.id_documento_identidad = idDocumentoIdentidad;
			this.numero_documento_identidad = numeroDocumentoIdentidad;
			this.primer_nombre = primerNombre;
			this.segundo_nombre = segundoNombre;
			this.fecha_nacimiento = fechaNacimiento;
			this.telefono = telefono;
			this.id_tipo_actor = idTipoActor;
			this.id_foto = idFoto;
			this.id_clase_actor = idClaseActor;
			this.id_estado_legal = idEstadoLegal;
			this.correo = correo;
			this.tercer_nombre = tercerNombre;
			this.pagina_web = paginaWeb;
			
		}
		protected void validarId(int idClaseActor, int idTipoActor, int idEstadoLegal, int idFoto, int idDocumentoIdentidad)
		{
			if(idClaseActor< 1){ throw new IdNoValidoException(idClaseActor , "clase actor"); }
			if(idTipoActor< 1){ throw new IdNoValidoException(idTipoActor , "tipo actor"); }
			if(idEstadoLegal< 1){ throw new IdNoValidoException(idEstadoLegal , "estado legal"); }
			if(idFoto< 1){ throw new IdNoValidoException(idFoto , "foto"); }
			if(idDocumentoIdentidad< 1){ throw new IdNoValidoException(idDocumentoIdentidad , "documento identidad"); }
			
		}
	}

    public partial class Actor_negocio
    {
		public Actor_negocio()
		{
		}
		public Actor_negocio(int idActor,int idRol,DateTime fechaInicio,DateTime fechaFin,string codigoNegocio,bool esVigente,String idUsuario)
		{
			setData(idActor,idRol,fechaInicio,fechaFin,codigoNegocio,esVigente,idUsuario);
			validarId(idActor, idRol);
		}
		
		public void setData(int idActor,int idRol,DateTime fechaInicio,DateTime fechaFin,string codigoNegocio,bool esVigente,String idUsuario)
		{
			this.id_actor = idActor;
			this.id_rol = idRol;
			this.fecha_inicio = fechaInicio;
			this.fecha_fin = fechaFin;
			this.codigo_negocio = codigoNegocio;
			this.es_vigente = esVigente;
			this.id_usuario = idUsuario;
			
		}
		protected void validarId(int idActor, int idRol)
		{
			if(idActor< 1){ throw new IdNoValidoException(idActor , "actor"); }
			if(idRol< 1){ throw new IdNoValidoException(idRol , "rol"); }
			
		}
	}

    public partial class Actor_negocio_por_sub_transaccion
    {
		public Actor_negocio_por_sub_transaccion()
		{
		}
		public Actor_negocio_por_sub_transaccion(long idSubTransaccion,int idRol,int idActorNegocio)
		{
			setData(idSubTransaccion,idRol,idActorNegocio);
			validarId(idActorNegocio, idRol);
		}
		
		public void setData(long idSubTransaccion,int idRol,int idActorNegocio)
		{
			this.id_sub_transaccion = idSubTransaccion;
			this.id_rol = idRol;
			this.id_actor_negocio = idActorNegocio;
			
		}
		protected void validarId(int idActorNegocio, int idRol)
		{
			if(idActorNegocio< 1){ throw new IdNoValidoException(idActorNegocio , "actor negocio"); }
			if(idRol< 1){ throw new IdNoValidoException(idRol , "rol"); }
			
		}
	}

    public partial class Actor_negocio_por_transaccion
    {
		public Actor_negocio_por_transaccion()
		{
		}
		public Actor_negocio_por_transaccion(long idTransaccion,int idRol,int idActorNegocio)
		{
			setData(idTransaccion,idRol,idActorNegocio);
			validarId(idActorNegocio, idRol, idTransaccion);
		}
		
		public void setData(long idTransaccion,int idRol,int idActorNegocio)
		{
			this.id_transaccion = idTransaccion;
			this.id_rol = idRol;
			this.id_actor_negocio = idActorNegocio;
			
		}
		protected void validarId(int idActorNegocio, int idRol, int idTransaccion)
		{
			if(idActorNegocio< 1){ throw new IdNoValidoException(idActorNegocio , "actor negocio"); }
			if(idRol< 1){ throw new IdNoValidoException(idRol , "rol"); }
			if(idTransaccion< 1){ throw new IdNoValidoException(idTransaccion , "transaccion"); }
			
		}
	}

    public partial class Actor_negocio_rol
    {
		public Actor_negocio_rol()
		{
		}
		public Actor_negocio_rol(int idActorNegocio,int idRol)
		{
			setData(idActorNegocio,idRol);
			validarId(idActorNegocio, idRol);
		}
		
		public void setData(int idActorNegocio,int idRol)
		{
			this.id_actor_negocio = idActorNegocio;
			this.id_rol = idRol;
			
		}
		protected void validarId(int idActorNegocio, int idRol)
		{
			if(idActorNegocio< 1){ throw new IdNoValidoException(idActorNegocio , "actor negocio"); }
			if(idRol< 1){ throw new IdNoValidoException(idRol , "rol"); }
			
		}
	}

    public partial class Atributo
    {
		public Atributo()
		{
		}
		public Atributo(string nombre,string regexp,int idRol)
		{
			setData(nombre,regexp,idRol);
			validarId(idRol);
		}
		
		public void setData(string nombre,string regexp,int idRol)
		{
			this.nombre = nombre;
			this.regexp = regexp;
			this.id_rol = idRol;
			
		}
		protected void validarId(int idRol)
		{
			if(idRol< 1){ throw new IdNoValidoException(idRol , "rol"); }
			
		}
	}

    public partial class Atributo_actor
    {
		public Atributo_actor()
		{
		}
		public Atributo_actor(int idActor,int idAtributo,string valor)
		{
			setData(idActor,idAtributo,valor);
			validarId(idActor, idAtributo);
		}
		
		public void setData(int idActor,int idAtributo,string valor)
		{
			this.id_actor = idActor;
			this.id_atributo = idAtributo;
			this.valor = valor;
			
		}
		protected void validarId(int idActor, int idAtributo)
		{
			if(idActor< 1){ throw new IdNoValidoException(idActor , "actor"); }
			if(idAtributo< 1){ throw new IdNoValidoException(idAtributo , "atributo"); }
			
		}
	}

    public partial class Caracteristica
    {
		public Caracteristica()
		{
		}
		public Caracteristica(string nombre,string descripcion,string regexp)
		{
			setData(nombre,descripcion,regexp);
			
		}
		
		public void setData(string nombre,string descripcion,string regexp)
		{
			this.nombre = nombre;
			this.descripcion = descripcion;
			this.regexp = regexp;
			
		}
		
	}

    public partial class Caracteristica_concepto
    {
		public Caracteristica_concepto()
		{
		}
		public Caracteristica_concepto(int idConcepto,int idCaracteristica,bool esRequerido)
		{
			setData(idConcepto,idCaracteristica,esRequerido);
			validarId(idCaracteristica, idConcepto);
		}
		
		public void setData(int idConcepto,int idCaracteristica,bool esRequerido)
		{
			this.id_concepto = idConcepto;
			this.id_caracteristica = idCaracteristica;
			this.es_requerido = esRequerido;
			
		}
		protected void validarId(int idCaracteristica, int idConcepto)
		{
			if(idCaracteristica< 1){ throw new IdNoValidoException(idCaracteristica , "caracteristica"); }
			if(idConcepto< 1){ throw new IdNoValidoException(idConcepto , "concepto"); }
			
		}
	}

    public partial class Categoria
    {
		public Categoria()
		{
		}
		public Categoria(string nombre,string descripcion,long idFoto,int idCategoriaPadre,int nivel)
		{
			setData(nombre,descripcion,idFoto,idCategoriaPadre,nivel);
			validarId(idCategoriaPadre, idFoto);
		}
		
		public void setData(string nombre,string descripcion,long idFoto,int idCategoriaPadre,int nivel)
		{
			this.nombre = nombre;
			this.descripcion = descripcion;
			this.id_foto = idFoto;
			this.id_categoria_padre = idCategoriaPadre;
			this.nivel = nivel;
			
		}
		protected void validarId(int idCategoriaPadre, int idFoto)
		{
			if(idCategoriaPadre< 1){ throw new IdNoValidoException(idCategoriaPadre , "categoria padre"); }
			if(idFoto< 1){ throw new IdNoValidoException(idFoto , "foto"); }
			
		}
	}

    public partial class Categoria_concepto
    {
		public Categoria_concepto()
		{
		}
		public Categoria_concepto(int idConcepto,int idCategoria)
		{
			setData(idConcepto,idCategoria);
			validarId(idCategoria, idConcepto);
		}
		
		public void setData(int idConcepto,int idCategoria)
		{
			this.id_concepto = idConcepto;
			this.id_categoria = idCategoria;
			
		}
		protected void validarId(int idCategoria, int idConcepto)
		{
			if(idCategoria< 1){ throw new IdNoValidoException(idCategoria , "categoria"); }
			if(idConcepto< 1){ throw new IdNoValidoException(idConcepto , "concepto"); }
			
		}
	}

    public partial class Clase_actor
    {
		public Clase_actor()
		{
		}
		public Clase_actor(string nombre,string descripcion,int idTipoActor)
		{
			setData(nombre,descripcion,idTipoActor);
			
		}
		
		public void setData(string nombre,string descripcion,int idTipoActor)
		{
			this.nombre = nombre;
			this.descripcion = descripcion;
			this.id_tipo_actor = idTipoActor;
			
		}
		
	}

    public partial class Comprobante
    {
		public Comprobante()
		{
		}
		public Comprobante(int idTipoComprobante,int idSerieComprobante,string numero,bool esValido,string numeroSerie)
		{
			setData(idTipoComprobante,idSerieComprobante,numero,esValido,numeroSerie);
			validarId(idTipoComprobante, idSerieComprobante);
		}
		
		public void setData(int idTipoComprobante,int idSerieComprobante,string numero,bool esValido,string numeroSerie)
		{
			this.id_tipo_comprobante = idTipoComprobante;
			this.id_serie_comprobante = idSerieComprobante;
			this.numero = numero;
			this.es_valido = esValido;
			this.numero_serie = numeroSerie;
			
		}
		protected void validarId(int idTipoComprobante, int idSerieComprobante)
		{
			if(idTipoComprobante< 1){ throw new IdNoValidoException(idTipoComprobante , "tipo comprobante"); }
			if(idSerieComprobante< 1){ throw new IdNoValidoException(idSerieComprobante , "serie comprobante"); }
			
		}
	}

    public partial class Concepto
    {
		public Concepto()
		{
		}
		public Concepto(string nombre,long idFoto)
		{
			setData(nombre,idFoto);
			validarId(idFoto);
		}
		
		public void setData(string nombre,long idFoto)
		{
			this.nombre = nombre;
			this.id_foto = idFoto;
			
		}
		protected void validarId(int idFoto)
		{
			if(idFoto< 1){ throw new IdNoValidoException(idFoto , "foto"); }
			
		}
	}

    public partial class Concepto_negocio
    {
		public Concepto_negocio()
		{
		}
		public Concepto_negocio(int idRol,string codigo,string codigoBarra,string nombre,string sufijo,string propiedades,int idUnidadMedidaPrimaria,int idModelo,int idPresentacion,decimal contenido,int idUnidadMedidaContenido,int idSubContenido,long idFoto,bool esVigente,int idUnidadMedidaSecundaria)
		{
			setData(idRol,codigo,codigoBarra,nombre,sufijo,propiedades,idUnidadMedidaPrimaria,idModelo,idPresentacion,contenido,idUnidadMedidaContenido,idSubContenido,idFoto,esVigente,idUnidadMedidaSecundaria);
			validarId(idSubContenido, idModelo, idFoto, idPresentacion, idUnidadMedidaPrimaria, idUnidadMedidaContenido, idUnidadMedidaSecundaria, idRol);
		}
		
		public void setData(int idRol,string codigo,string codigoBarra,string nombre,string sufijo,string propiedades,int idUnidadMedidaPrimaria,int idModelo,int idPresentacion,decimal contenido,int idUnidadMedidaContenido,int idSubContenido,long idFoto,bool esVigente,int idUnidadMedidaSecundaria)
		{
			this.id_rol = idRol;
			this.codigo = codigo;
			this.codigo_barra = codigoBarra;
			this.nombre = nombre;
			this.sufijo = sufijo;
			this.propiedades = propiedades;
			this.id_unidad_medida_primaria = idUnidadMedidaPrimaria;
			this.id_modelo = idModelo;
			this.id_presentacion = idPresentacion;
			this.contenido = contenido;
			this.id_unidad_medida_contenido = idUnidadMedidaContenido;
			this.id_sub_contenido = idSubContenido;
			this.id_foto = idFoto;
			this.es_vigente = esVigente;
			this.id_unidad_medida_secundaria = idUnidadMedidaSecundaria;
			
		}
		protected void validarId(int idSubContenido, int idModelo, int idFoto, int idPresentacion, int idUnidadMedidaPrimaria, int idUnidadMedidaContenido, int idUnidadMedidaSecundaria, int idRol)
		{
			if(idSubContenido< 1){ throw new IdNoValidoException(idSubContenido , "sub contenido"); }
			if(idModelo< 1){ throw new IdNoValidoException(idModelo , "modelo"); }
			if(idFoto< 1){ throw new IdNoValidoException(idFoto , "foto"); }
			if(idPresentacion< 1){ throw new IdNoValidoException(idPresentacion , "presentacion"); }
			if(idUnidadMedidaPrimaria< 1){ throw new IdNoValidoException(idUnidadMedidaPrimaria , "unidad medida primaria"); }
			if(idUnidadMedidaContenido< 1){ throw new IdNoValidoException(idUnidadMedidaContenido , "unidad medida contenido"); }
			if(idUnidadMedidaSecundaria< 1){ throw new IdNoValidoException(idUnidadMedidaSecundaria , "unidad medida secundaria"); }
			if(idRol< 1){ throw new IdNoValidoException(idRol , "rol"); }
			
		}
	}

    public partial class Cuenta_contable
    {
		public Cuenta_contable()
		{
		}
		public Cuenta_contable(int idPlan,string codigo,string nombre,string tipoCuenta,int nivelSaldo,int idTipoAnexo,int idTipoAnexoReferencia,int tasa,bool requiereCentroCosto,bool requiereDocumentoReferencia,bool requiereFechaVencimiento,bool requiereArea,bool requiereCuentaAjuste,bool requiereConciliacionBancos,bool requiereDocumentoReferencia2,bool requiereTipoMedioPago,bool registroManual,int idMonedaReferencia,int idCuentaCargo,int idCuentaAbono)
		{
			setData(idPlan,codigo,nombre,tipoCuenta,nivelSaldo,idTipoAnexo,idTipoAnexoReferencia,tasa,requiereCentroCosto,requiereDocumentoReferencia,requiereFechaVencimiento,requiereArea,requiereCuentaAjuste,requiereConciliacionBancos,requiereDocumentoReferencia2,requiereTipoMedioPago,registroManual,idMonedaReferencia,idCuentaCargo,idCuentaAbono);
			validarId(idPlan);
		}
		
		public void setData(int idPlan,string codigo,string nombre,string tipoCuenta,int nivelSaldo,int idTipoAnexo,int idTipoAnexoReferencia,int tasa,bool requiereCentroCosto,bool requiereDocumentoReferencia,bool requiereFechaVencimiento,bool requiereArea,bool requiereCuentaAjuste,bool requiereConciliacionBancos,bool requiereDocumentoReferencia2,bool requiereTipoMedioPago,bool registroManual,int idMonedaReferencia,int idCuentaCargo,int idCuentaAbono)
		{
			this.id_plan = idPlan;
			this.codigo = codigo;
			this.nombre = nombre;
			this.tipo_cuenta = tipoCuenta;
			this.nivel_saldo = nivelSaldo;
			this.id_tipo_anexo = idTipoAnexo;
			this.id_tipo_anexo_referencia = idTipoAnexoReferencia;
			this.tasa = tasa;
			this.requiere_centro_costo = requiereCentroCosto;
			this.requiere_documento_referencia = requiereDocumentoReferencia;
			this.requiere_fecha_vencimiento = requiereFechaVencimiento;
			this.requiere_area = requiereArea;
			this.requiere_cuenta_ajuste = requiereCuentaAjuste;
			this.requiere_conciliacion_bancos = requiereConciliacionBancos;
			this.requiere_documento_referencia2 = requiereDocumentoReferencia2;
			this.requiere_tipo_medio_pago = requiereTipoMedioPago;
			this.registro_manual = registroManual;
			this.id_moneda_referencia = idMonedaReferencia;
			this.id_cuenta_cargo = idCuentaCargo;
			this.id_cuenta_abono = idCuentaAbono;
			
		}
		protected void validarId(int idPlan)
		{
			if(idPlan< 1){ throw new IdNoValidoException(idPlan , "plan"); }
			
		}
	}

    public partial class Cuota
    {
		public Cuota()
		{
		}
		public Cuota(string codigo,string codigoUnicoBanco,DateTime fechaEmision,DateTime fechaVencimiento,decimal total,decimal capital,decimal interes,long idTransaccion,bool letraCambio,string comentario,bool cuotaInicial,long idComprobante,int idBanco,bool porCobrar)
		{
			setData(codigo,codigoUnicoBanco,fechaEmision,fechaVencimiento,total,capital,interes,idTransaccion,letraCambio,comentario,cuotaInicial,idComprobante,idBanco,porCobrar);
			validarId(idBanco, idTransaccion, idComprobante);
		}
		
		public void setData(string codigo,string codigoUnicoBanco,DateTime fechaEmision,DateTime fechaVencimiento,decimal total,decimal capital,decimal interes,long idTransaccion,bool letraCambio,string comentario,bool cuotaInicial,long idComprobante,int idBanco,bool porCobrar)
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
			
		}
		protected void validarId(int idBanco, int idTransaccion, int idComprobante)
		{
			if(idBanco< 1){ throw new IdNoValidoException(idBanco , "banco"); }
			if(idTransaccion< 1){ throw new IdNoValidoException(idTransaccion , "transaccion"); }
			if(idComprobante< 1){ throw new IdNoValidoException(idComprobante , "comprobante"); }
			
		}
	}

    public partial class Detalle_detalle_maestro
    {
		public Detalle_detalle_maestro()
		{
		}
		public Detalle_detalle_maestro(int idDetalleMaestroPrincipal,int idDetalleMaestroSecundario)
		{
			setData(idDetalleMaestroPrincipal,idDetalleMaestroSecundario);
			validarId(idDetalleMaestroPrincipal, idDetalleMaestroSecundario);
		}
		
		public void setData(int idDetalleMaestroPrincipal,int idDetalleMaestroSecundario)
		{
			this.id_detalle_maestro_principal = idDetalleMaestroPrincipal;
			this.id_detalle_maestro_secundario = idDetalleMaestroSecundario;
			
		}
		protected void validarId(int idDetalleMaestroPrincipal, int idDetalleMaestroSecundario)
		{
			if(idDetalleMaestroPrincipal< 1){ throw new IdNoValidoException(idDetalleMaestroPrincipal , "detalle maestro principal"); }
			if(idDetalleMaestroSecundario< 1){ throw new IdNoValidoException(idDetalleMaestroSecundario , "detalle maestro secundario"); }
			
		}
	}

    public partial class Detalle_maestro
    {
		public Detalle_maestro()
		{
		}
		public Detalle_maestro(int idMaestro,string codigo,string nombre,string valor,DateTime fechaRegistro)
		{
			setData(idMaestro,codigo,nombre,valor,fechaRegistro);
			validarId(idMaestro);
		}
		
		public void setData(int idMaestro,string codigo,string nombre,string valor,DateTime fechaRegistro)
		{
			this.id_maestro = idMaestro;
			this.codigo = codigo;
			this.nombre = nombre;
			this.valor = valor;
			this.fecha_registro = fechaRegistro;
			
		}
		protected void validarId(int idMaestro)
		{
			if(idMaestro< 1){ throw new IdNoValidoException(idMaestro , "maestro"); }
			
		}
	}

    public partial class Detalle_transaccion
    {
		public Detalle_transaccion()
		{
		}
		public Detalle_transaccion(long idTransaccion,decimal cantidad,int idConceptoNegocio,string detalle,decimal precioUnitario,decimal total,int idPrecio,decimal cantidadSecundaria,int indicadorMultiproposito,int idCuentaContable)
		{
			setData(idTransaccion,cantidad,idConceptoNegocio,detalle,precioUnitario,total,idPrecio,cantidadSecundaria,indicadorMultiproposito,idCuentaContable);
			validarId(idCuentaContable, idTransaccion, idConceptoNegocio, idPrecio);
		}
		
		public void setData(long idTransaccion,decimal cantidad,int idConceptoNegocio,string detalle,decimal precioUnitario,decimal total,int idPrecio,decimal cantidadSecundaria,int indicadorMultiproposito,int idCuentaContable)
		{
			this.id_transaccion = idTransaccion;
			this.cantidad = cantidad;
			this.id_concepto_negocio = idConceptoNegocio;
			this.detalle = detalle;
			this.precio_unitario = precioUnitario;
			this.total = total;
			this.id_precio = idPrecio;
			this.cantidad_secundaria = cantidadSecundaria;
			this.indicadorMultiproposito = indicadorMultiproposito;
			this.id_cuenta_contable = idCuentaContable;
			
		}
		protected void validarId(int idCuentaContable, int idTransaccion, int idConceptoNegocio, int idPrecio)
		{
			if(idCuentaContable< 1){ throw new IdNoValidoException(idCuentaContable , "cuenta contable"); }
			if(idTransaccion< 1){ throw new IdNoValidoException(idTransaccion , "transaccion"); }
			if(idConceptoNegocio< 1){ throw new IdNoValidoException(idConceptoNegocio , "concepto negocio"); }
			if(idPrecio< 1){ throw new IdNoValidoException(idPrecio , "precio"); }
			
		}
	}

    public partial class Direccion
    {
		public Direccion()
		{
		}
		public Direccion(int idTipoDireccion,int idNacion,int idUbigeo,string detalle,int idActor,int idTipoVia,int idTipoZona,bool esPrincipal,bool esActivo)
		{
			setData(idTipoDireccion,idNacion,idUbigeo,detalle,idActor,idTipoVia,idTipoZona,esPrincipal,esActivo);
			validarId(idActor, idTipoDireccion, idNacion, idTipoVia, idTipoZona, idUbigeo);
		}
		
		public void setData(int idTipoDireccion,int idNacion,int idUbigeo,string detalle,int idActor,int idTipoVia,int idTipoZona,bool esPrincipal,bool esActivo)
		{
			this.id_tipo_direccion = idTipoDireccion;
			this.id_nacion = idNacion;
			this.id_ubigeo = idUbigeo;
			this.detalle = detalle;
			this.id_actor = idActor;
			this.id_tipo_via = idTipoVia;
			this.id_tipo_zona = idTipoZona;
			this.es_principal = esPrincipal;
			this.es_activo = esActivo;
			
		}
		protected void validarId(int idActor, int idTipoDireccion, int idNacion, int idTipoVia, int idTipoZona, int idUbigeo)
		{
			if(idActor< 1){ throw new IdNoValidoException(idActor , "actor"); }
			if(idTipoDireccion< 1){ throw new IdNoValidoException(idTipoDireccion , "tipo direccion"); }
			if(idNacion< 1){ throw new IdNoValidoException(idNacion , "nacion"); }
			if(idTipoVia< 1){ throw new IdNoValidoException(idTipoVia , "tipo via"); }
			if(idTipoZona< 1){ throw new IdNoValidoException(idTipoZona , "tipo zona"); }
			if(idUbigeo< 1){ throw new IdNoValidoException(idUbigeo , "ubigeo"); }
			
		}
	}

    public partial class Documento_identidad
    {
		public Documento_identidad()
		{
		}
		public Documento_identidad(string nombreCorto,string nombreLargo,int longitud,string mascara,string detalleLegal,bool esPrimario,bool esParaNegocio)
		{
			setData(nombreCorto,nombreLargo,longitud,mascara,detalleLegal,esPrimario,esParaNegocio);
			
		}
		
		public void setData(string nombreCorto,string nombreLargo,int longitud,string mascara,string detalleLegal,bool esPrimario,bool esParaNegocio)
		{
			this.nombre_corto = nombreCorto;
			this.nombre_largo = nombreLargo;
			this.longitud = longitud;
			this.mascara = mascara;
			this.detalle_legal = detalleLegal;
			this.es_primario = esPrimario;
			this.es_para_negocio = esParaNegocio;
			
		}
		
	}

    public partial class Documento_identidad_por_tipo_actor
    {
		public Documento_identidad_por_tipo_actor()
		{
		}
		public Documento_identidad_por_tipo_actor(int identityDocumentId,int actorTypeId)
		{
			setData(identityDocumentId,actorTypeId);
			validarId(identityDocumentId, actorTypeId);
		}
		
		public void setData(int identityDocumentId,int actorTypeId)
		{
			this.identityDocumentId = identityDocumentId;
			this.actorTypeId = actorTypeId;
			
		}
		protected void validarId(int identityDocumentId, int actorTypeId)
		{
			if(identityDocumentId< 1){ throw new IdNoValidoException(identityDocumentId , "ntityDocumentId"); }
			if(actorTypeId< 1){ throw new IdNoValidoException(actorTypeId , "orTypeId"); }
			
		}
	}

    public partial class Estado_cuota
    {
		public Estado_cuota()
		{
		}
		public Estado_cuota(int idCuota,int idEmpleado,int idEstado,DateTime fecha,string comentario)
		{
			setData(idCuota,idEmpleado,idEstado,fecha,comentario);
			validarId(idEmpleado, idEstado, idCuota);
		}
		
		public void setData(int idCuota,int idEmpleado,int idEstado,DateTime fecha,string comentario)
		{
			this.id_cuota = idCuota;
			this.id_empleado = idEmpleado;
			this.id_estado = idEstado;
			this.fecha = fecha;
			this.comentario = comentario;
			
		}
		protected void validarId(int idEmpleado, int idEstado, int idCuota)
		{
			if(idEmpleado< 1){ throw new IdNoValidoException(idEmpleado , "empleado"); }
			if(idEstado< 1){ throw new IdNoValidoException(idEstado , "estado"); }
			if(idCuota< 1){ throw new IdNoValidoException(idCuota , "cuota"); }
			
		}
	}

    public partial class Estado_legal
    {
		public Estado_legal()
		{
		}
		public Estado_legal(string nombre,string descripcion,int idTipoActor)
		{
			setData(nombre,descripcion,idTipoActor);
			
		}
		
		public void setData(string nombre,string descripcion,int idTipoActor)
		{
			this.nombre = nombre;
			this.descripcion = descripcion;
			this.id_tipo_actor = idTipoActor;
			
		}
		
	}

    public partial class Estado_sub_transaccion
    {
		public Estado_sub_transaccion()
		{
		}
		public Estado_sub_transaccion(long idSubTransaccion,int idEmpleado,int idEstado,DateTime fecha,string comentario)
		{
			setData(idSubTransaccion,idEmpleado,idEstado,fecha,comentario);
			validarId(idEmpleado, idEstado);
		}
		
		public void setData(long idSubTransaccion,int idEmpleado,int idEstado,DateTime fecha,string comentario)
		{
			this.id_sub_transaccion = idSubTransaccion;
			this.id_empleado = idEmpleado;
			this.id_estado = idEstado;
			this.fecha = fecha;
			this.comentario = comentario;
			
		}
		protected void validarId(int idEmpleado, int idEstado)
		{
			if(idEmpleado< 1){ throw new IdNoValidoException(idEmpleado , "empleado"); }
			if(idEstado< 1){ throw new IdNoValidoException(idEstado , "estado"); }
			
		}
	}

    public partial class Estado_transaccion
    {
		public Estado_transaccion()
		{
		}
		public Estado_transaccion(long idTransaccion,int idEmpleado,int idEstado,DateTime fecha,string comentario)
		{
			setData(idTransaccion,idEmpleado,idEstado,fecha,comentario);
			validarId(idEmpleado, idEmpleado, idTransaccion);
		}
		
		public void setData(long idTransaccion,int idEmpleado,int idEstado,DateTime fecha,string comentario)
		{
			this.id_transaccion = idTransaccion;
			this.id_empleado = idEmpleado;
			this.id_estado = idEstado;
			this.fecha = fecha;
			this.comentario = comentario;
			
		}
		protected void validarId(int idEmpleado, int idEmpleado, int idTransaccion)
		{
			if(idEmpleado< 1){ throw new IdNoValidoException(idEmpleado , "empleado"); }
			if(idEmpleado< 1){ throw new IdNoValidoException(idEmpleado , "empleado"); }
			if(idTransaccion< 1){ throw new IdNoValidoException(idTransaccion , "transaccion"); }
			
		}
	}

    public partial class Foto
    {
		public Foto()
		{
		}
		public Foto(string nombre,byte[] imagen)
		{
			setData(nombre,imagen);
			
		}
		
		public void setData(string nombre,byte[] imagen)
		{
			this.nombre = nombre;
			this.imagen = imagen;
			
		}
		
	}

    public partial class Maestro
    {
		public Maestro()
		{
		}
		public Maestro(string codigo,string nombre,int idTipo)
		{
			setData(codigo,nombre,idTipo);
			
		}
		
		public void setData(string codigo,string nombre,int idTipo)
		{
			this.codigo = codigo;
			this.nombre = nombre;
			this.idTipo = idTipo;
			
		}
		
	}

    public partial class Mapeo_cuenta_contable_concepto
    {
		public Mapeo_cuenta_contable_concepto()
		{
		}
		public Mapeo_cuenta_contable_concepto(int idConceptoNegocio,int idCuentaContable,bool esVigente)
		{
			setData(idConceptoNegocio,idCuentaContable,esVigente);
			validarId(idConceptoNegocio, idCuentaContable);
		}
		
		public void setData(int idConceptoNegocio,int idCuentaContable,bool esVigente)
		{
			this.id_concepto_negocio = idConceptoNegocio;
			this.id_cuenta_contable = idCuentaContable;
			this.es_vigente = esVigente;
			
		}
		protected void validarId(int idConceptoNegocio, int idCuentaContable)
		{
			if(idConceptoNegocio< 1){ throw new IdNoValidoException(idConceptoNegocio , "concepto negocio"); }
			if(idCuentaContable< 1){ throw new IdNoValidoException(idCuentaContable , "cuenta contable"); }
			
		}
	}

    public partial class Marca
    {
		public Marca()
		{
		}
		public Marca(string nombre,long idFoto)
		{
			setData(nombre,idFoto);
			validarId(idFoto);
		}
		
		public void setData(string nombre,long idFoto)
		{
			this.nombre = nombre;
			this.id_foto = idFoto;
			
		}
		protected void validarId(int idFoto)
		{
			if(idFoto< 1){ throw new IdNoValidoException(idFoto , "foto"); }
			
		}
	}

    public partial class Marca_concepto
    {
		public Marca_concepto()
		{
		}
		public Marca_concepto(int idMarca,int idConcepto)
		{
			setData(idMarca,idConcepto);
			validarId(idConcepto, idMarca);
		}
		
		public void setData(int idMarca,int idConcepto)
		{
			this.id_marca = idMarca;
			this.id_concepto = idConcepto;
			
		}
		protected void validarId(int idConcepto, int idMarca)
		{
			if(idConcepto< 1){ throw new IdNoValidoException(idConcepto , "concepto"); }
			if(idMarca< 1){ throw new IdNoValidoException(idMarca , "marca"); }
			
		}
	}

    public partial class Modelo
    {
		public Modelo()
		{
		}
		public Modelo(string nombre,string descripcion,int idMarcaConcepto,long idFoto)
		{
			setData(nombre,descripcion,idMarcaConcepto,idFoto);
			validarId(idMarcaConcepto, idFoto);
		}
		
		public void setData(string nombre,string descripcion,int idMarcaConcepto,long idFoto)
		{
			this.nombre = nombre;
			this.descripcion = descripcion;
			this.id_marca_concepto = idMarcaConcepto;
			this.id_foto = idFoto;
			
		}
		protected void validarId(int idMarcaConcepto, int idFoto)
		{
			if(idMarcaConcepto< 1){ throw new IdNoValidoException(idMarcaConcepto , "marca concepto"); }
			if(idFoto< 1){ throw new IdNoValidoException(idFoto , "foto"); }
			
		}
	}

    public partial class Modo_pago
    {
		public Modo_pago()
		{
		}
		public Modo_pago(string nombre,string descripcion,string mascara)
		{
			setData(nombre,descripcion,mascara);
			
		}
		
		public void setData(string nombre,string descripcion,string mascara)
		{
			this.nombre = nombre;
			this.descripcion = descripcion;
			this.mascara = mascara;
			
		}
		
	}

    public partial class Pago_cuota
    {
		public Pago_cuota()
		{
		}
		public Pago_cuota(int idCuota,long idTransaccion)
		{
			setData(idCuota,idTransaccion);
			validarId(idCuota, idTransaccion);
		}
		
		public void setData(int idCuota,long idTransaccion)
		{
			this.id_cuota = idCuota;
			this.id_transaccion = idTransaccion;
			
		}
		protected void validarId(int idCuota, int idTransaccion)
		{
			if(idCuota< 1){ throw new IdNoValidoException(idCuota , "cuota"); }
			if(idTransaccion< 1){ throw new IdNoValidoException(idTransaccion , "transaccion"); }
			
		}
	}

    public partial class Parametro_actor_negocio
    {
		public Parametro_actor_negocio()
		{
		}
		public Parametro_actor_negocio(int idActorNegocio,int idParametro,int idValorParametro,string valor)
		{
			setData(idActorNegocio,idParametro,idValorParametro,valor);
			validarId(idActorNegocio, idParametro, idValorParametro);
		}
		
		public void setData(int idActorNegocio,int idParametro,int idValorParametro,string valor)
		{
			this.id_actor_negocio = idActorNegocio;
			this.id_parametro = idParametro;
			this.id_valor_parametro = idValorParametro;
			this.valor = valor;
			
		}
		protected void validarId(int idActorNegocio, int idParametro, int idValorParametro)
		{
			if(idActorNegocio< 1){ throw new IdNoValidoException(idActorNegocio , "actor negocio"); }
			if(idParametro< 1){ throw new IdNoValidoException(idParametro , "parametro"); }
			if(idValorParametro< 1){ throw new IdNoValidoException(idValorParametro , "valor parametro"); }
			
		}
	}

    public partial class Parametro_por_rol
    {
		public Parametro_por_rol()
		{
		}
		public Parametro_por_rol(int idRol,int idParametro,int numeroOrden,int tipo)
		{
			setData(idRol,idParametro,numeroOrden,tipo);
			validarId(idParametro, idRol);
		}
		
		public void setData(int idRol,int idParametro,int numeroOrden,int tipo)
		{
			this.id_rol = idRol;
			this.id_parametro = idParametro;
			this.numero_orden = numeroOrden;
			this.tipo = tipo;
			
		}
		protected void validarId(int idParametro, int idRol)
		{
			if(idParametro< 1){ throw new IdNoValidoException(idParametro , "parametro"); }
			if(idRol< 1){ throw new IdNoValidoException(idRol , "rol"); }
			
		}
	}

    public partial class Parametro_por_unidad_negocio
    {
		public Parametro_por_unidad_negocio()
		{
		}
		public Parametro_por_unidad_negocio(int idUnidadNegocio,int idParametro,int numeroOrden,int tipo)
		{
			setData(idUnidadNegocio,idParametro,numeroOrden,tipo);
			validarId(idUnidadNegocio, idParametro);
		}
		
		public void setData(int idUnidadNegocio,int idParametro,int numeroOrden,int tipo)
		{
			this.id_unidad_negocio = idUnidadNegocio;
			this.id_parametro = idParametro;
			this.numero_orden = numeroOrden;
			this.tipo = tipo;
			
		}
		protected void validarId(int idUnidadNegocio, int idParametro)
		{
			if(idUnidadNegocio< 1){ throw new IdNoValidoException(idUnidadNegocio , "unidad negocio"); }
			if(idParametro< 1){ throw new IdNoValidoException(idParametro , "parametro"); }
			
		}
	}

    public partial class Parametro_subtransaccion
    {
		public Parametro_subtransaccion()
		{
		}
		public Parametro_subtransaccion(long idSubTransaccion,int idParametro,int idValorParametro,string valor)
		{
			setData(idSubTransaccion,idParametro,idValorParametro,valor);
			validarId(idValorParametro, idParametro);
		}
		
		public void setData(long idSubTransaccion,int idParametro,int idValorParametro,string valor)
		{
			this.id_sub_transaccion = idSubTransaccion;
			this.id_parametro = idParametro;
			this.id_valor_parametro = idValorParametro;
			this.valor = valor;
			
		}
		protected void validarId(int idValorParametro, int idParametro)
		{
			if(idValorParametro< 1){ throw new IdNoValidoException(idValorParametro , "valor parametro"); }
			if(idParametro< 1){ throw new IdNoValidoException(idParametro , "parametro"); }
			
		}
	}

    public partial class Parametro_transaccion
    {
		public Parametro_transaccion()
		{
		}
		public Parametro_transaccion(long idTransaccion,int idParametro,int idValorParametro,string valor)
		{
			setData(idTransaccion,idParametro,idValorParametro,valor);
			validarId(idValorParametro, idTransaccion);
		}
		
		public void setData(long idTransaccion,int idParametro,int idValorParametro,string valor)
		{
			this.id_transaccion = idTransaccion;
			this.id_parametro = idParametro;
			this.id_valor_parametro = idValorParametro;
			this.valor = valor;
			
		}
		protected void validarId(int idValorParametro, int idTransaccion)
		{
			if(idValorParametro< 1){ throw new IdNoValidoException(idValorParametro , "valor parametro"); }
			if(idTransaccion< 1){ throw new IdNoValidoException(idTransaccion , "transaccion"); }
			
		}
	}

    public partial class Parentesco
    {
		public Parentesco()
		{
		}
		public Parentesco(int idActorPrincipal,int idParentesco,int idTipoParentesco,DateTime fechaInicio,DateTime fechaFin)
		{
			setData(idActorPrincipal,idParentesco,idTipoParentesco,fechaInicio,fechaFin);
			validarId(idActorPrincipal, idParentesco, idTipoParentesco);
		}
		
		public void setData(int idActorPrincipal,int idParentesco,int idTipoParentesco,DateTime fechaInicio,DateTime fechaFin)
		{
			this.id_actor_principal = idActorPrincipal;
			this.id_parentesco = idParentesco;
			this.id_tipo_parentesco = idTipoParentesco;
			this.fecha_inicio = fechaInicio;
			this.fecha_fin = fechaFin;
			
		}
		protected void validarId(int idActorPrincipal, int idParentesco, int idTipoParentesco)
		{
			if(idActorPrincipal< 1){ throw new IdNoValidoException(idActorPrincipal , "actor principal"); }
			if(idParentesco< 1){ throw new IdNoValidoException(idParentesco , "parentesco"); }
			if(idTipoParentesco< 1){ throw new IdNoValidoException(idTipoParentesco , "tipo parentesco"); }
			
		}
	}

    public partial class Plan_contable
    {
		public Plan_contable()
		{
		}
		public Plan_contable(string nombre,string descripcion,DateTime fechaInicio,DateTime fechaFin,bool esVigente)
		{
			setData(nombre,descripcion,fechaInicio,fechaFin,esVigente);
			
		}
		
		public void setData(string nombre,string descripcion,DateTime fechaInicio,DateTime fechaFin,bool esVigente)
		{
			this.nombre = nombre;
			this.descripcion = descripcion;
			this.fecha_inicio = fechaInicio;
			this.fecha_fin = fechaFin;
			this.es_vigente = esVigente;
			
		}
		
	}

    public partial class Precio
    {
		public Precio()
		{
		}
		public Precio(int idActorNegocio,int idUnidadNegocio,int idConceptoNegocio,decimal importe,int idTarifa,int idMoneda,DateTime fechaInicio,DateTime fechaFin,DateTime fechaModificacion,bool indicadorMultiproposito,bool esVigente)
		{
			setData(idActorNegocio,idUnidadNegocio,idConceptoNegocio,importe,idTarifa,idMoneda,fechaInicio,fechaFin,fechaModificacion,indicadorMultiproposito,esVigente);
			validarId(idActorNegocio, idUnidadNegocio, idMoneda, idConceptoNegocio, idTarifa);
		}
		
		public void setData(int idActorNegocio,int idUnidadNegocio,int idConceptoNegocio,decimal importe,int idTarifa,int idMoneda,DateTime fechaInicio,DateTime fechaFin,DateTime fechaModificacion,bool indicadorMultiproposito,bool esVigente)
		{
			this.id_actor_negocio = idActorNegocio;
			this.id_unidad_negocio = idUnidadNegocio;
			this.id_concepto_negocio = idConceptoNegocio;
			this.importe = importe;
			this.id_tarifa = idTarifa;
			this.id_moneda = idMoneda;
			this.fecha_inicio = fechaInicio;
			this.fecha_fin = fechaFin;
			this.fecha_modificacion = fechaModificacion;
			this.indicador_multiproposito = indicadorMultiproposito;
			this.es_vigente = esVigente;
			
		}
		protected void validarId(int idActorNegocio, int idUnidadNegocio, int idMoneda, int idConceptoNegocio, int idTarifa)
		{
			if(idActorNegocio< 1){ throw new IdNoValidoException(idActorNegocio , "actor negocio"); }
			if(idUnidadNegocio< 1){ throw new IdNoValidoException(idUnidadNegocio , "unidad negocio"); }
			if(idMoneda< 1){ throw new IdNoValidoException(idMoneda , "moneda"); }
			if(idConceptoNegocio< 1){ throw new IdNoValidoException(idConceptoNegocio , "concepto negocio"); }
			if(idTarifa< 1){ throw new IdNoValidoException(idTarifa , "tarifa"); }
			
		}
	}

    public partial class Presentacion
    {
		public Presentacion()
		{
		}
		public Presentacion(string nombre,string descripcion)
		{
			setData(nombre,descripcion);
			
		}
		
		public void setData(string nombre,string descripcion)
		{
			this.nombre = nombre;
			this.descripcion = descripcion;
			
		}
		
	}

    public partial class Referencia_detalle_transaccion
    {
		public Referencia_detalle_transaccion()
		{
		}
		public Referencia_detalle_transaccion(long idDetalle,long idTransaccion,decimal cantidad)
		{
			setData(idDetalle,idTransaccion,cantidad);
			validarId(idDetalle, idTransaccion);
		}
		
		public void setData(long idDetalle,long idTransaccion,decimal cantidad)
		{
			this.id_detalle = idDetalle;
			this.id_transaccion = idTransaccion;
			this.cantidad = cantidad;
			
		}
		protected void validarId(int idDetalle, int idTransaccion)
		{
			if(idDetalle< 1){ throw new IdNoValidoException(idDetalle , "detalle"); }
			if(idTransaccion< 1){ throw new IdNoValidoException(idTransaccion , "transaccion"); }
			
		}
	}

    public partial class Registro_sucesos
    {
		public Registro_sucesos()
		{
		}
		public Registro_sucesos(string usuario,string ubicacion,string suceso,int nivel,int idDestinatario)
		{
			setData(usuario,ubicacion,suceso,nivel,idDestinatario);
			validarId(idDestinatario);
		}
		
		public void setData(string usuario,string ubicacion,string suceso,int nivel,int idDestinatario)
		{
			this.usuario = usuario;
			this.ubicacion = ubicacion;
			this.suceso = suceso;
			this.nivel = nivel;
			this.id_destinatario = idDestinatario;
			
		}
		protected void validarId(int idDestinatario)
		{
			if(idDestinatario< 1){ throw new IdNoValidoException(idDestinatario , "destinatario"); }
			
		}
	}

    public partial class Rol
    {
		public Rol()
		{
		}
		public Rol(string nombre,string descripcion,bool esPredeterminado,int idRolPadre,int aplicaA)
		{
			setData(nombre,descripcion,esPredeterminado,idRolPadre,aplicaA);
			validarId(idRolPadre);
		}
		
		public void setData(string nombre,string descripcion,bool esPredeterminado,int idRolPadre,int aplicaA)
		{
			this.nombre = nombre;
			this.descripcion = descripcion;
			this.es_predeterminado = esPredeterminado;
			this.id_rol_padre = idRolPadre;
			this.aplica_a = aplicaA;
			
		}
		protected void validarId(int idRolPadre)
		{
			if(idRolPadre< 1){ throw new IdNoValidoException(idRolPadre , "rol padre"); }
			
		}
	}

    public partial class Rol_por_tipo_actor
    {
		public Rol_por_tipo_actor()
		{
		}
		public Rol_por_tipo_actor(int idRol,int idTipoActor)
		{
			setData(idRol,idTipoActor);
			validarId(idTipoActor, idRol);
		}
		
		public void setData(int idRol,int idTipoActor)
		{
			this.id_rol = idRol;
			this.id_tipo_actor = idTipoActor;
			
		}
		protected void validarId(int idTipoActor, int idRol)
		{
			if(idTipoActor< 1){ throw new IdNoValidoException(idTipoActor , "tipo actor"); }
			if(idRol< 1){ throw new IdNoValidoException(idRol , "rol"); }
			
		}
	}

    public partial class Rol_por_unidad_negocio
    {
		public Rol_por_unidad_negocio()
		{
		}
		public Rol_por_unidad_negocio(int idRol,int idUnidadNegocio,int numeroOrden)
		{
			setData(idRol,idUnidadNegocio,numeroOrden);
			validarId(idRol, idUnidadNegocio);
		}
		
		public void setData(int idRol,int idUnidadNegocio,int numeroOrden)
		{
			this.id_rol = idRol;
			this.id_unidad_negocio = idUnidadNegocio;
			this.numero_orden = numeroOrden;
			
		}
		protected void validarId(int idRol, int idUnidadNegocio)
		{
			if(idRol< 1){ throw new IdNoValidoException(idRol , "rol"); }
			if(idUnidadNegocio< 1){ throw new IdNoValidoException(idUnidadNegocio , "unidad negocio"); }
			
		}
	}

    public partial class Serie_comprobante
    {
		public Serie_comprobante()
		{
		}
		public Serie_comprobante(string numero,int idPropietario,bool esVigente,int idTipoComprobante,string proximoNumero,bool esAutonumerable,int idModeloComprobante)
		{
			setData(numero,idPropietario,esVigente,idTipoComprobante,proximoNumero,esAutonumerable,idModeloComprobante);
			validarId(idTipoComprobante, idPropietario, idModeloComprobante);
		}
		
		public void setData(string numero,int idPropietario,bool esVigente,int idTipoComprobante,string proximoNumero,bool esAutonumerable,int idModeloComprobante)
		{
			this.numero = numero;
			this.id_propietario = idPropietario;
			this.es_vigente = esVigente;
			this.id_tipo_comprobante = idTipoComprobante;
			this.proximo_numero = proximoNumero;
			this.es_autonumerable = esAutonumerable;
			this.id_modelo_comprobante = idModeloComprobante;
			
		}
		protected void validarId(int idTipoComprobante, int idPropietario, int idModeloComprobante)
		{
			if(idTipoComprobante< 1){ throw new IdNoValidoException(idTipoComprobante , "tipo comprobante"); }
			if(idPropietario< 1){ throw new IdNoValidoException(idPropietario , "propietario"); }
			if(idModeloComprobante< 1){ throw new IdNoValidoException(idModeloComprobante , "modelo comprobante"); }
			
		}
	}

    public partial class sysdiagrams
    {
		public sysdiagrams()
		{
		}
		public sysdiagrams(String name,int principalId,int diagramId,int version,String definition)
		{
			setData(name,principalId,diagramId,version,definition);
			
		}
		
		public void setData(String name,int principalId,int diagramId,int version,String definition)
		{
			this.name = name;
			this.principal_id = principalId;
			this.diagram_id = diagramId;
			this.version = version;
			this.definition = definition;
			
		}
		
	}

    public partial class Tarifa
    {
		public Tarifa()
		{
		}
		public Tarifa(string nombre,string descripcion,int tipo)
		{
			setData(nombre,descripcion,tipo);
			
		}
		
		public void setData(string nombre,string descripcion,int tipo)
		{
			this.nombre = nombre;
			this.descripcion = descripcion;
			this.tipo = tipo;
			
		}
		
	}

    public partial class Tipo_actor
    {
		public Tipo_actor()
		{
		}
		public Tipo_actor(string nombre,string primeraDenominacion,string segundaDenominacion,string dominacionDeClase)
		{
			setData(nombre,primeraDenominacion,segundaDenominacion,dominacionDeClase);
			
		}
		
		public void setData(string nombre,string primeraDenominacion,string segundaDenominacion,string dominacionDeClase)
		{
			this.nombre = nombre;
			this.primera_denominacion = primeraDenominacion;
			this.segunda_denominacion = segundaDenominacion;
			this.dominacion_de_clase = dominacionDeClase;
			
		}
		
	}

    public partial class Tipo_cambio
    {
		public Tipo_cambio()
		{
		}
		public Tipo_cambio(int idMoneda,String fecha,decimal valorCompra,decimal valorVenta)
		{
			setData(idMoneda,fecha,valorCompra,valorVenta);
			validarId(idMoneda);
		}
		
		public void setData(int idMoneda,String fecha,decimal valorCompra,decimal valorVenta)
		{
			this.idMoneda = idMoneda;
			this.fecha = fecha;
			this.valorCompra = valorCompra;
			this.valorVenta = valorVenta;
			
		}
		protected void validarId(int idMoneda)
		{
			if(idMoneda< 1){ throw new IdNoValidoException(idMoneda , "oneda"); }
			
		}
	}

    public partial class Tipo_parentesco
    {
		public Tipo_parentesco()
		{
		}
		public Tipo_parentesco(string nombre,string descripcion)
		{
			setData(nombre,descripcion);
			
		}
		
		public void setData(string nombre,string descripcion)
		{
			this.nombre = nombre;
			this.descripcion = descripcion;
			
		}
		
	}

    public partial class Tipo_transaccion
    {
		public Tipo_transaccion()
		{
		}
		public Tipo_transaccion(string nombre,string descripcion,int accionesDeEntradaOSalida,int idTipoTransaccionPadre,string cuenta,int idTipoComprobanteDefecto)
		{
			setData(nombre,descripcion,accionesDeEntradaOSalida,idTipoTransaccionPadre,cuenta,idTipoComprobanteDefecto);
			validarId(idTipoTransaccionPadre);
		}
		
		public void setData(string nombre,string descripcion,int accionesDeEntradaOSalida,int idTipoTransaccionPadre,string cuenta,int idTipoComprobanteDefecto)
		{
			this.nombre = nombre;
			this.descripcion = descripcion;
			this.acciones_de_entrada_o_salida = accionesDeEntradaOSalida;
			this.id_tipo_transaccion_padre = idTipoTransaccionPadre;
			this.cuenta = cuenta;
			this.id_tipo_comprobante_defecto = idTipoComprobanteDefecto;
			
		}
		protected void validarId(int idTipoTransaccionPadre)
		{
			if(idTipoTransaccionPadre< 1){ throw new IdNoValidoException(idTipoTransaccionPadre , "tipo transaccion padre"); }
			
		}
	}

    public partial class Tipo_transaccion_tipo_comprobante
    {
		public Tipo_transaccion_tipo_comprobante()
		{
		}
		public Tipo_transaccion_tipo_comprobante(int idTipoTransaccion,int idTipoComprobante,bool esPropio)
		{
			setData(idTipoTransaccion,idTipoComprobante,esPropio);
			validarId(idTipoComprobante, idTipoTransaccion);
		}
		
		public void setData(int idTipoTransaccion,int idTipoComprobante,bool esPropio)
		{
			this.id_tipo_transaccion = idTipoTransaccion;
			this.id_tipo_comprobante = idTipoComprobante;
			this.es_propio = esPropio;
			
		}
		protected void validarId(int idTipoComprobante, int idTipoTransaccion)
		{
			if(idTipoComprobante< 1){ throw new IdNoValidoException(idTipoComprobante , "tipo comprobante"); }
			if(idTipoTransaccion< 1){ throw new IdNoValidoException(idTipoTransaccion , "tipo transaccion"); }
			
		}
	}

    public partial class Transaccion
    {
		public Transaccion()
		{
		}
		public Transaccion(string codigo,long idTransaccionPadre,DateTime fechaRegistroSistema,int idTipoTransaccion,int idUnidadNegocio,bool esConcreta,DateTime fechaInicio,DateTime fechaFin,long idComprobante,string comentario,DateTime fechaRegistroContable,int idEmpleado,decimal importeTotal,int idActorNegocioInterno,int idMoneda,decimal tipoCambio,long idTransaccionReferencia,int idActorNegocioExterno)
		{
			setData(codigo,idTransaccionPadre,fechaRegistroSistema,idTipoTransaccion,idUnidadNegocio,esConcreta,fechaInicio,fechaFin,idComprobante,comentario,fechaRegistroContable,idEmpleado,importeTotal,idActorNegocioInterno,idMoneda,tipoCambio,idTransaccionReferencia,idActorNegocioExterno);
			validarId(idEmpleado, idActorNegocioExterno, idActorNegocioInterno, idComprobante, idMoneda, idTipoTransaccion, idTransaccionPadre, idTransaccionReferencia);
		}
		
		public void setData(string codigo,long idTransaccionPadre,DateTime fechaRegistroSistema,int idTipoTransaccion,int idUnidadNegocio,bool esConcreta,DateTime fechaInicio,DateTime fechaFin,long idComprobante,string comentario,DateTime fechaRegistroContable,int idEmpleado,decimal importeTotal,int idActorNegocioInterno,int idMoneda,decimal tipoCambio,long idTransaccionReferencia,int idActorNegocioExterno)
		{
			this.codigo = codigo;
			this.id_transaccion_padre = idTransaccionPadre;
			this.fecha_registro_sistema = fechaRegistroSistema;
			this.id_tipo_transaccion = idTipoTransaccion;
			this.id_unidad_negocio = idUnidadNegocio;
			this.es_concreta = esConcreta;
			this.fecha_inicio = fechaInicio;
			this.fecha_fin = fechaFin;
			this.id_comprobante = idComprobante;
			this.comentario = comentario;
			this.fecha_registro_contable = fechaRegistroContable;
			this.id_empleado = idEmpleado;
			this.importe_total = importeTotal;
			this.id_actor_negocio_interno = idActorNegocioInterno;
			this.id_moneda = idMoneda;
			this.tipo_cambio = tipoCambio;
			this.id_transaccion_referencia = idTransaccionReferencia;
			this.id_actor_negocio_externo = idActorNegocioExterno;
			
		}
		protected void validarId(int idEmpleado, int idActorNegocioExterno, int idActorNegocioInterno, int idComprobante, int idMoneda, int idTipoTransaccion, int idTransaccionPadre, int idTransaccionReferencia)
		{
			if(idEmpleado< 1){ throw new IdNoValidoException(idEmpleado , "empleado"); }
			if(idActorNegocioExterno< 1){ throw new IdNoValidoException(idActorNegocioExterno , "actor negocio externo"); }
			if(idActorNegocioInterno< 1){ throw new IdNoValidoException(idActorNegocioInterno , "actor negocio interno"); }
			if(idComprobante< 1){ throw new IdNoValidoException(idComprobante , "comprobante"); }
			if(idMoneda< 1){ throw new IdNoValidoException(idMoneda , "moneda"); }
			if(idTipoTransaccion< 1){ throw new IdNoValidoException(idTipoTransaccion , "tipo transaccion"); }
			if(idTransaccionPadre< 1){ throw new IdNoValidoException(idTransaccionPadre , "transaccion padre"); }
			if(idTransaccionReferencia< 1){ throw new IdNoValidoException(idTransaccionReferencia , "transaccion referencia"); }
			
		}
	}

    public partial class Traza_pago
    {
		public Traza_pago()
		{
		}
		public Traza_pago(long idTransaccion,int idMedioPago,string traza,int idEntidadBancaria,int idTipoPago)
		{
			setData(idTransaccion,idMedioPago,traza,idEntidadBancaria,idTipoPago);
			validarId(idEntidadBancaria, idMedioPago, idTipoPago, idTransaccion, idTransaccion);
		}
		
		public void setData(long idTransaccion,int idMedioPago,string traza,int idEntidadBancaria,int idTipoPago)
		{
			this.id_transaccion = idTransaccion;
			this.id_medio_pago = idMedioPago;
			this.traza = traza;
			this.id_entidad_bancaria = idEntidadBancaria;
			this.id_tipo_pago = idTipoPago;
			
		}
		protected void validarId(int idEntidadBancaria, int idMedioPago, int idTipoPago, int idTransaccion, int idTransaccion)
		{
			if(idEntidadBancaria< 1){ throw new IdNoValidoException(idEntidadBancaria , "entidad bancaria"); }
			if(idMedioPago< 1){ throw new IdNoValidoException(idMedioPago , "medio pago"); }
			if(idTipoPago< 1){ throw new IdNoValidoException(idTipoPago , "tipo pago"); }
			if(idTransaccion< 1){ throw new IdNoValidoException(idTransaccion , "transaccion"); }
			if(idTransaccion< 1){ throw new IdNoValidoException(idTransaccion , "transaccion"); }
			
		}
	}

    public partial class Ubigeo
    {
		public Ubigeo()
		{
		}
		public Ubigeo(int idRegion,int idProvincia,int idDistrito,string descripcionLarga,string descripcionCorta,int idPais)
		{
			setData(idRegion,idProvincia,idDistrito,descripcionLarga,descripcionCorta,idPais);
			
		}
		
		public void setData(int idRegion,int idProvincia,int idDistrito,string descripcionLarga,string descripcionCorta,int idPais)
		{
			this.id_region = idRegion;
			this.id_provincia = idProvincia;
			this.id_distrito = idDistrito;
			this.descripcion_larga = descripcionLarga;
			this.descripcion_corta = descripcionCorta;
			this.id_pais = idPais;
			
		}
		
	}

    public partial class Valor_caracteristica
    {
		public Valor_caracteristica()
		{
		}
		public Valor_caracteristica(int idCaracteristica,string valor)
		{
			setData(idCaracteristica,valor);
			validarId(idCaracteristica);
		}
		
		public void setData(int idCaracteristica,string valor)
		{
			this.id_caracteristica = idCaracteristica;
			this.valor = valor;
			
		}
		protected void validarId(int idCaracteristica)
		{
			if(idCaracteristica< 1){ throw new IdNoValidoException(idCaracteristica , "caracteristica"); }
			
		}
	}

    public partial class Valor_caracteristica_concepto
    {
		public Valor_caracteristica_concepto()
		{
		}
		public Valor_caracteristica_concepto(int idConcepto,int idValorCaracteristica)
		{
			setData(idConcepto,idValorCaracteristica);
			validarId(idValorCaracteristica, idConcepto);
		}
		
		public void setData(int idConcepto,int idValorCaracteristica)
		{
			this.id_concepto = idConcepto;
			this.id_valor_caracteristica = idValorCaracteristica;
			
		}
		protected void validarId(int idValorCaracteristica, int idConcepto)
		{
			if(idValorCaracteristica< 1){ throw new IdNoValidoException(idValorCaracteristica , "valor caracteristica"); }
			if(idConcepto< 1){ throw new IdNoValidoException(idConcepto , "concepto"); }
			
		}
	}

    public partial class Valor_caracteristica_concepto_negocio
    {
		public Valor_caracteristica_concepto_negocio()
		{
		}
		public Valor_caracteristica_concepto_negocio(int idConceptoNegocio,int idValorCaracteristica)
		{
			setData(idConceptoNegocio,idValorCaracteristica);
			validarId(idConceptoNegocio, idValorCaracteristica);
		}
		
		public void setData(int idConceptoNegocio,int idValorCaracteristica)
		{
			this.id_concepto_negocio = idConceptoNegocio;
			this.id_valor_caracteristica = idValorCaracteristica;
			
		}
		protected void validarId(int idConceptoNegocio, int idValorCaracteristica)
		{
			if(idConceptoNegocio< 1){ throw new IdNoValidoException(idConceptoNegocio , "concepto negocio"); }
			if(idValorCaracteristica< 1){ throw new IdNoValidoException(idValorCaracteristica , "valor caracteristica"); }
			
		}
	}

    public partial class VoucherModel
    {
		public VoucherModel()
		{
		}
		public VoucherModel(string name,string description,int linesNumber,int lineWidth,int lineHeight,int amountDetailWidth,int unitPriceDetailWidth,int totalMoneyDetailWidth,int headerLinesNumber,int detailLinesNumber,int resumeLinesNumber,int footerLinesNumber,string fontType,int fontSize,int columnPadding,int leftMargin,int upMargin,int pageHeight,int pageWidth)
		{
			setData(name,description,linesNumber,lineWidth,lineHeight,amountDetailWidth,unitPriceDetailWidth,totalMoneyDetailWidth,headerLinesNumber,detailLinesNumber,resumeLinesNumber,footerLinesNumber,fontType,fontSize,columnPadding,leftMargin,upMargin,pageHeight,pageWidth);
			
		}
		
		public void setData(string name,string description,int linesNumber,int lineWidth,int lineHeight,int amountDetailWidth,int unitPriceDetailWidth,int totalMoneyDetailWidth,int headerLinesNumber,int detailLinesNumber,int resumeLinesNumber,int footerLinesNumber,string fontType,int fontSize,int columnPadding,int leftMargin,int upMargin,int pageHeight,int pageWidth)
		{
			this.name = name;
			this.description = description;
			this.linesNumber = linesNumber;
			this.lineWidth = lineWidth;
			this.lineHeight = lineHeight;
			this.amountDetailWidth = amountDetailWidth;
			this.unitPriceDetailWidth = unitPriceDetailWidth;
			this.totalMoneyDetailWidth = totalMoneyDetailWidth;
			this.headerLinesNumber = headerLinesNumber;
			this.detailLinesNumber = detailLinesNumber;
			this.resumeLinesNumber = resumeLinesNumber;
			this.footerLinesNumber = footerLinesNumber;
			this.fontType = fontType;
			this.fontSize = fontSize;
			this.columnPadding = columnPadding;
			this.leftMargin = leftMargin;
			this.upMargin = upMargin;
			this.pageHeight = pageHeight;
			this.pageWidth = pageWidth;
			
		}
		
	}
}
