﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FCoin.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public int QtdMoeda { get; set; } = 100;
        public DateTime? InvalidoAte { get; set; } = new(); 
    }
}
