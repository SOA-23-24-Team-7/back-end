using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class KeyPointSecret : ValueObject
    {
        public List<string>? Images { get; private set; }
        public string Description { get; private set; }

        public KeyPointSecret(List<string>? images, string description)
        {
            Images = images;
            Description = description;
        }

        public void setImages(List<string>? images)
        {
            Images = images;
        }
        public void setDescription(string description)
        {
            Description = description;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Images;
            yield return Description;
        }
    }
}
