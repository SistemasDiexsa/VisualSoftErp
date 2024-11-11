using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftErp.Clases.Tesk.Response;

namespace VisualSoftErp.Interfaces
{
    public class MalocClient
    {
        public string strCuentaContable { get; set; }
        public T PeticionesWSGet<T>(string url,string action) where T : class, new()
        {
            T modelo = new T();   //T es un tipo de dato X (cualquier tipo de dato)
            var cliente = new RestClient(url);
            try
            {
                RestClient restclient = new RestClient(url + action);
                var request = new RestRequest(Method.Get.ToString());
                RestResponse restResponse = restclient.Execute(request);
                modelo = JsonConvert.DeserializeObject<T>(restResponse.Content);

            }
            catch(Exception ex)
            {
                throw ex;
            }

            return modelo;
        } //Eof:PeticionesWSGet

        public T PeticionesWSPost<T>(string url, string action, object Datos = null) where T : class, new()
        {
            T modelo = new T();
            var cliente = new RestClient(url);

            try
            {
                RestClient restclient = new RestClient(url + action);
                var request = new RestRequest(Method.Post.ToString());
                if (Datos != null)
                {
                    JObject dataRequest = JObject.FromObject(Datos);
                    request.AddParameter("application/json", dataRequest.ToString(Newtonsoft.Json.Formatting.None), ParameterType.RequestBody);
                }
                RestResponse restResponse = restclient.Execute(request);
                modelo = JsonConvert.DeserializeObject<T>(restResponse.Content);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return modelo;
        }

        public T PeticionesWSPostTesk<T>(string url, string action, object Datos = null) where T : class, new()
        {
            T modelo = new T();
            var cliente = new RestClient(url);

            try
            {

                RestClient restclient = new RestClient(url + action);
                var request = new RestRequest(Method.Post.ToString());
                if (Datos != null)
                {
                    JObject dataRequest = JObject.FromObject(Datos);
                    request.AddParameter("application/json", dataRequest.ToString(Newtonsoft.Json.Formatting.None), ParameterType.RequestBody);
                }
                RestResponse restResponse = restclient.Execute(request);

                teskloginresponse login = new teskloginresponse();
                login.token = JsonConvert.DeserializeObject<string>(restResponse.Content);
                string jsonString = JsonConvert.SerializeObject(login);

                if (modelo == null)
                    return modelo;
                else
                    modelo = JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch (Exception)
            {
                return null;
                //throw ex;
            }

            return modelo;
        }

        public T PeticionesWSPostWithHeaders<T>(string url, string action, string token, string codigoempresa, object Datos = null) where T : class, new()
        {
            T modelo = new T();
            var cliente = new RestClient(url);
            string location = string.Empty;

            try
            {
                RestClient restclient = new RestClient(url + action);
                var request = new RestRequest(Method.Post.ToString());
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddHeader("CodigoEmpresa", codigoempresa);
                if (Datos != null)
                {
                    JObject dataRequest = JObject.FromObject(Datos);
                    request.AddParameter("application/json", dataRequest.ToString(Newtonsoft.Json.Formatting.None), ParameterType.RequestBody);
                }
                RestResponse restResponse = restclient.Execute(request);
                modelo = JsonConvert.DeserializeObject<T>(restResponse.Content);


                location = restResponse.Headers.ToList().Find(x => x.Name == "Location") == null ? "" : restResponse.Headers.ToList().Find(x => x.Name == "Location").Value.ToString();

                if (modelo == null)
                    strCuentaContable = location;
                else
                    strCuentaContable = string.Empty;

            }
            catch (Exception ex)
            {
                string x = string.Empty;
                //throw ex;
            }

            return modelo;
        }

        public T PeticionesWSPostWithHeadersString<T>(string url, string action, string token, string codigoempresa, object Datos = null) where T : class, new()
        {
            T modelo = new T();
            var cliente = new RestClient(url);
            string respuesta = string.Empty;

            try
            {
                RestClient restclient = new RestClient(url + action);
                var request = new RestRequest(Method.Post.ToString());
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddHeader("CodigoEmpresa", codigoempresa);
                if (Datos != null)
                {
                    JObject dataRequest = JObject.FromObject(Datos);
                    request.AddParameter("application/json", dataRequest.ToString(Newtonsoft.Json.Formatting.None), ParameterType.RequestBody);
                }
                RestResponse restResponse = restclient.Execute(request);
                modelo = JsonConvert.DeserializeObject<T>(restResponse.Content);

                string location = string.Empty;

                //location = restResponse.StatusCode.ToString();
                //if (location == "OK")
                //{
                //    respuesta = restResponse.StatusDescription.ToString();
                //    //modelo = JsonConvert.DeserializeObject<T>(restResponse.Content);
                //}
                //else
                //{
                //    respuesta = restResponse.Content.ToString();
                //}

            }
            catch (Exception ex)
            {
                string x = string.Empty;
                //throw ex;
            }

            return modelo;
        }

        public T PeticionesWSPostWithHeadersReturnZip<T>(string url, string action, string token, string codigoempresa, object Datos = null) where T : class, new()
        {
            T modelo = new T();
            var cliente = new RestClient(url);

            try
            {
                RestClient restclient = new RestClient(url + action);
                var request = new RestRequest(Method.Post.ToString());
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddHeader("CodigoEmpresa", codigoempresa);
                if (Datos != null)
                {
                    JObject dataRequest = JObject.FromObject(Datos);
                    request.AddParameter("application/json", dataRequest.ToString(Newtonsoft.Json.Formatting.None), ParameterType.RequestBody);
                }
                RestResponse restResponse = restclient.Execute(request);
                //modelo = JsonConvert.DeserializeObject<T>(restResponse.Content);
                if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    modelo = null;
                else
                    modelo = JsonConvert.DeserializeObject<T>(restResponse.Content);

                string location = restResponse.Headers.ToList().Find(x => x.Name == "Location") == null ? "" : restResponse.Headers.ToList().Find(x => x.Name == "Location").Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return modelo;
        }
    } //Eof:MalocClient
}
