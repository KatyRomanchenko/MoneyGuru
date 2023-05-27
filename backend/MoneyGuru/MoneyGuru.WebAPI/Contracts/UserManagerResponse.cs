using System.Collections.Generic;
using System;
using System.Text;
using MoneyGuru;
using MoneyGuru.ViewModels;
using MoneyGuruWebAPI;
using MoneyGuruWebAPI.Contracts;

namespace MoneyGuruWebAPI.Contracts
{
    public class UserManagerResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
