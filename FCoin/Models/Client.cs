namespace FCoin.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public int QtdMoeda { get; set; }
        public DateTime InvalidoAte { get; set; } = new();
        public int Flags { get; set; } = 0;
    }
}
