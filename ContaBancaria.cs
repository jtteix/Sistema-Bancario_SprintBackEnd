using System;

namespace SistemaBancario
{
    public abstract class ContaBancaria : IImprimivel
    {
        public string NumeroConta { get; private set; }
        public string Titular { get; private set; }
        public decimal Saldo { get; protected set; }

        public ContaBancaria(string numeroConta, string titular, decimal saldoInicial)
        {
            if (string.IsNullOrWhiteSpace(numeroConta))
                throw new ArgumentException("O número da conta não pode ser vazio.");
            if (string.IsNullOrWhiteSpace(titular))
                throw new ArgumentException("O nome do titular não pode ser vazio.");
            if (saldoInicial < 0)
                throw new ArgumentException("O saldo inicial não pode ser negativo.");

            NumeroConta = numeroConta;
            Titular = titular;
            Saldo = saldoInicial;
        }

        public decimal ConsultarSaldo() => Saldo;

        public virtual void Depositar(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("O valor do depósito deve ser maior que zero.");

            Saldo += valor;
            Console.WriteLine($"Depósito de R$ {valor:F2} realizado com sucesso!");
        }

        public abstract void Sacar(decimal valor);

        public virtual void ExibirExtrato()
        {
            Console.WriteLine($"Conta: {NumeroConta} | Titular: {Titular} | Saldo: R$ {Saldo:F2}");
        }
    }
}