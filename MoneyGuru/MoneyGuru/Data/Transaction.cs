using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

public class Transaction
{
    [PrimaryKey, AutoIncrement]
    public int TransactionID { get; set; }
    public int UserID { get; set; }
    public string Category { get; set; }

    [Unique]
    public decimal Amount { get; set; }
    public TransactionType TransactionType { get; set; }
    public DateTime Date { get; set; }
}

public enum TransactionType
{
    Income,
    Expense
}
