namespace DigitalFamilyCookbook.IntegrationTests.Helpers;

public static class PayloadBuilder
{
    public static StringContent Build(object content)
    {
        var payload = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8);
        payload.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        return payload;
    }
}
