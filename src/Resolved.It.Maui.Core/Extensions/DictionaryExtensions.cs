using System.Collections.Generic;

namespace Resolved.It.Maui.Core.Extensions;

public static class DictionaryExtensions
{
    public static bool ValueAsBool(this IDictionary<string, object?> dictionary, string key, bool defaultValue = false) =>
        dictionary.TryGetValue(key, out var value) && value is bool dictValue
            ? dictValue
            : defaultValue;

    public static int ValueAsInt(this IDictionary<string, object?> dictionary, string key, int defaultValue = 0) =>
        dictionary.TryGetValue(key, out var value) && value is int intValue
            ? intValue
            : defaultValue;
}