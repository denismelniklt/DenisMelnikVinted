using Domain;
using System.Collections.Generic;

using ITransactionServiceDal = DAL.ITransactionService;
using ITransactionServiceBll = BLL.ITransactionService;
using IShipmentServiceDal = DAL.IShipmentService;

namespace BLL
{
    public class TransactionService : ITransactionServiceBll
    {
        private readonly ITransactionServiceDal TransactionServiceDal;
        private readonly IShipmentServiceDal ShipmentServiceDal;

        public TransactionService(ITransactionServiceDal transactionServiceDal, IShipmentServiceDal shipmentPriceServiceDal)
        {
            TransactionServiceDal = transactionServiceDal;
            ShipmentServiceDal = shipmentPriceServiceDal;
        }

        public IEnumerable<Transaction> GetTransactions(string filePath)
        {
            var transactions = TransactionServiceDal.GetTransactions(filePath);

            foreach (var transaction in transactions)
            {
                var shipment = ShipmentServiceDal.GetShipment(transaction);

                transaction.Package.Shipment = shipment;
            }

            return transactions;
        }
    }
}