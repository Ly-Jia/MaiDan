using MaiDan.Billing.Domain;
using MaiDan.Ordering.Domain;

namespace MaiDan.Api.Services
{
    public interface ICashRegister
    {
        Bill Calculate(Order order);
        void Print(Order order);
    }
}
