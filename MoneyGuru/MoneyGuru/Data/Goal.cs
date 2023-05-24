using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

public class Goal
{
    [PrimaryKey, AutoIncrement]
    public int GoalID { get; set; }
    public int UserID { get; set; }

    [Unique]
    public string Email { get; set; }
    public string Password { get; set; }  //Зробити хешування
    public string Token { get; set; }
    public decimal CurrentBalance { get; set; } 
}
