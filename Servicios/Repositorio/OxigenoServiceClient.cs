using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace Servicios.Repositorio
{
    public static class OxigenoServiceClient
    {
        #region PruebaHttpClient
        //private readonly static OxigenoServiceClient _instance = new OxigenoServiceClient();
        //private readonly HttpClient httpClient;

        //public static OxigenoServiceClient Current
        //{
        //    get
        //    {
        //        return _instance;
        //    }
        //}

        //private OxigenoServiceClient()
        //{
        //    httpClient = new HttpClient();
        //    httpClient.BaseAddress = new Uri("https://localhost:44355/api/");
        //    httpClient.DefaultRequestHeaders.Accept.Clear();
        //    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //}

        //public async Task<HttpResponseMessage> EnviarAsync(IoTDominio.Oxigeno oxigeno)
        //{
        //    var enviarJSON = JsonConvert.SerializeObject(oxigeno);
        //    var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
        //    var responseHttp = await httpClient.PostAsync("Oxigeno", enviarContent);
        //    return responseHttp;
        //}
        #endregion

        public static string Post(IoTDominio.Oxigeno oxigeno)
        {
            var url = $"https://localhost:44355/api/Oxigeno/";
            var request = (HttpWebRequest)WebRequest.Create(url);
            var enviarJSON = JsonConvert.SerializeObject(oxigeno);
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
