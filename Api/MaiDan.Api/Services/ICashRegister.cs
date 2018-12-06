using MaiDan.Accounting.Domain;
using MaiDan.Billing.Domain;
using MaiDan.Ordering.Domain;

namespace MaiDan.Api.Services
{
    public interface ICashRegister
    {
        Bill Calculate(Order order);
        void Print(Order order);
        void Pay(Bill bill);
        void AddPayments(Slip slip);
        void CloseDay(DaySlip daySlip);
    }
}
