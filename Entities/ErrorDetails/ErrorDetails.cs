using Newtonsoft.Json;

namespace Entity.ErrorDetails
{
    public class ErrorDetails
    {
                public string Message { get; set; }
                public int StatusCod { get; set; }
               public override string ToString()
                {
                            return JsonConvert.SerializeObject(this);
                }
    }
}