using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiImagemSegurança.Models;

namespace WebApiImagemSegurança.Repository
{
   public interface IPortaoRepository : IRepository<Portao>
    {
        void Abrir(Portao entity);
        void Fechar(Portao entity);
    }
}
