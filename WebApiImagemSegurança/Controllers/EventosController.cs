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
    public class EventosController : ApiController
    {
        [Route("api/eventos/{id:int}")]
        [HttpGet]
        public EventosDispositivo GetEventos(int id)
        {
            EventosDispositivo evento = null;
            using (UnitOfWork uow = new UnitOfWork())
            {
                evento = uow.EventosRepositorio.Get(e => e.idCamera == id);
            }

            return evento;
        }
    }
}
