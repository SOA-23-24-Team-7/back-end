using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourRespondeDtoNew
    {

        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("authorId")]
        public long? AuthorId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("difficulty")]
        public int Difficulty { get; set; }
        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }
        [JsonPropertyName("status")]
        public TourStatus Status { get; set; }
        [JsonPropertyName("price")]
        public double Price { get; set; }
        [JsonPropertyName("isDeleted")]
        public bool IsDeleted { get; set; }
        [JsonPropertyName("distance")]
        public double Distance { get; set; }
        [JsonPropertyName("averageRating")]
        public double? AverageRating { get; set; }
        public List<KeyPointResponseDto> KeyPoints { get; set; }
        public List<TourDurationResponseDto> Durations { get; set; }
        [JsonPropertyName("publishDate")]
        public DateTime PublishDate { get; set; }
        [JsonPropertyName("archiveDate")]
        public DateTime ArchiveDate { get; set; }
        [JsonPropertyName("category")]
        public TourCategory Category { get; set; }
    }
}
