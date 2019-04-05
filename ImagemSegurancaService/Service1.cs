using ImagemSegurancaService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ImagemSegurancaService
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteToFile("Service is started at " + DateTime.Now);
            CallApi();
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 5000; //number in milisecinds  
            timer.Enabled = true;
        }

        private void CallApi()
        {
            Camera cam = new Camera { idCamera = 1 };
            //Verifica se o sensor está ativado
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60935/");
                var response = client.PutAsJsonAsync("api/ativarsensor/" + cam.idCamera, cam).Result;
                if (response.IsSuccessStatusCode)
                    WriteToFile("Sensor da Camera" + cam.idCamera + " Ativado");

            }

            using (var client = new HttpClient())
            {
                cam.cameraLigada = true;
                client.BaseAddress = new Uri("http://localhost:60935/");
                var response = client.PutAsJsonAsync("api/ativarcamera/" + cam.idCamera, cam).Result;
                if (response.IsSuccessStatusCode)
                {
                    WriteToFile("Ativação realizada com sucesso e registro salvo na base de dados");
                }
                else
                    WriteToFile("Error" + response.RequestMessage);
            }

        }

        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteToFile("Service is recall at " + DateTime.Now);
        }

        protected override void OnStop()
        {
            WriteToFile("Service is stopped at " + DateTime.Now);
        }
    }
}
