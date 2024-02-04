namespace OperatorFx.Infrastructure.Configuration;

public class HostingOptions
{
    public const string SectionName = "Hosting";

    public string? KubeConfig { get; set; }
    public bool InstallCrds { get; set; } = true;
}
