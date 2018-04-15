using System;

namespace BankAccountsSystem
{
    class CheckingAccount : CustomerAccount
    {
        public long DebitCardNumber { get; set; }
        public string DebitCardPin { get; set; }

        public new void ShowInfo()
        {
            Console.WriteLine($"\nThis is information panel of a checking account with following data: "
            + $"\n Social Security Number:  { SSN }"
            + $"\n First Name:              { FirstName }"
            + $"\n Last Name:               { LastName }"
            + $"\n Account Type:            { AccountType }"
            + $"\n Deposit:                 { InitialDeposit }"
            + $"\n Digital Account Number:  { DigitalAccountNumber}"
            + $"\n Debit Card Number:       { DebitCardNumber }"
            + $"\n Debit Card Pin:          { DebitCardPin }"
            + $"\n Base Interest Rate:      { BaseInterestRateCalculator() }");
        }

    }
}
