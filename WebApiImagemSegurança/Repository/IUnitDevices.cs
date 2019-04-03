using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiImagemSegurança.Repository
{
  public interface IUnitDevices
    {
        ICameraRepository cameraRepository { get; }
        IPortaoRepository portaoRepository { get; }
        IEventosDispositivosRepository eventosDispositivosRepository { get; }
    }
}
