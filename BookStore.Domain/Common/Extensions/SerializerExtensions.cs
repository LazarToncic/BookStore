using System.Text.Json;

namespace BookStore.Domain.Common.Extensions;

public static class SerializerExtensions
{
    public static readonly JsonSerializerOptions DefaultOptions = new();

    public static readonly JsonSerializerOptions SettingsWebOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    
    public static readonly JsonSerializerOptions SettingsHardwareOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };
    
    public static readonly JsonSerializerOptions SettingsTestCreateProductOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        MaxDepth = 10
    };
    
    public static string Serialize(this object? json, JsonSerializerOptions settings)
    {
        return JsonSerializer.Serialize(json, settings);
    }

    public static T? Deserialize<T>(this string json, JsonSerializerOptions settings)
    {
        return JsonSerializer.Deserialize<T>(json, settings);
    }

    public static bool TryDeserializeJson<T>(this string obj, out T? result, JsonSerializerOptions settings)
    {
        try
        {
            result = Deserialize<T>(obj, settings);
            return true;
        }
        catch (Exception e)
        {
            result = default;
            return false;
        }
    }
}