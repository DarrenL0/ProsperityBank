using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ProsperityBank.Data;
using ProsperityBank.Models;


namespace ProsperityBank.BackgroundServices
{
    public class BillPayBackgroundService : BackgroundService
    {

        private readonly IServiceProvider _services;
        private readonly ILogger<BillPayBackgroundService> _logger;
        public BillPayBackgroundService(IServiceProvider services, ILogger<BillPayBackgroundService> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("BillPay Background service is running");

            while (!stoppingToken.IsCancellationRequested)
            {
                await DoWork(stoppingToken);

                _logger.LogInformation("Service is waiting 30 seconds.");

                await Task.Delay(TimeSpan.FromSeconds(30.00), stoppingToken);
            }
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Service is working.");
            using var scope = _services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ProsperityBankDBContext>();

            await UpdateBillPay(context);

        }

        /*
         * This method checks all billpay schedule date if the schedule date is in the past it would go through the validator
         * after passing the validator it would update the account balance and create a transaction to portray a billpay been processed
         * any failed bill pay would be ignored
         */
        private async Task UpdateBillPay(ProsperityBankDBContext _context)
        {
            var billPayList = await _context.BillPays.ToListAsync();

            foreach (var billPay in billPayList)
            {
                // check if billpay has been blocked
                if (billPay.Blocked == false)
                {
                    //check the date to see if current day pass schedule date
                    if (billPay.ScheduleDate.CompareTo(DateTime.UtcNow) <= 0)
                    {
                        _logger.LogInformation("Billpay is being processed.");
                        var account = await _context.Accounts.FindAsync(billPay.AccountNumber);

                        //check if balance is enough
                        if (account.ValidateDebit(billPay.Amount, TransactionType.BillPay))
                        {
                            //decrease amount and create transaction
                            account.ScheduleBillPay(billPay);
                            //save changes in db
                            await _context.SaveChangesAsync();

                            //handling the billpay
                            await billPayHandle(billPay, _context);

                            _logger.LogInformation("Billpay has been processed");
                        }
                    }
                    else
                    {
                        _logger.LogInformation("Billpay transaction has failed");
                    }
                }

            }
        }

        /*
         * This method helps schedule payment that has been processed to be updated on to the new
         * schedule payment so that the system wouldn't process the same schedule payment repetively.
         */
        private async Task billPayHandle(BillPay billPay, ProsperityBankDBContext _context)
        {
            switch (billPay.PeriodType)
            {
                case PeriodType.Monthly:
                    await AddMonth(billPay.BillPayID, _context);
                    break;
                case PeriodType.Quarterly:
                    await AddQuarter(billPay.BillPayID, _context);
                    break;
                case PeriodType.OnceOff:
                    await DeleteOnce(billPay.BillPayID, _context);
                    break;
            }
        }


        private async Task AddMonth(int id, ProsperityBankDBContext _context)
        {
            //find the bill pay
            var billPay = _context.BillPays.Find(id);
            //add a month to the schedule pay
            billPay.ScheduleDate = billPay.ScheduleDate.AddMonths(1);
            //add the edited bill pay into the List and update the db
            _context.Add(billPay);
            _context.Update(billPay);
            await _context.SaveChangesAsync();
        }

        private async Task AddQuarter(int id, ProsperityBankDBContext _context)
        {
            //find the bill pay
            var billPay = _context.BillPays.Find(id);
            //add a 3 month to the schedule pay
            billPay.ScheduleDate = billPay.ScheduleDate.AddMonths(3);
            //add the edited bill pay into the List and update the db
            _context.Add(billPay);
            _context.Update(billPay);
            await _context.SaveChangesAsync();
        }

        private async Task DeleteOnce(int id, ProsperityBankDBContext _context)
        {
            //find the bill pay
            var billPay = _context.BillPays.Find(id);
            //remove the bill pay from the list 
            _context.Remove(billPay);
            //update db
            await _context.SaveChangesAsync();
        }

    }
}
