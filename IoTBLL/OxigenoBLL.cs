using System;
using System.Threading.Tasks;
using IoTDominio;
using Servicios;
using Servicios.Repositorio;

namespace IoTBLL
{
    public sealed class OxigenoBLL
    {
        #region Singleton
        private readonly static OxigenoBLL _instance = new OxigenoBLL();

        public static OxigenoBLL Current
        {
            get
            {
                return _instance;
            }
        }

        private OxigenoBLL()
        {
            //Implent here the initialization of your singleton
        }
        #endregion


        private int paciente = Convert.ToInt32(ApplicationSettings.PacienteSet);

        public async Task<string> EnviarAsync(string valor)
        {
            try
            {
                Oxigeno oxigeno = new Oxigeno();
                oxigeno.Valor = Convert.ToInt32(valor);
                oxigeno.Unidad = "SPO2";
                oxigeno.Fecha = DateTime.Now;
                oxigeno.IdPaciente = paciente;

                //var response = await OxigenoServiceClient.Current.EnviarAsync(oxigeno);
                //return response.Content.ReadAsStringAsync().ToString();
                var response = OxigenoServiceClient.Post(oxigeno);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
