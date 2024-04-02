using System.Dynamic;
using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Mottu.Service.Responses
{
    public class BaseResponse
    {
        public BaseResponse()
        {
        }

        public BaseResponse(string json)
        {
            Result = GetObject(json);
        }

        internal ExpandoObject? GetObject(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<ExpandoObject>(json);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [JsonIgnore]
        public ExpandoObject? Result { get; private set; }

        [JsonIgnore]
        public bool IsSucess { get; internal set; }

        [JsonIgnore]
        public HttpStatusCode StatusCode { get; internal set; }
        [JsonIgnore]
        public string Message { get; set; }
    }
}