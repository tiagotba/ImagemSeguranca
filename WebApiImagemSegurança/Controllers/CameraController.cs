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
    public class CameraController : ApiController
    {

        [HttpGet]
        public IEnumerable<Camera> Get()
        {
            IEnumerable<Camera> cameras = null;
            using (UnitOfWork uow = new UnitOfWork())
            {
                cameras = uow.CameraRepositorio.GetAll().ToList();
            }

            return cameras;
        }

        [Route("api/ativarcamera/{id:int}")]
        [HttpPut]
        public object AtivarCamera(int id, [FromBody] Camera camera)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                try
                {
                    EventosDispositivo eventosDispositivo = null;
                    var lCamera = uow.CameraRepositorio.Get(c => c.idCamera == id);

                    if (lCamera != null && lCamera.idCamera == camera.idCamera)
                    {
                        if (camera.cameraLigada)
                        {
                            lCamera.cameraLigada = camera.cameraLigada;
                            uow.cameraRepository.Desliga(lCamera);
                        }
                        else
                        {
                            lCamera.cameraLigada = camera.cameraLigada;
                            uow.cameraRepository.Liga(lCamera);
                        }
                        //uow.Commit();
                        eventosDispositivo = new EventosDispositivo();
                        eventosDispositivo.Camera = camera;
                        eventosDispositivo.idCamera = camera.idCamera;
                        eventosDispositivo.idPortao = 1;
                        eventosDispositivo.dataEvento = DateTime.Now;
                        eventosDispositivo.statusFalha = false;
                        eventosDispositivo.statusSucesso = true;

                        uow.eventosDispositivos.SaveEvento(eventosDispositivo);
                        uow.Commit();

                        return new
                        {
                            Evento = eventosDispositivo
                        };
                    }
                    else
                    {
                        eventosDispositivo = new EventosDispositivo();
                        eventosDispositivo.Camera = camera;
                        eventosDispositivo.idCamera = camera.idCamera;
                        eventosDispositivo.idPortao = 1;
                        eventosDispositivo.dataEvento = DateTime.Now;
                        eventosDispositivo.statusFalha = true;
                        eventosDispositivo.statusSucesso = false;
                        uow.eventosDispositivos.SaveEvento(eventosDispositivo);
                        uow.Commit();
                        var resp = new HttpResponseMessage(HttpStatusCode.NotImplemented)
                        {
                            Content = new StringContent("Erro de processamento"),
                            ReasonPhrase = "A Camera não existe no cliente." + eventosDispositivo.idCamera + "," + eventosDispositivo.statusFalha
                        };

                        throw new HttpResponseException(resp);
                    }
                }
                catch (Exception ex)
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("Erro de processamento na Api"),
                        ReasonPhrase = "Excessão: " + ex.Message + "," + ex.InnerException
                    };

                    throw new HttpResponseException(resp);

                }

            }
        }

        [Route("api/ativarsensor/{id:int}")]
        [HttpPut]
        public bool AtivarSensor(int id, [FromBody] Camera camera)
        {
            bool sensorLigado = false;
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {

                    var lCamera = uow.CameraRepositorio.Get(c => c.idCamera == id);
                    if (lCamera != null && lCamera.idCamera == camera.idCamera)
                    {
                        if (lCamera.sensorLigado)
                        {
                            sensorLigado = uow.cameraRepository.VerificaSensor(lCamera);
                        }

                    }
                }

                return sensorLigado;
            }
            catch (Exception ex)
            {

                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Erro de processamento na Api"),
                    ReasonPhrase = "Excessão: " + ex.Message + "," + ex.InnerException
                };

                throw new HttpResponseException(resp);
            }
           
        }

    }
}
