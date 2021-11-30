using System;
using System.Threading.Tasks;
using IoTDominio;
using Servicios;
using Servicios.Repositorio;

namespace IoTBLL
{
    public sealed class TemperaturaBLL
    {
        #region Singleton
        private readonly static TemperaturaBLL _instance = new TemperaturaBLL();

        public static TemperaturaBLL Current
        {
            get
            {
                return _instance;
            }
        }

        private TemperaturaBLL()
        {
            //Implent here the initialization of your singleton
        }
        #endregion


        private int paciente = Convert.ToInt32(ApplicationSettings.PacienteSet);

        public async Task<string> EnviarAsync(string valor)
        {
            try
            {
                Temperatura temperatura = new Temperatura();
                temperatura.Valor = Convert.ToInt32(valor);
                temperatura.Unidad = "°C";
                temperatura.Fecha = DateTime.Now;
                temperatura.IdPaciente = paciente;

                var response = TemperaturaServiceClient.Post(temperatura);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }

}
