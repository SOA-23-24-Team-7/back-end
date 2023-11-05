namespace Explorer.Tours.API.Internal
{
    public interface IInternalTourService
    {
        IEnumerable<long> GetAuthorsTours(long id);
        string GetToursName(long id);
    }
}
