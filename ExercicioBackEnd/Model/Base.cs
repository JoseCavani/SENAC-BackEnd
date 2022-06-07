using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExercicioBackEnd.Model
{
    public abstract class Base
    {
        public int numero;
        public string nome;

        protected Base(int numero, string nome)
        {
            this.numero = numero;
            this.nome = nome;
        }
        public Base()
        {

        }

        public abstract override string ToString();

    }
}
