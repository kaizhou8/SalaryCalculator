using System;

namespace SalaryCalculator.Domain.Models
{
    public class SalaryPackage
    {
        public const decimal SuperannuationRate = 0.095m; // 9.5%

        public SalaryPackage(Money grossPackage, PayFrequency payFrequency)
        {
            if (grossPackage <= Money.Zero)
                throw new ArgumentException("Gross package must be greater than zero", nameof(grossPackage));

            GrossPackage = grossPackage;
            PayFrequency = payFrequency;

            // Calculate superannuation and taxable income
            var unroundedTaxableIncome = grossPackage / (1 + SuperannuationRate);
            Superannuation = (grossPackage - unroundedTaxableIncome).RoundUpToNearestCent();
            TaxableIncome = grossPackage - Superannuation;

        }

        public Money GrossPackage { get; }
        public Money Superannuation { get; }
        public Money TaxableIncome { get; }
        public PayFrequency PayFrequency { get; }

        public Money CalculatePayPacketAmount(Money netIncome)
        {
            return (netIncome / PayFrequency.GetPayPeriodsPerYear()).RoundUpToNearestCent();
        }
    }
}
