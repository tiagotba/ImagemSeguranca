using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagemSegurancaService.Models
{
  public  class Camera
    {
        public int idCamera { get; set; }
        public string nome { get; set; }
        public bool sensorLigado { get; set; }
        public bool cameraLigada { get; set; }
    }
}
