using Domain;

namespace Core
{
    public interface IDiscountRule
    {
        decimal GetDiscount(Transaction transaction, decimal shipmentDiscount = new decimal());
    }
}