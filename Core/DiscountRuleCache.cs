using System.Collections.Generic;

namespace Core
{
    public static class DiscountRuleCache
    {
        public const decimal AccumulatedDiscountCeiling = 10M;
        public static decimal MinimalSmallSizePackagePrice { get; set; }
        public static Dictionary<string, byte> LargeSizePackagePerCalendarMonthCount = new Dictionary<string, byte>();
        public static Dictionary<string, decimal> AccumulatedMonthlyDiscounts = new Dictionary<string, decimal>();

        public static void Clear()
        {
            LargeSizePackagePerCalendarMonthCount.Clear();
            AccumulatedMonthlyDiscounts.Clear();
        }
    }
}