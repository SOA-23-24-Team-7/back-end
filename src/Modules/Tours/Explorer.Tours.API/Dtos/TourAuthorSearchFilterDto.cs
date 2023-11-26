namespace Explorer.Tours.API.Dtos;

public class TourAuthorSearchFilterDto : TourTouristSearchFilterDto
{
    public string? TourStatus { get; set; }

    public TourAuthorSearchFilterDto(TourTouristSearchFilterDto dto)
    {
        this.Name = dto.Name;
        this.MinPrice = dto.MinPrice;
        this.MaxPrice = dto.MaxPrice;
        this.MinDifficulty = dto.MinDifficulty;
        this.MaxDifficulty = dto.MaxDifficulty;
        this.MinDuration = dto.MinDuration;
        this.MaxDuration = dto.MaxDuration;
        this.MinAverageRating = dto.MinAverageRating;
        this.MinLength = dto.MinLength;
        this.MaxLength = dto.MaxLength;
        this.Longitude = dto.Longitude;
        this.Latitude = dto.Latitude;
        this.MaxDistance = dto.MaxDistance;
        this.Page = dto.Page;
        this.PageSize = dto.PageSize;
        this.AuthodId = dto.AuthodId;
        this.TourStatus = "Published";
    }
}
