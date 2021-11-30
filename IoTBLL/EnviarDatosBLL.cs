using System;
using System.Threading.Tasks;
using IoTDominio;
using Servicios.Repositorio;

namespace IoTBLL
{
    public sealed class EnviarDatosBLL
    {
        #region Singleton
        private readonly static EnviarDatosBLL _instance = new EnviarDatosBLL();

        public static EnviarDatosBLL Current
        {
            get
            {
                return _instance;
            }
        }

        private EnviarDatosBLL()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        public async Task EnviarAsync(string oxigeno, string cardio, string temp)
        {
            try
            {
                await OxigenoBLL.Current.EnviarAsync(oxigeno);

                await CardioBLL.Current.EnviarAsync(cardio);

                await TemperaturaBLL.Current.EnviarAsync(temp);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
