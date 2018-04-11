using Domain;
using System.Collections.Generic;

namespace DAL
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetTransactions(string filePath);
    }
}
