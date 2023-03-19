using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Interfaces.Logica;

namespace Tsp.Sigescom.UnitTestLogica
{
    [TestClass]
    public class ClienteLogicaUnitTest
    {
        private readonly IActorNegocioLogica logica;

        



        public ClienteLogicaUnitTest()
        {
            logica = Dependencia.Resolve<IActorNegocioLogica>();
        }


        public string ObtenerDireccionSinUbigeo(string direccionCompleta)
        {
            string[] separatorGuion = new string[] { " - " };
            string[] separatorEspacio = new string[] { " " };

            string[] separatorComa = new string[] { ", " };
            string[] separatorParentesis = new string[] { ") " };
            string[] primeraParticion = direccionCompleta.Split(separatorGuion, StringSplitOptions.RemoveEmptyEntries);


            string detalle = string.Join("",primeraParticion)  ;
            string[] detallesFinal = detalle.Split(separatorEspacio, StringSplitOptions.None);

            string detalleFinal = string.Join(" ", detallesFinal.Take(detallesFinal.Count()-2).ToArray());

            return detalleFinal;
        }

        [TestMethod]
        public void ExtraerDetalleDireccionClienteSinReferenciaUnitTest()
        {
            ///arrenge
            ///
            string[] separatorGuion = new string[] { " - " };
            string[] separatorEspacio = new string[] { " " };
            string[] separatorComa = new string[] { ", " };
            string[] separatorParentesis = new string[] { ") " };
            string direccionCompleta = "PJ. JORGE BASADRE NRO. 158 URB. POP LA UNIVERSAL 2DA ET. LIMA - LIMA - SANTA ANITA";
            string valorEsperado = "PJ. JORGE BASADRE NRO. 158 URB. POP LA UNIVERSAL 2DA ET. ";

            ///Actions
            string valorObtenido = ObtenerDireccionSinUbigeo(direccionCompleta);
            ///Assert
            Assert.AreEqual(valorEsperado, valorObtenido);
        }


        [TestMethod]
        public void ExtraerUbigeoClienteSinReferenciaUnitTest()
        {
            ///arrenge
            ///
            string[] separatorGuion = new string[] { " - " };
            string[] separatorEspacio = new string[] { " " };
            string[] separatorComa = new string[] { ", " };
            string[] separatorParentesis = new string[] { ") " };
            string direccionCompleta = "MZA. G LOTE. 3 P.J. AMPLIACION SOCABAYA  AREQUIPA - AREQUIPA - SOCABAYA";
            string ubigeoEsperado = "AREQUIPA - AREQUIPA - SOCABAYA";

            ///Actions
            ///
            //string ubigeoObtenido= string.Join(" - ", direccionCompleta.Split(separatorParentesis, StringSplitOptions.RemoveEmptyEntries)[1].
            //        Split(separatorGuion, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()));

            string ubigeoObtenido="";
            string[] primeraParticion = direccionCompleta.Split(separatorGuion, StringSplitOptions.RemoveEmptyEntries);
            string[] segundaParticion = primeraParticion[primeraParticion.Count() - 3].Split(separatorEspacio, StringSplitOptions.RemoveEmptyEntries);
            ubigeoObtenido = segundaParticion[segundaParticion.Count() - 1] + " - " + primeraParticion[primeraParticion.Count() - 2]+ " - " +primeraParticion[primeraParticion.Count() - 1];
            ///Assert
            Assert.AreEqual(ubigeoEsperado,ubigeoObtenido);
        }



