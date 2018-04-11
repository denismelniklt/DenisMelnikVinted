using BLL;
using Microsoft.Extensions.DependencyInjection;
using System;

using IShipmentServiceDal = DAL.IShipmentService;
using ShipmentServiceDal = DAL.ShipmentService;
using ITransactionServiceDal = DAL.ITransactionService;
using TransactionServiceDal = DAL.TransactionService;
using ITransactionServiceBll = BLL.ITransactionService;
using TransactionServiceBll = BLL.TransactionService;
using AutoMapper;
using AutoMapper.Mappers;

namespace ApplicationConsole
{
    public class Program
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
