using Microsoft.AspNetCore.Http;

namespace DigitalFamilyCookbook.Tests.Utilities.Mocks;

public static class MockFile
{
	public static Mock<IFormFile> GenerateFile(string filename = "test.pdf", string contentType = "application/pdf")
	{
        var fileMock = new Mock<IFormFile>();

        var content = MockDataGenerator.RandomString(100);

        var ms = new MemoryStream();
        var writer = new StreamWriter(ms);

        writer.Write(content);
        writer.Flush();
        ms.Position = 0;

        fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
        fileMock.Setup(_ => _.FileName).Returns(filename);
        fileMock.Setup(_ => _.Length).Returns(ms.Length);
        fileMock.Setup(_ => _.ContentType).Returns(contentType);

        return fileMock;
    }
}
