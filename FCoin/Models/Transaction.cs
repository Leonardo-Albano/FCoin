namespace FCoin.Models
{
    public class Transaction
    {
        public float TransfValue { get; set; }
        public float CurrentAccountValue { get; set; }
        public DateTime ActualTime { get; set; }
        public DateTime LastTransactionTime { get; set; }
        public int QuantityTransactions { get; set; }

    }
}
