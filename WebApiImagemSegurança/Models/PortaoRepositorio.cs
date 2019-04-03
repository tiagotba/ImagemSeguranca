using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using WebApiImagemSegurança.Context;
using WebApiImagemSegurança.Repository;

namespace WebApiImagemSegurança.Models
{
    public class PortaoRepositorio : IRepository<Portao>
    {

        private BD_Context _context = null;

        public PortaoRepositorio(BD_Context context)
        {
            _context = context;
        }

        void IRepository<Portao>.Add(Portao entity)
        {
            _context.Portoes.Add(entity);
        }

        void IRepository<Portao>.Desliga(Portao entity)
        {
            if (entity.portaoLigado)
            {
                entity.portaoLigado = false;
            }
            _context.Portoes.Attach(entity);
            ((IObjectContextAdapter)_context).ObjectContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
        }

        IEnumerable<Portao> IRepository<Portao>.GetAll()
        {
            return _context.Portoes.AsEnumerable();
        }

        void IRepository<Portao>.Liga(Portao entity)
        {
            if (entity.portaoLigado == false)
            {
                entity.portaoLigado = true;
            }
            _context.Portoes.Attach(entity);
            ((IObjectContextAdapter)_context).ObjectContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
        }
    }
}