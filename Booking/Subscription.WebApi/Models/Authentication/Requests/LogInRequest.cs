
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Subscription.WebApi.Models.Authentication.Requests
{
    public class LogInRequest
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(255)]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [Required]
        [StringLength(32)]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
