using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercicio1.Model
{
    internal class Cliente : Base
    {
        public Cliente(int numero, string nome) : base(numero, nome)
        {
        }

        public override string ToString()
        {
            return $"numero {numero}\n nome {nome} \n";
        }
    }
}
