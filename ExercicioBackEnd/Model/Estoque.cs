using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExercicioBackEnd.Model
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
            if (dataAlteracao != DateTime.MinValue)
            return $"{numero}\n{nome}\n{quantidade}\n{dataCadastro}\n{dataAlteracao}";
            else
                return $"{numero}\n{nome}\n{quantidade}\n{dataCadastro}";
        }
    }
}
