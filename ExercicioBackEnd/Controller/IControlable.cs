using ExercicioBackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExercicioBackEnd.Controller
{
    internal interface IControlable
    {
        public abstract void Inserir();
        public abstract void Editar();
        public abstract void Excluir();
        public abstract void SelecionarTodos();
        public abstract Base SelecionarPorNumero();
    }
}
