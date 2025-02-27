using System;
using System.Collections.Generic;

public class BankAccount
{
    private decimal balance;
    private string accountNumber;z    private List<Transaction> transactions;

    public BankAccount(string accountNumber, decimal initialBalance)
    {
        this.accountNumber = accountNumber;
        balance = initialBalance;
        transactions = new List<Transaction>();
    }

    public void ProcessTransaction(Transaction transaction)
    {
        // Fix 1: Null check
        if (transaction == null)
        {
            Console.WriteLine("Error: Null transaction cannot be processed.");
            return;
        }

        // Fix 2: Insufficient funds check
        if (transaction.Type == TransactionType.Withdrawal && transaction.Amount > balance)
        {
            Console.WriteLine("Error: Insufficient funds for withdrawal.");
            return;
        }

        // Add transaction and update balance
        transactions.Add(transaction);

        if (transaction.Type == TransactionType.Deposit)
        {
            balance += transaction.Amount;
        }
        else if (transaction.Type == TransactionType.Withdrawal)
        {
            balance -= transaction.Amount;
        }
    }

    public decimal GetBalance()
    {
        // Fix 3: Correct balance calculation logic
        decimal calculatedBalance = 0;

        foreach (var transaction in transactions)
        {
            if (transaction.Type == TransactionType.Deposit)
                calculatedBalance += transaction.Amount;
            else if (transaction.Type == TransactionType.Withdrawal)
                calculatedBalance -= transaction.Amount;
        }

        return calculatedBalance;
    }
}

public class Transaction
{
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public DateTime Date { get; set; }
}

public enum TransactionType
{
    Deposit,
    Withdrawal
}

class Program
{
    static void Main()
    {
        var account = new BankAccount("1234", 1000);

        // Valid deposit
        account.ProcessTransaction(new Transaction { Amount = 500, Type = TransactionType.Deposit });
        // Valid withdrawal
        account.ProcessTransaction(new Transaction { Amount = 200, Type = TransactionType.Withdrawal });
        // Null transaction
        account.ProcessTransaction(null);
        // Withdrawal with insufficient funds
        account.ProcessTransaction(new Transaction { Amount = 2000, Type = TransactionType.Withdrawal });

        Console.WriteLine($"Final balance: {account.GetBalance()}");
    }
}
