using System;

namespace MoneyGuru
{
public class AddTransactionDetail
    {
        public double Spent { get; set; }

        public CategoryPicker Category { get; set; }

        //[Display(Name = "Expense Description")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Description should not be empty")]
        public string ExpenseDescription { get; set; }

        public DateTime Date { get; set; }

        public enum CategoryPicker
        {
            Home,
            Entertainment,
            Food,
            Charity,
            Utilities,
            Auto,
            Education,
            HelathAndWellness,
            Shopping
        };
    }
}