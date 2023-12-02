namespace Explorer.Tours.API.Internal;

public interface IInternalNotificationService
{
    int CountNotSeen(long userId);
}