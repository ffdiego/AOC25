using System;
using System.Diagnostics;
using AOC24.Solucoes;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace AOC24.Utils;

public class GerenciadorDeSolucoes
{
    private AOCClient client;
    private Dictionary<int, string> Inputs;
    public GerenciadorDeSolucoes(AOCClient client)
    {
        this.client = client;
        Inputs = new();
    }

    private (string solucao, string tempo) Roda(ISolucionador solucionador, string input, bool parte2 = false)
    {
        Stopwatch sw = Stopwatch.StartNew();

        string solucao;
        try
        {
            solucao = parte2 ? solucionador.SolucaoParte2(input) : solucionador.SolucaoParte1(input);
        }
        catch (Exception ex)
        {
            solucao = ex.Message;
        }

        sw.Stop();

        string solucaoFormatada = $"[underline {(parte2 ? "Red" : "Green")}]{solucao}[/]";

        return (solucaoFormatada, $"({sw.ElapsedMilliseconds}ms)");
    }

    public async Task ObtemSolucaoDoDia(int dia)
    {
        string input = await ObtemInput(dia);

        ISolucionador solucionador = InstanciarSolucao(dia);

        AnsiConsole.Clear();
        Rule titulo = new Rule("Advent of Code 2025");
        titulo.LeftJustified().DoubleBorder();
        titulo.Style = Style.Parse("green");
        AnsiConsole.Write(titulo);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"(Dia {dia:D2})");
        AnsiConsole.MarkupLine($"[blue][link]{AOCClient.baseURI}day/{dia}/[/][/]");
        AnsiConsole.WriteLine();

        Table table = new Table();
        table.RoundedBorder();
        table.AddColumn("Parte");
        table.AddColumn("Solução");
        table.AddColumn("Tempo");
        table.AddRow(new Text("1").Centered());
        table.AddRow(new Text("2").Centered());

        AnsiConsole.Live(table)
            .Start(ctx =>
        {
            var parte1 = Roda(solucionador, input, false);
            table.UpdateCell(0, 1, parte1.solucao);
            table.UpdateCell(0, 2, parte1.tempo);
            ctx.Refresh();

            var parte2 = Roda(solucionador, input, true);
            table.UpdateCell(1, 1, parte2.solucao);
            table.UpdateCell(1, 2, parte2.tempo);
        });

    }

    public async Task<string> ObtemInput(int dia)
    {
        if (!Inputs.TryGetValue(dia, out var input))
        {
            string inputNovoParse = await client.GetInputAsync(dia);
            Inputs.Add(dia, inputNovoParse);
            return inputNovoParse;
        }

        return input;
    }

    public List<int> DiasComSolucao()
    {
        List<int> dias = new();
        for (int i = 1; i <= 30; i++)
        {
            if (GetClasseSolucao(i) != null)
            {
                dias.Add(i);
            }
        }

        return dias;
    }

    private static ISolucionador InstanciarSolucao(int dia)
    {
        Type? type = GetClasseSolucao(dia) ?? throw new ArgumentException($"Nenhuma classe encontrada para o dia {dia}");

        if (Activator.CreateInstance(type) is ISolucionador solucao)
        {
            return solucao;
        }

        throw new InvalidOperationException($"A classe {type} não implementa ISolucao");
    }

    private static Type? GetClasseSolucao(int dia)
    {
        string className = $"AOC24.Solucoes.Dia{dia}";

        return Type.GetType(className);
    }
}
