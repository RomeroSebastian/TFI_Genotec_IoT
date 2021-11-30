using System;
using System.Threading.Tasks;
using IoTDominio;
using Servicios;
using Servicios.Repositorio;

namespace IoTBLL
{
    public sealed class CardioBLL
    {
        #region Singleton
        private readonly static CardioBLL _instance = new CardioBLL();

        public static CardioBLL Current
        {
            get
            {
                return _instance;
            }
        }

        private CardioBLL()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        private int paciente = Convert.ToInt32(ApplicationSettings.PacienteSet);
        
        public async Task<string> EnviarAsync(string valor)
        {
            try
            {
                Cardio cardio = new Cardio();
                cardio.Valor = Convert.ToInt32(valor);
                cardio.Unidad = "LPM";
                cardio.Fecha = DateTime.Now;
                cardio.IdPaciente = paciente;

                var response = CardioServiceClient.Post(cardio);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
