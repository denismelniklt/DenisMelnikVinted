using Domain;
using System;
using System.Collections.Generic;

namespace Infrastructure
{
    public static class TransactionDtoExtensions
    {
        private const string DiscountIsZeroTrail = "-";
        private const char PropertySeparator = ' ';
        private const string DateFormat = "yyyy-MM-dd";
        private const string IgnoredTransactionSuffix = " Ignored";

        public static string GetTransactionLine(this TransactionDto transactionDto)
        {
            var transactionDtoProperties = transactionDto.GetTransactionDtoProperties();

            var transactionLine = string.Join(PropertySeparator, transactionDtoProperties);

            if (transactionDto is IgnoredTransactionDto)
            {
                transactionLine = string.Concat(transactionLine, IgnoredTransactionSuffix);
            }

            return transactionLine;
        }

        private static IEnumerable<string> GetTransactionDtoProperties(this TransactionDto transactionDto)
        {
            var transactionDtoProperties = new List<string>();

            if (transactionDto is IgnoredTransactionDto)
            {
                var ignoredTransactionDto = transactionDto as IgnoredTransactionDto;
                transactionDtoProperties.Add(ignoredTransactionDto.TextLine);
            }
            else
            {
                DateTime.TryParse(transactionDto.Date, out DateTime temporaryDate);
                var transactionDtoDate = temporaryDate.ToString(DateFormat);
                transactionDtoProperties.Add(transactionDtoDate);

                var transactionDtoPackageSize = transactionDto.Package.Size;
                transactionDtoProperties.Add(transactionDtoPackageSize);

                var transactionDtoProvider = transactionDto.Package.Provider;
                transactionDtoProperties.Add(transactionDtoProvider);

                var transactionDtoPriceString = transactionDto.Package.Shipment.Price;
                decimal.TryParse(transactionDtoPriceString, out decimal transactionDtoPrice);
                transactionDtoProperties.Add(transactionDtoPrice.ToString("0.00"));

                var transactionDtoDiscountString = transactionDto.Package.Shipment.Discount;
                decimal.TryParse(transactionDtoDiscountString, out decimal transactionDtoDiscount);

                var transactionDtoDiscountStringNew = string.Empty;
                if (transactionDtoDiscount == decimal.Zero)
                {
                    transactionDtoDiscountStringNew = DiscountIsZeroTrail;
                }
                else
                {
                    transactionDtoDiscountStringNew = transactionDtoDiscount.ToString("0.00");
                }
                transactionDtoProperties.Add(transactionDtoDiscountStringNew);
            }

            return transactionDtoProperties;
        }
    }
}