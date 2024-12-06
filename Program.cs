using System.Runtime.CompilerServices;
using System.Security.Principal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Inheritance___operator_overloading
{
    public static class AccountUtil
    {
        // Utility helper functions for Account class

        public static void Display(List<Account> accounts)
        {
            Console.WriteLine("\n=== Accounts ==========================================");
            foreach (var acc in accounts)
            {
                Console.WriteLine(acc);
            }
        }

        public static void Deposit(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing to Accounts =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void Withdraw(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing from Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }
    }
    public class Account
    {
        public Account(string Name = "Unnamed Account", double Balance = 0.0)
        {
            this.Name = Name;
            this.Balance = Balance;
        }
        public string Name { get; set; }
        public double Balance { get; set; }

        public virtual bool Deposit(double amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                return true;
            }

            return false;
        }

        public virtual bool Withdraw(double amount)
        {
            if (Balance > amount)
            {
                Balance -= amount;
                return true;
            }

            return false;
        }
        public static Account operator +(Account acc1,Account acc2)
        {
            Account newacc = new Account()
            {
                Balance = acc1.Balance + acc2.Balance,
            };
            return newacc;
        }

        public override string ToString()
        {
            return $" Name: {Name}, Balance: {Balance}";
        }
    }
    public class SavingsAccount : Account
    {
        public SavingsAccount(string Name= "Unnamed Account", double Balance = 0.0 , double InterestRate=0.0) : base(Name, Balance)
        {
            this.Rate = InterestRate;
        }

        public double Rate { get; set; }

        public override bool Withdraw(double amount)
        {
            return base.Withdraw(amount + Rate);
        }
        public override bool Deposit(double amount)
        {
            return base.Deposit(amount);
        }
        
        public override string ToString()
        {
            return $"{base.ToString()}, Rate: {Rate}";
        }
    }
    public class CheckingAccount : Account
    {        
        public double Fee{ get; set; }

        public CheckingAccount(string Name = "Unnamed Account", double Balance = 0.0) : base(Name, Balance)
        {        
            this.Fee = 1.5;

        }
        public override bool Withdraw(double amount)
        {
            return base.Withdraw(amount + Fee);
        }
        public override string ToString()
        {
            return $"{base.ToString()}, Fee: {Fee:C}";
        }

    }
    public class TrustAccount :SavingsAccount
    {
        public int Count { get; set; }= 0;
        public DateTime finalDate { get; set; }
        public int Bonus { get; set; }

        public TrustAccount(string Name = "Unnamed Account", double Balance = 0.0, double InterestRate =0.0) : base(Name, Balance,InterestRate)
        {
            this.Bonus = Bonus;
            this.Count = Count;
            finalDate = DateTime.Now.AddYears(1);
        }
        public override bool Deposit(double amount)
        {
            if (amount >= 5000)
            {
                Console.WriteLine("you got 50$ bonus");
                return base.Deposit(amount+50);
            }
            else 
            {
                return base.Deposit(amount);
            }
        }
     
        public override bool Withdraw(double amount)
        {
            Console.WriteLine(DateTime.Now);
            if (DateTime.Now < finalDate )
            {
                Count++;
            }
            if (amount < Balance * .2 && Count <= 3)
            {                
                return base.Withdraw(amount);
            }
            return false;
        
        }
        public override string ToString()
        {
            return $"{base.ToString()}";
        }


    }
        
internal class Program { 

    static void Main(string[] args)
        {

            ///////////////////////////
            //Accounts
            var accounts = new List<Account>();
            accounts.Add(new Account());
            accounts.Add(new Account("Larry"));
            accounts.Add(new Account("Moe", 2000));
            accounts.Add(new Account("Curly", 5000));

            AccountUtil.Display(accounts);
            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);

            // Savings
            var savAccounts = new List<Account>();
            savAccounts.Add(new SavingsAccount());
            savAccounts.Add(new SavingsAccount("Superman"));
            savAccounts.Add(new SavingsAccount("Batman", 2000));
            savAccounts.Add(new SavingsAccount("Wonderwoman", 5000, 5.0));

            AccountUtil.Display(savAccounts);
            AccountUtil.Deposit(savAccounts, 1000);
            AccountUtil.Withdraw(savAccounts, 2000);

            //Checking
            var checAccounts = new List<Account>();
            checAccounts.Add(new CheckingAccount());
            checAccounts.Add(new CheckingAccount("Larry2"));
            checAccounts.Add(new CheckingAccount("Moe2", 2000));
            checAccounts.Add(new CheckingAccount("Curly2", 5000));

            AccountUtil.Display(checAccounts);
            AccountUtil.Deposit(checAccounts, 1000);
            AccountUtil.Withdraw(checAccounts, 2000);
            AccountUtil.Withdraw(checAccounts, 2000);

            //// Trust
            var trustAccounts = new List<Account>();
            trustAccounts.Add(new TrustAccount());
            trustAccounts.Add(new TrustAccount("Superman2"));
            trustAccounts.Add(new TrustAccount("Batman2", 2000));
            trustAccounts.Add(new TrustAccount("Wonderwoman2", 5000, 5.0));

            AccountUtil.Display(trustAccounts);
            //AccountUtil.Deposit(trustAccounts, 1000);
            AccountUtil.Deposit(trustAccounts, 60000);
            AccountUtil.Withdraw(trustAccounts, 2000);
            AccountUtil.Withdraw(trustAccounts, 3000);
            AccountUtil.Withdraw(trustAccounts, 500);
            AccountUtil.Withdraw(trustAccounts, 500);
            //
            //Operator
            Account a = new Account("ahmed", 100);
            Account b = new Account("salem", 200);
            Console.WriteLine(a + b);
            Console.WriteLine();
        }
    }
}
