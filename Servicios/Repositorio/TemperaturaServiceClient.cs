using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace Servicios.Repositorio
{
    public static class TemperaturaServiceClient
    {
        public static string Post(IoTDominio.Temperatura temperatura)
        {
            var url = $"https://localhost:44355/api/Temperatura/";
            var request = (HttpWebRequest)WebRequest.Create(url);
            var enviarJSON = JsonConvert.SerializeObject(temperatura);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(enviarJSON);
                streamWriter.Flush();
                streamWriter.Close();
            }
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return null;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            return responseBody;
                            //Console.WriteLine(responseBody);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
                return ex.Message;
            }
        }
    }
}
