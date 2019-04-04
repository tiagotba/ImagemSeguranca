using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiImagemSegurança.Models;

namespace WebApiImagemSegurança.Repository
{
   public interface IEventosDispositivosRepository
    {
        void SaveEvento(EventosDispositivo eventos);
        EventosDispositivo GetDispositivo(System.Linq.Expressions.Expression<Func<EventosDispositivo, bool>> predicate);
    }
}
