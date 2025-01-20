using SalaryCalculator.Domain.Models;
using System.Collections.Generic;

namespace SalaryCalculator.Application.Models
{
    public class SalaryBreakdown
    {
        public Money GrossPackage { get; set; }
        public Money Superannuation { get; set; }
        public Money TaxableIncome { get; set; }
        public Dictionary<string, Money> Deductions { get; set; } = new Dictionary<string, Money>();
        public Money NetIncome { get; set; }
        public Money PayPacket { get; set; }
        public PayFrequency PayFrequency { get; set; }
    }
}
