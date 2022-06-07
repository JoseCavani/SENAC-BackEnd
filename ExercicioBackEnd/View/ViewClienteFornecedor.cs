using ExercicioBackEnd.Controller;
using ExercicioBackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExercicioBackEnd.View
{
    public class ViewClienteFornecedor
    {


        internal Base Cadastro<T>() where T : Base
        {
            Console.WriteLine("Nome");
            string nome = Console.ReadLine();

            if (typeof(T)  == typeof(Cliente))
            return new Cliente(0, nome);
            else 
            return new Fornecedor(0, nome);
        }

        internal int PegaId()
        {
            Console.WriteLine("id");
            return int.Parse(Console.ReadLine());
            
        }

        internal void MostrarTodos<T>(List<T> registros) where T : Base
        {
            foreach (var item in registros)
            {
               Console.WriteLine(item.ToString());
            }
            Console.ReadKey();
        }
    }
}


