using EControle.Core.Data.Models;
using EControle.Data.EF.Connection;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EControle.Data.SDK
{
    public class Worker : IWorker
    {
        public DataCtx _context;

        public Worker(DataCtx context)
        {
            _context = context;

            //Accounts = new Repository<Account>(_context);
            InternalTransactions = new Repository<InternalTransaction>(_context);
            Transactions = new Repository<Transaction>(_context);
            Users = new Repository<User>(_context);
            BalanceSheet = new Repository<BalanceSheet>(_context);
            SedcAccounts = new Repository<SedcAccount>(_context);
            SEDCTransaction = new Repository<SEDCTransaction>(_context);
            

        }

         
        //public Repository<Account> Accounts { get; private set; }
        public Repository<InternalTransaction> InternalTransactions { get; private set; }
        public Repository<Transaction> Transactions { get; private set; }
        public Repository<User> Users { get; private set; }
        public Repository<SedcAccount> SedcAccounts { get; private set; }
        public Repository<BalanceSheet> BalanceSheet { get; set; }
        public Repository<SEDCTransaction> SEDCTransaction { get; set; }
        


        public async Task<bool> Complete()
        {
            try
            {
                return (await _context.SaveChangesAsync() >= 1);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                //ignore the error.
                return false;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}