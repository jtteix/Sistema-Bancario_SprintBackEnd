using System;

namespace SistemaBancario
{
    public class ContaCorrente : ContaBancaria
    {
        public decimal LimiteChequeEspecial { get; private set; }
        private const decimal TaxaSaque = 2.50m;

        public ContaCorrente(string numeroConta, string titular, decimal saldoInicial, decimal limite)
            : base(numeroConta, titular, saldoInicial)
        {
            LimiteChequeEspecial = limite;
        }

        public override void Sacar(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("O valor do saque deve ser maior que zero.");

            decimal valorTotal = valor + TaxaSaque;

            if (valorTotal > Saldo + LimiteChequeEspecial)
                throw new InvalidOperationException("Saldo e limite insuficientes para realizar o saque.");

            Saldo -= valorTotal;
            Console.WriteLine($"Saque de R$ {valor:F2} realizado! (Taxa de R$ {TaxaSaque:F2} aplicada)");
        }

        public override void ExibirExtrato()
        {
            Console.WriteLine($"[CONTA CORRENTE] {Titular} (Conta: {NumeroConta})");
            Console.WriteLine($"Saldo: R$ {Saldo:F2} | Limite: R$ {LimiteChequeEspecial:F2} | Disponível: R$ {(Saldo + LimiteChequeEspecial):F2}");
        }
    }
}