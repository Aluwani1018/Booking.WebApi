
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Subscription.WebApi.Models.Authentication.Requests
{
    public class RefreshTokenRequest
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(255)]
        [JsonPropertyName("email")]
        public string UserEmail { get; set; }
    }
}
