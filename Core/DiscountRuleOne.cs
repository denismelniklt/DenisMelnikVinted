using Domain;
using Infrastructure;

namespace Core
{
    public class DiscountRuleOne : IDiscountRule
    {
        public DiscountRuleOne()
        {
        }

        public decimal GetDiscount(Transaction transaction, decimal shipmentDiscount = 0M)
        {
            var discount = new decimal();

            var yearMonthString = transaction.Date.GetYearAndMonthString();

            if (transaction.Package.Size == PackageSize.S)
            {
                if (DiscountRuleCache.AccumulatedMonthlyDiscounts.ContainsKey(yearMonthString))
                {
                }
                else
                {
                    DiscountRuleCache.AccumulatedMonthlyDiscounts.Add(yearMonthString, 0M);
                }

                if (DiscountRuleCache.AccumulatedMonthlyDiscounts[yearMonthString] < DiscountRuleCache.AccumulatedDiscountCeiling)
                {
                    var price = transaction.Package.Shipment.Price;
                    discount = price - DiscountRuleCache.MinimalSmallSizePackagePrice;

                    if (DiscountRuleCache.AccumulatedMonthlyDiscounts[yearMonthString] + discount > DiscountRuleCache.AccumulatedDiscountCeiling)
                    {
                        discount = DiscountRuleCache.AccumulatedDiscountCeiling - DiscountRuleCache.AccumulatedMonthlyDiscounts[yearMonthString];
                    }

                    DiscountRuleCache.AccumulatedMonthlyDiscounts[yearMonthString] += discount;
                }
            }

            return discount;
        }
    }
}