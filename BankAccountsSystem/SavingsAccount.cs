using System;

namespace BankAccountsSystem
{
    class SavingsAccount : CustomerAccount
    {
        public int SafetyDepositBoxNumber { get; set; }
        public string SafetyDepositBoxCode { get; set; }

        public new void ShowInfo()
        {
            Console.WriteLine($"\nThis is information panel of a savings account with following data: "
            + $"\n Social Security Number:    { SSN }"
            + $"\n First Name:                { FirstName }"
            + $"\n Last Name:                 { LastName }"
            + $"\n Account Type:              { AccountType }"
            + $"\n Deposit:                   { InitialDeposit }"
            + $"\n Digital Account Number:    { DigitalAccountNumber}"
            + $"\n Safety Deposit Box Number: { SafetyDepositBoxNumber }"
            + $"\n Safety Deposit Box Code:   { SafetyDepositBoxCode }"
            + $"\n Base Interest Rate:        { BaseInterestRateCalculator() }");
        }

        public override double BaseInterestRateCalculator()
        {
            double savingsAccountsCondition = 0.25;
            return base.BaseInterestRateCalculator() - savingsAccountsCondition;
        }
    }
}
