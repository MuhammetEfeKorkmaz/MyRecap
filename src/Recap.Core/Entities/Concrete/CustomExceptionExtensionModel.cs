using Newtonsoft.Json;

namespace Recap.Core.Entities.Concrete
{
    public class CustomExceptionExtensionModel
    {
        public string ErrorStackTrace { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetail { get; set; }
        public int Level { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
