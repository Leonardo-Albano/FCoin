namespace FCoin.Models
{
    public class Validator
    {
        public int Id { get; set; }
        public string Token { get; set; } = Guid.NewGuid().ToString();
    }
}
