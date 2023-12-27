using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourHotInfo
    {
        public long numberOfReviews { get; init; }
        public double averageReviewRating { get; init; }
        public long numberOfPurchases { get; init; }
        public TourHotInfo(long numberOfReviews, double averageReviewRating, long numberOfPurchases)
        {
            if(numberOfPurchases < 0 || averageReviewRating < 0 || numberOfReviews < 0)
            {
                throw new ArgumentException("Invalid arguments in TourHotInfo constructor, all arguments must be non negative");
            }
            this.numberOfReviews = numberOfReviews;
            this.averageReviewRating = averageReviewRating;
            this.numberOfPurchases = numberOfPurchases;
        }
        public double GetScore()
        {
            double averageReviewContribution; 
            if(averageReviewRating == 0)
            {
                averageReviewContribution = 0;
            }
            else
            {
                averageReviewContribution = Math.Pow(12 * (averageReviewRating - 3.5) - 3, 1 / 3) + 2.2;
            }
            double reviewCountContribution = Math.Log(numberOfReviews + 1);
            double purchaseCountContribution = 1.0 / 9.0 * numberOfPurchases;
            return averageReviewContribution * reviewCountContribution + purchaseCountContribution;
        }
    }
}
