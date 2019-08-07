using System;

namespace Moneybox.App.Utilities
{
    /// <summary>
    /// This basic static class will contain very high level checks which can be easily accessed across the app.
    /// However as the application grows I would remove this class and implement specific validation rules for different classes.
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Throws an exception if a negative sum of money is input.
        /// </summary>
        /// <param name="money">The money to be Checked.</param>
        public static void CheckAmountPositive(decimal money)
        {
            if (!(money > 0))
            {
                throw new InvalidOperationException("The Amount must be positive!");
            }
        }
    }
}