using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WebApiImagemSegurança.Context;
using WebApiImagemSegurança.Models;

namespace WebApiImagemSegurança.Repository
{
    public class EventosDispositivoRepositorio : IEventosDispositivosRepository
    {
        private BD_Context _context = null;

        public EventosDispositivoRepositorio(BD_Context context)
        {
            _context = context;
        }


        EventosDispositivo IEventosDispositivosRepository.GetDispositivo(Expression<Func<EventosDispositivo, bool>> predicate)
        {
            return _context.EventosDispositivos.SingleOrDefault(predicate);
        }

        void IEventosDispositivosRepository.SaveEvento(EventosDispositivo eventos)
        {
            _context.EventosDispositivos.Add(eventos);
        }
    }
}