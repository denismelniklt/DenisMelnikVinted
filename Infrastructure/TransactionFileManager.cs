using Domain;
using System.Collections.Generic;
using System.IO;

namespace Infrastructure
{
    public static class TransactionFileManager
    {
        public static IEnumerable<TransactionDto> ReadTransactionDtos(string filePath)
        {
            var lines = File.ReadAllLines(filePath);

            var transactionDtoList = new List<TransactionDto>();

            foreach (var line in lines)
            {
                yield return line.ToTransactionDto();
            }
        }
    }
}