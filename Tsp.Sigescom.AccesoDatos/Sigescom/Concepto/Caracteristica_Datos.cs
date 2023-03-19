using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Concepto;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio.Conceptos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.AccesoDatos.Conceptos

{
    public partial class Caracteristica_Datos : ICaracteristica_Repositorio
    {
        public ConceptoConSusCaracteristicas ObtenerConceptoNegocioConSusCaracteristicasYSusValores(int idConceptoNegocio)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var concepto = _db.Concepto_negocio.Where(cn => cn.id == idConceptoNegocio).Select(c => new ConceptoConSusCaracteristicas()
                {
                    IdConcepto = c.id,
                    NombreConceptoSinCaracteristicas = c.Detalle_maestro4.nombre + " " + c.sufijo
                }).FirstOrDefault();
                concepto.Caracteristicas = new List<ItemGenerico>();
                var caracteristicas = _db.Concepto_negocio.Single(cn => cn.id == idConceptoNegocio).Detalle_maestro4.Caracteristica_concepto.Select(cc => cc.Detalle_maestro1).Where(dm => dm.es_vigente).Select(c => new ItemGenerico()
                {
                    Id = c.id,
                    Nombre = c.nombre
                }).ToList();
                var valoresCaracteristicasConceptoNegocio = _db.Valor_caracteristica_concepto_negocio.Where(cn => cn.id_concepto_negocio == idConceptoNegocio).Select(c => new ItemGenerico()
                {
                    Id = c.Valor_caracteristica.id_caracteristica,
                    Valor = c.Valor_caracteristica.valor
                }).ToList();
                foreach (var item in valoresCaracteristicasConceptoNegocio)
                {
                    concepto.Caracteristicas.Add(new ItemGenerico() { Id = item.Id, Nombre = caracteristicas.Single(c => c.Id == item.Id).Nombre, Valor = item.Valor });
                }
                return concepto;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener las caracteristicas y sus valores de conceptos de negocio");
            }
        }

    }
}