using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiImagemSegurança.Models
{
    public class Camera: Dispositivo
    {
        public int idCamera { get; set; }
        public string nome { get; set; }
        public bool sensorLigado { get; set; }
        public bool cameraLigada { get; set; }
        public List<EventosDispositivo> eventos { get; set; }
    }
}