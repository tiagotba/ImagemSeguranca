using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiImagemSegurança.Context;
using WebApiImagemSegurança.Models;

namespace WebApiImagemSegurança.Repository
{
    public class UnitOfWork : IUnitWork, IDisposable
    {
        private BD_Context _context = null;
        private Repositorio<Portao> portaoRepositorio = null;
        IPortaoRepository portaoRepository = null;
        ICameraRepository camera = null;
        private Repositorio<Camera> cameraRepositorio = null;
        private Repositorio<EventosDispositivo> eventosRepositorio = null;

        public UnitOfWork()
        {
            _context = new BD_Context();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public IRepository<Camera> CameraRepositorio
        {
            get
            {
                if (cameraRepositorio == null)
                {
                    cameraRepositorio = new Repositorio<Camera>(_context);
                }
                return cameraRepositorio;
            }
        }

        public IRepository<Portao> PortaoRepositorio
        {
            get
            {
                if (portaoRepositorio == null)
                {
                    portaoRepositorio = new Repositorio<Portao>(_context);
                }
                return portaoRepositorio;
            }
        }

        public IRepository<EventosDispositivo> EventosRepositorio
        {
            get
            {
                if (eventosRepositorio == null)
                {
                    eventosRepositorio = new Repositorio<EventosDispositivo>(_context);
                }
                return eventosRepositorio;
            }
        }

        public IPortaoRepository portao
        {

            get
            {
                if (portaoRepository == null)
                {
                    portaoRepository = new PortaoRepositorio(_context);
                }

                return portaoRepository;
            }
        }

        public ICameraRepository cameraRepository
        {

            get
            {
                if (camera == null)
                {
                    camera = new CameraRepositorio(_context);
                }

                return camera;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}