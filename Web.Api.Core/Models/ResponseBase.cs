using System.Text.Json.Serialization;

namespace Web.Api.Core.Models
{
    /// <summary>
    /// Response base class.
    /// </summary>
    public class ResponseBase
    {
        /// <summary>
        /// Indicates if the response was successful.
        /// </summary>
        [JsonIgnore] 
        public bool Success{ get; init; }
    }
}