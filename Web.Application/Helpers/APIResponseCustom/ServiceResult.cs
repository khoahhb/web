using System.Net;

namespace Web.Application.Helpers.APIResponseCustom
{
    public class ServiceResult<T>
    {
        public T SuccessData { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ServiceResult<T> SuccessResult(HttpStatusCode status, T data) => new ServiceResult<T> { StatusCode = status, SuccessData = data };
        public ServiceResult<T> Failure(HttpStatusCode status) => new ServiceResult<T> { StatusCode = status };
    }

}
