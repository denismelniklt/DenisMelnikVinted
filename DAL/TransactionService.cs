using AutoMapper;
using Domain;
using Infrastructure;
using System.Collections.Generic;

namespace DAL
{
    public class TransactionService : ITransactionService
    {
        public IEnumerable<Transaction> GetTransactions(string filePath)
        {
            var transactionDtos = TransactionFileManager.ReadTransactionDtos(filePath);
            var transactions = new List<Transaction>();

            foreach (var transactionDto in transactionDtos)
            {
                Transaction transaction;

                if (transactionDto is IgnoredTransactionDto)
                {
                    var textLine = (transactionDto as IgnoredTransactionDto).TextLine;
                    transaction = new IgnoredTransaction(textLine);
                }
                else
                {
                    transaction = Mapper.Map<Transaction>(transactionDto);
                }

                transactions.Add(transaction);
            }

            return transactions;
        }
    }
}