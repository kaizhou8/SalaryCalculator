using Microsoft.Extensions.DependencyInjection;
using SalaryCalculator.Application.Services;
using SalaryCalculator.Domain.Interfaces;
using SalaryCalculator.Domain.Models;
using SalaryCalculator.Domain.Services;
using System;
using System.Collections.Generic;

namespace SalaryCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IEnumerable<ITaxCalculator>>(sp => new List<ITaxCalculator>
                {
                    new MedicareLevyCalculator(),
                    new BudgetRepairLevyCalculator(),
                    new IncomeTaxCalculator()
                })
                .AddSingleton<SalaryCalculatorService>()
                .BuildServiceProvider();

            var calculator = serviceProvider.GetRequiredService<SalaryCalculatorService>();

            try
            {
                // Get user input
                Console.Write("Enter your salary package amount: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal grossAmount))
                {
                    throw new ArgumentException("Invalid salary amount");
                }

                Console.Write("Enter your pay frequency (W for weekly, F for fortnightly, M for monthly): ");
                var frequencyInput = Console.ReadLine()?.Trim().ToUpper();
                var frequency = frequencyInput switch
                {
                    "W" => PayFrequency.Weekly,
                    "F" => PayFrequency.Fortnightly,
                    "M" => PayFrequency.Monthly,
                    _ => throw new ArgumentException("Invalid pay frequency")
                };

                Console.WriteLine("\nCalculating salary details...");

                // Calculate breakdown
                var breakdown = calculator.CalculateSalaryBreakdown(new Money(grossAmount), frequency);

                // Display results
                Console.WriteLine($"Gross package: {breakdown.GrossPackage}");
                Console.WriteLine($"Superannuation: {breakdown.Superannuation}");
                Console.WriteLine($"Taxable income: {breakdown.TaxableIncome}");
                Console.WriteLine("\nDeductions:");
                foreach (var deduction in breakdown.Deductions)
                {
                    Console.WriteLine($"{deduction.Key}: {deduction.Value}");
                }
                Console.WriteLine($"\nNet income: {breakdown.NetIncome}");
                Console.WriteLine($"Pay packet: {breakdown.PayPacket} per {breakdown.PayFrequency.ToDisplayString()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to end...");
            Console.ReadKey();
        }
    }
}
