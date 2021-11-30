using System.Configuration;

namespace Servicios
{
    public static class ApplicationSettings
    {
        public static string PacienteSet = ConfigurationManager.AppSettings["Paciente"].ToString();
    }
}
