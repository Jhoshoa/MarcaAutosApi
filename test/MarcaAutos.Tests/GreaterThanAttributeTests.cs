using MarcaAutos.Api.Validations.Attributes;

namespace MarcaAutos.Tests;

public sealed class GreaterThanAttributeTests
{
    [Theory]
    [InlineData(1899, false)]
    [InlineData(1900, false)]
    [InlineData(1901, true)]
    [InlineData(null, true)]
    [InlineData("1901", false)]
    public void IsValid_ReturnsExpectedResult(object? value, bool expected)
    {
        // Arrange
        var attribute = new GreaterThanAttribute(1900);

        // Act
        var isValid = attribute.IsValid(value);

        // Assert
        Assert.Equal(expected, isValid);
    }
}
