using SalaryCalculator.Domain.Interfaces;
using SalaryCalculator.Domain.Models;

namespace SalaryCalculator.Domain.Services
{
    public class BudgetRepairLevyCalculator : ITaxCalculator
    {
        private const decimal Threshold = 180000m;
        private const decimal Rate = 0.02m;

        public string TaxName => "Budget Repair Levy";

        public Money CalculateTax(Money taxableIncome)
        {
            if (taxableIncome <= new Money(Threshold))
                return Money.Zero;

            return ((taxableIncome - new Money(Threshold)) * Rate).RoundUpToNearestDollar();
        }
    }
}
