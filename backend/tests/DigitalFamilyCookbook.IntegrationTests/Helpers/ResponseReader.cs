using Newtonsoft.Json;

namespace DigitalFamilyCookbook.IntegrationTests.Helpers;

public static class ResponseReader
{
    public static T? Read<T>(string response) where T : class
    {
        return JsonConvert.DeserializeObject<T>(response);
    }
}