using System;

namespace SistemaBancario
{
    public class ContaPoupanca : ContaBancaria
    {
        public decimal TaxaRendimento { get; private set; }

        public ContaPoupanca(string numeroConta, string titular, decimal saldoInicial, decimal taxaRendimento = 0.005m)
            : base(numeroConta, titular, saldoInicial)
        {
            TaxaRendimento = taxaRendimento;
        }

        public override void Sacar(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("O valor do saque deve ser maior que zero.");

            if (valor > Saldo)
                throw new InvalidOperationException("Saldo insuficiente na Conta Poupança.");

            Saldo -= valor;
            Console.WriteLine($"Saque de R$ {valor:F2} realizado com sucesso!");
        }

        public void RenderJuros()
        {
            decimal rendimento = Saldo * TaxaRendimento;
            Saldo += rendimento;
            Console.WriteLine($"Rendimento de R$ {rendimento:F2} aplicado com sucesso!");
        }

        public override void ExibirExtrato()
        {
            Console.WriteLine($"[CONTA POUPANÇA] {Titular} (Conta: {NumeroConta})");
            Console.WriteLine($"Saldo: R$ {Saldo:F2} | Taxa de Rendimento Mensal: {(TaxaRendimento * 100)}%");
        }
    }
}