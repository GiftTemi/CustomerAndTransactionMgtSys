using System.Net;

namespace CustomerAndTransactionMgt.Models.Model
{
    public class ResponseModel
    {
        public string Message { get; set; }
        public object Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public ResponseModel(string _Msg,object _Data, HttpStatusCode _statusCode)
        {
            Message = _Msg;
            Data = _Data;
            StatusCode = _statusCode;                
        }
        public ResponseModel()
        {

        }
    }
}
