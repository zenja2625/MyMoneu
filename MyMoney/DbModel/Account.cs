using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyMoney.DbModel
{
    public class Account
    {
        [Key]
        public string Name { get; set; }
        public int Amount { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
