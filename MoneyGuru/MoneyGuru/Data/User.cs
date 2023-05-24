using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

public class User
{
   // [PrimaryKey, AutoIncrement]
    public int UserID { get; set; }

    //[Unique]
    public string Email { get; set; }
    public string Password { get; set; }  //Зробити хешування
    //public string Token { get; set; }
}
