using SalaryCalculator.Domain.Interfaces;
using SalaryCalculator.Domain.Models;

namespace SalaryCalculator.Domain.Services
{
    public class MedicareLevyCalculator : ITaxCalculator
    {
        private const decimal LowerThreshold = 21335m;
        private const decimal UpperThreshold = 26668m;
        private const decimal LowerRate = 0.0m;
        private const decimal MidRate = 0.10m;
        private const decimal UpperRate = 0.02m;

        public string TaxName => "Medicare Levy";

        public Money CalculateTax(Money taxableIncome)
        {
            if (taxableIncome <= new Money(LowerThreshold))
                return Money.Zero;

            if (taxableIncome <= new Money(UpperThreshold))
            {
                return ((taxableIncome - new Money(LowerThreshold)) * MidRate).RoundUpToNearestDollar();
            }

            return (taxableIncome * UpperRate).RoundUpToNearestDollar();
        }
    }
}
