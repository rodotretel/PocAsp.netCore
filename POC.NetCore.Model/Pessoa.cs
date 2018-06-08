using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POC.NetCore.Model
{
    public class Pessoa
    {
        public int Id { get; set; }
        [Required]
        public string PrimeiroNome { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Titulo { get; set; }
        public int Idade { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        [Required]
        [Phone]
        public string Telefone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
