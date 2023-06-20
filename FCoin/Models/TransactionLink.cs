using System.ComponentModel.DataAnnotations;

namespace FCoin.Models
{
    public class TransactionLink
    {
        [Key]
        public int Id { get; set; }
        public int ValidatorId { get; set; }
        public int TransactionId { get; set; }
    }
}
