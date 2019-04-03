using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiImagemSegurança.Models
{
    public class Dispositivo
    {
        public long id { get; set; }
        public string ip { get; set; }
        public string mac { get; set; }
        public bool status { get; set; }

    }
}