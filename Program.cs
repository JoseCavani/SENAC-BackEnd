using Exercicio1.Controller;
using Exercicio1.Model;
using Exercicio1.View;
using System;

namespace Exercicio1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IControlable controller;
            while (true) { 
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
                    break;
            }


                 void opcoes() {
                    Console.WriteLine("1 - inserir\n 2-editar\n 3-selecionarTodos\n 4- SelecionarNumero \n 5-Excluir");

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
                        case "4":
                            controller.SelecionarPorNumero();
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
