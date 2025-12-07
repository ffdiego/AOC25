using AOC24.Solucoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AOC24.Solucoes;

public class ListaIngredientes
{
    private readonly List<(long menor, long maior)> rangeIngredientesFrescos;
    private readonly List<long> ingredientesAVerificar;
    public ListaIngredientes(string input)
    {
        var inputs = input.Split("\n\n");
        var listaRange = inputs[0];
        var listaChecagem = inputs[1];

        this.rangeIngredientesFrescos = listaRange
            .Split("\n")
            .Select(range => (long.Parse(range.Split("-")[0]), long.Parse(range.Split("-")[1])))
            .ToList();
        this.ingredientesAVerificar = listaChecagem
            .Split("\n")
            .Select(long.Parse)
            .ToList();
    }

    public int QuantidadeVerificadosFrescos()
    {
        int frescos = 0;

        foreach (var ingrediente in ingredientesAVerificar)
        {
            if (rangeIngredientesFrescos.Any(range => (range.menor <= ingrediente && range.maior >= ingrediente)))
            {
                frescos++; 
            }
        }

        return frescos;
    }

    public int QuantidadeFrescos()
    {
        int frescos = 0;

        HashSet<(long, long)> olhados = [];
        HashSet<(long, long)> rangesUnicos = [];

        foreach (var range in rangeIngredientesFrescos.OrderBy(r => r.menor))
        {
            if (olhados.Contains(range))
            {
                continue;
            }

            var rangesInterceptam = rangeIngredientesFrescos
                .Where(r => (r.menor <= range.maior && r.menor >= range.menor))
                .Where(r => !olhados.Contains(r)).ToList();

            foreach (var item in rangesInterceptam)
            {
                olhados.Add(item);
            }

            rangesUnicos.Add((range.menor, rangesInterceptam.Max(r => r.maior)));
        }

        return frescos;
    }
}

public class Dia5 : ISolucionador
{
    public string SolucaoParte1(string input)
    {
        var listaIngredientes = new ListaIngredientes(input);

        return listaIngredientes.QuantidadeVerificadosFrescos().ToString();
    }

    public string SolucaoParte2(string input)
    {

        input = "3-5\r\n10-14\r\n16-20\r\n12-18\r\n\r\n1\r\n5\r\n8\r\n11\r\n17\r\n32".Replace("\r", "");

        var listaIngredientes = new ListaIngredientes(input);


        return listaIngredientes.QuantidadeFrescos().ToString();
    }
}
