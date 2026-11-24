using System.ComponentModel.DataAnnotations;

namespace MarcaAutos.Api.Validations.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class GreatherThan : ValidationAttribute
    {
        private readonly int _minValue;
        
        public GreatherThan(int minValue)
        {
            _minValue = minValue;
            ErrorMessage = $"{{0}} must be greather that {minValue}.";
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