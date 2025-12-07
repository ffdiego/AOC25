using AOC24.Solucoes;
using AOC25.Models.Comuns;
using System;
using System.Collections.Generic;
using System.Text;

namespace AOC24.Solucoes;

public class Operacoes
{
    private readonly List<Queue<int>> Numeros;
    private readonly List<char> Operadores;

    public Operacoes(string input)
    {
        Numeros = [];
        Operadores = [];

        foreach (var linha in input.Split('\n'))
        {
            if (linha[0] is '+' or '*')
            {
                Operadores = linha.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(str => str[0]).ToList();
            } 
            else
            {
                Queue<int> numerosLinha = [];
                foreach (var item in linha.Split(" ", StringSplitOptions.RemoveEmptyEntries))
                {
                    numerosLinha.Enqueue(int.Parse(item));
                }
                Numeros.Add(numerosLinha);
            }
        }

        if (Numeros.Any(fila => fila.Count != Operadores.Count))
        {
            throw new Exception("Tamanhos não batem");
        }
    }

    public long SomatorioOperacoes()
    {
        long total = 0;

        for (int i = 0; i < Operadores.Count; i++)
        {
            Func<long, int, long> funcaoAggregate = (Operadores[i] == '*')
                ? (long acc, int numero) => acc * numero
                : (long acc, int numero) => acc + numero;

            long inicial = (Operadores[i] == '*')
                ? 1
                : 0;

            long operacaoAtual = Numeros.Select(lista => lista.ElementAt(i)).Aggregate(inicial, funcaoAggregate);

            total += operacaoAtual;
        }
        

        return total;
    }
}

public class OperacoesMapa(Mapa mapa)
{
    private int MontaNumero(Stack<int> digitos)
    {
        int total = 0;

        for (int i = 0; digitos.Count > 0; i++)
        {
            total += digitos.Pop() * (int)Math.Pow(10, i);
        }

        return total;
    }

    public long SomatorioOperacoesPorColuna()
    {
        ArgumentNullException.ThrowIfNull(mapa);

        long total = 0;

        List<int> numeros = [];

        for (int coluna = mapa.Largura - 1; coluna >= 0; coluna--)
        {
            Stack<int> digitos = [];

            for (int y = 0; (y < mapa.Altura - 1); y++)
            {
                int digito = mapa.GetItemInt(coluna, y);

                if (digito != -1)
                {
                    digitos.Push(digito);
                }
            }

            if (digitos.Any())
            {
                numeros.Add(MontaNumero(digitos));
            }

            char operacao = mapa.GetItem(coluna, mapa.Altura - 1);
            if (operacao == ' ')
            {
                continue;
            }

            Func<long, int, long> funcaoAggregate = (operacao == '*')
                ? (long acc, int numero) => acc * numero
                : (long acc, int numero) => acc + numero;

            long inicial = (operacao == '*')
                ? 1
                : 0;

            long operacaoAtual = numeros.Aggregate(inicial, funcaoAggregate);
            total += operacaoAtual;

            numeros.Clear();
        }

        return total;
    }
}

public class Dia6 : ISolucionador
{
    public string SolucaoParte1(string input)
    {
        var operacoes = new Operacoes(input);

        return operacoes.SomatorioOperacoes().ToString();
    }

    public string SolucaoParte2(string input)
    {
        var operacoes = new OperacoesMapa(new Mapa(input, ' '));

        return operacoes.SomatorioOperacoesPorColuna().ToString();
    }
}
