using System;

namespace Moneybox.App.Utilities
{
    /// <summary>
    /// This basic static class will contain very high level checks which can easily accessed across the app.
    /// However as the application grows I would implement a specific validation service for all input and make most classes implement it.
    /// </summary>
    public static class Validator
    {
        public static void CheckAmountPositive(decimal money)
        {
            if (!(money > 0))
            {
                throw new InvalidOperationException("The Amount must be positive!");
            }
        }
    }
}