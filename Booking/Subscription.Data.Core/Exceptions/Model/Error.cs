using System.Text.Json.Serialization;

namespace Subscription.Infrastructure.Exceptions
{
    public class Error
    {
        [JsonPropertyName("code")]
        public int ErrorCode { get; set; }

        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }
    }
}
