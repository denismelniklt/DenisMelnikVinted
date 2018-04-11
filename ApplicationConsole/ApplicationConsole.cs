using Microsoft.Extensions.DependencyInjection;
using System;
using Infrastructure;
using AutoMapper;
using AutoMapper.Mappers;
using Domain;
using System.Collections.Generic;

using IShipmentServiceDal = DAL.IShipmentService;
using ShipmentServiceDal = DAL.ShipmentService;
using ITransactionServiceDal = DAL.ITransactionService;
using TransactionServiceDal = DAL.TransactionService;
using ITransactionServiceBll = BLL.ITransactionService;
using TransactionServiceBll = BLL.TransactionService;


namespace ApplicationConsole
{
    public class ApplicationConsole
    {
        private const string InputFilePath = "input.txt";
        private static ITransactionServiceBll TransactionServiceBll { get; set; }

        static void Main(string[] args)
        {
            RegisterServices();
            InitializeAutomapper();

            var inputFilePath = InputFilePath;
            if (args.Length == 1)
            {
                inputFilePath = args[0];
            }

            var transactions = TransactionServiceBll.GetTransactions(inputFilePath);

            ShowTransactionsInConsole(transactions);

            Console.ReadKey();
        }

        private static void ShowTransactionsInConsole(IEnumerable<Transaction> transactions)
        {
            var transactionDtos = new List<TransactionDto>();

            foreach (var transaction in transactions)
            {
                TransactionDto transactionDto;

                if (transaction is IgnoredTransaction)
                {
                    var ignoredTransaction = transaction as IgnoredTransaction;
                    transactionDto = new IgnoredTransactionDto(ignoredTransaction.TextLine);
                }
                else
                {
                    transactionDto = Mapper.Map<TransactionDto>(transaction);
                }

                Console.WriteLine(transactionDto.GetTransactionLine());
            }
        }

        private static void RegisterServices()
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<IShipmentServiceDal, ShipmentServiceDal>()
                .AddTransient<ITransactionServiceDal, TransactionServiceDal>()
                .AddTransient<ITransactionServiceBll, TransactionServiceBll>()
                .BuildServiceProvider();

            TransactionServiceBll = serviceProvider.GetService<ITransactionServiceBll>();
        }

        private static void InitializeAutomapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddConditionalObjectMapper().Where((s, d) => s.Name == d.Name + "Dto");
            });
        }
    }
}
