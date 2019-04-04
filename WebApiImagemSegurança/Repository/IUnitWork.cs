using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiImagemSegurança.Models;

namespace WebApiImagemSegurança.Repository
{
  public interface IUnitWork
    {
        IRepository<Camera> CameraRepositorio { get; }
        IRepository<Portao> PortaoRepositorio { get; }
        IPortaoRepository portao { get; }
        ICameraRepository cameraRepository { get; }
        IRepository<EventosDispositivo> EventosRepositorio { get; }
        void Commit();
    }
}
