using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using MyMoney.DbModel;
using MyMoney.Enums;
using System.Data.Entity;

namespace MyMoney.ViewModels
{
    public class StartViewModel : BaseViewModel
    {
        private int balance;
        private int monthlyExpenses;
        private List<Account> accounts;

        public StartViewModel()
        {
            Update();
            UpdateMoney = Update;
        }

        public Action UpdateMoney { get; set; }

        private void Update()
        {
            using (var db = new AccountHistoryContext())
            {
                Balance = db.Accounts.Sum(x => (int?)x.Amount) ?? 0;
                MonthlyExpenses = db.Transactions.Where(x => x.Date.Month == DateTime.Now.Month && x.Date.Year == DateTime.Now.Year && x.Type == TransactionType.Expense).Sum(x => (int?)x.Amount) ?? 0;

                Accounts = db.Accounts.ToList();
            }

        }
        public int Balance
        {
            get { return balance; }
            set { balance = value; OnPropertyChanged(); }
        }
        public int MonthlyExpenses
        {
            get { return monthlyExpenses; }
            set { monthlyExpenses = value; OnPropertyChanged(); }
        }
        public List<Account> Accounts
    {
            get { return accounts; }
            set { accounts = value; OnPropertyChanged(); }
        }

    }
}
