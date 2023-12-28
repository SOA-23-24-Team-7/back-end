using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Payments.Core.Domain
{
    public class Coupon : Entity
    {
        public string Code { get; init; }
        public double Discount { get;init; }
        public long TourId { get; init; }
        public DateTime ExpirationDate { get; init; }
        public bool AllFromAuthor { get; init; }    //ako se checkbox odabere da vazi za sve ture tog autora
        public long AuthorId { get; init; }
        public Coupon(double discount, long tourId, DateTime expirationDate, bool allFromAuthor, long authorId)
        {
            if (discount < 0) throw new ArgumentException("Invalid discount.");

            Discount = discount;
            TourId = tourId;
            ExpirationDate = expirationDate;
            AllFromAuthor = allFromAuthor;
            AuthorId = authorId;
            Code = GenerateCode();
        }
        private string GenerateCode()
        {
            Random random = new Random();
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            int length = 8;
            StringBuilder code = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(characters.Length);
                code.Append(characters[index]);
            }

            return code.ToString();

        }

        public bool IsValid()
        {
            return ExpirationDate >= DateTime.Now;
        }

        public bool IsForAllTours()
        {
            return AllFromAuthor;
        }
    }
}
