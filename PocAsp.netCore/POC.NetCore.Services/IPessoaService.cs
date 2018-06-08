using POC.NetCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace POC.NetCore.Services
{
    public interface IPessoaService
    {
        IEnumerable<Pessoa> GetAll();
        Pessoa Get(int id);
        Pessoa Add(Pessoa Pessoa);
        void Update(int id, Pessoa Pessoa);
        void Delete(int id);
    }
}
