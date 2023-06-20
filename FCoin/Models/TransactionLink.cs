using System.ComponentModel.DataAnnotations;

namespace FCoin.Models
{
    public class TransactionLink
    {
        [Key]
        public int Id { get; set; }
        public int ValidatorId { get; set; }
        public int TransactionId { get; set; }
        public int Success { get; set; } = 0;// 1-Concluida / 2-Erro / 0-Não executada
    }
}
