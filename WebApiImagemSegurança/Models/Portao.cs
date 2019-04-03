using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiImagemSegurança.Models
{
    public class Portao : Dispositivo
    {
        public int idPortao { get; set; }
        public string nome { get; set; }
        public List<EventosDispositivo> eventos { get; set; }
    }
}