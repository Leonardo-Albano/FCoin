using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FCoin.Models
{
    public class Validator
    {
        [Key]
        public int Id { get; set; }
        public string Token { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("Selector")]
        public int SelectorId { get; set; }
        [JsonIgnore]
        public Selector Selector { get; set; }
    }
}
