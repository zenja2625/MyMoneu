using MyMoney.DataBindEnums;
using System.ComponentModel;

namespace MyMoney.Enums
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum TransactionDateRange
    {
        [Description("Месяц")]
        Month,
        [Description("Неделя")]
        Week,
        [Description("День")]
        Day,
        [Description("Год")]
        Year,
        [Description("Всё")]
        All
    }
}
