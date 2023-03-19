using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tsp.Sigescom.Modelo.Entidades;
using System.Xml;
using Tsp.FacturacionElectronica.AccesoDatos;
//using OpenInvoicePeru.Comun.Constantes;

namespace Tsp.UnitTestFacturacionElectronica
{
    [TestClass]
    public class UnitTest1
    {
        FacturacionDatos facturacionDatos = new FacturacionDatos();
        //public void GuardarMaestro(Maestro maestro)
        //{

        //    File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Content\images\codigo-qr.png"), ObtenerQRVenta(documentoImprimible.Hashcode));
        //    //System.IO.File.WriteAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"/Content/images/codigo-qr.png"), ObtenerQRVenta(documentoImprimible.Hashcode));
        //    codigoQRControl.Src = "~/Content/images/codigo-qr.png";


        //    DataContractJsonSerializer serializador = new DataContractJsonSerializer(typeof(Maestro));

        //    if (isf.FileExists("E://Maestro.json"))
        //    {
        //        isf.DeleteFile("E://Maestro.json");
        //    }
        //    Stream stream = isf.CreateFile("E://Maestro.json");
        //    serializador.WriteObject(stream, maestro);
        //    stream.Close();
        //}
        //XmlDocument documentoXml = new XmlDocument();

        //[TestMethod]
        //public void Main()
        //{
        //    FacturacionDatos facturacionDatos = new FacturacionDatos();

        //    int[] idDocumento = { 1, 2, 3, 4, 5, 6, 7 };

        //    for (int i = 0; i < idDocumento.Length; i++)
        //    {
        //        byte[] respuesta = facturacionDatos.ObtenerRespuestaXml(idDocumento[i]);
        //        System.Text.ASCIIEncoding codificador = new System.Text.ASCIIEncoding();
        //        documentoXml.LoadXml(codificador.GetString(respuesta));

        //        var xmlnsManager = new XmlNamespaceManager(documentoXml.NameTable); 

        //        xmlnsManager.AddNamespace("ar", EspacioNombres.ar);
        //        xmlnsManager.AddNamespace("cac", EspacioNombres.cac);
        //        xmlnsManager.AddNamespace("cbc", EspacioNombres.cbc);
        //        xmlnsManager.AddNamespace("sac", EspacioNombres.sac);
        //        xmlnsManager.AddNamespace("ext", EspacioNombres.ext);

        //        string IdReferencia = documentoXml.SelectSingleNode("//cbc:ReferenceID", xmlnsManager)?.InnerText;
        //        string CodRespuesta = documentoXml.SelectSingleNode("//cbc:ResponseCode", xmlnsManager)?.InnerText;
        //        string Descripcion = documentoXml.SelectSingleNode("//cbc:Description", xmlnsManager)?.InnerText;
        //        //this.Gravadas = Convert.ToDecimal(documentoXml.ChildNodes[1].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[1]?.InnerText);
        //        //this.Inafectas = Convert.ToDecimal(documentoXml.ChildNodes[1].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[1].ChildNodes[1]?.InnerText);
        //        //this.Exoneradas = Convert.ToDecimal(documentoXml.ChildNodes[1].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[2].ChildNodes[1]?.InnerText);
        //        //this.Gratuitas = Convert.ToDecimal(documentoXml.ChildNodes[1].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[3].ChildNodes[1]?.InnerText);
        //        //this.TotalIgv = Convert.ToDecimal(documentoXml.ChildNodes[1].ChildNodes[10].ChildNodes[1].ChildNodes[0]?.InnerText);

        //        //string result = System.Text.Encoding.UTF8.GetString(respuesta);
        //        //File.WriteAllBytes(@"E:\" + i + ".xml", Convert.FromBase64String(result));
        //    }

        //}

        


        //[TestMethod]
        //public void Main()
        //{
        //    Maestro maestro = new Maestro();
        //    maestro.id = 1;
        //    maestro.nombre = "PRINCE";
        //    maestro.codigo = "COD";
        //    maestro.idTipo = 123;


        //    // read file into a string and deserialize JSON to a type
        //    File.WriteAllText(@"E:\maestro.json", JsonConvert.SerializeObject(maestro));


        //    // serialize JSON directly to a file
        //    using (StreamWriter file = File.CreateText(@"E:\maestro.json"))
        //    {
        //        JsonSerializer serializer = new JsonSerializer();
        //        serializer.Serialize(file, maestro);
        //    }




        //    // read file into a string and deserialize JSON to a type
        //    Maestro miMaestro = JsonConvert.DeserializeObject<Maestro>(File.ReadAllText(@"E:\maestro.json"));


        //    // deserialize JSON directly from a file
        //    using (StreamReader file = File.OpenText(@"E:\maestro.json"))
        //    {
        //        JsonSerializer serializer = new JsonSerializer();
        //        Maestro miMaestro2 = (Maestro)serializer.Deserialize(file, typeof(Maestro));

        //    }

        //Maestro maestro = new Maestro();
        //maestro.id = 1;
        //maestro.nombre = "PRINCE";
        //maestro.codigo = "COD";
        //maestro.idTipo = 123;

        //GuardarMaestro(maestro);

        //DataContractJsonSerializer serializador = new DataContractJsonSerializer(typeof(Maestro));
        //IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
        //if (isf.FileExists("E:/Maestro.json"))
        //{
        //    Stream stream = isf.OpenFile("E://Maestro.json", FileMode.Open);
        //    Maestro miMaestro = serializador.ReadObject(stream) as Maestro;
        //    Assert.IsNotNull(miMaestro);
        //}



        //private int CountCharacters()
        //{
        //    int count = 0;
        //    // Create a StreamReader and point it to the file to read
        //    using (StreamReader reader = new StreamReader("C:\\Data\\Data.txt"))
        //    {
        //        string content = reader.ReadToEnd();
        //        count = content.Length;
        //        // Make the program look busy for 5 seconds
        //        Thread.Sleep(5000);
        //    }

        //    return count;
        //}
        //[TestMethod]
        //public void ContarCaracteres()
        //{
        //    ContarCaracteres_();
        //    Assert.IsTrue(1 == 1);
        //}

        //private async void ContarCaracteres_()
        //{
        //    Task<int> task = new Task<int>(CountCharacters);
        //    task.Start();

        //    Console.WriteLine("Processing file. Please wait...");
        //    int count = await task;
        //    Console.WriteLine(count.ToString() + " characters in file");
        //}
    }
}