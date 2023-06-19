using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FCoin.Models
{
    public class Selector
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Ip { get; set; }

        [JsonIgnore]
        public List<Validator> Validators { get; set; } = new();
    }
}
