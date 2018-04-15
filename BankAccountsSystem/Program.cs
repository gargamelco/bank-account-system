using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BankAccountsSystem
{
    static class Program
    {
        static void Main(string[] args)
        {
            var SSNS = new List<string>();
            string path = Directory.GetCurrentDirectory() + "\\bankinfo.csv";

            try
            {
                using (var rd = new StreamReader(path))
                {
                    while (!rd.EndOfStream)
                    {
                        var splits = rd.ReadLine().Split(';');
                        SSNS.Add(splits[2]);
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine("CSV file is currently being used by another program or absent.");
                return;
            }

            Console.WriteLine("To enter the system type your SSN: ");
            var enteredSSN = Console.ReadLine();
            bool correctSSN = false;

            while (true)
            {
                foreach (var SSN in SSNS)
                {
                    if (enteredSSN == SSN)
                    {
                        Console.WriteLine("You have successfully entered your SSN!");
                        correctSSN = true;
                    }
                }
                if (correctSSN)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong SSN, please reenter correctly your SSN: ");
                    enteredSSN = Console.ReadLine();
                }
            }

            var customer = File.ReadLines(path).Skip(1)
                             .Select(LineParser)
                             .Where(s => s.SSN == int.Parse(enteredSSN))
                             .FirstOrDefault();

            CustomerAccount specificCustomer = АssignAccountType(customer);


            Console.WriteLine("Enter following keys to access the features:"
                         + "\n1 for depositing money"
                         + "\n2 for withdrawing money"
                         + "\n3 for transfering money"
                         + "\n4 for showing personal information"
                         + "\n5 for exiting program");

            bool finish = false;

            while (true)
            {
                string strReadKey = Console.ReadKey().KeyChar.ToString();
                int selectionKey;
                int.TryParse(strReadKey, out selectionKey);

                switch (selectionKey)
                {
                    case 1:
                        if (specificCustomer.GetType() == typeof(SavingsAccount))
                        {
                            ((SavingsAccount)specificCustomer).Deposit();
                        }
                        else if (specificCustomer.GetType() == typeof(CheckingAccount))
                        {
                            ((CheckingAccount)specificCustomer).Deposit();
                        }
                        break;
                    case 2:
                        if (specificCustomer.GetType() == typeof(SavingsAccount))
                        {
                            ((SavingsAccount)specificCustomer).Withdraw();
                        }
                        else if (specificCustomer.GetType() == typeof(CheckingAccount))
                        {
                            ((CheckingAccount)specificCustomer).Withdraw();
                        }
                        break;
                    case 3:
                        if (specificCustomer.GetType() == typeof(SavingsAccount))
                        {
                            ((SavingsAccount)specificCustomer).Transfer(SSNS);
                        }
                        else if (specificCustomer.GetType() == typeof(CheckingAccount))
                        {
                            ((CheckingAccount)specificCustomer).Transfer(SSNS);
                        }
                        break;
                    case 4:
                        if (specificCustomer.GetType() == typeof(SavingsAccount))
                        {
                            ((SavingsAccount)specificCustomer).ShowInfo();
                        }
                        else if (specificCustomer.GetType() == typeof(CheckingAccount))
                        {
                            ((CheckingAccount)specificCustomer).ShowInfo();
                        }
                        break;
                    case 5:
                        finish = true;
                        Console.WriteLine("\nBank Application closed.\n");
                        break;
                    default:
                        Console.WriteLine("\nInvalid key pressed!\n");
                        break;
                }

                if (finish)
                {
                    break;
                }
            }
        }

        private static CustomerAccount LineParser(string line)
        {
            string[] splits = line.Split(';');
            return new CustomerAccount
            {
                FirstName = splits[0],
                LastName = splits[1],
                SSN = int.Parse(splits[2]),
                AccountType = splits[3],
                InitialDeposit = int.Parse(splits[4])
            };
        }

        private static CustomerAccount АssignAccountType(CustomerAccount customer)
        {
            if (customer.AccountType == "Savings")
            {
                return new SavingsAccount()
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    SSN = customer.SSN,
                    AccountType = customer.AccountType,
                    InitialDeposit = customer.InitialDeposit,
                    DigitalAccountNumber = "1" + (customer.SSN % 100).ToString()
                                             + (new Random().Next(10000, 100000)).ToString()
                                             + (new Random().Next(100, 1000)).ToString(),
                    SafetyDepositBoxNumber = new Random().Next(100, 1000),
                    SafetyDepositBoxCode = Guid.NewGuid().ToString().Substring(0, 4)
                };
            }
            else
            {
                return new CheckingAccount()
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    SSN = customer.SSN,
                    AccountType = customer.AccountType,
                    InitialDeposit = customer.InitialDeposit,
                    DigitalAccountNumber = "1" + (customer.SSN % 100).ToString()
                                             + (new Random().Next(10000, 100000)).ToString()
                                             + (new Random().Next(100, 1000)).ToString(),
                    DebitCardNumber = NextLong(new Random(), 100000000000, 1000000000000),
                    DebitCardPin = Guid.NewGuid().ToString().Substring(0, 4)
                };
            }
        }

        private static long NextLong(this Random self, long min, long max)
        {
            var buf = new byte[sizeof(ulong)];
            self.NextBytes(buf);
            ulong n = BitConverter.ToUInt64(buf, 0);

            double normalised = n / (ulong.MaxValue + 1.0);

            double range = (double)max - min;

            return (long)(normalised * range) + min;
        }
    }
}

