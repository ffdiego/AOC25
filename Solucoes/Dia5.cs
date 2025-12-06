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

    public int QuantidadeFrescos()
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
}

public class Dia5 : ISolucionador
{
    public string SolucaoParte1(string input)
    {
        var listaIngredientes = new ListaIngredientes(input);

        return listaIngredientes.QuantidadeFrescos().ToString();
    }

    public string SolucaoParte2(string input)
    {
        return "";
    }
}
