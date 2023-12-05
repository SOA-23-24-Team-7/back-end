using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Campaign : Entity
    {
        public long TouristId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public double Distance { get; private set; }
        public double AverageDifficulty { get; private set; }
        public List<long> TourIds { get; init; } = new List<long>();
        public List<long> EquipmentIds { get; private set; } = new List<long>();
        public List<long> KeyPointIds { get; init; } = new List<long>();
        [NotMapped]
        public ICollection<Equipment> Equipments { get; private set; } = new List<Equipment>();
        [NotMapped]
        public ICollection<KeyPoint> KeyPoints { get; init; } = new List<KeyPoint>();

        public Campaign() { }
        public Campaign(long touristId, string name, string description, List<Tour> tours)
        {
            TouristId = touristId;
            Name = name;
            Description = description;
            SetUp(tours);
        }
        private void SetUp(List<Tour> tours)
        {
            Distance = tours.Sum(t => t.Distance);
            AverageDifficulty = tours.Average(t => t.Difficulty);
            foreach(Tour tour in tours)
                TourIds.Add(tour.Id);
            AddEquipment(tours);
            AddKeyPoints(tours);
        }
        private void AddKeyPoints(List<Tour> tours)
        {
            foreach (Tour tour in tours)
                foreach (KeyPoint keyPoint in tour.KeyPoints)
                {
                    KeyPointIds.Add(keyPoint.Id);
                    KeyPoints.Add(keyPoint);
                }
        }
        private void AddEquipment(List<Tour> tours)
        {
            foreach (Tour tour in tours)
                foreach (Equipment equipment in tour.EquipmentList)
                {
                    EquipmentIds.Add(equipment.Id);
                    Equipments.Add(equipment);
                }
            EquipmentIds = EquipmentIds.Distinct().ToList();
            Equipments = Equipments.Distinct().ToList();
        }
    }
}
