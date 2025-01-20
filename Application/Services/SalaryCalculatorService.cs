using SalaryCalculator.Application.Models;
using SalaryCalculator.Domain.Interfaces;
using SalaryCalculator.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace SalaryCalculator.Application.Services
{
    public class SalaryCalculatorService
    {
        private readonly IEnumerable<ITaxCalculator> _taxCalculators;

        public SalaryCalculatorService(IEnumerable<ITaxCalculator> taxCalculators)
        {
            _taxCalculators = taxCalculators;
        }

        public SalaryBreakdown CalculateSalaryBreakdown(Money grossPackage, PayFrequency payFrequency)
        {
            var package = new SalaryPackage(grossPackage, payFrequency);
            var deductions = new Dictionary<string, Money>();
            var totalDeductions = Money.Zero;

            // Calculate all deductions
            foreach (var calculator in _taxCalculators)
            {
                var tax = calculator.CalculateTax(package.TaxableIncome);
                deductions[calculator.TaxName] = tax;
                totalDeductions += tax;
            }

            var netIncome = package.GrossPackage - package.Superannuation - totalDeductions;
            var payPacket = package.CalculatePayPacketAmount(netIncome);

            return new SalaryBreakdown
            {
                GrossPackage = package.GrossPackage,
                Superannuation = package.Superannuation,
                TaxableIncome = package.TaxableIncome,
                Deductions = deductions,
                NetIncome = netIncome,
                PayPacket = payPacket,
                PayFrequency = payFrequency
            };
        }
    }
}
