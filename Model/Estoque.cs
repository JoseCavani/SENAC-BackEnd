using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercicio1.Model
{
    internal class Estoque : Base
    {
       public int quantidade;
       public DateTime dataCadastro;
       public  DateTime dataAlteracao;

        public Estoque(int id, string nome,int quantidade, DateTime dataCadastro) : base(id,nome)
        {
            this.quantidade = quantidade;
            this.dataCadastro = dataCadastro;
        }

        public override string ToString()
        {
            return $"{numero}\n{nome}\n{quantidade}\n{dataCadastro}\n{dataAlteracao}";
        }
    }
}
