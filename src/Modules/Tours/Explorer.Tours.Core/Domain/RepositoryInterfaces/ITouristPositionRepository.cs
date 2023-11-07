namespace Explorer.Tours.Core.Domain.RepositoryInterfaces;

public interface ITouristPositionRepository
{
    TouristPosition GetByTouristId(long touristId);
}
