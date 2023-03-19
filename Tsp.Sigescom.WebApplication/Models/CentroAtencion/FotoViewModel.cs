using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class FotoViewModel
    {

        public string Foto { get; set; }
        public string FotoSrc { get; set; }
        public bool HayFoto { get; set; }


        public FotoViewModel()
        {

        }

        public FotoViewModel(bool hayFoto, byte [] foto)
        {

            this.Foto = Convert.ToBase64String(foto);
            this.HayFoto = hayFoto;
            this.FotoSrc = ResolverFoto(hayFoto , foto);
        }

        public string  ResolverFoto(bool hayFoto, byte[] foto)
        {
            string fotoSrc = "";
            if (hayFoto)
            {
                fotoSrc = ("data:image/jpeg;base64," + Convert.ToBase64String(foto, 0, foto.Length));
            }
            else
            {
                fotoSrc = "http://www.placehold.it/200x150/EFEFEF/AAAAAA&amp;text=no+image";
            }

            return fotoSrc;
        }

    }


}