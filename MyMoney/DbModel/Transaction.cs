using MyMoney.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMoney.DbModel
{
    public class Transaction
    {
        [Key]
        public DateTime Date { get; set; }
        public string Description { get; set; }
        [ForeignKey("Account")]
        public string Account_Name { get; set; }
        public Account Account { get; set; }
        public int Amount { get; set; }
        public TransactionType Type { get; set; }
        public int Quantity { get; set; }
        public Category Category { get; set; }
    }
}