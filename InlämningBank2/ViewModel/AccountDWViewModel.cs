using System.ComponentModel.DataAnnotations;

namespace InlämningBank2.ViewModel
{
    public class AccountDWViewModel
    {
        public int AccountId { get; set; }

        [StringLength(20)]
        public string AccountNo { get; set; }
        public decimal Balance { get; set; }
    }
}