        [TestMethod]
        public void guardarClienteUnitTest()
        {
            ////Declacion de variables a guardar
            //int idTipoPersona = 2;
            //string razonSocial = "Universidad Nacional Agraria de la selva";
            //string apellidoMaterno = "";
            //string apellidoPaterno = ""; 
            //string nombres = "";
            //string nombreComercial = "UNASSEL";
            //string nombreCorto = "UNAS";
            //int idTipoDocumentoIdentidad = 71;
            //string numeroDocumentoIdentidad = "20172356720";
            //string correo = "unas@gmail.com";
            //string telefono = "956741235";
            //int idClaseActor = 3;
            //int idEstadoLegalActor = 2;
            //Direccion midireccion = new Direccion(12, 46, 150112, "Jr. Limones 945", null, null, true, true);
            //List<Direccion> direcciones = new List<Direccion>();
            //direcciones.Add(midireccion);

            ////Ejecucion del metodo
            //OperationResult result = logica.crearCliente(idTipoPersona, razonSocial,apellidoPaterno, apellidoMaterno,nombres, nombreComercial, nombreCorto, idTipoDocumentoIdentidad, numeroDocumentoIdentidad, idClaseActor, idEstadoLegalActor, correo, telefono, direcciones);

            ////Comprobacion del assert
            //Assert.AreEqual("Success", result.code_result);
        }


    }
}

//[TestMethod]
//        public void obtenerClientesTestMethod()
//        {
//            var list = logica.obtenerClientes();

//            Assert.IsNotNull(list);
//        }

//        //[TestMethod]
//        //public void guardarClientesTestMethod()
//        //{
//        //    var list = logica.guardarCliente();
//        //    Assert.IsNotNull(list);
//        //}
//        //----------------------------------------
//        [TestMethod]
//        public void guardarClientesTestMethod()
//        {
//            for (int k = 1; k <= 10; k++)
//            {
//                string[] Nombres = new string[] { "MATT", "JOANNE", "ROBERT","ARON","ABEL","ABELARDO","ABIGAÍL",
//            "ABRAHAM","ABRIL","ADA","ADALBERTO","ADÁN","ADRIÁN","AGUSTÍN","ALAN","ANDRÉS","ANÍBAL","ANTONIETA",
//            "AUGUSTO","AZUZENA", "AXEL","ALEX","BARACK","BARTOLOMÉ","BÁRBARA","BENJAMÍN","BERNARDO","BIANCA",
//            "BRENDA" , "BRUNO","MABEL","MAITE","MANUEL","MARCELO","MARGARITA","MARÍA","MARIANA","MIGUEL","MARTA",
//            "PALOMA","PASCUAL","PATRICIA","PAULA", "PEDRO","PENÉLOPE","PERLA","PILAR","VALENTÍN","VANESA",
//            "VALENTINA","VALERIA","TRIANA","TATIANA","TADEO","SAMUEL","MATEO","MATÍAS","MATILDE","MAITE",
//            "MAURICIO","MAXIMILIANO","MÁXIMO","SALVADOR","SANTIAGO","SERGIO","SOFÍA","NÉSTOR","NICOLÁS","NORMA",
//            "NOEMÍ","GLORIA","GABRIELA","GUSTAVO","ZOE","XIMENA","DAYA","SEBASTIAN","RICARDO","JUAN","LUIS",
//            "FERNANDO","ORLANDO","ELIZABEHT","LORENA","VICTOR","ANDY","JORDAN","ALDAIR","HUGO","WILLIAM","GIANELLA",
//            "YESSENIA","PAOLA","DANNA","ALEXANDER","DAYSI"};

