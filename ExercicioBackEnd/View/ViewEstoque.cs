using ExercicioBackEnd.Controller;
using ExercicioBackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExercicioBackEnd.View
{
    internal class ViewEstoque
    {
        internal Estoque Cadastro()
        {
            Console.WriteLine("Nome");
            string nome = Console.ReadLine();

            Console.WriteLine("quantidade");
            int quantidade = int.Parse(Console.ReadLine());

            return new Estoque(0, nome,quantidade,DateTime.Now);
        }

        internal int pegaId()
        {
            Console.WriteLine("id");
            return int.Parse(Console.ReadLine());
        }

        internal void MostrarTodos(List<Estoque> registros)
        {
            foreach (var item in registros)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadKey();
        }
    }
}
