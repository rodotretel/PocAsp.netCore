using GenFu;
using Newtonsoft.Json;
using POC.NetCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POC.NetCore.Services
{
    public class PessoaService : IPessoaService
    {
        private List<Pessoa> Pessoas { get; set; }
        private Rabbit rabbitmq = new Rabbit();
        public PessoaService()
        {
            var i = 0;
            Pessoas = A.ListOf<Pessoa>(50);
            Pessoas.ForEach(Pessoa =>
            {
                i++;
                Pessoa.Id = i;
            });
        }

        public IEnumerable<Pessoa> GetAll()
        {
            return Pessoas;
        }

        public Pessoa Get(int id)
        {
            return Pessoas.First(_ => _.Id == id);
        }

        public Pessoa Add(Pessoa Pessoa)
        {
            var newid = Pessoas.OrderBy(_ => _.Id).Last().Id + 1;
            Pessoa.Id = newid;

            rabbitmq.Sender(JsonConvert.SerializeObject(Pessoa));

            return Pessoa;
        }

        public void Update(int id, Pessoa Pessoa)
        {
            var existing = Pessoas.First(_ => _.Id == id);
            existing.PrimeiroNome = Pessoa.PrimeiroNome;
            existing.Endereco = Pessoa.Endereco;
            existing.Idade = Pessoa.Idade;
            existing.Cidade = Pessoa.Cidade;
            existing.Email = Pessoa.Email;
            existing.Telefone = Pessoa.Telefone;
            existing.Titulo = Pessoa.Titulo;
        }

        public void Delete(int id)
        {
            var existing = Pessoas.First(_ => _.Id == id);
            Pessoas.Remove(existing);
        }
    }
}