//            string[] Apellidos = new string[] { "FLORES", "RODRÍGUEZ", "SÁNCHEZ", "GARCÍA", "ROJAS", "DÍAZ",
//            "TORRES","LÓPEZ","GONZALES", "PÉREZ", "CHÁVEZ", "VÁSQUEZ","RAMOS","RAMÍREZ","MENDOZA","ESPINOZA",
//            "CASTILLO","HUAMÁN", "VARGAS", "MAMANI","FERNÁNDEZ","GUTIÉRREZ","RUIZ", "CASTRO","ROMERO", "SALAZAR",
//            "CRUZ","GÓMEZ","RIVERA","ABAD", "ACEVEDO", "ACUÑA", "ALEGRE", "ALVAREZ", "ARROYO", "BALLESTEROS",
//            "BECERRA","CABANILLAS","CAMPOS", "ECHEVARRIA","GARCILASO","GODOY","JARA", "PIZARRO","QUINTANA",
//            "VALENZUELA", "ZAPATA","BUSTAMANTE", "CALDERON", "CEBALLOS","CEBRIÁN","CEPEDA","CERDÁ","CERDÁN",
//            "CERVANTES", "CERVERA","CHACÓN","CHAMORRO","CHAPÍN","CHÁVARRI","DORARTE","DORCAS","DORÍA",
//            "DÓRIGA","LLANOS","LLAVE","MURILLO","NADAL","NARVÁEZ","NAVAJA","NAVARRO","NEIRA","OLIVA",
//            "OLIVARES","QUINTANA", "QUINTERO","QUIÑONES","RÍOS","RIPOL","ROLDÁN","ROMAY","ROMERO","SALABERRY",
//            "SALAS","SALAZAR","SALCEDO","SALES","SALGADO","SALINAS","CAMPO","CARNERO","DE LA CERDA",
//            "DE LAS HERAS","DEL CAMPO"};


//            string[] listaDirecciones = new string[] {"LOS ANDES","AYACUCHO","SAN CRISTOBAL","IQUITOS","LEON VELARDE",
//            "JOSE OLAYA","UNIVERSITARIA","MCAL FELIPE","SALAVERRY","GUAYABAMBA","TRUJILLO","AYACUCHO","PIURA", "DANIEL ALCIDES CARRION",
//            "PASCO", "YANACANCHA", "MANCO CAPAC","HUAMANGA","AYACUCHO", "OQUENDO", "CALLAO", "VILLA", "JUDICIAL", "LA LIBERTAD",
//            "LA ESPERANZA","SANTIAGO MAMANI" , "CALANA","SANTO DOMINGO" ,"AMAZONAS","JORGE CHAVEZ","BARRIO BELEN", "ANCASH", "HUARAZ",
//            "FRANCISCO BOLOGNESI" };
                
                        
//            Random aleatorio = new Random();

//            int idTipoPersona = 1;
//            string razonSocial = Nombres[aleatorio.Next(1, 100)];
//            string nombreComercial = Apellidos[aleatorio.Next(1, 100)];
//            string nombreCorto = nombreComercial;
//            int idTipoDocumentoIdentidad = 1;
//            string numeroDocumentoIdentidad = string.Format("{0:00000000}", aleatorio.Next(1,99999999));
//            int idClaseActor =1;
//            int idEstadoLegalActor =1;
//            string correo = razonSocial.ToLower()+"."+nombreComercial.ToLower()+"@gmail.com";
//            string telefono = 9+ string.Format("{0:00000000}", aleatorio.Next(1, 99999999)); ;
//            Direccion midireccion = new Direccion(72, 64, 23, listaDirecciones[aleatorio.Next(1, 13)]+" N° " + aleatorio.Next(1, 999), aleatorio.Next(80, 90),
//                92, true, true);
//            List <Direccion> direcciones= new List<Direccion>();
//            direcciones.Add(midireccion);
//            //OperationResult result =logica.crearCliente(idTipoPersona, razonSocial,nombreComercial,nombreCorto,
//            //    idTipoDocumentoIdentidad,numeroDocumentoIdentidad,idClaseActor,idEstadoLegalActor,
//            //    correo,telefono, direcciones);
//            //Assert.IsNotNull(list);
//            //1998-01-01    ----     1947-01-01

//            }
//       }
//    }

//}






























