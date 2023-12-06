using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Encounters.Core.Domain
{
    public class TouristProgress : Entity
    {
        public long UserId { get; init; }
        public int Xp { get; private set; }
        public int Level { get; private set; }

        public TouristProgress(long userId, int xp, int level)
        {
            UserId = userId;
            Xp = xp;
            Level = level;
        }

        public void AddXp(int xp)
        {
            while (xp > 0)
            {
                int xpNeeded = 100 - Xp;
                if (xp > xpNeeded)
                {
                    Xp = 0;
                    xp -= xpNeeded;
                    Level++;
                }
                else
                {
                    Xp += xp;
                    xp = 0;
                }
            }
        }

    }
}
