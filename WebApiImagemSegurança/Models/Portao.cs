using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiImagemSegurança.Models
{
    public class Portao : Dispositivo
    {
        public string nome { get; set; }
        public List<EventosDispositivo> eventos { get; set; }
    }
}