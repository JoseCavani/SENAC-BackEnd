using ExercicioBackEnd.Controller;
using System;

namespace ExercicioBackEnd
{
    class Program
    {
        static void Main(string[] args)
        {
            IControlable controller;
            while (true)
            {
                Console.WriteLine("1-cliente\n 2-Estoque\n3-Fornecedor");
                switch (Console.ReadLine())
                {
                    case "1":
                        controller = new ControllerCliente();
                        opcoes();
                        break;
                    case "2":
                        controller = new ControllerEstoque();
                        opcoes();
                        break;
                    case "3":
                        controller = new ControllerFornecedor();
                        opcoes();
                        break;
                }


                void opcoes()
                {
                    Console.WriteLine("1 - inserir\n 2-editar\n 3-selecionarTodos\n 5-Excluir");

                    switch (Console.ReadLine())
                    {
                        case "1":
                            controller.Inserir();
                            break;
                        case "2":
                            controller.Editar();
                            break;
                        case "3":
                            controller.SelecionarTodos();
                            break;
                        case "5":
                            controller.Excluir();
                            break;
                    }
                }
            }

        }
    }
}
