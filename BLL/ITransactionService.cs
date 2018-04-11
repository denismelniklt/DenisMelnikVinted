using System.Collections.Generic;
using Domain;

namespace BLL
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetTransactions(string filePath);
    }
}
