using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FCoin.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("RemetenteObj")]
        public int Remetente { get; set; }

        [ForeignKey("RecebedorObj")]
        public int Recebedor { get; set; }

        public int Valor { get; set; }
        public int Status { get; set; } = 0; // 1-Concluida / 2-Erro / 0-Não executada
        public DateTime Data { get; set; }
    }
}
