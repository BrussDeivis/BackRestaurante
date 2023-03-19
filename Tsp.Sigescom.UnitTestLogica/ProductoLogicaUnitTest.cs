using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Interfaces.Logica;


namespace Tsp.Sigescom.UnitTestLogica
{
    [TestClass]
    public class ProductoLogicaUnitTest
    {
        private readonly IConceptoLogica logica;
        int idCentroAtencion=4;
        public ProductoLogicaUnitTest()
        {
            logica = Dependencia.Resolve<IConceptoLogica>();
            
        }

        #region productos aleatorios
        /*
        string[] Productos = new string[] {"ADEREZOS CONSOME","CREMA PARA CAFÉ",
            "PURE DE TOMATE", "ATOLE", "AVENA", "AZÚCAR", "CAFÉ", "CEREALES", "CHILE PIQUÍN",
            "ESPECIAS", "FLAN EN POLVO", "FORMULAS INFANTILES", "GELATINAS EN POLVO", "HARINA",
            "HARINA PREPARADA", "MOLE", "SAL", "SALSAS ENVASADAS", "SAZONADORES", "SOPAS EN SOBRE",
            "CAJETA", "CATSUP", "MAYONESA", "MERMELADA", "MIEL", "TE", "VINAGRE", "HUEVO", "PASTAS",
            "ACEITUNAS", "CHAMPIÑONES ENTEROS/REBANADOS", "CHÍCHARO CON ZANAHORIA", "CHÍCHAROS ENLATADOS",
            "FRIJOLES ENLATADOS", "FRUTAS EN ALMÍBAR", "SARDINAS", "ATÚN EN AGUA/ACEITE", "CHILES ENLATADOS",
            "CHILES ENVASADOS", "ENSALADAS ENLATADAS", "GRANOS DE ELOTE ENLATADOS", "SOPA EN LATA",
            "VEGETALES EN CONSERVA", "LECHE CONDESADA", "LECHE DESLACTOSADA", "LECHE EN POLVO",
            "LECHE EVAPORADA", "LECHE LIGHT", "LECHE PASTEURIZADA", "LECHE SABORIZADA",
            "LECHE SEMIDESCREMADA", "CREMA", "YOGHURT", "MANTEQUILLA", "MARGARINA", "MEDIA CREMA",
            "QUESO", "PAPA", "PALOMITAS", "FRITURAS DE MAÍZ", "CACAHUATES", "BOTANAS SALADAS",
            "BARRAS ALIMENTICIAS", "NUECES Y SEMILLAS", "CARAMELOS", "DULCES ENCHILADOS",
            "CHOCOLATE DE MESA", "CHOCOLATE EN POLVO", "CHOCOLATES", "GOMAS DE MASCAR", "MAZAPÁN",
            "MALVAVISCOS", "PULPA DE TAMARINDO", "PASTILLAS DE DULCE", "PALETAS DE DULCE",
            "TORTILLAS DE HARINA/MAÍZ", "GALLETAS DULCES", "GALLETAS SALADAS", "PASTELILLOS",
            "PAN DE CAJA", "PAN DULCE", "PAN MOLIDO", "PAN TOSTADO", " AGUACATES", "AJOS", "CEBOLLAS",
            "CHILES", "CILANTRO/PEREJIL", "JITOMATE", "PAPAS", "LIMONES", "MANZANAS", "NARANJAS",
            "PLÁTANOS", "AGUA MINERAL", "AGUA NATURAL", "AGUA SABORIZADA", "JARABES", "JUGOS/NÉCTARES",
            "NARANJADAS", "BEBIDAS DE SOYA", "BEBIDAS EN POLVO", "BEBIDAS INFANTILES", "BEBIDAS ISOTÓNICAS",
            "ENERGETIZANTES", "ISOTÓNICOS", "REFRESCOS", "BEBIDAS PREPARADAS", "CERVEZA", "ANÍS", "BRANDY",
            "GINEBRA", "CORDIALES", "MEZCAL", "JEREZ", "RON", "TEQUILA", "SIDRA", "WHISKEY", "VODKA",
            "PASTAS LISTAS PARA COMER", "SOPAS EN VASO", "SALCHICHA", "MORTADELA", "TOCINO", "JAMÓN",
            "MANTECA", "CHORIZO", "CARNE DE PUERCO/RES/POLLO", "SUERO", "AGUA OXIGENADA", "PRESERVATIVOS",
            "ALCOHOL", "GASAS", "ANALGÉSICOS", "ANTIGRIPALES", "ANTIÁCIDOS", "TOALLAS HÚMEDAS",
            "ACEITE PARA BEBE", "TOALLAS FEMENINAS", "ALGODÓN", "TINTE PARA EL CABELLO", "BIBERONES",
            "TALCO", "CEPILLO DE DIENTES", "SHAMPOO/ ACONDICIONADOR", "COTONETES", "RASTRILLOS",
            "CREMA CORPORAL/FACIAL", "PAPEL HIGIÉNICO", "CREMA PARA AFEITAR", "PAÑUELOS FACIALES",
            "DENTÍFRICOS", "PAÑUELOS DESECHABLES", "DESODORANTES EN BARRA/AEROSOL", "MAQUILLAJE",
            "ENJUAGUE BUCAL", "LUBRICANTES PARA LABIOS", "GEL/SPRAY", "LOCIÓN HIDRATANTE",
            "JABONES CORPORALES/TOCADOS", "SUAVIZANTE DE TELAS", "ÁCIDO MURIÁTICO", "SOSA CAUSTICA",
            "ALUMINIO", "PILAS", "SHAMPOO PARA ROPA", "SERVILLETAS", "SERVITOALLAS", "AROMATIZANTES",
            "CERA PARA AUTOMÓVIL", "CERA PARA CALZADOS", "PASTILLAS SANITARIAS", "LIMPIADORES LÍQUIDOS",
            "LIMPIADORES PARA PISOS", "JABÓN DE BARRA", "CERILLOS", "CLORO/BLANQUEADOR", "CLORO PARA ROPA",
            "JABÓN EN BARRA", "INSECTICIDAS", "FIBRAS LIMPIADORAS", "DESINFECTANTES",
            "DETERGENTES PARA TRASTES", "DETERGENTE PARA ROPA", "PALETAS/ HELADOS", "VELADORAS/VELAS",
            "CEPILLO DE PLÁSTICO", "VASOS DESECHABLES", "CINTA ADHESIVA", "CUCHARAS DE PLÁSTICO",
            "ESCOBAS/TRAPEADORES/MECHUDOS", "TRAMPAS PARA RATAS", "TENEDORES DE PLÁSTICO",
            "EXTENSIONES/MULTICONTACTO", "RECOGEDOR DE METAL/PLÁSTICO", "POPOTES", "PLATOS DESECHABLES",
            "FOCOS", "FUSIBLES", "JERGAS/FRANELAS", "MATAMOSCAS", "PEGAMENTO", "MECATE/CUERDA"};*/
        #endregion

