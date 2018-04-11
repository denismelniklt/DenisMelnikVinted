using Domain;
using System;

namespace Infrastructure
{
    public static class StringExtensions
    {
        private const char PropertySeparator = ' ';

        public static TransactionDto ToTransactionDto(this string line)
        {
            var properties = line.Split(PropertySeparator);

            var isValid = true;

            var date = string.Empty;
            var packageDto = new PackageDto();

            if (properties.Length >= 1)
            {
                date = properties[0];

                if (DateTime.TryParse(date, out DateTime temporaryDateTime))
                {
                }
                else
                {
                    isValid = false;
                    date = string.Empty;
                }
            }

            if (properties.Length >= 2)
            {
                packageDto.Size = properties[1];
                if (Enum.TryParse(packageDto.Size, out PackageSize temporaryPackageSize))
                {
                }
                else
                {
                    isValid = false;
                }
            }

            if (properties.Length >= 3)
            {
                packageDto.Provider = properties[2];
                if (Enum.TryParse(packageDto.Provider, out PackageProvider temporaryPackageProvider))
                {
                }
                else
                {
                    isValid = false;
                }
            }

            var transactionDto = new TransactionDto
            {
                Date = date,
                Package = packageDto
            };

            if (isValid)
            {
            }
            else
            {
                transactionDto = new IgnoredTransactionDto(line);
            }

            return transactionDto;
        }
    }
}