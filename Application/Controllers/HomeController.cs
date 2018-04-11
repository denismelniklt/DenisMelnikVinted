using AutoMapper;
using BLL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Application.Controllers
{
    public class HomeController : Controller
    {
        private ITransactionService TransactionService { get; set; }

        public HomeController(ITransactionService transactionService)
        {
            TransactionService = transactionService;
        }

        public IActionResult Index()
        {
            var transactions = TransactionService.GetTransactions(ApplicationEnvironment.InputFilePath);

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

                transactionDtos.Add(transactionDto);
            }

            return View(transactionDtos);
        }
    }
}
