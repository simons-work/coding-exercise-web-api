namespace Web.Api.Core.Models
{
    /// <summary>
    /// Error response details.
    /// </summary>
    public class ErrorResponseDto : ResponseBase
    {
        /// <summary>
        /// Error message.
        /// </summary>
        public string? Message { get; set; } 
    }
}