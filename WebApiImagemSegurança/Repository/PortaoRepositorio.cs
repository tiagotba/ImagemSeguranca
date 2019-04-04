using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using WebApiImagemSegurança.Context;
using WebApiImagemSegurança.Models;
using WebApiImagemSegurança.Repository;

namespace WebApiImagemSegurança.Repository
{
    public class PortaoRepositorio : IRepository<Portao> , IPortaoRepository
    {

        private BD_Context _context = null;

        public PortaoRepositorio(BD_Context context)
        {
            _context = context;
        }

        public Portao Get(System.Linq.Expressions.Expression<Func<Portao, bool>> predicate)
        {
            return _context.Portoes.SingleOrDefault(predicate);
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
            return _context.Portoes.AsEnumerable().ToList();
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

        void IPortaoRepository.Abrir(Portao entity)
        {
            if (entity.portaoAberto == false)
            {
                entity.portaoAberto = true;
            }
            _context.Portoes.Attach(entity);
            ((IObjectContextAdapter)_context).ObjectContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
        }

        void IPortaoRepository.Fechar(Portao entity)
        {
            if (entity.portaoAberto)
            {
                entity.portaoAberto = false;
            }
            _context.Portoes.Attach(entity);
            ((IObjectContextAdapter)_context).ObjectContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
        }
    }
}