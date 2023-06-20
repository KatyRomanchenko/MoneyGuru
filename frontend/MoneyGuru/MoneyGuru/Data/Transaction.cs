﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

public class Transaction
{
    public int TransactionID { get; set; }
    public string TransactionName { get; set; }
    public string Wallet { get; set; }
    public int UserID { get; set; }
    public string Category { get; set; }
    public decimal Amount { get; set; }
    public string TransactionType { get; set; }
    public DateTime Date { get; set; }

    [JsonIgnore]
    public string Color { get; set; }
}

public enum TransactionType
{
    Income,
    Expense
}
