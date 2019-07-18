using MyMoney.DbModel;
using MyMoney.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Data;

namespace MyMoney.ViewModels
{
    class PurseViewModel : BaseViewModel
    {
        private AccountHistoryContext db;
        private TransactionType transactionType;
        private TransactionDateRange dateRange;
        private List<Transaction> transactions;
        private string transactionName;
        private int totalAmount;

        public PurseViewModel()
        {
            db = new AccountHistoryContext();

            transactions = new List<Transaction>();

            TransactionName = string.Empty;
            TransactionType = TransactionType.Expense;


            TransactionsView = CollectionViewSource.GetDefaultView(transactions);

            TransactionsView.Filter = TransactionFilter;

            DateRange = TransactionDateRange.Month;
        }
        public TransactionDateRange DateRange
        {
            get { return dateRange; }
            set
            {
                dateRange = value;
                OnPropertyChanged();
                transactions.Clear();
                transactions.AddRange(GetTransactions());
                TransactionsView?.Refresh();
                CalculateTotalAmount();
            }
        }
        public TransactionType TransactionType
        {
            get { return transactionType; }
            set
            {
                transactionType = value;
                OnPropertyChanged();
                TransactionsView?.Refresh();
                CalculateTotalAmount();
            }
        }
        public string TransactionName
        {
            get { return transactionName; }
            set
            {
                transactionName = value;
                OnPropertyChanged();
                TransactionsView?.Refresh();
                CalculateTotalAmount();
            }
        }
        public int TotalAmount
        {
            get { return totalAmount; }
            set { totalAmount = value; OnPropertyChanged(); }
        }
        public ICollectionView TransactionsView { get; set; }
        private bool TransactionFilter(object obj)
        {
            var transaction = obj as Transaction;

            bool IsCantain = transaction.Category.Name.ToUpper().StartsWith(TransactionName.ToUpper());
            bool IsTrueType = transaction.Type == TransactionType;

            return IsCantain && IsTrueType;
        }
        private void CalculateTotalAmount()
        {
            if (TransactionsView != null)
            {
                TotalAmount = 0;
                foreach (var item in TransactionsView)
                {
                    var transaction = item as Transaction;
                    TotalAmount += transaction.Amount * transaction.Quantity;
                }
            }
        }
        private List<Transaction> GetTransactions()
        {
            DateTime first;
            DateTime last;

            switch (DateRange)
            {
                case TransactionDateRange.Month:
                    first = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    last = first.AddMonths(1).AddSeconds(-1);
                    break;
                case TransactionDateRange.Year:
                    first = new DateTime(DateTime.Now.Year, 1, 1);
                    last = new DateTime(DateTime.Now.Year, 12, DateTime.DaysInMonth(DateTime.Now.Year, 12));
                    break;
                case TransactionDateRange.Week:
                    var dayOfWeek = (int)DateTime.Now.DayOfWeek;
                    var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    first = date.AddDays(-(dayOfWeek != 0 ? dayOfWeek - 1 : 6));
                    last = first.AddDays(7).AddSeconds(-1);
                    break;
                case TransactionDateRange.Day:
                    first = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    last = first.AddDays(1).AddSeconds(-1);
                    break;
                default:
                    first = last = DateTime.Now;
                    break;
            }

            var transactions = (from trans in db.Transactions
                                where DateRange == TransactionDateRange.All ? true : trans.Date >= first && trans.Date <= last
                                select trans).Include(t => t.Category);

            return transactions.ToList();
        }
    }
}
