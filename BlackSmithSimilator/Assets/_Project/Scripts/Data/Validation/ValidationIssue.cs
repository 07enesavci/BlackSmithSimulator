namespace BlacksmithSimulator.Data.Validation
{
    public sealed class ValidationIssue
    {
        public ValidationIssue(ValidationSeverity severity, string message)
        {
            Severity = severity;
            Message = message;
        }

        public ValidationSeverity Severity { get; }
        public string Message { get; }
    }
}
