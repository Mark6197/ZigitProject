using System.Text.Json.Serialization;

namespace ZigitBackend.Authentication
{
    /// <summary>
    /// The result model that returned when user is authenticated
    /// </summary>
    public class JwtAuthResult
    {
        /// <summary>
        /// The JWT access token
        /// </summary>
        [JsonPropertyName("token")]
        public string AccessToken { get; set; }
    }
}