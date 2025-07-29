﻿namespace OrderManagementSystem.API.Models.Errors
{
    public class ValidationErrorResponse
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; } = default!;
        public IEnumerable<ValidationError> ValidationErrors { get; set; } = default!;
    }
    public class ValidationError
    {
        public string Field { get; set; } = default!;
        public IEnumerable<string> Errors { get; set; } = default!;
    }
}
