namespace IdunnoAPI.Helpers
{
    /// <summary> Helper class, will be used in cases when boolean or other type will be not enough to handle different error scenarios
    public class ValidationResult
    {
        public bool Succeded { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}
