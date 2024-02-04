using System.Collections;
using OperatorFx.Domain.Extensions;

namespace OperatorFx.Tests.Infrastructure.Services;

public class OpenApiExtensionsTests
{
    [Theory]
    // Booleans
    [InlineData(typeof(bool), OpenApiType.Boolean)]
    [InlineData(typeof(bool?), OpenApiType.Boolean)]
    // Objects
    [InlineData(typeof(object), OpenApiType.Object)]
    // Strings
    [InlineData(typeof(string), OpenApiType.String)]
    [InlineData(typeof(DateTime), OpenApiType.String)]
    [InlineData(typeof(DateTimeOffset), OpenApiType.String)]
    [InlineData(typeof(TimeSpan), OpenApiType.String)]
    [InlineData(typeof(Guid), OpenApiType.String)]
    [InlineData(typeof(Uri), OpenApiType.String)]
    [InlineData(typeof(char), OpenApiType.String)]
    [InlineData(typeof(byte), OpenApiType.String)]
    [InlineData(typeof(sbyte), OpenApiType.String)]
    // Numbers
    [InlineData(typeof(int), OpenApiType.Number)]
    [InlineData(typeof(decimal), OpenApiType.Number)]
    [InlineData(typeof(double), OpenApiType.Number)]
    [InlineData(typeof(float), OpenApiType.Number)]
    [InlineData(typeof(uint), OpenApiType.Number)]
    [InlineData(typeof(long), OpenApiType.Number)]
    [InlineData(typeof(ulong), OpenApiType.Number)]
    [InlineData(typeof(short), OpenApiType.Number)]
    [InlineData(typeof(ushort), OpenApiType.Number)]
    // Collections
    [InlineData(typeof(string[]), OpenApiType.Array)]
    [InlineData(typeof(List<>), OpenApiType.Array)]
    [InlineData(typeof(List<string>), OpenApiType.Array)]
    [InlineData(typeof(Array), OpenApiType.Array)]
    [InlineData(typeof(IList), OpenApiType.Array)]
    [InlineData(typeof(IList<string>), OpenApiType.Array)]
    [InlineData(typeof(IList<object>), OpenApiType.Array)]
    public void WhenCheckingApiType(Type t, string expectedType)
    {
        var result = t.ToOpenApiType();
        Assert.Equal(expectedType, result);
    }
}
