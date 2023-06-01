using Newtonsoft.Json;
using System.Collections.Generic;

namespace MoneyGuru
{
    public class Wallet
    {
        [JsonProperty("walletID")]
        public int WalletID { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("amountOfMoney")]
        public string Type { get; set; } //Cash or card
        //public User User { get; set; }
        public decimal AmountOfMoney { get; set; }
        [JsonProperty("walletName")]
        public string WalletName { get; set; }
    }
}
