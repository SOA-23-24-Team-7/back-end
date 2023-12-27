namespace Explorer.Payments.API.Dtos
{
    public class WishlistNotificationResponseDto
    {
        public long TourId { get; set; }
        public string Description { get; set; }
        public long TouristId { get; set; }

        public WishlistNotificationResponseDto(long tourId, long touristId, string description)
        {
            TourId = tourId;
            TouristId = tourId;
            Description = description;
        }
    }
}
