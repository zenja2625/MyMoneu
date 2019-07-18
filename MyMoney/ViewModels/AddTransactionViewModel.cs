using MyMoney.DbModel;
using MyMoney.Enums;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace MyMoney.ViewModels
{
    class AddTransactionViewModel : BaseViewModel
    {
        private string description;
        private string amount;
        private TransactionType? selectedTransactionType;
        private string category;
        private string quantity;
        private string account;
        private RelayCommand _addTransaction;
        private AccountHistoryContext db;
        public AddTransactionViewModel()
        {
            db = new AccountHistoryContext();
            db.Categories.Load();
            db.Accounts.Load();
            Accounts = CollectionViewSource.GetDefaultView(db.Accounts.Local);
            Categories = CollectionViewSource.GetDefaultView(db.Categories.Local);

            Quantity = "1";
        }

        public RelayCommand AddTransaction => _addTransaction ??
                         (_addTransaction = new RelayCommand(updateMoney =>
                         {
                             var updateAccount = db.Accounts.SingleOrDefault(acc => acc.Name == Account) ?? new Account() { Name = Account, Amount = 0 };

                             if (SelectedTransactionType == TransactionType.Income)
                                 updateAccount.Amount += (int)(decimal.Parse(Amount) * 100) * int.Parse(Quantity);
                             else
                                 updateAccount.Amount -= (int)(decimal.Parse(Amount) * 100) * int.Parse(Quantity);

                             db.Transactions.Add(new Transaction()
                             {
                                 Date = DateTime.Now,
                                 Account = updateAccount,
                                 Amount = (int)(decimal.Parse(Amount) * 100),
                                 Quantity = int.Parse(Quantity),
                                 Type = SelectedTransactionType.Value,
                                 Category = db.Categories.Local.SingleOrDefault(category => category.Name == Category) ?? new Category() { Name = Category },
                                 Description = Description
                             });

                             db.SaveChanges();

                             (updateMoney as Action)();

                             ClearProperties();

                         }, obj => !(Amount == null || SelectedTransactionType == null || Category == null || Account == null)));
        public string Description
        {
            get { return description; }
            set { description = value != "" ? value : null; OnPropertyChanged(); }
        }
        public string Amount
        {
            get { return amount; }
            set
            {
                value = value.Replace('.', ',');

                if (new Regex(@"^\d+,?\d{0,2}$").IsMatch(value))
                    amount = value;
                else if (value == "")
                    amount = null;
                else if (new Regex(@"^,\d{0,2}$").IsMatch(value))
                    amount = value.Insert(0, "0");

                OnPropertyChanged();
            }
        }
        public TransactionType? SelectedTransactionType
        {
            get { return selectedTransactionType; }
            set { selectedTransactionType = value; OnPropertyChanged(); }
        }
        public string Category
        {
            get { return category; }
            set { category = value != "" ? value : null; OnPropertyChanged(); }
        }
        public string Quantity
        {
            get { return quantity; }
            set { quantity = uint.TryParse(value, out _) && value != "0" ? value : "1"; OnPropertyChanged(); }
        }
        public string Account
        {
            get { return account; }
            set { account = value != "" ? value : null; OnPropertyChanged(); }
        }
        public ICollectionView Accounts { get; set; }
        public ICollectionView Categories { get; set; }

        private void ClearProperties()
        {
            Description = null;
            Amount = "";
            SelectedTransactionType = null;
            Quantity = "1";
            Account = null;
            Category = null;
        }


    }
}
