using System;
using System.Collections.Generic;

namespace SistemaBancario
{
    class Program
    {
        static List<ContaBancaria> contas = new List<ContaBancaria>();

        static void Main(string[] args)
        {
            bool rodando = true;

            while (rodando)
            {
                Console.Clear();
                Console.WriteLine("=================================");
                Console.WriteLine("         SISTEMA BANCÁRIO        ");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Criar Conta Corrente");
                Console.WriteLine("2. Criar Conta Poupança");
                Console.WriteLine("3. Realizar Depósito");
                Console.WriteLine("4. Realizar Saque");
                Console.WriteLine("5. Consultar Extrato de Todas as Contas");
                Console.WriteLine("6. Aplicar Rendimento (Poupança)");
                Console.WriteLine("7. Sair");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1": CriarConta(isCorrente: true); break;
                    case "2": CriarConta(isCorrente: false); break;
                    case "3": OperacaoDeposito(); break;
                    case "4": OperacaoSaque(); break;
                    case "5": ListarExtratos(); break;
                    case "6": AplicarRendimentoPoupança(); break;
                    case "7":
                        rodando = false;
                        Console.WriteLine("\nSaindo do sistema...");
                        break;
                    default:
                        Pausar("Opção inválida! Pressione ENTER para tentar novamente.");
                        break;
                }
            }
        }

        static void CriarConta(bool isCorrente)
        {
            try
            {
                Console.Clear();
                Console.WriteLine(isCorrente ? "--- NOVA CONTA CORRENTE ---" : "--- NOVA CONTA POUPANÇA ---");

                Console.Write("Número da Conta: ");
                string num = Console.ReadLine();

                Console.Write("Nome do Titular: ");
                string titular = Console.ReadLine();

                Console.Write("Saldo Inicial (R$): ");
                decimal saldo = decimal.Parse(Console.ReadLine());

                if (isCorrente)
                {
                    Console.Write("Limite do Cheque Especial (R$): ");
                    decimal limite = decimal.Parse(Console.ReadLine());
                    contas.Add(new ContaCorrente(num, titular, saldo, limite));
                }
                else
                {
                    contas.Add(new ContaPoupanca(num, titular, saldo));
                }

                Pausar("Conta cadastrada com sucesso!");
            }
            catch (FormatException)
            {
                Pausar("Erro: Entrada numérica em formato inválido!");
            }
            catch (Exception ex)
            {
                Pausar($"Erro ao criar conta: {ex.Message}");
            }
        }

        static void OperacaoDeposito()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("--- DEPÓSITO ---");
                ContaBancaria conta = BuscarConta();

                if (conta != null)
                {
                    Console.Write("Valor a depositar: R$ ");
                    decimal valor = decimal.Parse(Console.ReadLine());
                    conta.Depositar(valor);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
            Pausar("");
        }

        static void OperacaoSaque()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("--- SAQUE ---");
                ContaBancaria conta = BuscarConta();

                if (conta != null)
                {
                    Console.Write("Valor a sacar: R$ ");
                    decimal valor = decimal.Parse(Console.ReadLine());
                    conta.Sacar(valor);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na operação: {ex.Message}");
            }
            Pausar("");
        }

        static ContaBancaria BuscarConta()
        {
            Console.Write("Digite o número da conta: ");
            string num = Console.ReadLine();

            ContaBancaria conta = contas.Find(c => c.NumeroConta == num);

            if (conta == null)
            {
                Console.WriteLine("Conta não encontrada!");
            }

            return conta;
        }

        static void ListarExtratos()
        {
            Console.Clear();
            Console.WriteLine("--- RELATÓRIO DE CONTAS ---");

            if (contas.Count == 0)
            {
                Console.WriteLine("Nenhuma conta registrada no sistema.");
            }
            else
            {
                foreach (var conta in contas)
                {
                    conta.ExibirExtrato();
                    Console.WriteLine("---------------------------------");
                }
            }
            Pausar("");
        }

        static void AplicarRendimentoPoupança()
        {
            Console.Clear();
            Console.WriteLine("--- APLICAR RENDIMENTO POUPANÇA ---");
            ContaBancaria conta = BuscarConta();

            if (conta is ContaPoupanca poupanca)
            {
                poupanca.RenderJuros();
            }
            else if (conta != null)
            {
                Console.WriteLine("Esta operação só é válida para Contas Poupança.");
            }
            Pausar("");
        }

        static void Pausar(string mensagem)
        {
            if (!string.IsNullOrEmpty(mensagem))
                Console.WriteLine(mensagem);

            Console.WriteLine("\nPressione ENTER para continuar...");
            Console.ReadLine();
        }
    }
}