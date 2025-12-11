using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_transporte_pilha
{
    class Program
    {
        static void Main(string[] args)
        {
            SistemaTransporte sistema = new SistemaTransporte();
            bool rodando = true;

            for (int i = 1; i <= 8; i++)
            {
                sistema.CadastrarVeiculo(i, 10); 
            }

            sistema.CadastrarGaragem(new Aeroporto("Congonhas"));
            sistema.CadastrarGaragem(new Aeroporto("Guarulhos"));

            while (rodando)
            {
                Console.WriteLine("\n=== MENU DE OPÇÕES ===");
                Console.WriteLine("0. Finalizar");
                Console.WriteLine("1. Cadastrar veículo");
                Console.WriteLine("2. Cadastrar garagem");
                Console.WriteLine("3. Iniciar jornada");
                Console.WriteLine("4. Encerrar jornada");
                Console.WriteLine("5. Liberar viagem");
                Console.WriteLine("6. Listar veículos em garagem");
                Console.WriteLine("7. Contar viagens entre aeroportos");
                Console.WriteLine("8. Listar viagens entre aeroportos");
                Console.WriteLine("9. Somar passageiros entre aeroportos");
                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                try
                {
                    switch (opcao)
                    {
                        case "0":
                            {
                                rodando = false;
                                break;
                            }

                        case "1":
                            {
                                Console.Write("ID do veículo: ");
                                int id = int.Parse(Console.ReadLine());
                                Console.Write("Capacidade: ");
                                int cap = int.Parse(Console.ReadLine());
                                sistema.CadastrarVeiculo(id, cap);
                                Console.WriteLine("Veículo cadastrado com sucesso!");
                                break;
                            }

                        case "2":
                            {
                                Console.Write("Nome do aeroporto: ");
                                string nome = Console.ReadLine();
                                sistema.CadastrarGaragem(new Aeroporto(nome));
                                Console.WriteLine("Garagem cadastrada com sucesso!");
                                break;
                            }

                        case "3":
                            {
                                sistema.IniciarJornada();
                                Console.WriteLine("Jornada iniciada e vans distribuídas!");
                                break;
                            }

                        case "4":
                            {
                                sistema.EncerrarJornada();
                                Console.WriteLine("Jornada encerrada!");
                                break;
                            }

                        case "5":
                            {
                                Console.Write("Aeroporto de origem: ");
                                string orig = Console.ReadLine();
                                Console.Write("Aeroporto de destino: ");
                                string dest = Console.ReadLine();
                                Console.Write("Número de passageiros: ");
                                int pax = int.Parse(Console.ReadLine());

                                Aeroporto origem = sistema.Aeroportos.Find(a => a.Nome == orig);
                                Aeroporto destino = sistema.Aeroportos.Find(a => a.Nome == dest);

                                sistema.LiberarViagem(origem, destino, pax);
                                Console.WriteLine("Viagem liberada!");
                                break;
                            }

                        case "6":
                            {
                                Console.Write("Aeroporto: ");
                                string aero = Console.ReadLine();
                                Aeroporto a = sistema.Aeroportos.Find(x => x.Nome == aero);
                                sistema.ListarVans(a);
                                break;
                            }

                        case "7":
                            {
                                Console.Write("Origem: ");
                                string o = Console.ReadLine();
                                Console.Write("Destino: ");
                                string d = Console.ReadLine();
                                Aeroporto orig2 = sistema.Aeroportos.Find(aero2 => aero2.Nome == o);
                                Aeroporto dest2 = sistema.Aeroportos.Find(aero2 => aero2.Nome == d);
                                int qViagens = sistema.ContarViagens(orig2, dest2);
                                Console.WriteLine($"Quantidade de viagens: {qViagens}");
                                break;
                            }

                        case "8":
                            {
                                Console.Write("Origem: ");
                                string o2 = Console.ReadLine();
                                Console.Write("Destino: ");
                                string d2 = Console.ReadLine();
                                Aeroporto orig3 = sistema.Aeroportos.Find(aero2 => aero2.Nome == o2);
                                Aeroporto dest3 = sistema.Aeroportos.Find(aero2 => aero2.Nome == d2);
                                sistema.ListarViagens(orig3, dest3);
                                break;
                            }

                        case "9":
                            {
                                Console.Write("Origem: ");
                                string o3 = Console.ReadLine();
                                Console.Write("Destino: ");
                                string d3 = Console.ReadLine();
                                Aeroporto orig4 = sistema.Aeroportos.Find(aero2 => aero2.Nome == o3);
                                Aeroporto dest4 = sistema.Aeroportos.Find(aero2 => aero2.Nome == d3);
                                int soma = sistema.SomarPassageiros(orig4, dest4);
                                Console.WriteLine($"Total de passageiros: {soma}");
                                break;
                            }

                        default:
                            {
                                Console.WriteLine("Opção inválida!");
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }

            Console.WriteLine("Programa finalizado.");

        }
    }
}
