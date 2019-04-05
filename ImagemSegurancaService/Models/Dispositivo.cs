using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagemSegurancaService.Models
{
  public  class Dispositivo
    {
        public string ip { get; set; }
        public string mac { get; set; }
        public bool status { get; set; }
    }
}
