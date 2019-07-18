using MyMoney.DataBindEnums;
using System.ComponentModel;

namespace MyMoney.Enums
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum TransactionType
    {
        [Description("Расход")]
        Expense,
        [Description("Доход")]
        Income
    }
}
