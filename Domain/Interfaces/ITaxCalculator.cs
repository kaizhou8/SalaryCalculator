using SalaryCalculator.Domain.Models;

namespace SalaryCalculator.Domain.Interfaces
{
    public interface ITaxCalculator
    {
        string TaxName { get; }
        Money CalculateTax(Money taxableIncome);
    }
}
