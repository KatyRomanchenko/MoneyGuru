using System.Collections.Generic;

namespace MoneyGuruWebAPI.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
    }
}
