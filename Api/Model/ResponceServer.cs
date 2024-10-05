using System.Net;

namespace Api.Model
{
    public class ResponceServer
    {
        public ResponceServer()
        {
            this.IsSucsesful = true;
            this.ErrorMessages = new();
        }
        public bool IsSucsesful { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }

    }
}
