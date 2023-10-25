using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class TourPreference : Entity
    {
        public long UserId { get; private set; }
        public int DifficultyLevel { get; private set; }
        public int WalkingRating { get; private set; }
        public int CyclingRating { get; private set; }
        public int CarRating { get; private set; }
        public int BoatRating { get; private set; }
        public List<string> SelectedTags { get; private set; }

        public TourPreference(long userId, int difficultyLevel, int walkingRating, int cyclingRating, int carRating, int boatRating, List<string> selectedTags) 
        {
            UserId = userId;
            if (difficultyLevel < 1 || difficultyLevel > 5) throw new ArgumentException("Difficulty level must be in range between 1 and 5!");
            DifficultyLevel = difficultyLevel;
            if (walkingRating < 0 || cyclingRating < 0 || carRating < 0 || boatRating < 0 || walkingRating > 3 || cyclingRating > 3 || carRating > 3 || boatRating > 3) throw new ArgumentException("Rating must be value between 0 and 3!");
            WalkingRating = walkingRating;
            CyclingRating = cyclingRating;
            CarRating = carRating;
            BoatRating = boatRating;
            SelectedTags = selectedTags;
        }
    }
}