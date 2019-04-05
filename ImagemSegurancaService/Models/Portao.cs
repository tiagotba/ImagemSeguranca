using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagemSegurancaService.Models
{
   public class Portao
    {
        public int idPortao { get; set; }
        public string nome { get; set; }
        public bool portaoLigado { get; set; }
        public bool portaoAberto { get; set; }
    }
}
