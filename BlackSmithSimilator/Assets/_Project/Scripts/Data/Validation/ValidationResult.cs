using System.Collections.Generic;
using System.Linq;

namespace BlacksmithSimulator.Data.Validation
{
    public sealed class ValidationResult
    {
        private readonly List<ValidationIssue> issues = new();

        public IReadOnlyList<ValidationIssue> Issues => issues;

        public bool HasErrors => issues.Any(i => i.Severity == ValidationSeverity.Error);

        public bool HasWarnings => issues.Any(i => i.Severity == ValidationSeverity.Warning);

        public void AddError(string message)
        {
            issues.Add(new ValidationIssue(ValidationSeverity.Error, message));
        }

        public void AddWarning(string message)
        {
            issues.Add(new ValidationIssue(ValidationSeverity.Warning, message));
        }
    }
}
