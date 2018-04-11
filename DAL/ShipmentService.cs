using Core;
using Domain;
using Infrastructure;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class ShipmentService : IShipmentService
    {
        private readonly Dictionary<PackageOption, decimal> ShipmentPriceDictionary = new Dictionary<PackageOption, decimal>();

        public ShipmentService()
        {
            PopulateShipmentPriceDictionary();

            DiscountRuleCache.Clear();
            DiscountRuleCache.MinimalSmallSizePackagePrice = GetMinimalSmallSizePackagePriceAmongProviders();
        }

        public Shipment GetShipment(Transaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if (transaction.Package == null)
            {
                throw new ArgumentNullException("transaction.Package");
            }

            var shipmentPrice = GetShipmentPrice(transaction);

            transaction.Package.Shipment = new Shipment
            {
                Price = shipmentPrice
            };

            var shipmentDiscount = GetShipmentDiscount(transaction);

            transaction.Package.Shipment = new Shipment
            {
                Price = shipmentPrice - shipmentDiscount,
                Discount = shipmentDiscount
            };

            return transaction.Package.Shipment;
        }

        private decimal GetMinimalSmallSizePackagePriceAmongProviders()
        {
            var price = decimal.MaxValue;

            foreach (var kvp in ShipmentPriceDictionary)
            {
                if (kvp.Key.Size == PackageSize.S)
                {
                    if (kvp.Value < price)
                    {
                        price = kvp.Value;
                    }
                }
            }

            return price;
        }

        private decimal GetShipmentPrice(Transaction transaction)
        {
            var shipmentPrice = new decimal();

            var packageOption = new PackageOption
            {
                Size = transaction.Package.Size,
                Provider = transaction.Package.Provider
            };

            if (ShipmentPriceDictionary.ContainsKey(packageOption))
            {
                shipmentPrice = ShipmentPriceDictionary[packageOption];
            }
            else
            {
                var message = string.Format("Shipment price dictionary does not contain key {0}", packageOption);
                throw new KeyNotFoundException(message);
            }

            return shipmentPrice;
        }

        private decimal GetShipmentDiscount(Transaction transaction)
        {
            var shipmentDiscount = new decimal();
            var discountRuleList = new List<IDiscountRule>();

            var discountRuleOne = new DiscountRuleOne() as IDiscountRule;
            discountRuleList.Add(discountRuleOne);
            var discountRuleTwo = new DiscountRuleTwo() as IDiscountRule;
            discountRuleList.Add(discountRuleTwo);

            foreach (var discountRule in discountRuleList)
            {
                 shipmentDiscount += discountRule.GetDiscount(transaction, shipmentDiscount);
            }

            return shipmentDiscount;
        }

        private void PopulateShipmentPriceDictionary()
        {
            var packageOptionLpS = new PackageOption
            {
                Provider = PackageProvider.LP,
                Size = PackageSize.S
            };
            ShipmentPriceDictionary.Add(packageOptionLpS, 1.50M);

            var packageOptionLpM = new PackageOption
            {
                Provider = PackageProvider.LP,
                Size = PackageSize.M
            };
            ShipmentPriceDictionary.Add(packageOptionLpM, 4.90M);

            var packageOptionLpL = new PackageOption
            {
                Provider = PackageProvider.LP,
                Size = PackageSize.L
            };
            ShipmentPriceDictionary.Add(packageOptionLpL, 6.90M);

            var packageOptionMrS = new PackageOption
            {
                Provider = PackageProvider.MR,
                Size = PackageSize.S
            };
            ShipmentPriceDictionary.Add(packageOptionMrS, 2M);

            var packageOptionMrM = new PackageOption
            {
                Provider = PackageProvider.MR,
                Size = PackageSize.M
            };
            ShipmentPriceDictionary.Add(packageOptionMrM, 3M);

            var packageOptionMrL = new PackageOption
            {
                Provider = PackageProvider.MR,
                Size = PackageSize.L
            };
            ShipmentPriceDictionary.Add(packageOptionMrL, 4M);
        }

    }
}
