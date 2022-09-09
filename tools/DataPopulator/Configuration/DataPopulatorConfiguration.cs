namespace DataPopulator.Configuration;

public class DataPopulatorConfiguration
{
    public ConnectionStringsConfiguration ConnectionStrings { get; set; } = new ConnectionStringsConfiguration();
}

public class ConnectionStringsConfiguration
{
    public string Main { get; set; } = string.Empty;
}