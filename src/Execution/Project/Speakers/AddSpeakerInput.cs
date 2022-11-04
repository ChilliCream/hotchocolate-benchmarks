namespace HotChocolate.Execution.Benchmarks.Project.Speakers
{
    public record AddSpeakerInput(
        string Name,
        string? Bio,
        string? WebSite);
}