using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExercicioBackEnd.Model
{
    internal class Fornecedor : Base
    {
        public Fornecedor(int numero, string nome) : base(numero, nome)
        {
        }

        public override string ToString()
        {
            return $"numero {numero}\n nome {nome} \n";
        }
    }
}
