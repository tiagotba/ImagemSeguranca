﻿using System;
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
        public void AtivarCamera(int id, [FromBody] Camera camera)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                EventosDispositivo eventosDispositivo = null;
                var lCamera = uow.CameraRepositorio.Get(c => c.idCamera == id);

                if (lCamera != null && lCamera.idCamera == camera.idCamera)
                {
                    if (camera.cameraLigada)
                    {
                        uow.CameraRepositorio.Desliga(camera);
                    }
                    else
                    {
                        uow.CameraRepositorio.Liga(camera);
                    }
                    eventosDispositivo = new EventosDispositivo();
                    eventosDispositivo.Camera = camera;
                    eventosDispositivo.dataEvento = DateTime.Now;
                    eventosDispositivo.statusFalha = false;
                    eventosDispositivo.statusSucesso = true;
                    uow.eventosDispositivos.SaveEvento(eventosDispositivo);
                }
                else
                {
                    eventosDispositivo = new EventosDispositivo();
                    eventosDispositivo.Camera = camera;
                    eventosDispositivo.dataEvento = DateTime.Now;
                    eventosDispositivo.statusFalha = true;
                    eventosDispositivo.statusSucesso = false;
                    uow.eventosDispositivos.SaveEvento(eventosDispositivo);
                }

                uow.Commit();
            }
        }

        [Route("api/ativarsensor/{id:int}")]
        [HttpPut]
        public bool AtivarSensor(int id, [FromBody] Camera camera)
        {
            bool sensorLigado = false;
            using (UnitOfWork uow = new UnitOfWork())
            {
                
                var lCamera = uow.CameraRepositorio.Get(c => c.idCamera == id);
                if (lCamera != null && lCamera.idCamera == camera.idCamera)
                {
                    if (camera.sensorLigado)
                    {
                       sensorLigado = uow.cameraRepository.VerificaSensor(camera);
                    }
                    else
                    {
                      sensorLigado =  uow.cameraRepository.VerificaSensor(camera);
                    }
                }
            }

            return sensorLigado;
        }

    }
}
