﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using WebApiImagemSegurança.Context;
using WebApiImagemSegurança.Models;

namespace WebApiImagemSegurança.Repository
{
    public class CameraRepositorio : IRepository<Camera>
    {

        private BD_Context _context = null;

        public CameraRepositorio(BD_Context context)
        {
            _context = context;
        }

        void IRepository<Camera>.Add(Camera entity)
        {
            _context.Cameras.Add(entity);
        }

        void IRepository<Camera>.Desliga(Camera entity)
        {
            if (entity.cameraLigada)
            {
                entity.cameraLigada = false;
            }
            _context.Cameras.Attach(entity);
            ((IObjectContextAdapter)_context).ObjectContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
        }

        IEnumerable<Camera> IRepository<Camera>.GetAll()
        {
            return _context.Cameras.AsEnumerable();
        }

        void IRepository<Camera>.Liga(Camera entity)
        {
            if (entity.cameraLigada == false)
            {
                entity.cameraLigada = true;
            }
            _context.Cameras.Attach(entity);
            ((IObjectContextAdapter)_context).ObjectContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
        }
    }
}