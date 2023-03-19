using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
namespace Tsp.Sigescom.GeneradorCodigo
{
    public class Class1
    {

        //public EBooksEntities db;
        public SigescomEntities _db;


        public string ConnectionString;
            //=        @"metadata=res://*/EBooksModel.csdl|res://*/EBooksModel.ssdl|res://*/EBooksModel.msl;provider=System.Data.SqlServerCe.4.0;provider connection string=&quot;Data Source =D:\Sigescom.Web\Tsp.Sigescom\Tsp.Sigescom.WebApplication\App_Data\Ebooks.sdf&quot;";
        public Class1(string con)
        {

            con = @"metadata=res://*/SigescomModel.csdl|res://*/SigescomModel.ssdl|res://*/SigescomModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=BABY2-PC;initial catalog=tsolperu_sigescom_dev;persist security info=True;user id=sa;password=tsp;multipleactiveresultsets=True;application name=EntityFramework&quot;";
            //db = new EBooksEntities(con);
            //_db = new SigescomEntities(con);

        }

        public  List<Actor> Books()
        {
            return _db.Actor.ToList();
        }

    }
}
