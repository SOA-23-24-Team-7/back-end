namespace Explorer.Stakeholders.Core.Utilities;

public class NotificationGenerator
{
    public static string GenerateAnswerHeader(string tourName)
    {
        return "Publisher answer on: " + tourName.ToUpper();
    }
    public static string GenerateCommentHeader(string tourName)
    {
        return "Comment for: " + tourName.ToUpper();
    }

    public static string GenerateDeadlineHeader(string tourName)
    {
        return "Deadline for: " + tourName.ToUpper();
    }

    public static string GenerateDeadlineMessage(string tourName, DateTime deadline)
    {
        return "Greetings, after thorough review of reported problem for: " + tourName.ToUpper() +
               " I have decided to set a resolving deadline for: " + deadline + ". Not resolving the problem after the given deadline has passed, will result in termination of tour " + tourName;
    }
}