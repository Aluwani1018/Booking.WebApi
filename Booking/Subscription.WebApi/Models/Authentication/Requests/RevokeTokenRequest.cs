using System.Text.Json.Serialization;

namespace Subscription.WebApi.Models.Authentication.Requests
{
    public class RevokeTokenRequest
    {
        [JsonPropertyName("refeshToken")]
        public string RefeshToken { get; set; }
    }
}
