using System.Collections.Concurrent;
using System.ComponentModel;
using System.Reflection;

namespace Api.Extensions;
public static class EnumExtension
{
    private static ConcurrentDictionary<string, Type>? _enumTypeDictionary;
    private static IEnumerable<Type>? _enumTypes;

    public static IEnumerable<Type> EnumTypes
    {
        get
        {
            if (_enumTypes == null)
            {
                var constAsm = typeof(PictureType).Assembly;
                _enumTypes = constAsm.GetTypes().Where(t => t.IsEnum);
            }

            return _enumTypes;
        }
    }

    private static string GetDescription(this Enum value)
    {
        FieldInfo? fieldInfo = value?.GetType()?.GetField(value.ToString());
        return fieldInfo?.GetCustomAttribute<DescriptionAttribute>()?.Description ?? string.Empty;
    }

    public static IEnumerable<string> GetDescriptions<TEnum>(this TEnum @enum) where TEnum : Enum
    {
        static IEnumerable<string> GetDescriptionsInternal(TEnum @enum)
        {
            var allValues = Enum.GetValues(typeof(TEnum)).OfType<TEnum>().ToList();

            foreach (var value in allValues)
            {
                var desc = value.GetDescription();
                if (@enum.HasFlag(value) && !string.IsNullOrWhiteSpace(desc))
                    yield return desc;
            }
        }

        return GetDescriptionsInternal(@enum);
    }

    public static IEnumerable<(int Id, string Description)> GetEnumList<TEnum>(this TEnum @enum) where TEnum : Enum
    {
        static IEnumerable<(int Id, string Description)> GetٍEnumListInternal(TEnum @enum)
        {
            var allValues = Enum.GetValues(typeof(TEnum)).OfType<TEnum>().ToList();

            foreach (var value in allValues)
            {
                int id = (int)(object)value;
                string desc = value.GetDescription();
                if (@enum.HasFlag(value) && !string.IsNullOrWhiteSpace(desc) && id != 0)
                    yield return new(id, desc);
            }
        }

        return GetٍEnumListInternal(@enum);
    }

    public static Type GetEnumType(string typeName, out bool success)
    {
        ArgumentNullException.ThrowIfNull(typeName);
        success = false;
        var key = typeName.ToUpperInvariant();

        if (_enumTypeDictionary == null)
        {
            _enumTypeDictionary = new ConcurrentDictionary<string, Type>();
        }

        if (!_enumTypeDictionary.ContainsKey(key))
        {
#pragma warning disable CA2251 // Use 'string.Equals'
#pragma warning disable CA1309 // Use ordinal string comparison
            var newType = EnumTypes?.FirstOrDefault(x =>
                string.Compare(x.Name, key, StringComparison.InvariantCultureIgnoreCase) == 0);
#pragma warning restore CA1309 // Use ordinal string comparison
#pragma warning restore CA2251 // Use 'string.Equals'

            if (newType != null)
            {
                _enumTypeDictionary.TryAdd(key, newType);
            }
        }

        if (!_enumTypeDictionary.ContainsKey(key))
            return typeof(object);
        success = true;
        return _enumTypeDictionary[key];
    }

    public static IList<GetAllDto> GetEnumDescriptions(Type enumType)
    {
#pragma warning disable CA1305 // Specify IFormatProvider
        var list = Enum.GetValues(enumType)
            .OfType<Enum>()
            .Where(x => !string.IsNullOrEmpty(x.GetDescription()))
            .Select(x =>
                new GetAllDto()
                {
                    Id = Convert.ToInt32(x),
                    Name = x.ToString(),
                    Description = x.GetDescription()
                }).ToList();
#pragma warning restore CA1305 // Specify IFormatProvider
        return list;
    }

//    public static IList<GetAllDto> GetEnumDescriptions(Type enumType)
//    {
//#pragma warning disable CA1305 // Specify IFormatProvider
//#pragma warning disable MA0011 // IFormatProvider is missing
//        var list = Enum.GetValues(enumType)
//            .OfType<Enum>()
//            .Where(x => !string.IsNullOrEmpty(x.GetDescription()))
//            .Select(x =>
//                new GetAllDto()
//                {
//                    Id = Convert.ToInt32(x),
//                    Description = x.GetDescription()
//                }).ToList();
//#pragma warning restore MA0011 // IFormatProvider is missing
//#pragma warning restore CA1305 // Specify IFormatProvider
//        return list;
//    }
}