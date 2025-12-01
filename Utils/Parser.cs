using System;
using System.Collections.Generic;
using System.Numerics;

namespace AOC24.Utils;

public static class Parser
{
    public static List<int> ListaDeIntsDeUnicoDigito(string input)
    {
        List<int> result = [];

        foreach (char c in input)
        {
            // Da forma que os algarismos são codificados no padrão ASCII, 
            // os 4 primeiros bits resultam no representação númerica de si.
            result.Add(c & 0b1111); 
        }

        return result;
    }

    public static List<int> ListaDeInts(string input)
    {
        return input.Split(" ").Select(s => int.Parse(s)).ToList();
    }

    public static (List<(int, int)> regrasOrdenacao, List<List<int>> numeroPaginas) ParseiaRegrasManuais(string txt)
    {
        List<(int, int)> regrasOrdenacao = [];
        List<List<int>> numeroPaginas = [];

        foreach (string line in txt.Split("\n\n")[0].Split('\n'))
        {
            string[] numeros = line.Split("|");
            (int, int) parseados = (int.Parse(numeros[0]), int.Parse(numeros[1]));
            regrasOrdenacao.Add(parseados);
        }

        foreach (string line in txt.Split("\n\n")[1].Split('\n'))
        {
            var numeros = line.Split(",").Select(n => int.Parse(n));
            numeroPaginas.Add(numeros.ToList());
        }

        return (regrasOrdenacao, numeroPaginas);
    } 

    public static List<string> LinhasDeTexto(string txt)
    {
        return txt.Split('\n').ToList();
    }

    public static List<List<char>> MatrizDeChars(string txt)
    {
        List<List<char>> listaDeListas = new();

        foreach (string line in txt.ReplaceLineEndings("\n").Split('\n'))
        {
            listaDeListas.Add(line.ToList());
        }

        return listaDeListas;
    }

    public static string MatrizDeChars(List<List<char>> entrada)
    {
        return string.Join("\n", entrada.Select(linha => new string(linha.ToArray())));
    }

    public static (List<int>, List<int>) ListaDuplaInts(string txt)
    {
        (List<int>, List<int>) listas;
        listas = (new List<int>(), new List<int>());

        foreach (string line in txt.Split('\n'))
        {
            try
            {
                string[] inteiros = line.Split("   ");
                listas.Item1.Add(int.Parse(inteiros[0]));
                listas.Item2.Add(int.Parse(inteiros[1]));
            }
            catch (Exception) {}
        }

        return listas;
    }

    public static List<List<int>> ListaDeListaDeInts(string txt) 
    {
        List<List<int>> listaDeListas = new();

        foreach (string line in txt.Split('\n'))
        {
            try
            {
                List<int> lista = line.Split(" ").Select(a => int.Parse(a)).ToList();
                listaDeListas.Add(lista);
            }
            catch (Exception) {}
        }

        return listaDeListas;
    }

    public static List<List<int>> MatrizDeInts(string txt)
    {
        List<List<int>> listaDeListas = new();

        foreach (string line in txt.Split('\n'))
        {
            List<int> lista = [];

            foreach (char c in line)
            {
                lista.Add(c & 0b1111);
            }

            listaDeListas.Add(lista);
        }

        return listaDeListas;
    }

}
