namespace HotChocolate.Execution.Benchmarks.Project.Attendees
{
    public record RegisterAttendeeInput(
        string FirstName,
        string LastName,
        string UserName,
        string EmailAddress);
}