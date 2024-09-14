namespace Hrmanagement.Services.Interfaces
{
    public interface IApiServices
    {
        Task<HttpResponseMessage> getapi(string endPoint);
        Task<string> postApi<T>(string endPoint, T payload);
        Task<HttpResponseMessage> putApi(string endPoint);
        Task<HttpResponseMessage> deleteApi(string endPoint);
        Task<HttpResponseMessage> PostApiById(string endPoint);
        Task<HttpResponseMessage> getApiWeb(string endPoint);

        Task<string> PostFormData<T>(string endPoint, T payload);
    }
}
