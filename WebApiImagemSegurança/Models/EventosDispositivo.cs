using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiImagemSegurança.Models
{
    public class EventosDispositivo
    {
        public long id { get; set; }
        public DateTime dataEvento { get; set; }
        public bool statusSucesso { get; set; }
        public bool statusFalha { get; set; }

        public int idPortao { get; set; }
        public Portao Portao { get; set; }
        public int idCamera { get; set; }
        public Camera Camera { get; set; }
    }
}