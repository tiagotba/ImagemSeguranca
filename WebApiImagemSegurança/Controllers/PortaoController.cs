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
               portoes  = uow.PortaoRepositorio.GetAll().ToList();
            }

            return portoes;
        }

        [Route("api/ativarportao/{id:int}")]
        [HttpPut]
        public void AtivarPortao(int id, [FromBody] Portao portao)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                EventosDispositivo eventosDispositivo = null;
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

                    eventosDispositivo = new EventosDispositivo();
                    eventosDispositivo.Portao = portao;
                    eventosDispositivo.idPortao = portao.idPortao;
                    eventosDispositivo.dataEvento = DateTime.Now;
                    eventosDispositivo.statusFalha = false;
                    eventosDispositivo.statusSucesso = true;
                    uow.eventosDispositivos.SaveEvento(eventosDispositivo);
                }
                else
                {
                    eventosDispositivo = new EventosDispositivo();
                    eventosDispositivo.Portao = portao;
                    eventosDispositivo.idPortao = portao.idPortao;
                    eventosDispositivo.dataEvento = DateTime.Now;
                    eventosDispositivo.statusFalha = true;
                    eventosDispositivo.statusSucesso = false;
                    uow.eventosDispositivos.SaveEvento(eventosDispositivo);
                }

                uow.Commit();
            }
        }

        [Route("api/abrirportao/{id:int}")]
        [HttpPut]
        public void AbrirPortao(int id, [FromBody] Portao portao)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                EventosDispositivo eventosDispositivo = null;
                var lPortao = uow.PortaoRepositorio.Get(p => p.idPortao == id);

                if (lPortao != null && lPortao.idPortao == portao.idPortao)
                {
                    if (portao.portaoAberto)
                    {
                        uow.portao.Abrir(portao);
                    }
                    else
                    {
                        uow.portao.Fechar(portao);
                    }

                    eventosDispositivo = new EventosDispositivo();
                    eventosDispositivo.Portao = portao;
                    eventosDispositivo.dataEvento = DateTime.Now;
                    eventosDispositivo.statusFalha = false;
                    eventosDispositivo.statusSucesso = true;
                    uow.eventosDispositivos.SaveEvento(eventosDispositivo);
                }
                else
                {
                    eventosDispositivo = new EventosDispositivo();
                    eventosDispositivo.Portao = portao;
                    eventosDispositivo.dataEvento = DateTime.Now;
                    eventosDispositivo.statusFalha = true;
                    eventosDispositivo.statusSucesso = false;
                    uow.eventosDispositivos.SaveEvento(eventosDispositivo);
                }

                uow.Commit();
            }
        }

    }
}
