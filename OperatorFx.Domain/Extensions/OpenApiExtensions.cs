using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace OperatorFx.Domain.Extensions;

[SuppressMessage("ReSharper", "SwitchStatementHandlesSomeKnownEnumValuesWithDefault")]
public static class OpenApiExtensions
{
    public static string ToOpenApiType(this Type type)
    {
        return type switch
        {
            not null when type.IsString() => "string",
            not null when type.IsNumber() => "number",
            not null when type.IsBoolean() => "boolean",
            not null when type.IsCollection() => "array",
            _ => "object"
        };
    }

    static bool IsString(this Type type)
    {
        return type switch
        {
            not null when type == typeof(DateTime) => true,
            not null when type == typeof(DateTimeOffset) => true,
            not null when type == typeof(TimeSpan) => true,
            not null when type == typeof(Guid) => true,
            not null when type == typeof(Uri) => true,
            not null when IsStringPrimitive(type) => true,
            _ => false
        };

        static bool IsStringPrimitive(Type type)
        {
            switch (type.GetTypeCode())
            {
                case TypeCode.String:
                case TypeCode.Char:
                case TypeCode.Byte:
                case TypeCode.SByte:
                    return true;
                default:
                    return false;
            }
        }
    }

    static bool IsNumber(this Type type)
    {
        switch (type.GetTypeCode())
        {
            case TypeCode.Decimal:
            case TypeCode.Double:
            case TypeCode.Single: // float
            case TypeCode.Int32: // int
            case TypeCode.UInt32: // uint
            case TypeCode.Int64: // long
            case TypeCode.UInt64: // ulong
            case TypeCode.Int16: // short
            case TypeCode.UInt16: // ushort
                return true;
            default:
                return false;
        }
    }

    static bool IsCollection(this Type type)
    {
        if (type.IsArray || typeof(IList).IsAssignableFrom(type.GetUnderlyingType()))
        {
            return true;
        }

        if (type.IsGenericType)
        {
            var genericType = type.GetGenericTypeDefinition();
            return genericType == typeof(List<>) || genericType == typeof(IList<>);
        }

        return false;
    }
    static bool IsBoolean(this Type type) => type.GetTypeCode() == TypeCode.Boolean;

    static TypeCode GetTypeCode(this Type t) => Type.GetTypeCode(t.GetUnderlyingType());

    static Type GetUnderlyingType(this Type type) => Nullable.GetUnderlyingType(type) ?? type;
}
