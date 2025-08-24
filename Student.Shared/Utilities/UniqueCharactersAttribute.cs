using System.ComponentModel.DataAnnotations;

namespace Student.Shared.Utilities
{
    public class UniqueCharactersAttribute : ValidationAttribute
    {
        private readonly int _minimumUniqueChars;

        public UniqueCharactersAttribute(int minimumUniqueChars)
        {
            _minimumUniqueChars = minimumUniqueChars;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var password = value.ToString();
            var uniqueCharCount = password.Distinct().Count();

            if (uniqueCharCount < _minimumUniqueChars)
            {
                return new ValidationResult(ErrorMessage ?? $"Must contain at least {_minimumUniqueChars} unique characters");
            }

            return ValidationResult.Success;
        }
    }
}
