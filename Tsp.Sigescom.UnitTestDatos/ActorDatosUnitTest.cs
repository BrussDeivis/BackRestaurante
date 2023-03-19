using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Tsp.Sigescom.AccesoDatos;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Actores;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;

namespace Tsp.Sigescom.UnitTestDatos
{
    [TestClass]
    public  class ActorDatosUnitTest
    {
        private readonly IActorRepositorio _repositorio;
        private readonly IActor_Repositorio __repositorio;


        public ActorDatosUnitTest()
        {
            _repositorio=Dependencia.Resolve<IActorRepositorio>();
            __repositorio = Dependencia.Resolve<IActor_Repositorio>();

        }

        [TestMethod]
        public void obtenerActorNegocioPorIdTestMethod()
        {
            int idRol=2, idActorNegocio=5;
            Actor_negocio actor = _repositorio.obtenerActorDeNegocio(idActorNegocio, idRol);
            Assert.IsNotNull(actor);
        }


        //[TestMethod]
        ////public void obtenerActorDeNegocioPorDocumentoIdentidadTestMethod()
        ////{
        ////    IActorRepositorio repositorio = new ActorDatos();
        ////    Actor_negocio actor = repositorio.obtenerActorDeNegocioPorDocumentoIdentidad("45765332", 7, DateTime.Now);

        ////    Assert.IsNotNull(actor);
        ////}

        [TestMethod]
        public void existeTestMethod()
        {
            IActorRepositorio repositorio = new ActorDatos();
            bool existe = repositorio.ExisteActorDeNegocio("45765332");

            Assert.IsTrue(existe);
        }

      
        [TestMethod]
        public void obtenerActorNegocioConRolesTestMethod()
        {
            IActorRepositorio repositorio = new ActorDatos();
            //Actor_negocio actor = repositorio.obtenerActorDeNegocioConRoles(2299, 5);

            //Assert.AreEqual("Luna", actor.PrimerNombre);
      
        }

        [TestMethod]
        public void obtenerActorPorRolTestMethod()
        {
            IActorRepositorio repositorio = new ActorDatos();
            List<Actor_negocio> actorNegocio = repositorio.obtenerActorDeNegocioPorRol(7).ToList();

            Assert.IsNotNull(actorNegocio);
        }

        //[TestMethod]
        //public void obtenerActorPorSubRolTestMethod()
        //{
        //    IActorRepositorio repositorio = new ActorDatos();
        //    List<Actor_negocio> an = repositorio.ObtenerActorDeNegocioPrincipalVigentes(8, 7).ToList();

        //    Assert.IsNotNull(an);
        //}

        //[TestMethod]
        //public void obtenerMaximoCodigoTestMethod()
        //{
        //    IActorRepositorio repositorio = new ActorDatos();
        //    string codigo = repositorio.obtenerMaximoCodigo(7, 865);

        //    Assert.IsNotNull(codigo);
        //}



        [TestMethod]
        public void obtenerActoresDeNegocioPrincipalPorRolTestMethod()
        {
            IActorRepositorio repositorio = new ActorDatos();
            List<Actor_negocio> _actorn = repositorio.obtenerActoresDeNegocioPrincipalPorRol(7).ToList();

            Assert.IsNotNull(_actorn);
        }

      

        [TestMethod]
        public void obtenerActorDeNegocioPorRolVigentesAhora()
        {

            Stopwatch sw = new Stopwatch();

            sw.Start();

            List<Actor_negocio> clientes = __repositorio.ObtenerActorDeNegocioPorRolVigentesAhora(ActorSettings.Default.IdRolCliente).ToList();
            sw.Stop();

            Console.WriteLine("Elapsed={0}", sw.Elapsed);

            Assert.IsNotNull(clientes);

          
        }

       



    }

}

    

