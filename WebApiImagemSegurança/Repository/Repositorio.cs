using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using WebApiImagemSegurança.Context;

namespace WebApiImagemSegurança.Repository
{
    public class Repositorio<T> : IRepository<T> where T : class
    {
        private BD_Context _context = null;

        DbSet<T> m_DbSet;

        public Repositorio(BD_Context context)
        {
            _context = context;
            m_DbSet = _context.Set<T>();
        }

        void IRepository<T>.Add(T entity)
        {
            m_DbSet.Add(entity);
        }

        void IRepository<T>.Desliga(T entity)
        {
            m_DbSet.Attach(entity);
            ((IObjectContextAdapter)_context).ObjectContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
        }

        IEnumerable<T> IRepository<T>.GetAll()
        {
            return m_DbSet.AsEnumerable();
        }

        void IRepository<T>.Liga(T entity)
        {
            m_DbSet.Attach(entity);
            ((IObjectContextAdapter)_context).ObjectContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
        }
    }
}