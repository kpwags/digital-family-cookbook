namespace DigitalFamilyCookbook.IntegrationTests.Helpers;

public static class PayloadBuilder
{
    public static StringContent Build(object content, string contentType = "application/json")
    {
        var payload = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, contentType);

        return payload;
    }
}
