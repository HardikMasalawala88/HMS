using HMS.Data.FormModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public class BaseService
    {
        public Response _response;
        public BaseService()
        {
            _response = new Response();
        }

        //public Response CreateResponse(object data, HttpStatusCode statusCode, string message)
        //{
        //    _response.Data = JsonConvert.SerializeObject(data);
        //    _response.Status = statusCode;
        //    _response.Message = message;

        //    return _response;
        //}
    }
}
