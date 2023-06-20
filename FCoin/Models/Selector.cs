using System.ComponentModel.DataAnnotations;

namespace FCoin.Models
{
    public class Selector
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Ip { get; set; }
    }
}