        /// <summary>
        /// En este método va a comprobar en el guardado de mercadería, teniendo en cuenta que cada concepto tiene su
        /// característica, pero cuando vamos a guardar, lo vamos hacer con características diferentes al concepto registrado
        /// para esto tenemos el concepto=Alicate que tien como características "MARCA" y "TAMAÑO"
        /// Probaremos con las siguientes característica
        /// MEDIDA=28(PULGADAS)
        /// COLOR=25(AZUL)
        /// </summary>
        [TestMethod]
        public void ConceptoCaracteristicaTestMethod()
        {
            // arrange
            string nombre ="ALICATE PRUEBA";
            string sufijo = "PRUEBA";
            int idConceptoBasico =415; //ALICATE
            int idUnidadDeMedidaCom = 5;    //UNIDAD
            int idUnidadDeMedidaRef = 5;    //UNIDAD
            int[] idCaracteristicas = new int[] { 25, 28 };        //IDMARCA=306 Y IDTAMAÑO=399 ---- PRUEBA:: IDMEDIDA=378 Y  IDCOLOR=414
            int idPresentacion=416;             //CAJA
            decimal cantidadPresentacion=1;     //CANTIDAD DE PRESENTACIÓN
            int idUnidadDeMedidaPre=5;          //UNIDAD
            int? idPresentacionSubContenido = null;
            byte[] foto = null;
            bool hayFoto = false;                 //FALSE
            List<Precio> precios = new List<Precio>();
            precios = AgregarPrecio();
            decimal? stockMinimo=0;
            int idEmpleado = 5;          //GENÉRICO


            // act  
            //OperationResult result = logica.guardarProducto(nombre,nombre, sufijo, idConceptoBasico, idUnidadDeMedidaCom, idUnidadDeMedidaRef,
            //idCaracteristicas, idPresentacion, cantidadPresentacion, idUnidadDeMedidaPre, idPresentacionSubContenido, foto,
            //hayFoto, precios, stockMinimo, idEmpleado, idCentroAtencion);

            ////assert
            //Assert.AreEqual(OperationResultEnum.Success, result.code_result);
            
            //public OperationResult guardarProducto(string nombre, string sufijo, int idConceptoBasico, int idUnidadDeMedidaCom, int idUnidadDeMedidaRef,
            //int[] idCaracteristicas, int idPresentacion, decimal cantidadPresentacion, int idUnidadDeMedidaPre, int? idPresentacionSubContenido, byte[] foto,
            //bool hayFoto, List<Precio> precios, decimal? stockMinimo, int idEmpleado)
        }


        public List<Precio> AgregarPrecio()
        {
            List<Precio> precios = new List<Precio>();
            Precio temPrecio;
            for (int i = 0; i < 3; i++)
            {
                temPrecio = new Precio();
                temPrecio.id_tarifa_d = 7 + i;
                temPrecio.valor = 5 - i;
                precios.Add(temPrecio);
            }
            return precios;
        }


    }
}
