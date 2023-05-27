using System.Collections.Generic;

namespace MoneyGuru.Data
{
    public class Wallet
    {
        public int WalletID { get; set; }
        public string Type { get; set; } //Cash or card
        public User User { get; set; }
        public decimal AmountOfMoney { get; set; }
        public string WalletName { get; set; }
    }
}
