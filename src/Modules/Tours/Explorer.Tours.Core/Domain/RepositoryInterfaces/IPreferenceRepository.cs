namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IPreferenceRepository
    {
        Preference Create(Preference tourPreference);
        Preference GetByUserId(int userId);
        void Delete(long id);
        Preference Update(Preference tourPreference);
    }
}
