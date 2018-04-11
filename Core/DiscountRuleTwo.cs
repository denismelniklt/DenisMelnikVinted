using Domain;
using Infrastructure;

namespace Core
{
    public class DiscountRuleTwo : IDiscountRule
    {
        public decimal GetDiscount(Transaction transaction, decimal shipmentDiscount = 0)
        {
            var discount = new decimal();

            if (transaction is IgnoredTransaction)
            {
            }
            else
            {
                if (transaction.Package.Size == PackageSize.L && transaction.Package.Provider == PackageProvider.LP)
                {
                    var yearMonthString = transaction.Date.GetYearAndMonthString();

                    if (DiscountRuleCache.AccumulatedMonthlyDiscounts.ContainsKey(yearMonthString))
                    {
                    }
                    else
                    {
                        DiscountRuleCache.AccumulatedMonthlyDiscounts.Add(yearMonthString, 0M);
                    }

                    if (DiscountRuleCache.AccumulatedMonthlyDiscounts[yearMonthString] < DiscountRuleCache.AccumulatedDiscountCeiling)
                    {
                        if (DiscountRuleCache.LargeSizePackagePerCalendarMonthCount.ContainsKey(yearMonthString))
                        {
                            DiscountRuleCache.LargeSizePackagePerCalendarMonthCount[yearMonthString]++;
                        }
                        else
                        {
                            DiscountRuleCache.LargeSizePackagePerCalendarMonthCount.Add(yearMonthString, 0);
                        }

                        var count = DiscountRuleCache.LargeSizePackagePerCalendarMonthCount[yearMonthString];

                        if (count == 2)
                        {
                            discount = transaction.Package.Shipment.Price;
                        }

                        if (DiscountRuleCache.AccumulatedMonthlyDiscounts[yearMonthString] + discount > DiscountRuleCache.AccumulatedDiscountCeiling)
                        {
                            discount = DiscountRuleCache.AccumulatedDiscountCeiling - DiscountRuleCache.AccumulatedMonthlyDiscounts[yearMonthString];
                        }

                        DiscountRuleCache.AccumulatedMonthlyDiscounts[yearMonthString] += discount;
                    }
                }
            }

            return discount;
        }
    }
}