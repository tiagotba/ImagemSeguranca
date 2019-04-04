using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
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

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return m_DbSet.FirstOrDefault(predicate);
        }

        void IRepository<T>.Desliga(T entity)
        {
            //m_DbSet.Add(entity);
            ((IObjectContextAdapter)_context).ObjectContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
        }

        IEnumerable<T> IRepository<T>.GetAll()
        {
            return m_DbSet.AsEnumerable();
        }

        void IRepository<T>.Liga(T entity)
        {
            //  _context.Entry(entity).State = EntityState.Detached;
              _context.Entry(m_DbSet).State = EntityState.Modified;
          // ((IObjectContextAdapter)_context).ObjectContext.Detach(entity);
            ((IObjectContextAdapter)_context.Entry(m_DbSet)).ObjectContext.ObjectStateManager.ChangeObjectState(m_DbSet, EntityState.Modified);
        }
    }
}