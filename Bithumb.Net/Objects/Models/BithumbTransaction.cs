using Bithumb.Net.Enums;

namespace Bithumb.Net.Objects.Models
{
    public record BithumbTransaction(DateTime transaction_date, BithumbTransactionType type, decimal units_traded, decimal price, decimal total);
}
