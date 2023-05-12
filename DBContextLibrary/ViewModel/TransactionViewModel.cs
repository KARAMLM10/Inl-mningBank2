namespace DBContextLibrary.ViewModel
{
    public class TransactionViewModel
    {
        public int Accountid { get; set; }
        public int Account { get; set; }
        public DateTime? Date { get; set; } 
        public decimal? Balance { get; set; }   
        public decimal? Amount { get; set; }  
        public int TransactionId { get; set; }
        public string Type { get; set; }       
    }
}
