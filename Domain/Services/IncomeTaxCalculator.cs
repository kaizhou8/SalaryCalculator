using SalaryCalculator.Domain.Interfaces;
using SalaryCalculator.Domain.Models;
using System.Collections.Generic;

namespace SalaryCalculator.Domain.Services
{
    public class IncomeTaxCalculator : ITaxCalculator
    {
        private readonly List<TaxBracket> _taxBrackets = new List<TaxBracket>
        {
            new TaxBracket(0m, 18200m, 0m, 0m),
            new TaxBracket(18201m, 37000m, 0m, 0.19m),
            new TaxBracket(37001m, 87000m, 3572m, 0.325m),
            new TaxBracket(87001m, 180000m, 19822m, 0.37m),
            new TaxBracket(180001m, decimal.MaxValue, 54232m, 0.47m)
        };

        public string TaxName => "Income Tax";

        public Money CalculateTax(Money taxableIncome)
        {
            foreach (var bracket in _taxBrackets)
            {
                if (taxableIncome <= new Money(bracket.UpperBound))
                {
                    var baseTax = new Money(bracket.BaseTax);
                    var additionalTax = (taxableIncome - new Money(bracket.LowerBound)) * bracket.Rate;
                    return (baseTax + additionalTax).RoundUpToNearestDollar();
                }
            }

            // Should never reach here due to decimal.MaxValue in last bracket
            return Money.Zero;
        }

        private class TaxBracket
        {
            public TaxBracket(decimal lowerBound, decimal upperBound, decimal baseTax, decimal rate)
            {
                LowerBound = lowerBound;
                UpperBound = upperBound;
                BaseTax = baseTax;
                Rate = rate;
            }

            public decimal LowerBound { get; }
            public decimal UpperBound { get; }
            public decimal BaseTax { get; }
            public decimal Rate { get; }
        }
    }
}
