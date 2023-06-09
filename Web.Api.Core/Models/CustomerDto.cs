﻿namespace Web.Api.Core.Models
{
    /// <summary>
    /// Customer details.
    /// </summary>
    public record CustomerDto
    {
        /// <summary>
        /// Customer First Name.
        /// </summary>
        public string FirstName { get; init; } = string.Empty;

        /// <summary>
        /// Customer Last Name.
        /// </summary>
        public string LastName { get; init; } = string.Empty;

        /// <summary>
        /// Customer Policy Number e.g. XX-123456.
        /// </summary>
        public string PolicyNumber { get; init; } = string.Empty;

        /// <summary>
        /// Customer Date of Birth e.g. 2000-12-31.
        /// </summary>
        public DateTime? DateOfBirth { get; init; }

        /// <summary>
        /// Customer Email.
        /// </summary>
        public string? Email { get; init; }
    }
}