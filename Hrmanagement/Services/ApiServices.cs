using Hrmanagement.Services.Interfaces;
using Newtonsoft.Json;
using NuGet.Common;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Security.Policy;
using System.Text;

namespace Hrmanagement.Services
{
    public class ApiServices : IApiServices
    {
        public IConfiguration _configuration;
        public string baseUrl { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiServices(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            baseUrl = _configuration.GetSection("BaseUrl:baseUrl").Value;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<HttpResponseMessage> getApiWeb(string endPoint)
        {
            string token = _httpContextAccessor.HttpContext?.Session.GetString("token");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync($"{baseUrl}{endPoint}");
            //client.DefaultRequestHeaders.Add("Custom-Header", "header_value");
            return response;
        }
        public async Task<HttpResponseMessage> getapi(string endPoint)
        {
            string token = _httpContextAccessor.HttpContext?.Session.GetString("token");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync($"{baseUrl}{endPoint}");
            return response;
        }
        public async Task<string> PostFormData<T>(string endPoint, T payload)
        {
            try
            {
                string token = _httpContextAccessor.HttpContext?.Session.GetString("token");
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using var content = new MultipartFormDataContent();
                foreach (var property in payload.GetType().GetProperties())
                {
                    if (property.PropertyType == typeof(IFormFile))
                    {
                        var imageFile = (IFormFile)property.GetValue(payload);
                        if (imageFile != null)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                await imageFile.CopyToAsync(memoryStream);
                                var byteArrayContent = new ByteArrayContent(memoryStream.ToArray());
                                content.Add(byteArrayContent, property.Name, imageFile.FileName);
                            }
                        }
                    }
                    else
                    {
                        content.Add(new StringContent(property.GetValue(payload)?.ToString() ?? ""), property.Name);
                    }
                }
                HttpResponseMessage response = await client.PostAsync($"{baseUrl}{endPoint}", content);
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return "Failed to make the request";
            }
        }
        public async Task<string> postApi<T>(string endPoint, T payload)
        {
            string token = _httpContextAccessor.HttpContext?.Session.GetString("token");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string jsonData = JsonConvert.SerializeObject(payload, Newtonsoft.Json.Formatting.Indented);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{baseUrl}{endPoint}", content);
            var rslt = await response.Content.ReadAsStringAsync();
            return rslt;
        }
        public async Task<HttpResponseMessage> putApi(string endPoint)
        {
            string token = _httpContextAccessor.HttpContext?.Session.GetString("token");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = $"{baseUrl}{endPoint}";
            HttpResponseMessage response = await client.PutAsync(url, null);
            return response;
        }
        public async Task<HttpResponseMessage> deleteApi(string endPoint)
        {
            string token = _httpContextAccessor.HttpContext?.Session.GetString("token");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.DeleteAsync($"{baseUrl}{endPoint}");
            return response;
        }
        public async Task<HttpResponseMessage> PostApiById(string endPoint)
        {
            string token = _httpContextAccessor.HttpContext?.Session.GetString("token");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = $"{baseUrl}{endPoint}";
            HttpResponseMessage response = await client.PostAsync(url, null);
            return response;
        }

        // change password work

        //public async Task<HttpResponseMessage> putApi(string endPoint)
        //{
        //    string token = _httpContextAccessor.HttpContext?.Session.GetString("token");
        //    var client = new HttpClient();
        //    client.DefaultRequestHeaders.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    var url = $"{baseUrl}{endPoint}";
        //    HttpResponseMessage response = await client.PutAsync(url, null);
        //    return response;
        //}
    }
}