//DANIEL ALCIDES CARRION PASCO, YANACANCHA, MANCO CAPAC, HUAMANGA, AYACUCHO, OQUENDO, CALLAO, VILLA, JUDICIAL,  LA LIBERTAD, LA ESPERANZA,
// SANTIAGO MAMANI , CALANA  , SANTO DOMINGO ,AMAZONAS, JORGE CHAVEZ, BARRIO BELEN, ANCASH, HUARAZ, FRANCISCO BOLOGNESI
//L. COMERCIAL    MZA. H INT. A LOTE. 1 URB. BUENOS AIRES ANCASH  SANTA NUEVO CHIMBOTE
//L. COMERCIAL    AV. DIAZ BERCENAS NRO. 627  APURIMAC    ABANCAY ABANCAY
//L. COMERCIAL    CAL. JERUSALEN NRO. 201H    AREQUIPA    AREQUIPA    AREQUIPA
//L. COMERCIAL    AV. JESUS NRO. 612 (BARRIO MARGINAL 15 DE ENERO)    AREQUIPA    AREQUIPA    PAUCARPATA
//L. COMERCIAL    AV. MARISCAL CACERES NRO. 1255  AYACUCHO    HUAMANGA    AYACUCHO
//L. COMERCIAL    JR. DEL COMERCIO NRO. 816   CAJAMARCA   CAJAMARCA   CAJAMARCA
//L. COMERCIAL    PJ. BRACAMOROS NRO. 160 (CENTRO JAEN)   CAJAMARCA   JAEN    JAEN
//L. COMERCIAL    AV. COLONIAL NRO. 2788 URB. TABOADITA   CALLAO  CALLAO  BELLAVISTA
//L. COMERCIAL    MLC. ANDRES A. CACERES MZA. C-1 LOTE. 13    CALLAO  CALLAO  VENTANILLA
//L. COMERCIAL    JR. BOLOGNESI NRO. 114 (CANCHIS 1 SICUANI)  CUSCO   CANCHIS SICUANI
//L. COMERCIAL    CAL. SIETE MASCARONES NRO. 486 INT. 01 (CUSCO-13-ZONA 3A-SANTIAGO)  CUSCO   CUSCO   SANTIAGO
//L. COMERCIAL    AV. CELESTINO MANCHEGO MUÑOZ NRO. 724   HUANCAVELICA    HUANCAVELICA    HUANCAVELICA
//L. COMERCIAL    JR. 2 DE MAYO NRO. 1391 (C.V.HUANUCO)   HUANUCO HUANUCO HUANUCO
//L. COMERCIAL    AV. RAYMONDI NRO. 342   HUANUCO LEONCIO PRADO   RUPA-RUPA
//L. COMERCIAL    JR. ITALIA NRO. 155 ICA CHINCHA CHINCHA ALTA
//L. COMERCIAL    AV. LOS MAESTROS NRO. 206 (LOCAL LC-160)    ICA ICA ICA
//L. COMERCIAL    CAL. SAN FRANCISCO NRO. 225 ICA PISCO   PISCO
//L. COMERCIAL    CAL. REAL NRO. 911  JUNIN   HUANCAYO    EL TAMBO
//L. COMERCIAL    JR. LIMA NRO. 842 SEC. TARMA    JUNIN   TARMA   TARMA
//L. COMERCIAL    AV. TREN MZA. D4 LOTE. 30 SEC. CENTRAL  LA LIBERTAD ASCOPE  CASA GRANDE
//L. COMERCIAL    CAL. TACNA NRO. 332 (PUEBLO OTUZCO) LA LIBERTAD OTUZCO  OTUZCO
//L. COMERCIAL    AV. CESAR VALLEJO MZA. E LOTE. 26 URB. INGENIERIA   LA LIBERTAD TRUJILLO    TRUJILLO
//L. COMERCIAL    CAL. MANUEL MARIA IZAGA NRO. 737    LAMBAYEQUE  CHICLAYO    CHICLAYO
//L. COMERCIAL    CAL. EL DORADO NRO. 915 URB. SAN CARLOS LAMBAYEQUE  CHICLAYO    JOSE LEONARDO ORTIZ
//L. COMERCIAL    AV. GRAN CHIMU NRO. 1045    LAMBAYEQUE  CHICLAYO    LA VICTORIA
//L. COMERCIAL    AV. GRAL. LA MAR NRO. 375   LIMA    CAÑETE  IMPERIAL
//L. COMERCIAL    CAL. DERECHA NRO. 836 (846) LIMA    HUARAL  HUARAL
//L. COMERCIAL    AV. TUPAC AMARU NRO. 209 (CERCADO DE HUACHO)    LIMA    HUAURA  HUACHO
//L. COMERCIAL    AV. HUAYLAS NRO. 205    LIMA    LIMA    CHORRILLOS
//L. COMERCIAL    AV. TUPAC AMARU NRO. 1417 URB. HUAQUILLAY   LIMA    LIMA    COMAS
//L. COMERCIAL    AV. ARENALES NRO. 724   LIMA    LIMA    JESUS MARIA
//L. COMERCIAL    AV. MANCO CAPAC NRO. 338    LIMA    LIMA    LA VICTORIA
//L. COMERCIAL    AV. LIMA SUR NRO. 752   LIMA    LIMA    LURIGANCHO
//L. COMERCIAL    PARCELA MZA. 1-B LOTE. 62 FND. SAN VICENTE  LIMA    LIMA    LURIN
//L. COMERCIAL    JR. RICARDO PALMA NRO. 158  LIMA    LIMA    PUENTE PIEDRA
//L. COMERCIAL    AV. GRAN CHIMU NRO. 976 DPTO. 2 LIMA    LIMA    SAN JUAN DE LURIGANCHO
//L. COMERCIAL    AV. LIZARDO MONTERO NRO. 508 URB. SAN JUAN (ZONA E) LIMA    LIMA    SAN JUAN DE MIRAFLORES
//L. COMERCIAL    AV. UNIVERSITARIA NRO. 301 U.POP CONDEVILLA DEL SEÑO (U.POP CONDEVILLA DEL SEÑOR)   LIMA    LIMA    SAN MARTIN DE PORRES
//L. COMERCIAL    AV. UNIVERSITARIA NRO. 837 URB. PANDO 4TA ET.   LIMA    LIMA    SAN MIGUEL
//L. COMERCIAL    AV. CAMINOS DEL INCA NRO. 3545 URB. PROLONGACION BENAVIDES  LIMA    LIMA    SANTIAGO DE SURCO
//L. COMERCIAL    MZA. I LOTE. 5 GRU. 3 (SECTOR 2)    LIMA    LIMA    VILLA EL SALVADOR
//L. COMERCIAL    AV. VILLA MARIA NRO. 907 P.J. CERCADO   LIMA    LIMA    VILLA MARIA DEL TRIUNFO
//L. COMERCIAL    CAL. BERMUDEZ NRO. 485 (CALLE BERMUDEZ 485-487) LORETO  MAYNAS  IQUITOS
//L. COMERCIAL    AV. LEON VELARDE NRO. 423   MADRE DE DIOS   TAMBOPATA   TAMBOPATA
//L. COMERCIAL    AV. M.L. URQUIETA NRO. 284  MOQUEGUA    ILO ILO
//L. COMERCIAL    CAL. ANCASH NRO. 210C   MOQUEGUA    MARISCAL NIETO  MOQUEGUA
//L. COMERCIAL    AV. CIRCUNVALACION ARENALES NRO. 190    PASCO   PASCO   CHAUPIMARCA
//L. COMERCIAL    CAL. JUNIN NRO. 499 PIURA   MORROPON    CHULUCANAS
//L. COMERCIAL    AV. PANAMERICANA NORTE MZA. K LOTE. 4 URB. MIRAFLORES LADO CENTRO   PIURA   PIURA   CASTILLA
//L. COMERCIAL    CAL. BOLIVAR NRO. 300   PIURA   SULLANA SULLANA
//L. COMERCIAL    PJ. TALARA NRO. 187 INT. 193 (CENTRO TALARA)    PIURA   TALARA  PARIÑAS
//L. COMERCIAL    AV. LA TORRE NRO. 123   PUNO    PUNO    PUNO
//L. COMERCIAL    JR. MARIANO E. NUÑEZ NRO. 610   PUNO    SAN ROMAN   JULIACA
//L. COMERCIAL    JR. HUALLAGA NRO. 849   SAN MARTIN  MARISCAL CACERES    JUANJUI
//L. COMERCIAL    JR. SERAFIN FILOMENO NRO. 257   SAN MARTIN  MOYOBAMBA   MOYOBAMBA
//L. COMERCIAL    JR. SAN MARTIN NRO. 422 (JR. SAN MARTIN 422 Y 426)  SAN MARTIN  SAN MARTIN  TARAPOTO
//L. COMERCIAL    AV. SAN MARTIN NRO. 889 TACNA   TACNA   TACNA
//L. COMERCIAL    JR. LOS ANDES NRO. 308  TUMBES  TUMBES  TUMBES
//L. COMERCIAL    JR. CORONEL PORTILLO NRO. 696 (698) UCAYALI CORONEL PORTILLO    CALLERIA
//SUCURSAL    MZA. A LOTE. 33 URB. CACHIMAYO (II ETAPA)   CUSCO   CUSCO   SAN SEBASTIAN
//SUCURSAL    AV. GONZALES PRADA NRO. 780 URB. SANTA MARIA (0 ETAPA)  LA LIBERTAD TRUJILLO    TRUJILLO
//SUCURSAL    MZA. F1 LOTE. 3 URB. MIRAFLORES PIURA   PIURA   CASTILLA
//SUCURSAL    JR. SAMANCO NRO. 366 URB. BUENOS AIRES  ANCASH  SANTA   NUEVO CHIMBOTE
//SUCURSAL    CAL. MARIANO MELGAR 302 MZA. Z LOTE. 7B URB. LA LIBERTAD    AREQUIPA    AREQUIPA    CERRO COLORADO
//SUCURSAL    AV. MARISCAL CACERES NRO. 1255 (1255 A 1257)    AYACUCHO    HUAMANGA    AYACUCHO
//SUCURSAL    JR. PEDRO RUIZ NRO. 145 BARRIO COLMENA BAJA CAJAMARCA   CAJAMARCA   CAJAMARCA
//SUCURSAL    JR. 2 DE MAYO NRO. 1391 (C.U HUANUCO)   HUANUCO HUANUCO HUANUCO
//SUCURSAL    CONDE DE NIEVA NRO. 438 ICA ICA ICA
//SUCURSAL    AV. 13 DE NOVIEMBRE NRO. 346 (SEC. EL TAMBO SECTOR 2)   JUNIN   HUANCAYO    EL TAMBO
//SUCURSAL    CAL. MANUEL MARIA IZAGA NRO. 737 (ZONA CERCADO DE CHICLAYO) LAMBAYEQUE  CHICLAYO    CHICLAYO
//SUCURSAL    AV. JOSE GRANDA 3136 MZA. Z LOTE. 12 COO. AMA KELLA 1RA ETAPA   LIMA    LIMA    SAN MARTIN DE PORRES
//SUCURSAL    MZA. E LOTE. 17 URB. EL ASESOR II   LIMA    LIMA    SANTA ANITA
//SUCURSAL    AV. SEPARADORA INDUSTRIAL MZA. I´ LOTE. 01 URB. PACHACAMAC (BARRIO 2 SECTOR 1)  LIMA    LIMA    VILLA EL SALVADOR
//SUCURSAL    CAL. MOORE NRO. 1012 SECTOR 5   LORETO  MAYNAS  IQUITOS
//SUCURSAL    JR. SANTIAGO MAMANI NRO. 154 LA RINCONADA   PUNO    SAN ROMAN   JULIACA
//SUCURSAL    JR. LOS GIRASOLES NRO. SN (CD 3 6B) SAN MARTIN  SAN MARTIN  TARAPOTO
//SUCURSAL    AV. TARATA NRO. 148 (148 A 150)}
