using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Tsp.Sigescom.Modelo.Interfaces.Infraestructura;
using ZXing;

namespace Tsp.Sigescom.Utilitarios
{
    public class BarCodeUtil : IBarCodeUtil
    {
        public byte[] ObtenerCodigoBarras(string code)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.CODE_128
            };
            var bitMap = writer.Write(code);
            writer.Options.Height = 550;
            writer.Options.Width = 550;
            //Bitmap barcodeBitmap = new Bitmap(result);
            MemoryStream ms = new MemoryStream();
            bitMap.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public byte[] ObtenerCodigoQR(string code)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE
            };
            var bitMap = writer.Write(code);
            writer.Options.Height = 550;
            writer.Options.Width = 550;
            //Bitmap barcodeBitmap = new Bitmap(result);
            MemoryStream ms = new MemoryStream();
            bitMap.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }

    }
}
