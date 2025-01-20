namespace SalaryCalculator.Domain.Models
{
    public enum PayFrequency
    {
        Weekly,
        Fortnightly,
        Monthly
    }

    public static class PayFrequencyExtensions
    {
        public static int GetPayPeriodsPerYear(this PayFrequency frequency)
        {
            return frequency switch
            {
                PayFrequency.Weekly => 52,
                PayFrequency.Fortnightly => 26,
                PayFrequency.Monthly => 12,
                _ => throw new System.ArgumentException($"Invalid pay frequency: {frequency}")
            };
        }

        public static string ToDisplayString(this PayFrequency frequency)
        {
            return frequency switch
            {
                PayFrequency.Weekly => "weekly",
                PayFrequency.Fortnightly => "fortnightly",
                PayFrequency.Monthly => "monthly",
                _ => throw new System.ArgumentException($"Invalid pay frequency: {frequency}")
            };
        }
    }
}
