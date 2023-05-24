using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

public class Transaction
{
    [PrimaryKey, AutoIncrement]
   // [Display(AutoGenerateField = false)]
    public int TransactionID { get; set; }
    public int UserID { get; set; }
    public string Category { get; set; }

    [Unique]
    public decimal Amount { get; set; }
    public enum TransactionType { }
    public DateTime Date { get; set; }
}
