using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiImagemSegurança.Models;
using WebApiImagemSegurança.Repository;

namespace WebApiImagemSegurança.Controllers
{
    public class PortaoController : ApiController
    {
        [HttpGet]
        public IEnumerable<Portao> Get()
        {
            IEnumerable<Portao> portoes = null;
            using (UnitOfWork uow = new UnitOfWork())
            {
               portoes  = uow.PortaoRepositorio.GetAll();
            }

            return portoes;
        }

        [Route("api/ativarportao/{id:int}")]
        [HttpPut]
        public void AtivarPortao(int id, [FromBody] Portao portao)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var lPortao = uow.PortaoRepositorio.Get(p=> p.idPortao == id);
                if (lPortao != null && lPortao.idPortao == portao.idPortao)
                {
                    if (portao.portaoLigado)
                    {
                        uow.PortaoRepositorio.Desliga(portao);
                    }
                    else
                    {
                        uow.PortaoRepositorio.Liga(portao);
                    }
                }
            }
        }

        //[Route("api/abrirportao/{id:int}")]
        //[HttpPut]
        //public void AbrirPortao(int id, [FromBody] Portao portao)
        //{
        //    using (UnitOfWork uow = new UnitOfWork())
        //    {
        //        var lPortao = uow.PortaoRepositorio.Get(p => p.idPortao == id);
        //        if (lPortao != null && lPortao.idPortao == portao.idPortao)
        //        {
        //            if (portao.portaoAberto)
        //            {
        //                uow.PortaoRepositorio.(portao);
        //            }
        //            else
        //            {
        //                uow.PortaoRepositorio.Liga(portao);
        //            }
        //        }
        //    }
        //}

    }
}
