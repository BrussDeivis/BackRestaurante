using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades
{

    public class WriteFile
    {
        [Conditional("DEBUG")]
        public static void WriteSQL(string data)
        {
            try
            {
                string path = @"c:\SQLtrace.txt";
                File.AppendAllText(path, data);
            }
            catch (Exception)
            {
            }

        }
    }
}
