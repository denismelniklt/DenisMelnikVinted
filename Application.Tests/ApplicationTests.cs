using Microsoft.VisualStudio.TestTools.UnitTesting;

using ITransactionServiceDal = DAL.ITransactionService;
using ITransactionServiceBll = BLL.ITransactionService;
using TransactionServiceDal = DAL.TransactionService;
using TransactionServiceBll = BLL.TransactionService;
using IShipmentServiceDal = DAL.IShipmentService;
using ShipmentServiceDal = DAL.ShipmentService;

using System.Linq;
using AutoMapper;
using AutoMapper.Mappers;
using Domain;

namespace Application.Tests
{
    [TestClass]
    public class ApplicationTests
    {
        private string InputTextPath { get; set; }
        private ITransactionServiceBll TransactionServiceBll { get; set; }
        private IShipmentServiceDal ShipmentServiceBll { get; set; }

        [TestInitialize]
        public void Setup()
        {
            InputTextPath = "input.txt";

            var transactionServiceDal = new TransactionServiceDal();
            var shipmentServiceDal = new ShipmentServiceDal();
            TransactionServiceBll = new TransactionServiceBll(transactionServiceDal, shipmentServiceDal);

            InitializeAutomapper();
        }

        [TestMethod]
        public void ReadingTransactionsFromFile()
        {
            var transactions = TransactionServiceBll.GetTransactions(InputTextPath);

            var transactionCount = transactions.Count();
            Assert.AreEqual(21, transactionCount);
        }

        [TestMethod]
        public void IgnoredTransactionCount()
        {
            var transactions = TransactionServiceBll.GetTransactions(InputTextPath);

            var ignoredTransactions = transactions.Where(transaction => transaction.GetType() == typeof(IgnoredTransaction));
            var ignoredTransactionCount = ignoredTransactions.Count();

            Assert.AreEqual(1, ignoredTransactionCount);
        }

        [TestMethod]
        public void CheckTransactionPriceAndDiscount()
        {
            const int TransactionsToSkip = 17;
            var selectedTransaction = TransactionServiceBll.GetTransactions(InputTextPath).Skip(TransactionsToSkip).FirstOrDefault();

            var price = selectedTransaction.Package.Shipment.Price;
            var discount = selectedTransaction.Package.Shipment.Discount;

            Assert.IsNotNull(selectedTransaction);
            Assert.AreEqual(price, 1.90M);
            Assert.AreEqual(discount, 0.10M);
        }

        private void InitializeAutomapper()
        {
            try
            {
                // Mapper cannot be initialized more than once
                Mapper.Initialize(cfg =>
                {
                    cfg.AddConditionalObjectMapper().Where((s, d) => s.Name == d.Name + "Dto");
                });
            }
            catch
            {
            }
        }
    }
}