using System;
using System.Collections.Generic;

namespace BankAccountsSystem
{
    class CustomerAccount : IInterest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SSN { get; set; }
        public string AccountType { get; set; }
        public int InitialDeposit { get; set; }
        public string DigitalAccountNumber { get; set; }

        public void Deposit()
        {
            Console.WriteLine($"\nAt the moment this account has { InitialDeposit } deposit, enter the amount you want to deposit: ");
            InitialDeposit += int.Parse(Console.ReadLine());
            Console.WriteLine($"After depositing this account has { InitialDeposit } deposit");
        }
        public void Withdraw()
        {
            Console.WriteLine($"\nAt the moment this account has { InitialDeposit } deposit, enter the amount you want to withdraw: ");
            int amount = int.Parse(Console.ReadLine());
            if ((InitialDeposit - amount) < 0)
            {
                Console.WriteLine("Sorry, this account doesn't have enough deposit to withdraw this amount.");
            }
            else
            {
                InitialDeposit -= amount;
                Console.WriteLine($"After withdrawing this account has { InitialDeposit } deposit");
            }
        }
        public void Transfer(List<string> SSNS)
        {
            Console.WriteLine($"\nAt the moment this account has { InitialDeposit } deposit, enter the amount you want to transfer, press enter and type SSN of the receiver: ");
            int amount = int.Parse(Console.ReadLine());
            string SSN = Console.ReadLine();
            if ((InitialDeposit - amount) < 0)
            {
                Console.WriteLine("Sorry, this account doesn't have enough deposit to transfer this amount.");
            }
            else if (!SSNS.Contains(SSN))
            {
                Console.WriteLine("Sorry, you have entered non existent SSN, try again please.");
            }
            else
            {
                InitialDeposit -= amount;
                Console.WriteLine($"After transfering to { SSN } this account has { InitialDeposit } deposit");
            }
        }
        public void ShowInfo()
        {
        }

        public virtual double BaseInterestRateCalculator()
        {
            if (InitialDeposit >= 0 && InitialDeposit < 1000)
            {
                return 5.00;
            }
            else if (InitialDeposit >= 1000 && InitialDeposit < 2000)
            {
                return 10.00;
            }
            else if (InitialDeposit >= 2000 && InitialDeposit < 5000)
            {
                return 15.00;
            }
            else if (InitialDeposit >= 5000 && InitialDeposit < 10000)
            {
                return 20.00;
            }
            else return 0;
        }
    }
}