using System.ComponentModel.DataAnnotations;

namespace MarcaAutos.Api.Validations.Attributes
{
    [AttributeUsage(
        AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false
    )]
    public sealed class GreaterThanAttribute : ValidationAttribute
    {
        private readonly int _minValue;

        public GreaterThanAttribute(int minValue)
        {
            _minValue = minValue;
            ErrorMessage = $"{{0}} must be greater than {minValue}.";
        }

        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return true;
            }

            return value is int number && number > _minValue;
        }
    }
}
